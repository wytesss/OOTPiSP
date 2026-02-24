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

            // INICIALIZACIA FIGURES
            shapeList.Add(new Line(new Point(500, 60), new Point(720, 190), Color.Red));
            shapeList.Add(new RectangleShape(new Rectangle(30, 70, 250, 100), Color.Blue));
            shapeList.Add(new EllipseShape(new Rectangle(95, 230, 300, 140), Color.Green));
            shapeList.Add(new Circle(new Point(390, 130), 90, Color.Purple));
            shapeList.Add(new Triangle(new Point(550, 150),
                                       new Point(420, 250),
                                       new Point(600, 400),
                                       Color.Orange));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            shapeList.DrawAll(e.Graphics);
        }
    }
}