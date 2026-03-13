using System.Collections.Generic;

namespace OOP_Lab2_GraphicEditor.Factories
{
    // Keeps a list of all shape factories available to the UI.
    public class ShapeFactoryRegistry
    {
        private readonly List<IShapeFactory> factories;

        public ShapeFactoryRegistry()
        {
            factories = new List<IShapeFactory>();

            // Register factories for all supported shapes.
            factories.Add(new CircleFactory());
            factories.Add(new TriangleFactory());
            factories.Add(new RectangleFactory());
            factories.Add(new EllipseFactory());
            factories.Add(new LineFactory());
        }

        public IEnumerable<IShapeFactory> GetAllFactories()
        {
            return factories;
        }
    }
}

