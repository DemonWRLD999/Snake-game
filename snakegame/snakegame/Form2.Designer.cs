namespace snakegame
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox3 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = Color.Lime;
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(200, 20);
            textBox1.TabIndex = 0;
            textBox1.TabStop = false;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(40, 40, 40);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(12, 54);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(200, 20);
            textBox2.TabIndex = 1;
            textBox2.TabStop = false;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(40, 40, 40);
            button1.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Courier New", 10F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(255, 140, 0);
            button1.Location = new Point(12, 142);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(40, 40, 40);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.ForeColor = Color.Gold;
            textBox3.Location = new Point(480, 12);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(308, 20);
            textBox3.TabIndex = 3;
            textBox3.TabStop = false;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(800, 451);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            DoubleBuffered = true;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Snake";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
    }
}