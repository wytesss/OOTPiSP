// =============================================================================
// Файл: SettingsForm.cs — окно «Настройки»: галочки только для загруженных плагинов обработки.
// =============================================================================

using lab6.Contracts.Plugins;
using lab6.Plugins;

namespace lab6
{
    public partial class SettingsForm : Form
    {
        private readonly AppSettings _settings;
        private readonly List<CheckBox> _processingCheckBoxes = new();

        public SettingsForm(AppSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            BuildProcessingOptions();
        }

        /// <summary>Динамический список чекбоксов из SerializationPluginRegistry.</summary>
        private void BuildProcessingOptions()
        {
            flowProcessing.Controls.Clear();
            _processingCheckBoxes.Clear();

            var plugins = SerializationPluginRegistry.Plugins;
            if (plugins.Count == 0)
            {
                var label = new Label
                {
                    Text = "Нет загруженных плагинов обработки. Загрузите DLL через меню «Плагины».",
                    AutoSize = true,
                    MaximumSize = new Size(420, 0)
                };
                flowProcessing.Controls.Add(label);
                return;
            }

            foreach (ISerializationProcessingPlugin plugin in plugins)
            {
                var checkBox = new CheckBox
                {
                    Text = plugin.DisplayName,
                    AutoSize = true,
                    Tag = plugin.ProcessingTypeId,
                    Checked = _settings.EnabledProcessingIds.Contains(plugin.ProcessingTypeId)
                };
                _processingCheckBoxes.Add(checkBox);
                flowProcessing.Controls.Add(checkBox);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _settings.EnabledProcessingIds.Clear();
            foreach (CheckBox checkBox in _processingCheckBoxes)
            {
                if (checkBox.Checked && checkBox.Tag is string id)
                {
                    _settings.EnabledProcessingIds.Add(id);
                }
            }

            _settings.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
