using System.Drawing;

namespace OOP_Lab2_GraphicEditor.Models
{
    // Base interface for all shapes in the editor (no drawing here).
    public interface IShape
    {
        // Main reference point of the shape (center, top-left, etc.).
        Point Position { get; }

        // Outline color of the shape.
        Color Color { get; }
    }
}

