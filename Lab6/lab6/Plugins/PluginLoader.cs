// =============================================================================
// Файл: PluginLoader.cs — загрузка DLL (lab6: без ошибки для DLL в Plugins/Friend).
// Адаптер XorCrypto регистрируется из lab6.Plugin.XorCryptoAdapter.dll в корне Plugins.
// =============================================================================

using System.Reflection;
using lab6.Contracts.Plugins;
using lab6.Domain;
using lab6.Serialization;

namespace lab6.Plugins
{
    public static class PluginLoader
    {
        public const string PluginsFolderName = "Plugins";

        public static IReadOnlyList<string> LoadErrors { get; private set; } = Array.Empty<string>();

        public static void LoadAll(string[]? commandLineArgs = null)
        {
            var errors = new List<string>();
            var loadedAssemblies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string pluginsDir = Path.Combine(AppContext.BaseDirectory, PluginsFolderName);
            if (Directory.Exists(pluginsDir))
            {
                foreach (string dllPath in Directory.EnumerateFiles(pluginsDir, "*.dll"))
                {
                    TryLoadAssembly(dllPath, errors, loadedAssemblies);
                }
            }

            if (commandLineArgs != null)
            {
                LoadFromCommandLine(commandLineArgs, errors, loadedAssemblies);
            }

            LoadErrors = errors.AsReadOnly();
        }

        public static void LoadFromFile(string dllPath)
        {
            var errors = new List<string>();
            var loadedAssemblies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            TryLoadAssembly(dllPath, errors, loadedAssemblies);
            LoadErrors = errors.AsReadOnly();
        }

        private static void LoadFromCommandLine(string[] args, List<string> errors, HashSet<string> loadedAssemblies)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (string.Equals(arg, "--plugin", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(arg, "-p", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        errors.Add("Missing path after --plugin argument.");
                        break;
                    }

                    string path = ResolvePluginPath(args[++i]);
                    TryLoadAssembly(path, errors, loadedAssemblies);
                }
            }
        }

        private static string ResolvePluginPath(string pathOrName)
        {
            if (Path.IsPathRooted(pathOrName) && File.Exists(pathOrName))
                return pathOrName;

            if (File.Exists(pathOrName))
                return Path.GetFullPath(pathOrName);

            string withDll = pathOrName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
                ? pathOrName
                : pathOrName + ".dll";

            string inPlugins = Path.Combine(AppContext.BaseDirectory, PluginsFolderName, withDll);
            if (File.Exists(inPlugins))
                return inPlugins;

            return withDll;
        }

        private static void TryLoadAssembly(string dllPath, List<string> errors, HashSet<string> loadedAssemblies)
        {
            if (!File.Exists(dllPath))
            {
                errors.Add($"Plugin not found: {dllPath}");
                return;
            }

            string fullPath = Path.GetFullPath(dllPath);
            if (!loadedAssemblies.Add(fullPath))
                return;

            try
            {
                var assembly = Assembly.LoadFrom(fullPath);
                LoadPluginsFromAssembly(assembly, fullPath, errors);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load '{dllPath}': {ex.Message}");
            }
        }

        private static void LoadPluginsFromAssembly(Assembly assembly, string sourcePath, List<string> errors)
        {
            bool foundVehicle = false;
            bool foundProcessing = false;

            try
            {
                foreach (Type type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
                {
                    if (typeof(IVehiclePlugin).IsAssignableFrom(type))
                    {
                        foundVehicle = true;
                        TryRegisterVehiclePlugin(type, sourcePath, errors);
                    }

                    if (typeof(ISerializationProcessingPlugin).IsAssignableFrom(type))
                    {
                        foundProcessing = true;
                        TryRegisterProcessingPlugin(type, sourcePath, errors);
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                errors.Add($"Cannot read types from '{sourcePath}': {ex.Message}");
                return;
            }

            if (!foundVehicle && !foundProcessing)
            {
                // Не ошибка: XorCryptoPlugin.dll лежит в Plugins/Friend, host грузит только адаптер.
            }
        }

        private static void TryRegisterVehiclePlugin(Type type, string sourcePath, List<string> errors)
        {
            try
            {
                if (Activator.CreateInstance(type) is not IVehiclePlugin plugin)
                    return;

                var context = new VehiclePluginContext(
                    (id, displayName, factory) =>
                        VehicleTypeRegistry.RegisterFromPlugin(plugin.Name, id, displayName, factory),
                    (vehicleType, discriminator) =>
                        RegisterBsonType(vehicleType, discriminator));

                plugin.Register(context);
            }
            catch (Exception ex)
            {
                errors.Add($"Vehicle plugin '{type.Name}' in '{sourcePath}': {ex.Message}");
            }
        }

        private static void TryRegisterProcessingPlugin(Type type, string sourcePath, List<string> errors)
        {
            try
            {
                if (Activator.CreateInstance(type) is ISerializationProcessingPlugin plugin)
                {
                    SerializationPluginRegistry.Register(plugin);
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Processing plugin '{type.Name}' in '{sourcePath}': {ex.Message}");
            }
        }

        private static void RegisterBsonType(Type vehicleType, string discriminator)
        {
            var method = typeof(VehicleBsonSerializer)
                .GetMethod(nameof(VehicleBsonSerializer.RegisterDerived), 1, new[] { typeof(string) })!
                .MakeGenericMethod(vehicleType);
            method.Invoke(null, new object[] { discriminator });
        }
    }
}
