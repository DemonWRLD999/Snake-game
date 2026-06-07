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
        private int minSpeed = 60;

        private int baseSpeed => Form1.Trudnosc;

        private System.Windows.Forms.Timer gameTimer;
        private Point kierunek = new Point(1, 0);
        private List<Point> wążCiało = new List<Point>();

        private int iloscJablek = 0;
        private int czasGry = 0;
        private bool pauza = false;
        private int rekord = 0;

        public Form2()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += Form2_KeyDown;
            this.Paint += new PaintEventHandler(Form2_Paint);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = Form1.Trudnosc;
            gameTimer.Tick += GameTimer_Tick;
            pauza = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeLogicalBoard();
            SetFullscreen(true);
            gameTimer.Start();
        }

        private void InitializeLogicalBoard()
        {
            Plansza = new int[BoardWidth, BoardHeight];

            for (int x = 0; x < BoardWidth; x++)
                for (int y = 0; y < BoardHeight; y++)
                    Plansza[x, y] = Nic;

            Point startPozycja = new Point(12, 10);
            wążCiało.Clear();
            wążCiało.Add(startPozycja);

            Plansza[startPozycja.X, startPozycja.Y] = SNAKE;
            Plansza[5, 5] = Japko;

            iloscJablek = 0;
            czasGry = 0;

            textBox1.Text = $"Zjadłeś {iloscJablek} japuszek";
            textBox3.Text = $"Tfuj rekord to {rekord} japuszek";

            this.Refresh();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (pauza) return;

            czasGry += gameTimer.Interval;
            PoruszWęża();
            textBox2.Text = (czasGry / 1000.0).ToString("0.0") + " s";
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
                iloscJablek++;
                textBox1.Text = $"Zjadłeś {iloscJablek} japuszek";

                if (iloscJablek > rekord)
                {
                    rekord = iloscJablek;
                    textBox3.Text = $"Tfuj rekord to {rekord} japuszek";
                }

                GenerujNoweJapko();

                if (gameTimer.Interval > minSpeed)
                    gameTimer.Interval -= 5;
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
            MessageBox.Show("Przegrałeś!", "Koniec Gry");
            InitializeLogicalBoard();
            kierunek = new Point(1, 0);
            pauza = true;
            button1.Text = "Start";
            gameTimer.Interval = baseSpeed;
            gameTimer.Start();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (Plansza == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            int totalBoardWidth = BoardWidth * FieldSize;
            int totalBoardHeight = BoardHeight * FieldSize;

            int offsetX = (this.ClientSize.Width - totalBoardWidth) / 2;
            int offsetY = (this.ClientSize.Height - totalBoardHeight) / 2;

            using (var outerBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height),
                Color.FromArgb(15, 30, 15),
                Color.FromArgb(5, 10, 5),
                45f))
            {
                g.FillRectangle(outerBrush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }

            using (Pen bgGridPen = new Pen(Color.FromArgb(20, 0, 80, 0)))
            {
                for (int x = 0; x < this.ClientSize.Width; x += FieldSize)
                    g.DrawLine(bgGridPen, x, 0, x, this.ClientSize.Height);
                for (int y = 0; y < this.ClientSize.Height; y += FieldSize)
                    g.DrawLine(bgGridPen, 0, y, this.ClientSize.Width, y);
            }

            for (int i = 12; i > 0; i--)
            {
                using (var glowPen = new Pen(Color.FromArgb(i * 3, 50, 200, 50), 1))
                    g.DrawRectangle(glowPen,
                        offsetX - i * 2,
                        offsetY - i * 2,
                        totalBoardWidth + i * 4,
                        totalBoardHeight + i * 4);
            }

            using (var bgBrush = new SolidBrush(Color.FromArgb(139, 134, 78)))
                g.FillRectangle(bgBrush, offsetX, offsetY, totalBoardWidth, totalBoardHeight);

            using (Pen gridPen = new Pen(Color.FromArgb(100, 100, 60, 0)))
            {
                for (int x = 0; x <= BoardWidth; x++)
                    g.DrawLine(gridPen,
                        offsetX + x * FieldSize, offsetY,
                        offsetX + x * FieldSize, offsetY + totalBoardHeight);
                for (int y = 0; y <= BoardHeight; y++)
                    g.DrawLine(gridPen,
                        offsetX, offsetY + y * FieldSize,
                        offsetX + totalBoardWidth, offsetY + y * FieldSize);
            }

            string title = "SNAKE";
            using (Font titleFont = new Font("Courier New", 36, FontStyle.Bold))
            {
                Color[] hotColors = {
                    Color.FromArgb(255, 80, 0),
                    Color.FromArgb(255, 140, 0),
                    Color.FromArgb(255, 200, 0),
                    Color.FromArgb(255, 80, 0),
                    Color.FromArgb(255, 40, 0)
                };

                SizeF charSize = g.MeasureString("W", titleFont);
                float totalWidth = charSize.Width * title.Length;
                float startX = offsetX + (totalBoardWidth - totalWidth) / 2;
                float titleY = offsetY - 70;

                for (int i = 0; i < title.Length; i++)
                {
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
                        g.DrawString(title[i].ToString(), titleFont, shadowBrush, startX + i * charSize.Width + 3, titleY + 3);

                    using (var charBrush = new SolidBrush(hotColors[i % hotColors.Length]))
                        g.DrawString(title[i].ToString(), titleFont, charBrush, startX + i * charSize.Width, titleY);
                }
            }

            Point głowa = wążCiało.Count > 0 ? wążCiało[0] : new Point(-1, -1);

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    int drawX = offsetX + x * FieldSize;
                    int drawY = offsetY + y * FieldSize;

                    if (Plansza[x, y] == SNAKE)
                    {
                        bool isHead = (x == głowa.X && y == głowa.Y);

                        if (isHead)
                        {
                            using (var headBrush = new SolidBrush(Color.FromArgb(80, 220, 80)))
                                g.FillRectangle(headBrush, drawX + 1, drawY + 1, FieldSize - 2, FieldSize - 2);

                            using (var headBorder = new Pen(Color.FromArgb(30, 130, 30), 2))
                                g.DrawRectangle(headBorder, drawX + 1, drawY + 1, FieldSize - 3, FieldSize - 3);

                            int eyeSize = 4;
                            int eyeOffsetX1, eyeOffsetX2, eyeOffsetY1, eyeOffsetY2;

                            if (kierunek.X == 1)
                            {
                                eyeOffsetX1 = drawX + FieldSize - 9;
                                eyeOffsetX2 = drawX + FieldSize - 9;
                                eyeOffsetY1 = drawY + 5;
                                eyeOffsetY2 = drawY + FieldSize - 9;
                            }
                            else if (kierunek.X == -1)
                            {
                                eyeOffsetX1 = drawX + 5;
                                eyeOffsetX2 = drawX + 5;
                                eyeOffsetY1 = drawY + 5;
                                eyeOffsetY2 = drawY + FieldSize - 9;
                            }
                            else if (kierunek.Y == -1)
                            {
                                eyeOffsetX1 = drawX + 5;
                                eyeOffsetX2 = drawX + FieldSize - 9;
                                eyeOffsetY1 = drawY + 5;
                                eyeOffsetY2 = drawY + 5;
                            }
                            else
                            {
                                eyeOffsetX1 = drawX + 5;
                                eyeOffsetX2 = drawX + FieldSize - 9;
                                eyeOffsetY1 = drawY + FieldSize - 9;
                                eyeOffsetY2 = drawY + FieldSize - 9;
                            }

                            g.FillRectangle(Brushes.White, eyeOffsetX1, eyeOffsetY1, eyeSize, eyeSize);
                            g.FillRectangle(Brushes.White, eyeOffsetX2, eyeOffsetY2, eyeSize, eyeSize);
                            g.FillRectangle(Brushes.Black, eyeOffsetX1 + 1, eyeOffsetY1 + 1, 2, 2);
                            g.FillRectangle(Brushes.Black, eyeOffsetX2 + 1, eyeOffsetY2 + 1, 2, 2);
                        }
                        else
                        {
                            using (var snakeBrush = new SolidBrush(Color.FromArgb(50, 205, 50)))
                                g.FillRectangle(snakeBrush, drawX + 1, drawY + 1, FieldSize - 2, FieldSize - 2);

                            using (var borderPen = new Pen(Color.FromArgb(30, 130, 30)))
                                g.DrawRectangle(borderPen, drawX + 1, drawY + 1, FieldSize - 3, FieldSize - 3);

                            using (var highlightBrush = new SolidBrush(Color.FromArgb(120, 144, 238, 144)))
                                g.FillRectangle(highlightBrush, drawX + 2, drawY + 2, FieldSize / 3, FieldSize / 3);
                        }
                    }
                    else if (Plansza[x, y] == Japko)
                    {
                        int p = FieldSize / 6;

                        using (var darkBrush = new SolidBrush(Color.FromArgb(120, 0, 0)))
                            g.FillRectangle(darkBrush, drawX + p + 1, drawY + p + 3, FieldSize - p * 2, FieldSize - p * 2 - 2);

                        using (var redBrush = new SolidBrush(Color.FromArgb(210, 30, 30)))
                            g.FillRectangle(redBrush, drawX + p, drawY + p + 2, FieldSize - p * 2, FieldSize - p * 2 - 2);

                        using (var lightBrush = new SolidBrush(Color.FromArgb(240, 80, 80)))
                            g.FillRectangle(lightBrush, drawX + p, drawY + p + 2, FieldSize - p * 2, (FieldSize - p * 2) / 2);

                        g.FillRectangle(Brushes.White, drawX + p + 2, drawY + p + 4, 4, 4);
                        g.FillRectangle(Brushes.White, drawX + p + 2, drawY + p + 4, 2, 2);

                        using (var stemBrush = new SolidBrush(Color.FromArgb(100, 60, 20)))
                            g.FillRectangle(stemBrush, drawX + FieldSize / 2 - 1, drawY + p - 2, 3, 5);

                        using (var leafBrush = new SolidBrush(Color.FromArgb(40, 160, 40)))
                            g.FillRectangle(leafBrush, drawX + FieldSize / 2 + 1, drawY + p - 2, 5, 3);

                        using (var leafDark = new SolidBrush(Color.FromArgb(20, 100, 20)))
                            g.FillRectangle(leafDark, drawX + FieldSize / 2 + 2, drawY + p - 1, 3, 2);
                    }
                }
            }

            int borderThick = 6;
            using (var outerPen = new Pen(Color.FromArgb(80, 70, 50), borderThick))
                g.DrawRectangle(outerPen,
                    offsetX - borderThick / 2,
                    offsetY - borderThick / 2,
                    totalBoardWidth + borderThick,
                    totalBoardHeight + borderThick);

            using (var innerPen = new Pen(Color.FromArgb(180, 160, 100), 2))
                g.DrawRectangle(innerPen,
                    offsetX - 2, offsetY - 2,
                    totalBoardWidth + 2, totalBoardHeight + 2);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                SetFullscreen(!fs);

            if (e.KeyCode == Keys.Space)
            {
                pauza = !pauza;
                button1.Text = pauza ? "Wznów" : "Pauza";
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Right && kierunek.X != -1)
                kierunek = new Point(1, 0);
            else if (e.KeyCode == Keys.Left && kierunek.X != 1)
                kierunek = new Point(-1, 0);
            else if (e.KeyCode == Keys.Up && kierunek.Y != 1)
                kierunek = new Point(0, -1);
            else if (e.KeyCode == Keys.Down && kierunek.Y != -1)
                kierunek = new Point(0, 1);

            if (e.KeyCode == Keys.D && kierunek.X != -1)
                kierunek = new Point(1, 0);
            else if (e.KeyCode == Keys.A && kierunek.X != 1)
                kierunek = new Point(-1, 0);
            else if (e.KeyCode == Keys.W && kierunek.Y != 1)
                kierunek = new Point(0, -1);
            else if (e.KeyCode == Keys.S && kierunek.Y != -1)
                kierunek = new Point(0, 1);
        }

        void SetFullscreen(bool on)
        {
            fs = on;
            FormBorderStyle = on ? FormBorderStyle.None : FormBorderStyle.Sizable;
            WindowState = on ? FormWindowState.Maximized : FormWindowState.Normal;
            TopMost = on;
            this.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            pauza = !pauza;
            button1.Text = pauza ? "Wznów" : "Pauza";
        }
    }
}