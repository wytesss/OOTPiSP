// =============================================================================
// Файл: Form1.cs — главное окно: список ТС, CRUD, сериализация, статус плагинов.
// Связи: VehicleTypeRegistry, VehicleBsonSerializer, PluginLoader.
// =============================================================================

using System.ComponentModel;
using lab4.Contracts.Domain;
using lab4.Domain;
using lab4.Plugins;
using lab4.Serialization;

namespace lab4
{
    public partial class Form1 : Form
    {
        private const string FilePath = "vehicles.bson";
        /// <summary>Список в памяти; BindingList обновляет ListBox автоматически.</summary>
        private readonly BindingList<Vehicle> _vehicles = new();

        public Form1()
        {
            InitializeComponent();
            InitializeDataBinding();
            UpdatePluginStatus();
        }

        /// <summary>Привязка UI + автозагрузка файла при старте + заполнение ComboBox типов.</summary>
        private void InitializeDataBinding()
        {
            listVehicles.DataSource = _vehicles;
            RefreshVehicleTypes();

            List<Vehicle> loaded = VehicleBsonSerializer.LoadFromFile(FilePath);
            foreach (Vehicle v in loaded)
            {
                _vehicles.Add(v);
            }
        }

        /// <summary>Источник типов — реестр (встроенные + из плагинов).</summary>
        private void RefreshVehicleTypes()
        {
            comboVehicleTypes.DataSource = null;
            comboVehicleTypes.DataSource = VehicleTypeRegistry.Types.ToList();
            comboVehicleTypes.DisplayMember = nameof(VehicleTypeInfo.DisplayName);
        }

        private void UpdatePluginStatus()
        {
            var plugins = VehicleTypeRegistry.LoadedPlugins.ToList();
            if (plugins.Count == 0)
            {
                labelPlugins.Text = "Plugins: none loaded";
                return;
            }

            labelPlugins.Text = "Plugins: " + string.Join(", ", plugins);
        }

        /// <summary>Создание через фабрику info.Factory() — паттерн «Фабрика».</summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboVehicleTypes.SelectedItem is not VehicleTypeInfo info)
            {
                MessageBox.Show("Please select vehicle type first.", "Add vehicle");
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
                MessageBox.Show("Please select vehicle in the list.", "Edit vehicle");
                return;
            }

            selected.Edit();
            _vehicles.ResetItem(listVehicles.SelectedIndex);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listVehicles.SelectedItem is not Vehicle selected)
            {
                MessageBox.Show("Please select vehicle in the list.", "Remove vehicle");
                return;
            }

            _vehicles.Remove(selected);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            VehicleBsonSerializer.SaveToFile(FilePath, _vehicles.ToList());
            MessageBox.Show("List successfully serialized to file.", "Serialize");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            List<Vehicle> loaded = VehicleBsonSerializer.LoadFromFile(FilePath);
            _vehicles.Clear();
            foreach (Vehicle v in loaded)
            {
                _vehicles.Add(v);
            }

            MessageBox.Show("List successfully deserialized from file.", "Deserialize");
        }

        /// <summary>Перезагрузка: очистить реестр → снова встроенные типы → LoadAll из Plugins.</summary>
        private void buttonReloadPlugins_Click(object sender, EventArgs e)
        {
            VehicleTypeRegistry.Clear();
            VehicleBootstrap.RegisterBuiltInTypes();
            PluginLoader.LoadAll();

            RefreshVehicleTypes();
            UpdatePluginStatus();

            if (PluginLoader.LoadErrors.Count > 0)
            {
                MessageBox.Show(
                    string.Join(Environment.NewLine, PluginLoader.LoadErrors),
                    "Plugin load warnings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Plugins reloaded from the Plugins folder.", "Plugins");
            }
        }
    }
}
