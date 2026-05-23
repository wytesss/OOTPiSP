// =============================================================================
// Файл: Form1.Designer.cs — разметка WinForms (создано дизайнером, правки вручную).
// InitializeComponent: создание контролов, расположение, привязка Click → Form1.cs
// =============================================================================

namespace lab4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox listVehicles;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonRemove;
        private Button buttonSave;
        private Button buttonLoad;
        private ComboBox comboVehicleTypes;
        private Label labelTypes;
        private Label labelPlugins;
        private Button buttonReloadPlugins;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listVehicles = new ListBox();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonRemove = new Button();
            buttonSave = new Button();
            buttonLoad = new Button();
            comboVehicleTypes = new ComboBox();
            labelTypes = new Label();
            labelPlugins = new Label();
            buttonReloadPlugins = new Button();
            SuspendLayout();
            //
            // listVehicles — основной список объектов Vehicle
            //
            listVehicles.FormattingEnabled = true;
            listVehicles.Location = new Point(14, 95);
            listVehicles.Name = "listVehicles";
            listVehicles.Size = new Size(685, 474);
            listVehicles.TabIndex = 0;
            //
            // buttonAdd — вызывает info.Factory() в Form1.cs
            //
            buttonAdd.Location = new Point(705, 95);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(137, 40);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            //
            // buttonEdit
            //
            buttonEdit.Location = new Point(705, 145);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(137, 40);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Edit";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            //
            // buttonRemove
            //
            buttonRemove.Location = new Point(705, 195);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(137, 40);
            buttonRemove.TabIndex = 3;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            //
            // buttonSave — сериализация в vehicles.bson
            //
            buttonSave.Location = new Point(705, 377);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(137, 40);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Serialize";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            //
            // buttonLoad
            //
            buttonLoad.Location = new Point(705, 427);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(137, 40);
            buttonLoad.TabIndex = 5;
            buttonLoad.Text = "Deserialize";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            //
            // comboVehicleTypes — данные из VehicleTypeRegistry.Types
            //
            comboVehicleTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboVehicleTypes.FormattingEnabled = true;
            comboVehicleTypes.Location = new Point(281, 17);
            comboVehicleTypes.Name = "comboVehicleTypes";
            comboVehicleTypes.Size = new Size(418, 28);
            comboVehicleTypes.TabIndex = 6;
            //
            // labelTypes
            //
            labelTypes.AutoSize = true;
            labelTypes.Location = new Point(14, 20);
            labelTypes.Name = "labelTypes";
            labelTypes.Size = new Size(140, 20);
            labelTypes.TabIndex = 7;
            labelTypes.Text = "Vehicle type to add:";
            //
            // labelPlugins — текст задаётся в UpdatePluginStatus()
            //
            labelPlugins.AutoSize = true;
            labelPlugins.Location = new Point(14, 52);
            labelPlugins.Name = "labelPlugins";
            labelPlugins.Size = new Size(120, 20);
            labelPlugins.TabIndex = 8;
            labelPlugins.Text = "Plugins: loading...";
            //
            // buttonReloadPlugins
            //
            buttonReloadPlugins.Location = new Point(705, 245);
            buttonReloadPlugins.Name = "buttonReloadPlugins";
            buttonReloadPlugins.Size = new Size(137, 40);
            buttonReloadPlugins.TabIndex = 9;
            buttonReloadPlugins.Text = "Reload plugins";
            buttonReloadPlugins.UseVisualStyleBackColor = true;
            buttonReloadPlugins.Click += buttonReloadPlugins_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 590);
            Controls.Add(buttonReloadPlugins);
            Controls.Add(labelPlugins);
            Controls.Add(labelTypes);
            Controls.Add(comboVehicleTypes);
            Controls.Add(buttonLoad);
            Controls.Add(buttonSave);
            Controls.Add(buttonRemove);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(listVehicles);
            Name = "Form1";
            Text = "Vehicle manager with plugins (Lab 4)";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
