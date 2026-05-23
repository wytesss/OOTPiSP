// =============================================================================
// Файл: IItemsPayloadPreprocessor.cs — расшифровка items до BSON-deserialize (lab6).
// Реализует XorCryptoAdapterPlugin вместе с ISerializationProcessingPlugin.
// =============================================================================

namespace lab6.Contracts.Plugins
{
    /// <summary>
    /// Плагин, которому нужно изменить байты items до десериализации (например, XOR Decrypt).
    /// </summary>
    public interface IItemsPayloadPreprocessor
    {
        /// <summary>Есть ли в metadata признак шифрования (xorCipher).</summary>
        bool ShouldPreprocessOnLoad(IReadOnlyDictionary<string, string> metadata);

        /// <summary>Вернуть открытые байты для DeserializeItems.</summary>
        byte[] PreprocessOnLoad(byte[] payload, IReadOnlyDictionary<string, string> metadata);
    }
}
