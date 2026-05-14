using System;
using System.Windows.Forms;

namespace snakegame
{
    public partial class Form2 : Form
    {
        bool fs = true;

        public Form2()
        {
            InitializeComponent();

            Load += Form2_Load;
            KeyPreview = true;
            KeyDown += Form2_KeyDown;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SetFullscreen(true);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                SetFullscreen(!fs);
        }

        void SetFullscreen(bool on)
        {
            fs = on;

            FormBorderStyle = on ? FormBorderStyle.None : FormBorderStyle.Sizable;
            WindowState = on ? FormWindowState.Maximized : FormWindowState.Normal;
            TopMost = on;
        }
    }
}