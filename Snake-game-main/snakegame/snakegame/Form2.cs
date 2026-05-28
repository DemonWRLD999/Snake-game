using System;
using System.Collections.Generic;
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

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer czasTimer;

        private int czasSekundy = 0;
        private int zjedzoneJablka = 0;

        private Point kierunek = new Point(1, 0);
        private List<Point> wążCiało = new List<Point>();

        public Form2()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += Form2_KeyDown;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new EventHandler(Form2_Load);
            this.Paint += new PaintEventHandler(Form2_Paint);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;

            czasTimer = new System.Windows.Forms.Timer();
            czasTimer.Interval = 1000;
            czasTimer.Tick += CzasTimer_Tick;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeLogicalBoard();
            SetFullscreen(true);

            gameTimer.Start();
            czasTimer.Start();
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

            czasSekundy = 0;
            zjedzoneJablka = 0;

            Point startPozycja = new Point(12, 10);

            wążCiało.Clear();
            wążCiało.Add(startPozycja);

            Plansza[startPozycja.X, startPozycja.Y] = SNAKE;
            Plansza[5, 5] = Japko;

            this.Refresh();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            PoruszWęża();
        }

        private void CzasTimer_Tick(object sender, EventArgs e)
        {
            czasSekundy++;
            this.Refresh();
        }

        private void PoruszWęża()
        {
            Point staraGłowa = wążCiało[0];
            Point nowaGłowa = new Point(staraGłowa.X + kierunek.X, staraGłowa.Y + kierunek.Y);

            if (nowaGłowa.X < 0 || nowaGłowa.X >= BoardWidth || nowaGłowa.Y < 0 || nowaGłowa.Y >= BoardHeight)
            {
                GacieKoniecGry();
                return;
            }

            if (Plansza[nowaGłowa.X, nowaGłowa.Y] == SNAKE)
            {
                GacieKoniecGry();
                return;
            }

            if (Plansza[nowaGłowa.X, nowaGłowa.Y] == Japko)
            {
                wążCiało.Insert(0, nowaGłowa);
                Plansza[nowaGłowa.X, nowaGłowa.Y] = SNAKE;

                zjedzoneJablka++;

                GenerujNoweJapko();
            }
            else
            {
                wążCiało.Insert(0, nowaGłowa);
                Plansza[nowaGłowa.X, nowaGłowa.Y] = SNAKE;

                Point ogon = wążCiało[wążCiało.Count - 1];
                Plansza[ogon.X, ogon.Y] = Nic;
                wążCiało.RemoveAt(wążCiało.Count - 1);
            }

            this.Refresh();
        }

        private void GenerujNoweJapko()
        {
            Random rand = new Random();

            int x, y;

            do
            {
                x = rand.Next(0, BoardWidth);
                y = rand.Next(0, BoardHeight);

            } while (Plansza[x, y] != Nic);

            Plansza[x, y] = Japko;
        }

        private void GacieKoniecGry()
        {
            gameTimer.Stop();
            czasTimer.Stop();

            MessageBox.Show(
                "Przegrałeś!\n\n" +
                "Zjedzone japka: " + zjedzoneJablka +
                "\nCzas gry: " + czasSekundy + " s",
                "Koniec Gry"
            );

            InitializeLogicalBoard();

            kierunek = new Point(1, 0);

            gameTimer.Start();
            czasTimer.Start();
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

            g.DrawString(
                "Japki: " + zjedzoneJablka,
                new Font("Arial", 16),
                Brushes.White,
                20,
                20
            );

            g.DrawString(
                "Czas: " + czasSekundy + " s",
                new Font("Arial", 16),
                Brushes.White,
                20,
                50
            );
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                SetFullscreen(!fs);

            if (e.KeyCode == Keys.Right && kierunek.X != -1)
            {
                kierunek = new Point(1, 0);
            }
            else if (e.KeyCode == Keys.Left && kierunek.X != 1)
            {
                kierunek = new Point(-1, 0);
            }
            else if (e.KeyCode == Keys.Up && kierunek.Y != 1)
            {
                kierunek = new Point(0, -1);
            }
            else if (e.KeyCode == Keys.Down && kierunek.Y != -1)
            {
                kierunek = new Point(0, 1);
            }

            if (e.KeyCode == Keys.D && kierunek.X != -1)
            {
                kierunek = new Point(1, 0);
            }
            else if (e.KeyCode == Keys.A && kierunek.X != 1)
            {
                kierunek = new Point(-1, 0);
            }
            else if (e.KeyCode == Keys.W && kierunek.Y != 1)
            {
                kierunek = new Point(0, -1);
            }
            else if (e.KeyCode == Keys.S && kierunek.Y != -1)
            {
                kierunek = new Point(0, 1);
            }
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