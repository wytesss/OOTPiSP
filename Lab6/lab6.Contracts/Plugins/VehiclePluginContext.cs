// =============================================================================
// Файл: VehiclePluginContext.cs — «мост» между плагином и host.
// Плагин не ссылается на VehicleTypeRegistry напрямую — только на делегаты context.
// =============================================================================

using System;
using lab6.Contracts.Domain;

namespace lab6.Contracts.Plugins
{
    /// <summary>
    /// Контекст регистрации: host передаёт callbacks, плагин вызывает RegisterVehicleType / RegisterBsonType.
    /// </summary>
    public sealed class VehiclePluginContext
    {
        private readonly Action<string, string, Func<Vehicle>> _registerType;
        private readonly Action<Type, string> _registerBson;

        public VehiclePluginContext(
            Action<string, string, Func<Vehicle>> registerType,
            Action<Type, string> registerBson)
        {
            _registerType = registerType;
            _registerBson = registerBson;
        }

        /// <summary>Добавить тип в реестр + ComboBox (id, отображаемое имя, фабрика).</summary>
        public void RegisterVehicleType(string id, string displayName, Func<Vehicle> factory)
        {
            _registerType(id, displayName, factory);
        }

        /// <summary>Сообщить сериализатору BSON-дискриминатор для наследника Vehicle.</summary>
        public void RegisterBsonType<T>(string discriminator) where T : Vehicle
        {
            _registerBson(typeof(T), discriminator);
        }
    }
}
