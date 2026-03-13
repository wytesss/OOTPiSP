using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Simple data object with values that all shape factories use.
    public class ShapeCreationParameters
    {
        public Point Position { get; set; }

        public int Size1 { get; set; }

        public int Size2 { get; set; }

        public Color Color { get; set; }
    }
}

