// =============================================================================
// Файл: Form1.Designer.cs — разметка UI lab6 (меню «Настройки»/«Плагины», кнопки, метки).
// =============================================================================

namespace lab6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem menuSettings;
        private ToolStripMenuItem menuSettingsProcessing;
        private ToolStripMenuItem menuPlugins;
        private ToolStripMenuItem menuPluginsLoadFile;
        private ToolStripMenuItem menuPluginsReloadFolder;
        private ListBox listVehicles;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonRemove;
        private Button buttonSave;
        private Button buttonLoad;
        private ComboBox comboVehicleTypes;
        private Label labelTypes;
        private Label labelPlugins;
        private Label labelProcessing;
        private Label labelLastLoadInfo;
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
            menuStripMain = new MenuStrip();
            menuSettings = new ToolStripMenuItem();
            menuSettingsProcessing = new ToolStripMenuItem();
            menuPlugins = new ToolStripMenuItem();
            menuPluginsLoadFile = new ToolStripMenuItem();
            menuPluginsReloadFolder = new ToolStripMenuItem();
            listVehicles = new ListBox();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonRemove = new Button();
            buttonSave = new Button();
            buttonLoad = new Button();
            comboVehicleTypes = new ComboBox();
            labelTypes = new Label();
            labelPlugins = new Label();
            labelProcessing = new Label();
            labelLastLoadInfo = new Label();
            buttonReloadPlugins = new Button();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            //
            // menuStripMain
            //
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { menuSettings, menuPlugins });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(882, 28);
            menuStripMain.TabIndex = 10;
            //
            // menuSettings
            //
            menuSettings.DropDownItems.AddRange(new ToolStripItem[] { menuSettingsProcessing });
            menuSettings.Name = "menuSettings";
            menuSettings.Size = new Size(98, 24);
            menuSettings.Text = "Настройки";
            //
            // menuSettingsProcessing
            //
            menuSettingsProcessing.Name = "menuSettingsProcessing";
            menuSettingsProcessing.Size = new Size(280, 26);
            menuSettingsProcessing.Text = "Обработка при сохранении...";
            menuSettingsProcessing.Click += menuSettingsProcessing_Click;
            //
            // menuPlugins
            //
            menuPlugins.DropDownItems.AddRange(new ToolStripItem[] { menuPluginsLoadFile, menuPluginsReloadFolder });
            menuPlugins.Name = "menuPlugins";
            menuPlugins.Size = new Size(72, 24);
            menuPlugins.Text = "Плагины";
            //
            // menuPluginsLoadFile
            //
            menuPluginsLoadFile.Name = "menuPluginsLoadFile";
            menuPluginsLoadFile.Size = new Size(280, 26);
            menuPluginsLoadFile.Text = "Загрузить из файла...";
            menuPluginsLoadFile.Click += menuPluginsLoadFile_Click;
            //
            // menuPluginsReloadFolder
            //
            menuPluginsReloadFolder.Name = "menuPluginsReloadFolder";
            menuPluginsReloadFolder.Size = new Size(280, 26);
            menuPluginsReloadFolder.Text = "Перезагрузить из папки Plugins";
            menuPluginsReloadFolder.Click += menuPluginsReloadFolder_Click;
            //
            // listVehicles
            //
            listVehicles.FormattingEnabled = true;
            listVehicles.Location = new Point(14, 130);
            listVehicles.Name = "listVehicles";
            listVehicles.Size = new Size(685, 454);
            listVehicles.TabIndex = 0;
            //
            // buttonAdd
            //
            buttonAdd.Location = new Point(705, 130);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(137, 40);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            //
            // buttonEdit
            //
            buttonEdit.Location = new Point(705, 180);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(137, 40);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Изменить";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            //
            // buttonRemove
            //
            buttonRemove.Location = new Point(705, 230);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(137, 40);
            buttonRemove.TabIndex = 3;
            buttonRemove.Text = "Удалить";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            //
            // buttonSave
            //
            buttonSave.Location = new Point(705, 412);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(137, 40);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            //
            // buttonLoad
            //
            buttonLoad.Location = new Point(705, 462);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(137, 40);
            buttonLoad.TabIndex = 5;
            buttonLoad.Text = "Загрузить";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            //
            // comboVehicleTypes
            //
            comboVehicleTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboVehicleTypes.FormattingEnabled = true;
            comboVehicleTypes.Location = new Point(281, 52);
            comboVehicleTypes.Name = "comboVehicleTypes";
            comboVehicleTypes.Size = new Size(418, 28);
            comboVehicleTypes.TabIndex = 6;
            //
            // labelTypes
            //
            labelTypes.AutoSize = true;
            labelTypes.Location = new Point(14, 55);
            labelTypes.Name = "labelTypes";
            labelTypes.Size = new Size(175, 20);
            labelTypes.TabIndex = 7;
            labelTypes.Text = "Тип для добавления:";
            //
            // labelPlugins
            //
            labelPlugins.AutoSize = true;
            labelPlugins.Location = new Point(14, 87);
            labelPlugins.Name = "labelPlugins";
            labelPlugins.Size = new Size(120, 20);
            labelPlugins.TabIndex = 8;
            labelPlugins.Text = "Плагины: ...";
            //
            // labelProcessing
            //
            labelProcessing.AutoSize = true;
            labelProcessing.Location = new Point(14, 107);
            labelProcessing.Name = "labelProcessing";
            labelProcessing.Size = new Size(150, 20);
            labelProcessing.TabIndex = 9;
            labelProcessing.Text = "Активная обработка: ...";
            //
            // labelLastLoadInfo
            //
            labelLastLoadInfo.AutoSize = true;
            labelLastLoadInfo.Location = new Point(14, 587);
            labelLastLoadInfo.MaximumSize = new Size(828, 0);
            labelLastLoadInfo.Name = "labelLastLoadInfo";
            labelLastLoadInfo.Size = new Size(0, 20);
            labelLastLoadInfo.TabIndex = 11;
            //
            // buttonReloadPlugins
            //
            buttonReloadPlugins.Location = new Point(705, 280);
            buttonReloadPlugins.Name = "buttonReloadPlugins";
            buttonReloadPlugins.Size = new Size(137, 40);
            buttonReloadPlugins.TabIndex = 12;
            buttonReloadPlugins.Text = "Reload plugins";
            buttonReloadPlugins.UseVisualStyleBackColor = true;
            buttonReloadPlugins.Click += buttonReloadPlugins_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 620);
            Controls.Add(labelLastLoadInfo);
            Controls.Add(buttonReloadPlugins);
            Controls.Add(labelProcessing);
            Controls.Add(labelPlugins);
            Controls.Add(labelTypes);
            Controls.Add(comboVehicleTypes);
            Controls.Add(buttonLoad);
            Controls.Add(buttonSave);
            Controls.Add(buttonRemove);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(listVehicles);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "Form1";
            Text = "Управление ТС с плагинами (Лаб. 5)";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
