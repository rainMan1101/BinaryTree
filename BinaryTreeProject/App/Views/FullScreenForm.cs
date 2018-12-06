using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryTreeProject.App.Views
{
    public partial class FullScreenForm : Form
    {
        public FullScreenForm()
        {
            InitializeComponent();
        }

        public PictureBox DrawWindow { get { return pictureBox1; } }
    }
}
