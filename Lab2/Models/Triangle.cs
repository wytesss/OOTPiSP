using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Simple isosceles triangle model (top point, base width, height).
    public class Triangle : IShape
    {
        // Create a triangle with top point, base width, height and color.
        public Triangle(Point topVertex, int baseWidth, int height, Color color)
        {
            Position = topVertex;
            BaseWidth = baseWidth;
            Height = height;
            Color = color;
        }

        public Point Position { get; }

        public Color Color { get; }

        // Base width of the triangle in pixels.
        public int BaseWidth { get; }

        // Height of the triangle in pixels.
        public int Height { get; }
    }
}

