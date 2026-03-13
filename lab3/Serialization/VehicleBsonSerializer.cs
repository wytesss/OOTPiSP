using System.Collections.Generic;
using System.IO;
using lab3.Domain;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace lab3.Serialization
{
    /// <summary>
    /// Provides helper methods for BSON serialization and deserialization of vehicle lists.
    /// </summary>
    public static class VehicleBsonSerializer
    {
        /// <summary>
        /// Flag indicates that BSON mappings were configured.
        /// </summary>
        private static bool _isConfigured;

        /// <summary>
        /// Configures BSON class maps for polymorphic vehicle hierarchy.
        /// </summary>
        private static void Configure()
        {
            if (_isConfigured)
            {
                return;
            }

            // Register base vehicle class as root for hierarchy.
            if (!BsonClassMap.IsClassMapRegistered(typeof(Vehicle)))
            {
                BsonClassMap.RegisterClassMap<Vehicle>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                    cm.SetDiscriminator("vehicle");
                });
            }

            // Register derived classes with discriminators to support correct deserialization.
            RegisterDerived<Car>("car");
            RegisterDerived<Bus>("bus");
            RegisterDerived<Truck>("truck");
            RegisterDerived<Van>("van");

            _isConfigured = true;
        }

        /// <summary>
        /// Registers single derived class in BSON class map.
        /// </summary>
        private static void RegisterDerived<T>(string discriminator) where T : Vehicle
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                return;
            }

            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator(discriminator);
            });
        }

        /// <summary>
        /// Serializes given list of vehicles to BSON file.
        /// List is wrapped into single root document with "items" field.
        /// </summary>
        public static void SaveToFile(string filePath, IList<Vehicle> vehicles)
        {
            Configure();

            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            using var writer = new BsonBinaryWriter(stream);

            // Root BSON element must be a document, not an array.
            writer.WriteStartDocument();
            writer.WriteName("items");
            BsonSerializer.Serialize(writer, vehicles);
            writer.WriteEndDocument();
        }

        /// <summary>
        /// Deserializes list of vehicles from BSON file. Returns empty list if file does not exist.
        /// </summary>
        public static List<Vehicle> LoadFromFile(string filePath)
        {
            Configure();

            if (!File.Exists(filePath))
            {
                return new List<Vehicle>();
            }

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var reader = new BsonBinaryReader(stream);

            try
            {
                // Preferred format: root BSON document with "items" array field.
                var document = BsonSerializer.Deserialize<BsonDocument>(reader);

                if (!document.Contains("items") || !document["items"].IsBsonArray)
                {
                    return new List<Vehicle>();
                }

                var array = document["items"].AsBsonArray;
                var result = new List<Vehicle>(array.Count);
                foreach (var value in array)
                {
                    if (value.IsBsonDocument)
                    {
                        var vehicle = BsonSerializer.Deserialize<Vehicle>(value.AsBsonDocument);
                        result.Add(vehicle);
                    }
                }

                return result;
            }
            catch
            {
                // Fallback for legacy format: root is array of vehicles.
                try
                {
                    stream.Position = 0;
                    using var readerArray = new BsonBinaryReader(stream);
                    return BsonSerializer.Deserialize<List<Vehicle>>(readerArray);
                }
                catch
                {
                    // If file is corrupted or of unknown format, just return empty list.
                    return new List<Vehicle>();
                }
            }
        }
    }
}

