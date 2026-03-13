using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Creates Line objects based on values from the dialog.
    // Treats the first point as a start and size values as offsets.
    public class LineFactory : IShapeFactory
    {
        // Short id used inside the app.
        public string Id => "Line";

        // Text shown in the shape type combo box.
        public string DisplayName => "Line";

        // Build a Line instance from generic creation parameters.
        public IShape Create(ShapeCreationParameters parameters)
        {
            // Interpret Position as start point and Size1/Size2 as offsets to compute the end point.
            var start = parameters.Position;
            var end = new Point(start.X + parameters.Size1, start.Y + parameters.Size2);

            return new Line(start, end,
                parameters.Color == Color.Empty ? Color.Black : parameters.Color);
        }

        // Describe how the dialog fields should look for a line.
        public ShapeParameterDescription GetParameterDescription()
        {
            return new ShapeParameterDescription
            {
                XLabel = "Start X",
                YLabel = "Start Y",
                Size1Label = "Delta X",
                Size2Label = "Delta Y",
                UseSize2 = true
            };
        }
    }
}

