using System.Drawing;
// KRUG
namespace Lab1
{
    public class Circle : Shape
    {
        public Point Center { get; set; }
        public int Radius { get; set; }

        public Circle(Point center, int radius, Color color)
            : base(color)
        {
            Center = center;
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawEllipse(pen,
                    Center.X - Radius,
                    Center.Y - Radius,
                    Radius * 2,
                    Radius * 2);
            }
        }
    }
}