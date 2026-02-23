using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private ShapeList shapeList = new ShapeList();

        public Form1()
        {
            InitializeComponent();

            // Статическая инициализация фигур
            shapeList.Add(new Line(new Point(10, 10), new Point(200, 50), Color.Red));
            shapeList.Add(new RectangleShape(new Rectangle(50, 70, 100, 60), Color.Blue));
            shapeList.Add(new EllipseShape(new Rectangle(200, 70, 100, 60), Color.Green));
            shapeList.Add(new Circle(new Point(150, 200), 40, Color.Purple));
            shapeList.Add(new Triangle(
                new Point(300, 150),
                new Point(350, 250),
                new Point(250, 250),
                Color.Orange));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            shapeList.DrawAll(e.Graphics);
        }
    }
}