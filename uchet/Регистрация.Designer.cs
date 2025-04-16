namespace uchet
{
    partial class Регистрация
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Регистрация));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            buttonBack = new Button();
            buttonRegister = new Button();
            comboBoxEducationLevel = new ComboBox();
            label2 = new Label();
            textBoxFullName = new TextBox();
            label3 = new Label();
            textBoxPassport = new TextBox();
            label4 = new Label();
            textBoxSNILS = new TextBox();
            label5 = new Label();
            textBoxEmail = new TextBox();
            label6 = new Label();
            textBoxPhone = new TextBox();
            label7 = new Label();
            textBoxParentName = new TextBox();
            label8 = new Label();
            textBoxSchool = new TextBox();
            label9 = new Label();
            panelScores = new Panel();
            label10 = new Label();
            comboBoxSpecializations = new ComboBox();
            label11 = new Label();
            listBoxSelectedSpecializations = new ListBox();
            buttonAddSpecialization = new Button();
            buttonRemoveSpecialization = new Button();
            label12 = new Label();
            buttonUploadDocument = new Button();
            labelDocumentName = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Montserrat", 13F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(38, 48, 69);
            label1.Location = new Point(136, 64);
            label1.Name = "label1";
            label1.Size = new Size(492, 30);
            label1.TabIndex = 6;
            label1.Text = "Система подачи заявок на поступление";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(32, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 104);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(120, 24);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(16, 104);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.FromArgb(185, 159, 110);
            buttonBack.Cursor = Cursors.Hand;
            buttonBack.FlatAppearance.BorderSize = 0;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Font = new Font("Montserrat SemiBold", 12F, FontStyle.Bold);
            buttonBack.ForeColor = Color.White;
            buttonBack.Location = new Point(32, 648);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(250, 50);
            buttonBack.TabIndex = 14;
            buttonBack.Text = "Вернуться";
            buttonBack.UseVisualStyleBackColor = false;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.FromArgb(38, 48, 69);
            buttonRegister.Cursor = Cursors.Hand;
            buttonRegister.FlatAppearance.BorderSize = 0;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Montserrat SemiBold", 12F, FontStyle.Bold);
            buttonRegister.ForeColor = Color.White;
            buttonRegister.Location = new Point(1040, 648);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(280, 50);
            buttonRegister.TabIndex = 13;
            buttonRegister.Text = "Подтвердить регистрацию";
            buttonRegister.UseVisualStyleBackColor = false;
            // 
            // comboBoxEducationLevel
            // 
            comboBoxEducationLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEducationLevel.Font = new Font("Montserrat", 10F);
            comboBoxEducationLevel.FormattingEnabled = true;
            comboBoxEducationLevel.Location = new Point(32, 184);
            comboBoxEducationLevel.Name = "comboBoxEducationLevel";
            comboBoxEducationLevel.Size = new Size(400, 32);
            comboBoxEducationLevel.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Montserrat", 10F);
            label2.Location = new Point(32, 152);
            label2.Name = "label2";
            label2.Size = new Size(211, 24);
            label2.TabIndex = 8;
            label2.Text = "Уровень образования:";
            // 
            // textBoxFullName
            // 
            textBoxFullName.Font = new Font("Montserrat", 10F);
            textBoxFullName.Location = new Point(32, 264);
            textBoxFullName.Name = "textBoxFullName";
            textBoxFullName.PlaceholderText = "Иванов Иван Иванович";
            textBoxFullName.Size = new Size(400, 28);
            textBoxFullName.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Montserrat", 10F);
            label3.Location = new Point(32, 232);
            label3.Name = "label3";
            label3.Size = new Size(58, 24);
            label3.TabIndex = 10;
            label3.Text = "ФИО:";
            // 
            // textBoxPassport
            // 
            textBoxPassport.Font = new Font("Montserrat", 10F);
            textBoxPassport.Location = new Point(32, 344);
            textBoxPassport.Name = "textBoxPassport";
            textBoxPassport.PlaceholderText = "Серия и номер паспорта";
            textBoxPassport.Size = new Size(400, 28);
            textBoxPassport.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Montserrat", 10F);
            label4.Location = new Point(32, 312);
            label4.Name = "label4";
            label4.Size = new Size(196, 24);
            label4.TabIndex = 12;
            label4.Text = "Паспортные данные:";
            // 
            // textBoxSNILS
            // 
            textBoxSNILS.Font = new Font("Montserrat", 10F);
            textBoxSNILS.Location = new Point(480, 344);
            textBoxSNILS.Name = "textBoxSNILS";
            textBoxSNILS.PlaceholderText = "XXX-XXX-XXX XX";
            textBoxSNILS.Size = new Size(400, 28);
            textBoxSNILS.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Montserrat", 10F);
            label5.Location = new Point(480, 312);
            label5.Name = "label5";
            label5.Size = new Size(79, 24);
            label5.TabIndex = 14;
            label5.Text = "СНИЛС:";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Montserrat", 10F);
            textBoxEmail.Location = new Point(480, 184);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.PlaceholderText = "example@mail.ru";
            textBoxEmail.Size = new Size(400, 28);
            textBoxEmail.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Montserrat", 10F);
            label6.Location = new Point(480, 152);
            label6.Name = "label6";
            label6.Size = new Size(63, 24);
            label6.TabIndex = 16;
            label6.Text = "Email:";
            // 
            // textBoxPhone
            // 
            textBoxPhone.Font = new Font("Montserrat", 10F);
            textBoxPhone.Location = new Point(480, 264);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.PlaceholderText = "+7 (XXX) XXX-XX-XX";
            textBoxPhone.Size = new Size(400, 28);
            textBoxPhone.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Montserrat", 10F);
            label7.Location = new Point(480, 232);
            label7.Name = "label7";
            label7.Size = new Size(91, 24);
            label7.TabIndex = 18;
            label7.Text = "Телефон:";
            // 
            // textBoxParentName
            // 
            textBoxParentName.Font = new Font("Montserrat", 10F);
            textBoxParentName.Location = new Point(920, 184);
            textBoxParentName.Name = "textBoxParentName";
            textBoxParentName.PlaceholderText = "Для несовершеннолетних";
            textBoxParentName.Size = new Size(400, 28);
            textBoxParentName.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Montserrat", 10F);
            label8.Location = new Point(920, 152);
            label8.Name = "label8";
            label8.Size = new Size(288, 24);
            label8.TabIndex = 20;
            label8.Text = "ФИО родителя/представителя:";
            // 
            // textBoxSchool
            // 
            textBoxSchool.Font = new Font("Montserrat", 10F);
            textBoxSchool.Location = new Point(920, 264);
            textBoxSchool.Name = "textBoxSchool";
            textBoxSchool.PlaceholderText = "Название учебного заведения";
            textBoxSchool.Size = new Size(400, 28);
            textBoxSchool.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Montserrat", 10F);
            label9.Location = new Point(920, 232);
            label9.Name = "label9";
            label9.Size = new Size(263, 24);
            label9.TabIndex = 22;
            label9.Text = "Учебное заведение (оконч.):";
            // 
            // panelScores
            // 
            panelScores.BackColor = Color.FromArgb(240, 240, 240);
            panelScores.BorderStyle = BorderStyle.FixedSingle;
            panelScores.Location = new Point(920, 344);
            panelScores.Name = "panelScores";
            panelScores.Size = new Size(400, 200);
            panelScores.TabIndex = 23;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(38, 48, 69);
            label10.Location = new Point(920, 312);
            label10.Name = "label10";
            label10.Size = new Size(157, 24);
            label10.TabIndex = 24;
            label10.Text = "Результаты ЕГЭ:";
            // 
            // comboBoxSpecializations
            // 
            comboBoxSpecializations.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSpecializations.Font = new Font("Montserrat", 10F);
            comboBoxSpecializations.FormattingEnabled = true;
            comboBoxSpecializations.Location = new Point(32, 424);
            comboBoxSpecializations.Name = "comboBoxSpecializations";
            comboBoxSpecializations.Size = new Size(720, 32);
            comboBoxSpecializations.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Montserrat", 10F);
            label11.Location = new Point(32, 392);
            label11.Name = "label11";
            label11.Size = new Size(244, 24);
            label11.TabIndex = 26;
            label11.Text = "Направление подготовки:";
            // 
            // listBoxSelectedSpecializations
            // 
            listBoxSelectedSpecializations.Font = new Font("Montserrat", 10F);
            listBoxSelectedSpecializations.FormattingEnabled = true;
            listBoxSelectedSpecializations.ItemHeight = 24;
            listBoxSelectedSpecializations.Location = new Point(32, 464);
            listBoxSelectedSpecializations.Name = "listBoxSelectedSpecializations";
            listBoxSelectedSpecializations.Size = new Size(720, 76);
            listBoxSelectedSpecializations.TabIndex = 27;
            // 
            // buttonAddSpecialization
            // 
            buttonAddSpecialization.BackColor = Color.FromArgb(38, 48, 69);
            buttonAddSpecialization.Cursor = Cursors.Hand;
            buttonAddSpecialization.FlatAppearance.BorderSize = 0;
            buttonAddSpecialization.FlatStyle = FlatStyle.Flat;
            buttonAddSpecialization.Font = new Font("Montserrat", 10F);
            buttonAddSpecialization.ForeColor = Color.White;
            buttonAddSpecialization.Location = new Point(768, 424);
            buttonAddSpecialization.Name = "buttonAddSpecialization";
            buttonAddSpecialization.Size = new Size(112, 32);
            buttonAddSpecialization.TabIndex = 11;
            buttonAddSpecialization.Text = "Добавить";
            buttonAddSpecialization.UseVisualStyleBackColor = false;
            // 
            // buttonRemoveSpecialization
            // 
            buttonRemoveSpecialization.BackColor = Color.FromArgb(185, 159, 110);
            buttonRemoveSpecialization.Cursor = Cursors.Hand;
            buttonRemoveSpecialization.FlatAppearance.BorderSize = 0;
            buttonRemoveSpecialization.FlatStyle = FlatStyle.Flat;
            buttonRemoveSpecialization.Font = new Font("Montserrat", 10F);
            buttonRemoveSpecialization.ForeColor = Color.White;
            buttonRemoveSpecialization.Location = new Point(768, 488);
            buttonRemoveSpecialization.Name = "buttonRemoveSpecialization";
            buttonRemoveSpecialization.Size = new Size(112, 32);
            buttonRemoveSpecialization.TabIndex = 12;
            buttonRemoveSpecialization.Text = "Удалить";
            buttonRemoveSpecialization.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Montserrat", 10F);
            label12.Location = new Point(32, 552);
            label12.Name = "label12";
            label12.Size = new Size(249, 24);
            label12.TabIndex = 30;
            label12.Text = "Документ об образовании:";
            // 
            // buttonUploadDocument
            // 
            buttonUploadDocument.BackColor = Color.FromArgb(38, 48, 69);
            buttonUploadDocument.Cursor = Cursors.Hand;
            buttonUploadDocument.FlatAppearance.BorderSize = 0;
            buttonUploadDocument.FlatStyle = FlatStyle.Flat;
            buttonUploadDocument.Font = new Font("Montserrat", 10F);
            buttonUploadDocument.ForeColor = Color.White;
            buttonUploadDocument.Location = new Point(32, 584);
            buttonUploadDocument.Name = "buttonUploadDocument";
            buttonUploadDocument.Size = new Size(200, 40);
            buttonUploadDocument.TabIndex = 9;
            buttonUploadDocument.Text = "Выбрать файл";
            buttonUploadDocument.UseVisualStyleBackColor = false;
            buttonUploadDocument.Click += buttonUploadDocument_Click_1;
            // 
            // labelDocumentName
            // 
            labelDocumentName.AutoSize = true;
            labelDocumentName.Font = new Font("Montserrat", 9F);
            labelDocumentName.ForeColor = Color.Gray;
            labelDocumentName.Location = new Point(248, 592);
            labelDocumentName.Name = "labelDocumentName";
            labelDocumentName.Size = new Size(139, 21);
            labelDocumentName.TabIndex = 32;
            labelDocumentName.Text = "Файл не выбран";
            // 
            // Регистрация
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1348, 721);
            Controls.Add(labelDocumentName);
            Controls.Add(buttonUploadDocument);
            Controls.Add(label12);
            Controls.Add(buttonRemoveSpecialization);
            Controls.Add(buttonAddSpecialization);
            Controls.Add(listBoxSelectedSpecializations);
            Controls.Add(label11);
            Controls.Add(comboBoxSpecializations);
            Controls.Add(label10);
            Controls.Add(panelScores);
            Controls.Add(label9);
            Controls.Add(textBoxSchool);
            Controls.Add(label8);
            Controls.Add(textBoxParentName);
            Controls.Add(label7);
            Controls.Add(textBoxPhone);
            Controls.Add(label6);
            Controls.Add(textBoxEmail);
            Controls.Add(label5);
            Controls.Add(textBoxSNILS);
            Controls.Add(label4);
            Controls.Add(textBoxPassport);
            Controls.Add(label3);
            Controls.Add(textBoxFullName);
            Controls.Add(label2);
            Controls.Add(comboBoxEducationLevel);
            Controls.Add(buttonRegister);
            Controls.Add(buttonBack);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Name = "Регистрация";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Форма регистрации абитуриента";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button buttonBack;
        private Button buttonRegister;
        private ComboBox comboBoxEducationLevel;
        private Label label2;
        private TextBox textBoxFullName;
        private Label label3;
        private TextBox textBoxPassport;
        private Label label4;
        private TextBox textBoxSNILS;
        private Label label5;
        private TextBox textBoxEmail;
        private Label label6;
        private TextBox textBoxPhone;
        private Label label7;
        private TextBox textBoxParentName;
        private Label label8;
        private TextBox textBoxSchool;
        private Label label9;
        private Panel panelScores;
        private Label label10;
        private ComboBox comboBoxSpecializations;
        private Label label11;
        private ListBox listBoxSelectedSpecializations;
        private Button buttonAddSpecialization;
        private Button buttonRemoveSpecialization;
        private Label label12;
        private Button buttonUploadDocument;
        private Label labelDocumentName;
    }
}