using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Creates RectangleShape objects based on values from the dialog.
    // Converts generic numeric fields into rectangle position and size.
    public class RectangleFactory : IShapeFactory
    {
        // Short id used inside the app.
        public string Id => "Rectangle";

        // Text shown in the shape type combo box.
        public string DisplayName => "Rectangle";

        // Build a RectangleShape instance from generic creation parameters.
        public IShape Create(ShapeCreationParameters parameters)
        {
            int width = parameters.Size1 <= 0 ? 10 : parameters.Size1;
            int height = parameters.Size2 <= 0 ? 10 : parameters.Size2;

            return new RectangleShape(parameters.Position, width, height,
                parameters.Color == Color.Empty ? Color.Black : parameters.Color);
        }

        // Describe how the dialog fields should look for a rectangle.
        public ShapeParameterDescription GetParameterDescription()
        {
            return new ShapeParameterDescription
            {
                XLabel = "Top-left X",
                YLabel = "Top-left Y",
                Size1Label = "Width",
                Size2Label = "Height",
                UseSize2 = true
            };
        }
    }
}

