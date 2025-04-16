namespace uchet
{
    partial class Заявление_абитуриента
    {
        private System.ComponentModel.IContainer components = null;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label labelEducationLevel;
        private Label labelApplicationDate;
        private Label labelStatus;
        private Label labelAverageScore;
        private ListBox listBoxDirections;
        private Label label6;
        private DataGridView dataGridViewScores;
        private DataGridViewTextBoxColumn Subject;
        private DataGridViewTextBoxColumn Score;
        private Label label7;
        private Button buttonPrintApplication;
        private Button buttonRefresh;
        private TextBox textBoxComments;
        private Label label8;
        private Label label9;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button buttonStatusHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Заявление_абитуриента));
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            labelEducationLevel = new Label();
            labelApplicationDate = new Label();
            labelStatus = new Label();
            labelAverageScore = new Label();
            listBoxDirections = new ListBox();
            label6 = new Label();
            dataGridViewScores = new DataGridView();
            Subject = new DataGridViewTextBoxColumn();
            Score = new DataGridViewTextBoxColumn();
            label7 = new Label();
            buttonPrintApplication = new Button();
            buttonRefresh = new Button();
            textBoxComments = new TextBox();
            label8 = new Label();
            label9 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            buttonStatusHistory = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewScores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Montserrat", 10F);
            label2.Location = new Point(24, 152);
            label2.Name = "label2";
            label2.Size = new Size(211, 24);
            label2.TabIndex = 1;
            label2.Text = "Уровень образования:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Montserrat", 10F);
            label3.Location = new Point(488, 144);
            label3.Name = "label3";
            label3.Size = new Size(125, 24);
            label3.TabIndex = 3;
            label3.Text = "Дата подачи:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Montserrat", 10F);
            label4.Location = new Point(488, 194);
            label4.Name = "label4";
            label4.Size = new Size(151, 24);
            label4.TabIndex = 5;
            label4.Text = "Текущий статус:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Montserrat", 10F);
            label5.Location = new Point(22, 192);
            label5.Name = "label5";
            label5.Size = new Size(181, 24);
            label5.TabIndex = 7;
            label5.Text = "Средний балл/ЕГЭ:";
            // 
            // labelEducationLevel
            // 
            labelEducationLevel.AutoSize = true;
            labelEducationLevel.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            labelEducationLevel.Location = new Point(288, 144);
            labelEducationLevel.Name = "labelEducationLevel";
            labelEducationLevel.Size = new Size(0, 24);
            labelEducationLevel.TabIndex = 2;
            // 
            // labelApplicationDate
            // 
            labelApplicationDate.AutoSize = true;
            labelApplicationDate.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            labelApplicationDate.Location = new Point(752, 144);
            labelApplicationDate.Name = "labelApplicationDate";
            labelApplicationDate.Size = new Size(0, 24);
            labelApplicationDate.TabIndex = 4;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            labelStatus.Location = new Point(752, 192);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(0, 24);
            labelStatus.TabIndex = 6;
            // 
            // labelAverageScore
            // 
            labelAverageScore.AutoSize = true;
            labelAverageScore.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            labelAverageScore.Location = new Point(288, 192);
            labelAverageScore.Name = "labelAverageScore";
            labelAverageScore.Size = new Size(0, 24);
            labelAverageScore.TabIndex = 8;
            // 
            // listBoxDirections
            // 
            listBoxDirections.Font = new Font("Montserrat", 10F);
            listBoxDirections.FormattingEnabled = true;
            listBoxDirections.ItemHeight = 24;
            listBoxDirections.Location = new Point(24, 472);
            listBoxDirections.Name = "listBoxDirections";
            listBoxDirections.Size = new Size(922, 148);
            listBoxDirections.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Montserrat", 10F);
            label6.Location = new Point(24, 442);
            label6.Name = "label6";
            label6.Size = new Size(242, 24);
            label6.TabIndex = 10;
            label6.Text = "Выбранные направления:";
            // 
            // dataGridViewScores
            // 
            dataGridViewScores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewScores.Columns.AddRange(new DataGridViewColumn[] { Subject, Score });
            dataGridViewScores.Location = new Point(24, 264);
            dataGridViewScores.Name = "dataGridViewScores";
            dataGridViewScores.RowHeadersWidth = 51;
            dataGridViewScores.RowTemplate.Height = 24;
            dataGridViewScores.Size = new Size(350, 158);
            dataGridViewScores.TabIndex = 11;
            // 
            // Subject
            // 
            Subject.HeaderText = "Предмет";
            Subject.MinimumWidth = 6;
            Subject.Name = "Subject";
            Subject.ReadOnly = true;
            Subject.Width = 200;
            // 
            // Score
            // 
            Score.HeaderText = "Балл";
            Score.MinimumWidth = 6;
            Score.Name = "Score";
            Score.ReadOnly = true;
            Score.Width = 125;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Montserrat", 10F);
            label7.Location = new Point(24, 234);
            label7.Name = "label7";
            label7.Size = new Size(147, 24);
            label7.TabIndex = 12;
            label7.Text = "Результаты ЕГЭ";
            // 
            // buttonPrintApplication
            // 
            buttonPrintApplication.Font = new Font("Montserrat", 10F);
            buttonPrintApplication.Location = new Point(1080, 584);
            buttonPrintApplication.Name = "buttonPrintApplication";
            buttonPrintApplication.Size = new Size(250, 40);
            buttonPrintApplication.TabIndex = 14;
            buttonPrintApplication.Text = "Распечатать заявление";
            buttonPrintApplication.UseVisualStyleBackColor = true;
            buttonPrintApplication.Click += buttonPrintApplication_Click;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Font = new Font("Montserrat", 10F);
            buttonRefresh.Location = new Point(1128, 528);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(200, 40);
            buttonRefresh.TabIndex = 15;
            buttonRefresh.Text = "Обновить данные";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // textBoxComments
            // 
            textBoxComments.Font = new Font("Montserrat", 10F);
            textBoxComments.Location = new Point(920, 144);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.ReadOnly = true;
            textBoxComments.ScrollBars = ScrollBars.Vertical;
            textBoxComments.Size = new Size(406, 192);
            textBoxComments.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Montserrat", 10F);
            label8.Location = new Point(920, 112);
            label8.Name = "label8";
            label8.Size = new Size(134, 24);
            label8.TabIndex = 17;
            label8.Text = "Комментарии";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Montserrat", 13F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(38, 48, 69);
            label9.Location = new Point(128, 56);
            label9.Name = "label9";
            label9.Size = new Size(492, 30);
            label9.TabIndex = 20;
            label9.Text = "Система подачи заявок на поступление";
            label9.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(24, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 104);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 18;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(112, 16);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(16, 104);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 19;
            pictureBox2.TabStop = false;
            pictureBox2.UseWaitCursor = true;
            // 
            // buttonStatusHistory
            // 
            buttonStatusHistory.Font = new Font("Montserrat", 10F);
            buttonStatusHistory.Location = new Point(1128, 472);
            buttonStatusHistory.Name = "buttonStatusHistory";
            buttonStatusHistory.Size = new Size(200, 40);
            buttonStatusHistory.TabIndex = 21;
            buttonStatusHistory.Text = "История статусов";
            buttonStatusHistory.UseVisualStyleBackColor = true;
            buttonStatusHistory.Click += buttonStatusHistory_Click;
            // 
            // Заявление_абитуриента
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1348, 721);
            Controls.Add(buttonStatusHistory);
            Controls.Add(label9);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(label8);
            Controls.Add(textBoxComments);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonPrintApplication);
            Controls.Add(label7);
            Controls.Add(dataGridViewScores);
            Controls.Add(label6);
            Controls.Add(listBoxDirections);
            Controls.Add(labelAverageScore);
            Controls.Add(label5);
            Controls.Add(labelStatus);
            Controls.Add(label4);
            Controls.Add(labelApplicationDate);
            Controls.Add(label3);
            Controls.Add(labelEducationLevel);
            Controls.Add(label2);
            Name = "Заявление_абитуриента";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Мое заявление на поступление";
            FormClosing += Заявление_абитуриента_FormClosing;
            Load += Заявление_абитуриента_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewScores).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}