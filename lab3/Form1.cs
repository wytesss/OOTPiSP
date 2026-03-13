using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using lab3.Domain;
using lab3.Serialization;

namespace lab3
{
    /// <summary>
    /// Main form that provides user interface for managing vehicle list.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Name of file that stores serialized list.
        /// </summary>
        private const string FilePath = "vehicles.bson";

        /// <summary>
        /// Binding list used as data source for list box.
        /// </summary>
        private readonly BindingList<Vehicle> _vehicles = new();

        /// <summary>
        /// Initializes new instance of main form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeDataBinding();
        }

        /// <summary>
        /// Configures data binding and loads initial data from file.
        /// </summary>
        private void InitializeDataBinding()
        {
            // Bind list of vehicles to list box.
            listVehicles.DataSource = _vehicles;

            // Load registered vehicle types into combo box.
            IList<VehicleTypeInfo> types = VehicleTypeRegistry.Types.ToList();
            comboVehicleTypes.DataSource = types;
            comboVehicleTypes.DisplayMember = nameof(VehicleTypeInfo.DisplayName);

            // Try to load previously saved data from BSON file.
            List<Vehicle> loaded = VehicleBsonSerializer.LoadFromFile(FilePath);
            foreach (Vehicle v in loaded)
            {
                _vehicles.Add(v);
            }
        }

        /// <summary>
        /// Handles Add button click and creates new vehicle instance using selected type.
        /// </summary>
        private void buttonAdd_Click(object sender, System.EventArgs e)
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

        /// <summary>
        /// Handles Edit button click and allows user to change properties of selected vehicle.
        /// </summary>
        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            if (listVehicles.SelectedItem is not Vehicle selected)
            {
                MessageBox.Show("Please select vehicle in the list.", "Edit vehicle");
                return;
            }

            selected.Edit();

            // Notify list box that item was updated.
            int index = listVehicles.SelectedIndex;
            _vehicles.ResetItem(index);
        }

        /// <summary>
        /// Handles Remove button click and deletes selected vehicle from the list.
        /// </summary>
        private void buttonRemove_Click(object sender, System.EventArgs e)
        {
            if (listVehicles.SelectedItem is not Vehicle selected)
            {
                MessageBox.Show("Please select vehicle in the list.", "Remove vehicle");
                return;
            }

            _vehicles.Remove(selected);
        }

        /// <summary>
        /// Handles Serialize button click and saves list of vehicles to BSON file.
        /// </summary>
        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            VehicleBsonSerializer.SaveToFile(FilePath, _vehicles.ToList());
            MessageBox.Show("List successfully serialized to file.", "Serialize");
        }

        /// <summary>
        /// Handles Deserialize button click and loads list of vehicles from BSON file.
        /// </summary>
        private void buttonLoad_Click(object sender, System.EventArgs e)
        {
            List<Vehicle> loaded = VehicleBsonSerializer.LoadFromFile(FilePath);

            _vehicles.Clear();
            foreach (Vehicle v in loaded)
            {
                _vehicles.Add(v);
            }

            MessageBox.Show("List successfully deserialized from file.", "Deserialize");
        }
    }
}

