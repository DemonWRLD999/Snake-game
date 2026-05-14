namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
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
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}