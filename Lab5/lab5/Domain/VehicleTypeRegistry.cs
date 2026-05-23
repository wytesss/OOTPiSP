// =============================================================================
// Файл: VehicleTypeRegistry.cs — паттерн «Реестр» типов транспорта.
// Хранит список VehicleTypeInfo; UI и PluginLoader только читают/регистрируют.
// =============================================================================

using System;
using System.Collections.Generic;
using lab5.Contracts.Domain;

namespace lab5.Domain
{
    public static class VehicleTypeRegistry
    {
        private static readonly List<VehicleTypeInfo> _types = new();
        /// <summary>Имена плагинов, уже добавивших типы (для labelPlugins).</summary>
        private static readonly HashSet<string> _loadedPlugins = new(StringComparer.OrdinalIgnoreCase);

        public static IReadOnlyList<VehicleTypeInfo> Types => _types.AsReadOnly();
        public static IReadOnlyCollection<string> LoadedPlugins => _loadedPlugins;

        /// <summary>Очистка при перезагрузке плагинов (перед повторной регистрацией встроенных типов).</summary>
        public static void Clear()
        {
            _types.Clear();
            _loadedPlugins.Clear();
        }

        /// <summary>Регистрация Car/Bus/Truck/Van из VehicleBootstrap.</summary>
        public static void RegisterBuiltIn(string id, string displayName, Func<Vehicle> factory)
        {
            Register(id, displayName, factory, "Built-in");
        }

        /// <summary>Регистрация из IVehiclePlugin (например Motorcycle).</summary>
        public static void RegisterFromPlugin(string pluginName, string id, string displayName, Func<Vehicle> factory)
        {
            Register(id, displayName, factory, pluginName);
            _loadedPlugins.Add(pluginName);
        }

        private static void Register(string id, string displayName, Func<Vehicle> factory, string source)
        {
            // Защита от дублирования id при повторной загрузке одного плагина
            if (_types.Exists(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Vehicle type '{id}' is already registered.");
            }

            _types.Add(new VehicleTypeInfo(id, displayName, factory, source));
        }
    }
}
