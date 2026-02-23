using System.Drawing;

namespace Lab1
{
    public class Triangle : Shape
    {
        public Point[] Points { get; set; }

        public Triangle(Point p1, Point p2, Point p3, Color color)
            : base(color)
        {
            Points = new Point[] { p1, p2, p3 };
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawPolygon(pen, Points);
            }
        }
    }
}