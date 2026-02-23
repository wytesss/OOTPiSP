using Lab1;
using System;
using System.Windows.Forms;

namespace Lab1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
        }
    }
}