using System;
using System.Drawing;
using System.Windows.Forms;
using OOP_Lab2_GraphicEditor.Factories;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.UI
{
    // Dialog window that asks the user for shape parameters and color.
    // Uses a shape factory to build the final shape object.
    public partial class ShapeCreationDialog : Form
    {
        private readonly IShapeFactory shapeFactory;

        public ShapeCreationDialog(IShapeFactory shapeFactory)
        {
            this.shapeFactory = shapeFactory ?? throw new ArgumentNullException(nameof(shapeFactory));
            InitializeComponent();
            ApplyParameterDescription();
        }

        public IShape? CreatedShape { get; private set; }

        private void ApplyParameterDescription()
        {
            Text = $"Create {shapeFactory.DisplayName}";
            lblShapeTypeInfo.Text = shapeFactory.DisplayName;

            var description = shapeFactory.GetParameterDescription();
            lblX.Text = description.XLabel;
            lblY.Text = description.YLabel;
            lblSize1.Text = description.Size1Label;
            lblSize2.Text = description.Size2Label;
            numSize2.Enabled = description.UseSize2;
            numSize2.Visible = description.UseSize2;
            lblSize2.Visible = description.UseSize2;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var parameters = new ShapeCreationParameters
                {
                    Position = new Point((int)numX.Value, (int)numY.Value),
                    Size1 = (int)numSize1.Value,
                    Size2 = (int)numSize2.Value,
                    Color = btnColor.BackColor
                };

                CreatedShape = shapeFactory.Create(parameters);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to create shape: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using var dialog = new ColorDialog
            {
                AllowFullOpen = true,
                AnyColor = true,
                SolidColorOnly = false,
                Color = btnColor.BackColor
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                btnColor.BackColor = dialog.Color;
            }
        }
    }
}

