using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Creates EllipseShape objects based on values from the dialog.
    // Uses generic numeric fields as bounding rectangle data.
    public class EllipseFactory : IShapeFactory
    {
        // Short id used inside the app.
        public string Id => "Ellipse";

        // Text shown in the shape type combo box.
        public string DisplayName => "Ellipse";

        // Build an EllipseShape instance from generic creation parameters.
        public IShape Create(ShapeCreationParameters parameters)
        {
            int width = parameters.Size1 <= 0 ? 10 : parameters.Size1;
            int height = parameters.Size2 <= 0 ? 10 : parameters.Size2;

            return new EllipseShape(parameters.Position, width, height,
                parameters.Color == Color.Empty ? Color.Black : parameters.Color);
        }

        // Describe how the dialog fields should look for an ellipse.
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

