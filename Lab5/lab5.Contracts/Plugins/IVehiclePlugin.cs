// =============================================================================
// Файл: IVehiclePlugin.cs — контракт плагина «новый тип транспорта».
// Host вызывает Register() после загрузки DLL через рефлексию.
// =============================================================================

namespace lab5.Contracts.Plugins
{
    /// <summary>
    /// Плагин расширяет список типов ТС: регистрирует фабрику и BSON-дискриминатор.
    /// </summary>
    public interface IVehiclePlugin
    {
        /// <summary>Имя плагина для статусной строки на форме.</summary>
        string Name { get; }

        /// <summary>Точка входа: плагин сообщает host, какие типы добавить.</summary>
        void Register(VehiclePluginContext context);
    }
}
