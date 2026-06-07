using System;
using System.Drawing;
using System.Windows.Forms;

namespace snakegame
{
    public partial class Form1 : Form
    {
        public static int Trudnosc = 150;
        private bool trudnoscWybrana = false;

        public Form1()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Button btn in new[] { button1, button2, button3, button4 })
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.Transparent;
                btn.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (trudnoscWybrana)
            {
                Form2 okno = new Form2();
                okno.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Trudnosc = 60;
            trudnoscWybrana = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Trudnosc = 120;
            trudnoscWybrana = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Trudnosc = 200;
            trudnoscWybrana = true;
        }
    }
}