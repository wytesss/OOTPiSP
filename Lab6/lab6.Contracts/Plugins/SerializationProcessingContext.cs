// =============================================================================
// Файл: SerializationProcessingContext.cs — данные, передаваемые плагинам обработки.
// =============================================================================

using System.Collections.Generic;
using lab6.Contracts.Domain;

namespace lab6.Contracts.Plugins
{
    /// <summary>
    /// Контекст одного прохода save/load: объекты, байты items, метаданные файла, сообщения UI.
    /// </summary>
    public sealed class SerializationProcessingContext
    {
        /// <summary>Список ТС (для плагинов, которым нужны объекты, не только байты).</summary>
        public IList<Vehicle> Vehicles { get; }
        /// <summary>Строковые поля верхнего уровня BSON (checksum, savedAtUtc…).</summary>
        public Dictionary<string, string> FileMetadata { get; }
        /// <summary>Сериализованный List&lt;Vehicle&gt; — основа для checksum и шифрования.</summary>
        public byte[] ItemsPayload { get; set; } = Array.Empty<byte>();
        /// <summary>Текст для MessageBox после загрузки.</summary>
        public List<string> Messages { get; } = new();

        public SerializationProcessingContext(
            IList<Vehicle> vehicles,
            Dictionary<string, string> fileMetadata)
        {
            Vehicles = vehicles;
            FileMetadata = fileMetadata;
        }
    }
}
