using System;
using System.Windows.Forms;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.App.Views
{
    public partial class MainForm : Form, IAppViewExtended
    {
        public MainForm()
        {
            InitializeComponent();

            // Init elements
            pictureBox = pictureBox1;


            radioButton1.CheckedChanged += InvokeTreeTypeChange;
            radioButton2.CheckedChanged += InvokeTreeTypeChange;

            // Событие изименения режима
            radioButton3.CheckedChanged += InvokeModeChanged;
            radioButton4.CheckedChanged += InvokeModeChanged;
            radioButton5.CheckedChanged += InvokeModeChanged;

            radioButton6.CheckedChanged += InvokeModeChanged;
            radioButton7.CheckedChanged += RadioButton7Changed;
            radioButton7.CheckedChanged += InvokeModeChanged;
            radioButton8.CheckedChanged += InvokeModeChanged;

            checkBox1.CheckedChanged += InvokeModeChanged;
            checkBox2.CheckedChanged += CheckBox2Changed;

            // Изменение соглашения
            radioButton9.CheckedChanged += InvokeAgreementChanged;
            radioButton10.CheckedChanged += InvokeAgreementChanged;

            // Изенение режима вывода
            radioButton11.CheckedChanged += OutputModeCanged;
            radioButton12.CheckedChanged += OutputModeCanged;
        }



        //private EWindowMode windowMode;

        private PictureBox pictureBox;


        
        /*                       Extended PART                    */

        public EDrawNodeMode DrawNodeMode
        {
            get
            {
                if (radioButton3.Checked)
                    return EDrawNodeMode.CircleMode;
                else if (radioButton4.Checked)
                    return EDrawNodeMode.RectangleMode;
                else
                    return EDrawNodeMode.NotNodeMode;
            }
        }


        public EDrawSymbolMode DrawSymbolMode
        {
            get
            {
                if (radioButton6.Checked) return EDrawSymbolMode.DrawSymbolsUnderNodes;
                else if (radioButton7.Checked) return EDrawSymbolMode.DrawSymbolsInNodes;
                else return EDrawSymbolMode.NotDrawSymbols;
            }
        }

        public bool DrawProbability { get { return checkBox1.Checked; } }

        public bool DrawBinaryCodes { get { return checkBox2.Checked; } }

        public event EventHandler ModeChanged;



        /*                       BASE PART                       */
        public string InputFile { get { return textBox1.Text; } }

        public string OutputFile { get { return textBox2.Text; } }

        public string ValueInfo { set { textBox6.Text = value; } }

        public PictureBox DrawWindow { get { return pictureBox; } }

        public ETreeType TreeType
        {
            get {
                return (radioButton1.Checked) ? ETreeType.ShannonTree : ETreeType.HaffmanTree;
            }
        }


        public EOutputMode OutputMode
        {
            get
            {
                if (radioButton11.Checked) return EOutputMode.TXTMode;
                else return EOutputMode.CSVMode;
            }
        }

        //public EWindowMode WindowMode { get { return windowMode; } }

        public bool Agreement { get { return radioButton9.Checked; } }

        public event EventHandler ResultClick;

        public event EventHandler TreeTypeChange;

        public event EventHandler AgreementChanged;

        public event EventHandler OutputModeChanged;



        /*                   Encode /Decode                       */
        public string OutputDecodeFile { get { return textBox5.Text; } }

        public string OriginalString { get { return textBox3.Text; } set { textBox3.Text = value; } }

        public string BinaryString { get { return textBox4.Text; } set { textBox4.Text = value; } }

        public event EventHandler EncodeClick;

        public event EventHandler DecodeClick;



        /*                   Проброс событий                       */
        private void button1_Click(object sender, EventArgs e)
        {
            ResultClick?.Invoke(sender, e);
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EncodeClick?.Invoke(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DecodeClick?.Invoke(sender, e);
        }

        private void RadioButton7Changed(object sender, EventArgs e)
        {
            // -= ModeChanged и += ModeChanged, для того, чтобы событие не вызывалась.
            // Так как radioButton7 итак вызовет ModeChanged, событие от checkBox2
            // фиксировать не нужно.

            if (((RadioButton)sender).Checked)
            {
                checkBox2.CheckedChanged -= ModeChanged;
                checkBox2.Checked = false;
                checkBox2.CheckedChanged += ModeChanged;
            }
        }

        private void CheckBox2Changed(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
                checkBox2.Checked = false;
            else
                InvokeModeChanged(sender, e);
        }

        private void InvokeModeChanged(object sender, EventArgs e)
        {
            ModeChanged?.Invoke(sender, e);
        }

        private void InvokeTreeTypeChange(object sender, EventArgs e)
        {
            TreeTypeChange?.Invoke(sender, e);
        }

        private void InvokeAgreementChanged(object sender, EventArgs e)
        {
            AgreementChanged?.Invoke(sender, e);
        }

        private void OutputModeCanged(object sender, EventArgs e)
        {
            OutputModeChanged?.Invoke(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FullScreenForm fullScreenForm = new FullScreenForm();
            fullScreenForm.FormClosed += FullScreenFormClosed;

            fullScreenForm.DrawWindow.Width = 2000;
            fullScreenForm.DrawWindow.Height = 1000;
            pictureBox = fullScreenForm.DrawWindow;
            fullScreenForm.Show();

            // For Redraw window
            ResultClick?.Invoke(sender, e);
        }

        private void FullScreenFormClosed(object sender, EventArgs e)
        {
            pictureBox = pictureBox1;
        }
    }
}
