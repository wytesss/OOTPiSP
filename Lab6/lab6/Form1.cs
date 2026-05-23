// =============================================================================
// Файл: Form1.cs — главное окно lab6; save/load через фасад VehiclePersistenceFacade.
// =============================================================================

using System.ComponentModel;
using lab6.Contracts.Domain;
using lab6.Domain;
using lab6.Plugins;
using lab6.Serialization;

namespace lab6
{
    public partial class Form1 : Form
    {
        private const string FilePath = "vehicles.bson";
        private readonly BindingList<Vehicle> _vehicles = new();
        private readonly AppSettings _settings;
        /// <summary>Паттерн «Фасад»: UI не вызывает VehicleBsonSerializer напрямую.</summary>
        private readonly VehiclePersistenceFacade _persistence;

        public Form1(AppSettings settings)
        {
            _settings = settings;
            _persistence = new VehiclePersistenceFacade(_settings);
            InitializeComponent();
            InitializeDataBinding();
            UpdatePluginStatus();
            UpdateProcessingStatus();
        }

        private void InitializeDataBinding()
        {
            listVehicles.DataSource = _vehicles;
            RefreshVehicleTypes();

            LoadResult loaded = _persistence.Load(FilePath);
            foreach (Vehicle v in loaded.Vehicles)
            {
                _vehicles.Add(v);
            }

            if (loaded.Messages.Count > 0)
            {
                labelLastLoadInfo.Text = string.Join(" | ", loaded.Messages);
            }
        }

        private void RefreshVehicleTypes()
        {
            comboVehicleTypes.DataSource = null;
            comboVehicleTypes.DataSource = VehicleTypeRegistry.Types.ToList();
            comboVehicleTypes.DisplayMember = nameof(VehicleTypeInfo.DisplayName);
        }

        private void UpdatePluginStatus()
        {
            var vehiclePlugins = VehicleTypeRegistry.LoadedPlugins.ToList();
            var processingPlugins = SerializationPluginRegistry.Plugins
                .Select(p => p.DisplayName)
                .ToList();

            var parts = new List<string>();
            if (vehiclePlugins.Count > 0)
                parts.Add("Типы: " + string.Join(", ", vehiclePlugins));
            if (processingPlugins.Count > 0)
                parts.Add("Обработка: " + string.Join(", ", processingPlugins));

            labelPlugins.Text = parts.Count > 0
                ? "Плагины: " + string.Join("; ", parts)
                : "Плагины: не загружены";
        }

        private void UpdateProcessingStatus()
        {
            var enabled = SerializationPluginRegistry.GetEnabled(_settings.EnabledProcessingIds)
                .Select(p => p.DisplayName)
                .ToList();

            labelProcessing.Text = enabled.Count > 0
                ? "Активная обработка: " + string.Join(", ", enabled)
                : "Активная обработка: отключена (см. Настройки)";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboVehicleTypes.SelectedItem is not VehicleTypeInfo info)
            {
                MessageBox.Show("Выберите тип транспортного средства.", "Добавление");
                return;
            }

            Vehicle vehicle = info.Factory();
            vehicle.Edit();
            _vehicles.Add(vehicle);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listVehicles.SelectedItem is not Vehicle selected)
            {
                MessageBox.Show("Выберите запись в списке.", "Редактирование");
                return;
            }

            selected.Edit();
            _vehicles.ResetItem(listVehicles.SelectedIndex);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listVehicles.SelectedItem is not Vehicle selected)
            {
                MessageBox.Show("Выберите запись в списке.", "Удаление");
                return;
            }

            _vehicles.Remove(selected);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _persistence.Save(FilePath, _vehicles.ToList());
            MessageBox.Show("Список сохранён в файл.", "Сериализация");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadResult loaded = _persistence.Load(FilePath);
            _vehicles.Clear();
            foreach (Vehicle v in loaded.Vehicles)
            {
                _vehicles.Add(v);
            }

            if (loaded.Messages.Count > 0)
            {
                labelLastLoadInfo.Text = string.Join(" | ", loaded.Messages);
                MessageBox.Show(
                    string.Join(Environment.NewLine, loaded.Messages),
                    "Десериализация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                labelLastLoadInfo.Text = "Загрузка без сообщений от плагинов.";
                MessageBox.Show("Список загружен из файла.", "Десериализация");
            }
        }

        private void ReloadPlugins(bool clearProcessingRegistry)
        {
            VehicleTypeRegistry.Clear();
            if (clearProcessingRegistry)
            {
                SerializationPluginRegistry.Clear();
            }

            VehicleBootstrap.RegisterBuiltInTypes();
            PluginLoader.LoadAll();

            RefreshVehicleTypes();
            UpdatePluginStatus();
            UpdateProcessingStatus();
        }

        private void buttonReloadPlugins_Click(object sender, EventArgs e)
        {
            ReloadPlugins(clearProcessingRegistry: true);
            ShowPluginLoadResult("Плагины перезагружены из папки Plugins.");
        }

        private void menuSettingsProcessing_Click(object sender, EventArgs e)
        {
            using var form = new SettingsForm(_settings);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                UpdateProcessingStatus();
            }
        }

        private void menuPluginsLoadFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выбор файла плагина",
                Filter = "Plugin assemblies (*.dll)|*.dll|All files (*.*)|*.*",
                InitialDirectory = Path.Combine(AppContext.BaseDirectory, PluginLoader.PluginsFolderName)
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            PluginLoader.LoadFromFile(dialog.FileName);
            RefreshVehicleTypes();
            UpdatePluginStatus();
            UpdateProcessingStatus();
            ShowPluginLoadResult($"Плагин загружен: {dialog.FileName}");
        }

        private void menuPluginsReloadFolder_Click(object sender, EventArgs e)
        {
            buttonReloadPlugins_Click(sender, e);
        }

        private void ShowPluginLoadResult(string successMessage)
        {
            if (PluginLoader.LoadErrors.Count > 0)
            {
                MessageBox.Show(
                    string.Join(Environment.NewLine, PluginLoader.LoadErrors),
                    "Предупреждения загрузки плагинов",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(successMessage, "Плагины");
            }
        }
    }
}
