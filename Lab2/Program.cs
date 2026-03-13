using System;
using System.Windows.Forms;

namespace OOP_Lab2_GraphicEditor
{
    // Entry point for the Windows Forms application.
    internal static class Program
    {
        // Configures and starts the main form.
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}

