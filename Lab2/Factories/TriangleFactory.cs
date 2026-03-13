using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Creates Triangle objects based on values from the dialog.
    // Converts generic numeric fields into triangle-specific data.
    public class TriangleFactory : IShapeFactory
    {
        // Short id used inside the app.
        public string Id => "Triangle";

        // Text shown in the shape type combo box.
        public string DisplayName => "Triangle";

        // Build a Triangle instance from generic creation parameters.
        public IShape Create(ShapeCreationParameters parameters)
        {
            // Interpret Size1 as base width and Size2 as height.
            int baseWidth = parameters.Size1 <= 0 ? 10 : parameters.Size1;
            int height = parameters.Size2 <= 0 ? 10 : parameters.Size2;

            return new Triangle(parameters.Position, baseWidth, height, parameters.Color == Color.Empty ? Color.Black : parameters.Color);
        }

        // Describe how the dialog fields should look for a triangle.
        public ShapeParameterDescription GetParameterDescription()
        {
            return new ShapeParameterDescription
            {
                XLabel = "Top X",
                YLabel = "Top Y",
                Size1Label = "Base width",
                Size2Label = "Height",
                UseSize2 = true
            };
        }
    }
}

