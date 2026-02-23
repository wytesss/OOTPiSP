using System.Drawing;

namespace Lab1
{
    public abstract class Shape
    {
        public Color Color { get; set; }

        public Shape(Color color)
        {
            Color = color;
        }

        // бхпрсюкэмши лернд
        public abstract void Draw(Graphics g);
    }
}