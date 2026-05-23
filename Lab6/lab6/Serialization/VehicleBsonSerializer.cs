// =============================================================================
// Файл: VehicleBsonSerializer.cs — BSON + pipeline; lab6: расшифровка ДО deserialize.
// =============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using lab6.Contracts.Domain;
using lab6.Contracts.Plugins;
using lab6.Plugins;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace lab6.Serialization
{
    public static class VehicleBsonSerializer
    {
        private static bool _rootConfigured;
        private static readonly HashSet<Type> _registeredTypes = new();

        public static void EnsureRootConfigured()
        {
            if (_rootConfigured)
                return;

            if (!BsonClassMap.IsClassMapRegistered(typeof(Vehicle)))
            {
                BsonClassMap.RegisterClassMap<Vehicle>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                    cm.SetDiscriminator("vehicle");
                });
            }

            _rootConfigured = true;
        }

        public static void RegisterDerived<T>(string discriminator) where T : Vehicle
        {
            EnsureRootConfigured();

            Type type = typeof(T);
            if (_registeredTypes.Contains(type))
                return;

            if (!BsonClassMap.IsClassMapRegistered(type))
            {
                BsonClassMap.RegisterClassMap<T>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator(discriminator);
                });
            }

            _registeredTypes.Add(type);
        }

        public static byte[] SerializeItems(IList<Vehicle> vehicles)
        {
            EnsureRootConfigured();
            // Создаём документ с одним полем "items", содержащим массив
            var doc = new BsonDocument
            {
                { "items", new BsonArray(vehicles.Select(v => v.ToBsonDocument())) }
            };
            return doc.ToBson();
        }

        public static List<Vehicle> DeserializeItems(byte[] payload)
        {
            EnsureRootConfigured();
            if (payload.Length == 0)
                return new List<Vehicle>();

            // Десериализуем документ
            var doc = BsonSerializer.Deserialize<BsonDocument>(payload);
            // Извлекаем массив по ключу "items"
            var bsonArray = doc["items"].AsBsonArray;
            var vehicles = new List<Vehicle>(bsonArray.Count);
            foreach (var item in bsonArray)
            {
                vehicles.Add(BsonSerializer.Deserialize<Vehicle>(item.AsBsonDocument));
            }
            return vehicles;
        }

        public static void SaveToFile(string filePath, IList<Vehicle> vehicles, AppSettings settings)
        {
            EnsureRootConfigured();

            byte[] itemsPayload = SerializeItems(vehicles.ToList());
            var metadata = new Dictionary<string, string>(StringComparer.Ordinal);

            var context = new SerializationProcessingContext(vehicles.ToList(), metadata)
            {
                ItemsPayload = itemsPayload
            };

            foreach (var plugin in SerializationPluginRegistry.GetEnabled(settings.EnabledProcessingIds))
            {
                plugin.ProcessBeforeSave(context);
            }

            var document = new BsonDocument
            {
                { "items", new BsonBinaryData(context.ItemsPayload, BsonBinarySubType.Binary) }
            };

            foreach (var pair in metadata)
            {
                document[pair.Key] = pair.Value;
            }

            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            using var writer = new BsonBinaryWriter(stream);
            BsonSerializer.Serialize(writer, document);
        }

        public static LoadResult LoadFromFile(string filePath, AppSettings settings)
        {
            EnsureRootConfigured();
            var result = new LoadResult();

            if (!File.Exists(filePath))
                return result;

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var reader = new BsonBinaryReader(stream);

            try
            {
                var document = BsonSerializer.Deserialize<BsonDocument>(reader);
                result.Metadata = ExtractMetadata(document);

                byte[] itemsPayload;
                if (document.Contains("items") && document["items"].IsBsonBinaryData)
                {
                    itemsPayload = document["items"].AsBsonBinaryData.Bytes;
                }
                else if (document.Contains("items") && document["items"].IsBsonArray)
                {
                    itemsPayload = SerializeItemsFromArray(document["items"].AsBsonArray);
                }
                else
                {
                    return result;
                }

                // XOR-адаптер: расшифровать байты до DeserializeItems (иначе BSON не разберётся)
                itemsPayload = ApplyPayloadPreprocessorsOnLoad(itemsPayload, result.Metadata, settings);

                result.Vehicles = DeserializeItems(itemsPayload);

                var context = new SerializationProcessingContext(result.Vehicles, result.Metadata)
                {
                    ItemsPayload = itemsPayload
                };

                foreach (var plugin in SerializationPluginRegistry.GetEnabled(settings.EnabledProcessingIds))
                {
                    plugin.ProcessAfterLoad(context);
                }

                result.Messages.AddRange(context.Messages);
            }
            catch (Exception ex)
            {
                result.Messages.Add($"Failed to load file: {ex.Message}");
            }

            return result;
        }

        /// <summary>Плагины IItemsPayloadPreprocessor (xor-cipher) — Decrypt перед десериализацией.</summary>
        private static byte[] ApplyPayloadPreprocessorsOnLoad(
            byte[] payload,
            IReadOnlyDictionary<string, string> metadata,
            AppSettings settings)
        {
            foreach (var plugin in SerializationPluginRegistry.GetEnabled(settings.EnabledProcessingIds))
            {
                if (plugin is IItemsPayloadPreprocessor preprocessor &&
                    preprocessor.ShouldPreprocessOnLoad(metadata))
                {
                    payload = preprocessor.PreprocessOnLoad(payload, metadata);
                }
            }

            return payload;
        }

        private static Dictionary<string, string> ExtractMetadata(BsonDocument document)
        {
            var metadata = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var element in document.Elements)
            {
                if (element.Name == "items")
                    continue;

                if (element.Value.IsString)
                {
                    metadata[element.Name] = element.Value.AsString;
                }
            }

            return metadata;
        }

        private static byte[] SerializeItemsFromArray(BsonArray array)
        {
            var list = new List<Vehicle>(array.Count);
            foreach (var value in array)
            {
                if (value.IsBsonDocument)
                {
                    list.Add(BsonSerializer.Deserialize<Vehicle>(value.AsBsonDocument));
                }
            }

            return SerializeItems(list);
        }

        public static string ComputeSha256Hex(byte[] data)
        {
            byte[] hash = SHA256.HashData(data);
            return Convert.ToHexString(hash);
        }
    }

    public sealed class LoadResult
    {
        public List<Vehicle> Vehicles { get; set; } = new();
        public Dictionary<string, string> Metadata { get; set; } = new(StringComparer.Ordinal);
        public List<string> Messages { get; } = new();
    }
}
