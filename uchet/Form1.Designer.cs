namespace uchet
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            textBoxEmail = new TextBox();
            textBoxPass = new TextBox();
            labelEmail = new Label();
            labelLogin = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(38, 48, 69);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Montserrat SemiBold", 13F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(528, 480);
            button1.Name = "button1";
            button1.Size = new Size(380, 50);
            button1.TabIndex = 4;
            button1.Text = "Регистрация абитуриента";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(185, 159, 110);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Montserrat SemiBold", 13F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(592, 400);
            button2.Name = "button2";
            button2.Size = new Size(250, 50);
            button2.TabIndex = 5;
            button2.Text = "Войти в систему";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Montserrat", 12F);
            textBoxEmail.Location = new Point(512, 248);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(440, 32);
            textBoxEmail.TabIndex = 1;
            // 
            // textBoxPass
            // 
            textBoxPass.Font = new Font("Montserrat", 12F);
            textBoxPass.Location = new Point(512, 328);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.Size = new Size(440, 32);
            textBoxPass.TabIndex = 2;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Font = new Font("Montserrat", 12F);
            labelEmail.Location = new Point(512, 216);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(72, 27);
            labelEmail.TabIndex = 6;
            labelEmail.Text = "Email:";
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Montserrat", 12F);
            labelLogin.Location = new Point(512, 296);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(96, 27);
            labelLogin.TabIndex = 7;
            labelLogin.Text = "Пароль:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Montserrat", 13F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(38, 48, 69);
            label1.Location = new Point(136, 64);
            label1.Name = "label1";
            label1.Size = new Size(492, 30);
            label1.TabIndex = 10;
            label1.Text = "Система подачи заявок на поступление";
            label1.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(32, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 104);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(120, 24);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(16, 104);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            pictureBox2.UseWaitCursor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1348, 721);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(labelLogin);
            Controls.Add(labelEmail);
            Controls.Add(textBoxPass);
            Controls.Add(textBoxEmail);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private TextBox textBoxEmail;
        private TextBox textBoxPass;
        private Label labelEmail;
        private Label labelLogin;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}