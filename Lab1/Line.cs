using System.Drawing;

namespace Lab1
{
    public class Line : Shape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Line(Point start, Point end, Color color)
            : base(color)
        {
            Start = start;
            End = end;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawLine(pen, Start, End);
            }
        }
    }
}