// =============================================================================
// Файл: MotorcyclePlugin.cs — точка входа DLL-плагина «Мотоцикл».
// Реализует IVehiclePlugin; host загружает класс через PluginLoader.
// =============================================================================

using lab6.Contracts.Plugins;

namespace lab6.Plugin.Motorcycle
{
    public sealed class MotorcyclePlugin : IVehiclePlugin
    {
        public string Name => "Motorcycle plugin";

        /// <summary>Регистрация типа в реестре host и дискриминатора BSON.</summary>
        public void Register(VehiclePluginContext context)
        {
            context.RegisterVehicleType("motorcycle", "Motorcycle (plugin)", () => new Motorcycle());
            context.RegisterBsonType<Motorcycle>("motorcycle");
        }
    }
}
