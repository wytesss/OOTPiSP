// =============================================================================
// Файл: ISerializationProcessingPlugin.cs — контракт плагинов обработки save/load.
// Отличие от IVehiclePlugin: не добавляет тип ТС, а меняет данные файла (checksum, время…).
// =============================================================================

namespace lab5.Contracts.Plugins
{
    /// <summary>
    /// Плагин обработки данных перед сохранением в файл и после загрузки (pipeline).
    /// </summary>
    public interface ISerializationProcessingPlugin
    {
        string Name { get; }

        /// <summary>Имя в окне «Настройки» (например, «Сохранение контрольной суммы»).</summary>
        string DisplayName { get; }

        /// <summary>Идентификатор для appsettings.json (checksum, save-timestamp…).</summary>
        string ProcessingTypeId { get; }

        /// <summary>Вызывается после сериализации списка, до записи BSON на диск.</summary>
        void ProcessBeforeSave(SerializationProcessingContext context);

        /// <summary>Вызывается после чтения и десериализации items из файла.</summary>
        void ProcessAfterLoad(SerializationProcessingContext context);
    }
}
