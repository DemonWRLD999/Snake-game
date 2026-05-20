using System;
using System.Drawing;
using System.Windows.Forms;

namespace snakegame
{
    public partial class Form2 : Form
    {
        bool fs = true;
        private int[,] Plansza;
        private const int BoardWidth = 24;
        private const int BoardHeight = 21;

        private const int Nic = 0;
        private const int SNAKE = 1;
        private const int Japko = 2;
        private const int FieldSize = 30;

        public Form2()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += Form2_KeyDown;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new EventHandler(Form2_Load);
            this.Paint += new PaintEventHandler(Form2_Paint);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeLogicalBoard();
            SetFullscreen(true);
        }

        private void InitializeLogicalBoard()
        {
            this.BackColor = Color.FromArgb(40, 40, 40);

            Plansza = new int[BoardWidth, BoardHeight];

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Plansza[x, y] = Nic;
                }
            }

            Plansza[12, 10] = SNAKE;
            Plansza[5, 5] = Japko;

            this.Refresh();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (Plansza == null) return;

            Graphics g = e.Graphics;

            int totalBoardWidth = BoardWidth * FieldSize;
            int totalBoardHeight = BoardHeight * FieldSize;

            int offsetX = (this.ClientSize.Width - totalBoardWidth) / 2;
            int offsetY = (this.ClientSize.Height - totalBoardHeight) / 2;

            g.FillRectangle(Brushes.Black, offsetX, offsetY, totalBoardWidth, totalBoardHeight);

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    int drawX = offsetX + (x * FieldSize);
                    int drawY = offsetY + (y * FieldSize);

                    if (Plansza[x, y] == SNAKE)
                    {
                        g.FillRectangle(Brushes.Green, drawX, drawY, FieldSize, FieldSize);
                    }
                    else if (Plansza[x, y] == Japko)
                    {
                        g.FillRectangle(Brushes.Red, drawX, drawY, FieldSize, FieldSize);
                    }
                }
            }
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

            this.Refresh();
        }
    }
}
