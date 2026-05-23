// =============================================================================
// Файл: SerializationPluginRegistry.cs — паттерн «Реестр» плагинов обработки.
// =============================================================================

using lab5.Contracts.Plugins;

namespace lab5.Plugins
{
    public static class SerializationPluginRegistry
    {
        private static readonly List<ISerializationProcessingPlugin> _plugins = new();

        public static IReadOnlyList<ISerializationProcessingPlugin> Plugins => _plugins.AsReadOnly();

        public static void Clear() => _plugins.Clear();

        /// <summary>Вызывается из PluginLoader при обнаружении ISerializationProcessingPlugin в DLL.</summary>
        public static void Register(ISerializationProcessingPlugin plugin)
        {
            if (_plugins.Exists(p => string.Equals(p.ProcessingTypeId, plugin.ProcessingTypeId, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    $"Processing plugin '{plugin.ProcessingTypeId}' is already registered.");
            }

            _plugins.Add(plugin);
        }

        /// <summary>Только плагины, отмеченные галочками в appsettings.json (порядок = порядок в списке).</summary>
        public static IEnumerable<ISerializationProcessingPlugin> GetEnabled(IReadOnlyCollection<string> enabledIds)
        {
            foreach (var plugin in _plugins)
            {
                if (enabledIds.Contains(plugin.ProcessingTypeId, StringComparer.OrdinalIgnoreCase))
                {
                    yield return plugin;
                }
            }
        }
    }
}
