using System.Drawing;
using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Rendering
{
    // Interface for classes that can draw shapes using a Graphics object.
    public interface IShapeRenderer
    {
        // Draw one shape on the given graphics.
        void DrawShape(Graphics graphics, IShape shape);
    }
}

