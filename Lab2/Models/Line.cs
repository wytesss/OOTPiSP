using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Line model defined by start and end points.
    public class Line : IShape
    {
        // Create a line with start point, end point and color.
        public Line(Point start, Point end, Color color)
        {
            Start = start;
            End = end;
            Color = color;
        }

        public Point Start { get; }

        public Point End { get; }

        // For IShape we use the start point as Position.
        public Point Position => Start;

        public Color Color { get; }
    }
}

