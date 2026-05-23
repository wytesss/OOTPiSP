// =============================================================================
// Файл: FriendPluginHost.cs — загрузка XorCryptoPlugin.dll из Plugins/Friend.
// Отдельный AssemblyLoadContext + рефлексия (контракт Lab5_OOP.Contracts).
// =============================================================================

using System.Reflection;
using System.Runtime.Loader;

namespace lab6.Plugin.XorCryptoAdapter
{
    internal static class FriendPluginHost
    {
        private const string FriendSubFolder = "Friend";
        private const string ContractsFileName = "Lab5_OOP.Contracts.dll";
        private const string PluginFileName = "XorCryptoPlugin.dll";
        private const string PluginTypeName = "XorCryptoPlugin.XorCipherPlugin";

        private static object? _cryptoInstance;
        private static MethodInfo? _encryptMethod;
        private static MethodInfo? _decryptMethod;
        private static string? _friendPluginId;
        private static string? _friendDisplayName;

        public static string? FriendPluginId => _friendPluginId;
        public static string? FriendDisplayName => _friendDisplayName;

        public static bool TryEnsureLoaded(out string? errorMessage)
        {
            if (_cryptoInstance != null)
            {
                errorMessage = null;
                return true;
            }

            try
            {
                string friendDir = Path.Combine(AppContext.BaseDirectory, "Plugins", FriendSubFolder);
                string contractsPath = Path.Combine(friendDir, ContractsFileName);
                string pluginPath = Path.Combine(friendDir, PluginFileName);

                if (!File.Exists(contractsPath))
                {
                    errorMessage = $"Не найден контракт товарища: {contractsPath}";
                    return false;
                }

                if (!File.Exists(pluginPath))
                {
                    errorMessage = $"Не найден плагин товарища: {pluginPath}";
                    return false;
                }

                var loadContext = new AssemblyLoadContext("friend-plugins", isCollectible: false);
                loadContext.Resolving += (_, assemblyName) =>
                {
                    if (string.Equals(assemblyName.Name, "Lab5_OOP.Contracts", StringComparison.OrdinalIgnoreCase))
                    {
                        return loadContext.LoadFromAssemblyPath(contractsPath);
                    }

                    return null;
                };

                var pluginAssembly = loadContext.LoadFromAssemblyPath(pluginPath);
                Type? pluginType = pluginAssembly.GetType(PluginTypeName, throwOnError: false);
                if (pluginType == null)
                {
                    errorMessage = $"Тип {PluginTypeName} не найден в {PluginFileName}.";
                    return false;
                }

                _cryptoInstance = Activator.CreateInstance(pluginType);
                if (_cryptoInstance == null)
                {
                    errorMessage = "Не удалось создать экземпляр плагина товарища.";
                    return false;
                }

                Type pluginRuntimeType = _cryptoInstance.GetType();
                _encryptMethod = pluginRuntimeType.GetMethod("Encrypt", new[] { typeof(byte[]) });
                _decryptMethod = pluginRuntimeType.GetMethod("Decrypt", new[] { typeof(byte[]) });

                if (_encryptMethod == null || _decryptMethod == null)
                {
                    errorMessage = "В плагине товарища не найдены методы Encrypt/Decrypt.";
                    return false;
                }

                _friendPluginId = pluginRuntimeType.GetProperty("PluginId")?.GetValue(_cryptoInstance) as string;
                _friendDisplayName = pluginRuntimeType.GetProperty("DisplayName")?.GetValue(_cryptoInstance) as string;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static byte[] Encrypt(byte[] plainData)
        {
            EnsureLoaded();
            return (byte[])_encryptMethod!.Invoke(_cryptoInstance, new object[] { plainData })!;
        }

        public static byte[] Decrypt(byte[] encryptedData)
        {
            EnsureLoaded();
            return (byte[])_decryptMethod!.Invoke(_cryptoInstance, new object[] { encryptedData })!;
        }

        private static void EnsureLoaded()
        {
            if (!TryEnsureLoaded(out string? error))
            {
                throw new InvalidOperationException(error ?? "Плагин товарища не загружен.");
            }
        }
    }
}
