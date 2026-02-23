using System.Drawing;
// PRYAMOUGOLNIK
namespace Lab1
{
    public class RectangleShape : Shape
    {
        public Rectangle Rect { get; set; }

        public RectangleShape(Rectangle rect, Color color)
            : base(color)
        {
            Rect = rect;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawRectangle(pen, Rect);
            }
        }
    }
}