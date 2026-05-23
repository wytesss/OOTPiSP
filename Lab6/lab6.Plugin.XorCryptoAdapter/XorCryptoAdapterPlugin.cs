// =============================================================================
// Файл: XorCryptoAdapterPlugin.cs — паттерн «Адаптер».
// IFileCryptoPlugin товарища → ISerializationProcessingPlugin + IItemsPayloadPreprocessor.
// =============================================================================

using lab6.Contracts.Plugins;

namespace lab6.Plugin.XorCryptoAdapter
{
    public sealed class XorCryptoAdapterPlugin : ISerializationProcessingPlugin, IItemsPayloadPreprocessor
    {
        public const string MetadataKey = "xorCipher";

        public string Name => "XorCrypto adapter";
        public string DisplayName => FriendPluginHost.FriendDisplayName ?? "XOR-шифрование (плагин товарища)";
        public string ProcessingTypeId => "xor-cipher";

        /// <summary>Шифруем ItemsPayload перед записью в BSON (через DLL товарища).</summary>
        public void ProcessBeforeSave(SerializationProcessingContext context)
        {
            if (!FriendPluginHost.TryEnsureLoaded(out string? error))
            {
                context.Messages.Add($"XOR-адаптер: {error}");
                return;
            }

            context.ItemsPayload = FriendPluginHost.Encrypt(context.ItemsPayload);
            context.FileMetadata[MetadataKey] = FriendPluginHost.FriendPluginId ?? "XorCipher";
        }

        public void ProcessAfterLoad(SerializationProcessingContext context)
        {
            if (!context.FileMetadata.ContainsKey(MetadataKey))
            {
                return;
            }

            context.Messages.Add("XOR-шифрование: данные успешно расшифрованы (плагин товарища через адаптер).");
        }

        public bool ShouldPreprocessOnLoad(IReadOnlyDictionary<string, string> metadata)
        {
            return metadata.ContainsKey(MetadataKey);
        }

        public byte[] PreprocessOnLoad(byte[] payload, IReadOnlyDictionary<string, string> metadata)
        {
            if (!ShouldPreprocessOnLoad(metadata))
            {
                return payload;
            }

            if (!FriendPluginHost.TryEnsureLoaded(out string? error))
            {
                throw new InvalidOperationException($"XOR-адаптер: {error}");
            }

            return FriendPluginHost.Decrypt(payload);
        }
    }
}
