using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Creates Circle objects based on values from the dialog.
    // This class hides how exactly a Circle is built from raw numbers.
    public class CircleFactory : IShapeFactory
    {
        // Short id used inside the app.
        public string Id => "Circle";

        // Text that the user sees in the combo box.
        public string DisplayName => "Circle";

        // Build a Circle instance from generic creation parameters.
        public IShape Create(ShapeCreationParameters parameters)
        {
            // Interpret Size1 as radius; Size2 is ignored for circles.
            int radius = parameters.Size1;
            if (radius <= 0)
            {
                radius = 1;
            }

            return new Circle(parameters.Position, radius, parameters.Color == Color.Empty ? Color.Black : parameters.Color);
        }

        // Describe how the dialog fields should look for a circle.
        public ShapeParameterDescription GetParameterDescription()
        {
            return new ShapeParameterDescription
            {
                XLabel = "Center X",
                YLabel = "Center Y",
                Size1Label = "Radius",
                Size2Label = "Not used",
                UseSize2 = false
            };
        }
    }
}

