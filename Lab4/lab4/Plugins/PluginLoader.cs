// =============================================================================
// Файл: PluginLoader.cs — динамическая загрузка DLL из папки Plugins.
// Использует рефлексию: Assembly.LoadFrom, поиск IVehiclePlugin, Activator.CreateInstance.
// =============================================================================

using System.Reflection;
using lab4.Contracts.Plugins;
using lab4.Domain;
using lab4.Serialization;

namespace lab4.Plugins
{
    public static class PluginLoader
    {
        public const string PluginsFolderName = "Plugins";

        /// <summary>Ошибки последней загрузки (показываются в MessageBox на Form1).</summary>
        public static IReadOnlyList<string> LoadErrors { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Сканирует Plugins/*.dll и опционально пути из командной строки (--plugin path).
        /// </summary>
        public static void LoadAll(string[]? commandLineArgs = null)
        {
            var errors = new List<string>();
            // Один и тот же файл не грузим дважды
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

        /// <summary>Разрешает относительный путь: cwd, Plugins/имя.dll.</summary>
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

        /// <summary>Ищет классы, реализующие IVehiclePlugin, и вызывает Register.</summary>
        private static void LoadPluginsFromAssembly(Assembly assembly, string sourcePath, List<string> errors)
        {
            Type pluginInterface = typeof(IVehiclePlugin);
            IEnumerable<Type> pluginTypes;

            try
            {
                pluginTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && pluginInterface.IsAssignableFrom(t));
            }
            catch (ReflectionTypeLoadException ex)
            {
                errors.Add($"Cannot read types from '{sourcePath}': {ex.Message}");
                return;
            }

            bool found = false;
            foreach (Type type in pluginTypes)
            {
                found = true;
                try
                {
                    if (Activator.CreateInstance(type) is not IVehiclePlugin plugin)
                        continue;

                    // Context связывает плагин с реестром host и BSON-сериализатором
                    var context = new VehiclePluginContext(
                        (id, displayName, factory) =>
                            VehicleTypeRegistry.RegisterFromPlugin(plugin.Name, id, displayName, factory),
                        (vehicleType, discriminator) =>
                            RegisterBsonType(vehicleType, discriminator));

                    plugin.Register(context);
                }
                catch (Exception ex)
                {
                    errors.Add($"Plugin '{type.Name}' in '{sourcePath}': {ex.Message}");
                }
            }

            if (!found)
            {
                errors.Add($"No IVehiclePlugin implementations found in '{sourcePath}'.");
            }
        }

        /// <summary>Вызов VehicleBsonSerializer.RegisterDerived&lt;T&gt; через MakeGenericMethod.</summary>
        private static void RegisterBsonType(Type vehicleType, string discriminator)
        {
            var method = typeof(VehicleBsonSerializer)
                .GetMethod(nameof(VehicleBsonSerializer.RegisterDerived), 1, new[] { typeof(string) })!
                .MakeGenericMethod(vehicleType);
            method.Invoke(null, new object[] { discriminator });
        }
    }
}
