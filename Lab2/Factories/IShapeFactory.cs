using OOP_Lab2_GraphicEditor.Models;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Interface for all shape factories.
    // Each factory knows how to build one concrete shape type from raw values.
    public interface IShapeFactory
    {
        // Unique id of the shape type (for example, "Circle").
        string Id { get; }

        // Text shown to the user in the UI.
        string DisplayName { get; }

        // Create a shape object using the given parameters from the dialog.
        IShape Create(ShapeCreationParameters parameters);

        // Describe how the dialog numeric fields should be labeled for this shape.
        ShapeParameterDescription GetParameterDescription();
    }
}

