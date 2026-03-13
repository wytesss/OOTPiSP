using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Ellipse model defined by its bounding rectangle.
    public class EllipseShape : IShape
    {
        // Create an ellipse with bounding rectangle and color.
        public EllipseShape(Point topLeft, int width, int height, Color color)
        {
            Position = topLeft;
            Width = width;
            Height = height;
            Color = color;
        }

        public Point Position { get; }

        public Color Color { get; }

        // Width of the bounding rectangle.
        public int Width { get; }

        // Height of the bounding rectangle.
        public int Height { get; }
    }
}

