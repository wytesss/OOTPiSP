// =============================================================================
// Файл: ChecksumProcessingPlugin.cs — обязательная обработка «контрольная сумма» (SHA-256).
// =============================================================================

using System.Security.Cryptography;
using lab5.Contracts.Plugins;

namespace lab5.Plugin.Checksum
{
    public sealed class ChecksumProcessingPlugin : ISerializationProcessingPlugin
    {
        public const string MetadataKey = "checksum";

        public string Name => "Checksum plugin";
        public string DisplayName => "Сохранение контрольной суммы";
        public string ProcessingTypeId => "checksum";

        /// <summary>Хеш от ItemsPayload записывается в корень BSON-файла.</summary>
        public void ProcessBeforeSave(SerializationProcessingContext context)
        {
            string checksum = ComputeSha256Hex(context.ItemsPayload);
            context.FileMetadata[MetadataKey] = checksum;
        }

        /// <summary>Сравнение сохранённого hash с пересчитанным после загрузки.</summary>
        public void ProcessAfterLoad(SerializationProcessingContext context)
        {
            if (!context.FileMetadata.TryGetValue(MetadataKey, out string? stored) || string.IsNullOrEmpty(stored))
            {
                context.Messages.Add("Контрольная сумма отсутствует в файле.");
                return;
            }

            string actual = ComputeSha256Hex(context.ItemsPayload);
            if (!string.Equals(stored, actual, StringComparison.OrdinalIgnoreCase))
            {
                context.Messages.Add(
                    $"Контрольная сумма не совпадает! Ожидалось: {stored}, фактически: {actual}");
            }
            else
            {
                context.Messages.Add("Контрольная сумма проверена успешно.");
            }
        }

        private static string ComputeSha256Hex(byte[] data)
        {
            byte[] hash = SHA256.HashData(data);
            return Convert.ToHexString(hash);
        }
    }
}
