using System.Windows.Forms;

namespace BinaryTreeProject.App.Views
{
    public partial class FullScreenForm : Form
    {
        public FullScreenForm()
        {
            InitializeComponent();
        }

        // Область для рисования 
        public PictureBox DrawWindow { get { return pictureBox1; } }
    }
}
