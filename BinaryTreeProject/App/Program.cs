using System;
using System.Windows.Forms;
using BinaryTreeProject.App.Presnters;
using BinaryTreeProject.App.Views;


namespace BinaryTreeProject.App
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            AppPresenter presenter = new AppPresenter(form);
            Application.Run(form);
        }
    }
}
