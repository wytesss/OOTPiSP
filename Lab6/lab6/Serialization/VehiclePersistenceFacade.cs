// =============================================================================
// Файл: VehiclePersistenceFacade.cs — паттерн «Фасад» для UI.
// Скрывает VehicleBsonSerializer, AppSettings и реестр плагинов обработки.
// =============================================================================

using lab6.Contracts.Domain;
using lab6.Plugins;

namespace lab6.Serialization
{
    public sealed class VehiclePersistenceFacade
    {
        private readonly AppSettings _settings;

        public VehiclePersistenceFacade(AppSettings settings)
        {
            _settings = settings;
        }

        public void Save(string filePath, IList<Vehicle> vehicles)
        {
            VehicleBsonSerializer.SaveToFile(filePath, vehicles, _settings);
        }

        public LoadResult Load(string filePath)
        {
            return VehicleBsonSerializer.LoadFromFile(filePath, _settings);
        }
    }
}
