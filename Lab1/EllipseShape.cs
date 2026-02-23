using System.Drawing;
// ELLIPS VPISAN V PRYAMOUGOLNIK
namespace Lab1
{
    public class EllipseShape : Shape
    {
        public Rectangle Rect { get; set; }

        public EllipseShape(Rectangle rect, Color color)
            : base(color)
        {
            Rect = rect;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawEllipse(pen, Rect);
            }
        }
    }
}