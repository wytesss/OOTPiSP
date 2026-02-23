using System.Drawing;
// NASLEDOVANIYE
namespace Lab1
{
    public abstract class Shape
    {
        public Color Color { get; set; }

        public Shape(Color color)
        {
            Color = color;
        }

        // VIRTUALNIY METOD
        public abstract void Draw(Graphics g);
    }
}