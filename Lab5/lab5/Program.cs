// =============================================================================
// Файл: Program.cs — точка входа lab5: настройки, bootstrap, плагины, Form1.
// =============================================================================

using lab5.Domain;
using lab5.Plugins;

namespace lab5
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            var settings = AppSettings.Load();
            // Первый запуск: включить checksum по умолчанию
            if (settings.EnabledProcessingIds.Count == 0)
            {
                settings.EnabledProcessingIds.Add("checksum");
            }

            VehicleBootstrap.RegisterBuiltInTypes();
            PluginLoader.LoadAll(args);

            Application.Run(new Form1(settings));
        }
    }
}
