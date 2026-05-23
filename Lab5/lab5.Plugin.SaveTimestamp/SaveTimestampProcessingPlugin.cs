// =============================================================================
// Файл: SaveTimestampProcessingPlugin.cs — учебный плагин «метка времени сохранения».
// =============================================================================

using lab5.Contracts.Plugins;

namespace lab5.Plugin.SaveTimestamp
{
    public sealed class SaveTimestampProcessingPlugin : ISerializationProcessingPlugin
    {
        public const string MetadataKey = "savedAtUtc";

        public string Name => "Save timestamp plugin";
        public string DisplayName => "Сохранение метки времени";
        public string ProcessingTypeId => "save-timestamp";

        public void ProcessBeforeSave(SerializationProcessingContext context)
        {
            context.FileMetadata[MetadataKey] = DateTime.UtcNow.ToString("O");
        }

        public void ProcessAfterLoad(SerializationProcessingContext context)
        {
            if (context.FileMetadata.TryGetValue(MetadataKey, out string? savedAt) &&
                DateTime.TryParse(savedAt, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime dt))
            {
                context.Messages.Add($"Файл сохранён: {dt.ToLocalTime():g}");
            }
        }
    }
}
