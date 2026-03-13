using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Rectangle model defined by top-left corner and size.
    public class RectangleShape : IShape
    {
        // Create a rectangle with position, width, height and color.
        public RectangleShape(Point topLeft, int width, int height, Color color)
        {
            Position = topLeft;
            Width = width;
            Height = height;
            Color = color;
        }

        public Point Position { get; }

        public Color Color { get; }

        // Width of the rectangle in pixels.
        public int Width { get; }

        // Height of the rectangle in pixels.
        public int Height { get; }
    }
}

