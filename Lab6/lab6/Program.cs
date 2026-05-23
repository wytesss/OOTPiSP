// =============================================================================
// Файл: Program.cs — точка входа lab6: checksum + xor-cipher (адаптер) по умолчанию.
// =============================================================================

using lab6.Domain;
using lab6.Plugins;

namespace lab6
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            var settings = AppSettings.Load();
            if (settings.EnabledProcessingIds.Count == 0)
            {
                settings.EnabledProcessingIds.Add("checksum");
                settings.EnabledProcessingIds.Add("xor-cipher");
            }

            VehicleBootstrap.RegisterBuiltInTypes();
            PluginLoader.LoadAll(args);

            Application.Run(new Form1(settings));
        }
    }
}
