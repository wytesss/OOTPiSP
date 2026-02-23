using System.Collections.Generic;
using System.Drawing;
// SPISOK
namespace Lab1
{
    public class ShapeList
    {
        private List<Shape> shapes = new List<Shape>();

        public void Add(Shape shape)
        {
            shapes.Add(shape);
        }

        public void DrawAll(Graphics g)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(g);  // TIPO POLYMORPHISM
            }
        }
    }
}