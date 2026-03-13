namespace OOP_Lab2_GraphicEditor.Factories
{
    // Describes how the numeric fields in the dialog should look for a shape.
    public class ShapeParameterDescription
    {
        public string XLabel { get; set; } = "X";

        public string YLabel { get; set; } = "Y";

        public string Size1Label { get; set; } = "Size 1";

        public string Size2Label { get; set; } = "Size 2";

        public bool UseSize2 { get; set; } = true;
    }
}

