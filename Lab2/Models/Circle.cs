using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Simple circle model (center + radius).
    // The class does not know how to draw itself.
    public class Circle : IShape
    {
        // Create a circle with center, radius and outline color.
        public Circle(Point center, int radius, Color color)
        {
            Position = center;
            Radius = radius;
            Color = color;
        }

        public Point Position { get; }

        public Color Color { get; }

        // Radius of the circle in pixels.
        public int Radius { get; }
    }
}

