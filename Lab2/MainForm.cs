using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OOP_Lab2_GraphicEditor.Factories;
using OOP_Lab2_GraphicEditor.Models;
using OOP_Lab2_GraphicEditor.Rendering;
using OOP_Lab2_GraphicEditor.UI;

namespace OOP_Lab2_GraphicEditor
{
    // Main window of the graphic editor.
    // Contains the drawing area and controls for working with shapes.
    public partial class MainForm : Form
    {
        private readonly List<IShape> shapes;
        private readonly ShapeFactoryRegistry shapeFactoryRegistry;
        private readonly GdiShapeRenderer shapeRenderer;

        public MainForm()
        {
            InitializeComponent();

            shapes = new List<IShape>();
            shapeFactoryRegistry = new ShapeFactoryRegistry();
            shapeRenderer = new GdiShapeRenderer();

            InitializeShapeTypeCombo();
        }

        private void InitializeShapeTypeCombo()
        {
            comboShapeType.DisplayMember = "DisplayName";
            comboShapeType.ValueMember = "Id";
            comboShapeType.DataSource = new List<IShapeFactory>(shapeFactoryRegistry.GetAllFactories());
        }

        private void btnAddShape_Click(object sender, EventArgs e)
        {
            if (comboShapeType.SelectedItem is not IShapeFactory selectedFactory)
            {
                MessageBox.Show("Please select a shape type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ShapeCreationDialog(selectedFactory);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IShape newShape = dialog.CreatedShape;
                if (newShape != null)
                {
                    shapes.Add(newShape);
                    canvasPanel.Invalidate();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            canvasPanel.Invalidate();
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var shape in shapes)
            {
                shapeRenderer.DrawShape(e.Graphics, shape);
            }
        }
    }
}

