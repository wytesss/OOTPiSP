// =============================================================================
// Файл: AppSettings.cs — какие плагины обработки включены (appsettings.json рядом с exe).
// =============================================================================

using System.Text.Json;

namespace lab6.Plugins
{
    public sealed class AppSettings
    {
        /// <summary>ProcessingTypeId включённых плагинов (checksum, save-timestamp…).</summary>
        public HashSet<string> EnabledProcessingIds { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        private static string SettingsPath =>
            Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        public static AppSettings Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    return new AppSettings();

                string json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsPath, json);
        }
    }
}
