// =============================================================================
// Файл: Program.cs — точка входа WinForms-приложения (лаб. 4).
// Порядок: UI init → встроенные типы → загрузка плагинов → Form1.
// =============================================================================

using lab4.Domain;
using lab4.Plugins;

namespace lab4
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            // Реестр: Car, Bus, Truck, Van + BSON-маппинг
            VehicleBootstrap.RegisterBuiltInTypes();
            // Рефлексия: Plugins/*.dll с IVehiclePlugin
            PluginLoader.LoadAll(args);

            Application.Run(new Form1());
        }
    }
}
