// =============================================================================
// Файл: VehicleBootstrap.cs — начальная регистрация встроенных типов при старте.
// Вызывается из Program.Main до загрузки DLL из Plugins/.
// =============================================================================

using lab6.Serialization;

namespace lab6.Domain
{
    public static class VehicleBootstrap
    {
        /// <summary>
        /// Регистрирует 4 встроенных типа: в реестре (фабрики) и в BSON (дискриминаторы).
        /// </summary>
        public static void RegisterBuiltInTypes()
        {
            // Паттерн «Фабрика»: каждая запись хранит () => new ...
            VehicleTypeRegistry.RegisterBuiltIn("car", "Car", () => new Car());
            VehicleTypeRegistry.RegisterBuiltIn("bus", "Bus", () => new Bus());
            VehicleTypeRegistry.RegisterBuiltIn("truck", "Truck", () => new Truck());
            VehicleTypeRegistry.RegisterBuiltIn("van", "Van", () => new Van());

            // Без RegisterDerived десериализация не узнает наследников Vehicle
            VehicleBsonSerializer.RegisterDerived<Car>("car");
            VehicleBsonSerializer.RegisterDerived<Bus>("bus");
            VehicleBsonSerializer.RegisterDerived<Truck>("truck");
            VehicleBsonSerializer.RegisterDerived<Van>("van");
        }
    }
}
