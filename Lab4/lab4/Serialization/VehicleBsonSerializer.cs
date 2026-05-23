// =============================================================================
// Файл: VehicleBsonSerializer.cs — сохранение/загрузка списка Vehicle в vehicles.bson.
// Формат: BSON-документ { "items": [ ... ] }. Поддержка полиморфизма через дискриминаторы.
// =============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using lab4.Contracts.Domain;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace lab4.Serialization
{
    public static class VehicleBsonSerializer
    {
        private static bool _rootConfigured;
        private static readonly HashSet<Type> _registeredTypes = new();

        /// <summary>Один раз регистрирует корневой класс Vehicle в MongoDB BSON mapper.</summary>
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

        /// <summary>Регистрация наследника (Car, Motorcycle…) — вызывается из bootstrap и плагинов.</summary>
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

        /// <summary>Запись: документ BSON с массивом items.</summary>
        public static void SaveToFile(string filePath, IList<Vehicle> vehicles)
        {
            EnsureRootConfigured();

            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            using var writer = new BsonBinaryWriter(stream);

            writer.WriteStartDocument();
            writer.WriteName("items");
            BsonSerializer.Serialize(writer, vehicles);
            writer.WriteEndDocument();
        }

        /// <summary>
        /// Чтение: сначала формат lab4 (items — массив), при ошибке — попытка как List напрямую.
        /// </summary>
        public static List<Vehicle> LoadFromFile(string filePath)
        {
            EnsureRootConfigured();

            if (!File.Exists(filePath))
                return new List<Vehicle>();

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var reader = new BsonBinaryReader(stream);

            try
            {
                var document = BsonSerializer.Deserialize<BsonDocument>(reader);

                if (!document.Contains("items") || !document["items"].IsBsonArray)
                    return new List<Vehicle>();

                var array = document["items"].AsBsonArray;
                var result = new List<Vehicle>(array.Count);
                foreach (var value in array)
                {
                    if (value.IsBsonDocument)
                    {
                        result.Add(BsonSerializer.Deserialize<Vehicle>(value.AsBsonDocument));
                    }
                }

                return result;
            }
            catch
            {
                // Совместимость со старым форматом: файл = просто List<Vehicle>
                try
                {
                    stream.Position = 0;
                    using var readerArray = new BsonBinaryReader(stream);
                    return BsonSerializer.Deserialize<List<Vehicle>>(readerArray);
                }
                catch
                {
                    return new List<Vehicle>();
                }
            }
        }
    }
}
