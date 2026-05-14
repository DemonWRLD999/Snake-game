namespace maingamesnake
{
    public partial class Form1 : Form

    {
        bool moveUp, moveDown, moveleft, moveright;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) moveUp = true;
            if (e.KeyCode == Keys.Down) moveDown = true;
            if (e.KeyCode == Keys.Left) moveleft = true;
            if (e.KeyCode == Keys.Right) moveright = true;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) moveUp = false;
            if (e.KeyCode == Keys.Down) moveDown = false;
            if (e.KeyCode == Keys.Left) moveleft = false;
            if (e.KeyCode == Keys.Right) moveright = false;
        }
    }
}