using System;
using System.Collections.Generic;
using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Rendering
{
    // Shape renderer that uses GDI+ (System.Drawing) to draw shapes.
    // Drawing code is separated from the shape model classes.
    public class GdiShapeRenderer : IShapeRenderer
    {
        private readonly Dictionary<Type, Action<Graphics, IShape>> renderers;

        public GdiShapeRenderer()
        {
            renderers = new Dictionary<Type, Action<Graphics, IShape>>
            {
                { typeof(Circle), DrawCircle },
                { typeof(Triangle), DrawTriangle },
                { typeof(RectangleShape), DrawRectangle },
                { typeof(EllipseShape), DrawEllipse },
                { typeof(Line), DrawLine }
            };
        }

        public void DrawShape(Graphics graphics, IShape shape)
        {
            if (graphics == null || shape == null)
            {
                return;
            }

            var type = shape.GetType();
            if (renderers.TryGetValue(type, out var drawAction))
            {
                drawAction(graphics, shape);
            }
        }

        private void DrawCircle(Graphics g, IShape shape)
        {
            var circle = (Circle)shape;
            int diameter = circle.Radius * 2;
            var topLeft = new Point(circle.Position.X - circle.Radius, circle.Position.Y - circle.Radius);

            using var pen = new Pen(circle.Color, 2);
            g.DrawEllipse(pen, new Rectangle(topLeft, new Size(diameter, diameter)));
        }

        private void DrawTriangle(Graphics g, IShape shape)
        {
            var triangle = (Triangle)shape;

            Point top = triangle.Position;
            Point left = new Point(top.X - triangle.BaseWidth / 2, top.Y + triangle.Height);
            Point right = new Point(top.X + triangle.BaseWidth / 2, top.Y + triangle.Height);

            using var pen = new Pen(triangle.Color, 2);
            g.DrawPolygon(pen, new[] { top, right, left });
        }

        private void DrawRectangle(Graphics g, IShape shape)
        {
            var rectShape = (RectangleShape)shape;
            var rect = new Rectangle(rectShape.Position.X, rectShape.Position.Y, rectShape.Width, rectShape.Height);

            using var pen = new Pen(rectShape.Color, 2);
            g.DrawRectangle(pen, rect);
        }

        private void DrawEllipse(Graphics g, IShape shape)
        {
            var ellipse = (EllipseShape)shape;
            var rect = new Rectangle(ellipse.Position.X, ellipse.Position.Y, ellipse.Width, ellipse.Height);

            using var pen = new Pen(ellipse.Color, 2);
            g.DrawEllipse(pen, rect);
        }

        private void DrawLine(Graphics g, IShape shape)
        {
            var line = (Line)shape;
            using var pen = new Pen(line.Color, 2);
            g.DrawLine(pen, line.Start, line.End);
        }
    }
}

