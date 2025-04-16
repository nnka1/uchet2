using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace uchet
{
    public partial class Регистрация : Form
    {
        private string documentPath = "";
        private List<StudyDirection> availableDirections = new List<StudyDirection>();
        private List<Subject> availableSubjects = new List<Subject>();
        private ToolTip toolTip1 = new ToolTip();

        public Регистрация()
        {
            InitializeComponent();
            comboBoxSpecializations.DropDown += comboBoxSpecializations_DropDown;
            LoadEducationLevels();
            LoadInitialData();

            comboBoxEducationLevel.SelectedIndexChanged += comboBoxEducationLevel_SelectedIndexChanged;
            buttonAddSpecialization.Click += buttonAddSpecialization_Click;
            buttonRemoveSpecialization.Click += buttonRemoveSpecialization_Click;
            buttonUploadDocument.Click += ButtonUploadDocument_Click;
            buttonRegister.Click += buttonRegister_Click;
            buttonBack.Click += buttonBack_Click;
        }

        private void ButtonUploadDocument_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Выберите документ об образовании"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                documentPath = openFileDialog.FileName;
                labelDocumentName.Text = Path.GetFileName(documentPath);

                FileInfo fileInfo = new FileInfo(documentPath);
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("Размер файла не должен превышать 5MB", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    documentPath = "";
                    labelDocumentName.Text = "Файл не выбран";
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void comboBoxSpecializations_DropDown(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo == null) return;

            int maxWidth = CalculateMaxItemWidth(combo);
            combo.DropDownWidth = maxWidth;
        }

        private int CalculateMaxItemWidth(ComboBox combo)
        {
            int maxWidth = 0;
            using (Graphics g = combo.CreateGraphics())
            {
                Font font = combo.Font;
                foreach (var item in combo.Items)
                {
                    string text = item.ToString();
                    int itemWidth = (int)g.MeasureString(text, font).Width + 20;
                    maxWidth = Math.Max(maxWidth, itemWidth);
                }
            }
            return Math.Min(Math.Max(maxWidth, combo.Width),
                          Screen.GetWorkingArea(combo).Width - 20);
        }

        private void LoadInitialData()
        {
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id_subject, subject_name FROM subjects", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        availableSubjects.Clear();
                        while (reader.Read())
                        {
                            availableSubjects.Add(new Subject
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
        }

        private void LoadEducationLevels()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id_education_level, name_level FROM education_level", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        comboBoxEducationLevel.Items.Clear();
                        while (reader.Read())
                        {
                            comboBoxEducationLevel.Items.Add(new EducationLevel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            comboBoxEducationLevel.DisplayMember = "Name";
        }

        private void comboBoxEducationLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEducationLevel.SelectedItem != null)
            {
                var selectedLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;
                LoadStudyDirections(selectedLevel.Id);
                UpdateScoreControls(selectedLevel.Id);
            }
        }

        private void LoadStudyDirections(int educationLevelId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(
                    "SELECT id_direction, direction_code, direction_name FROM study_directions WHERE id_education_level = @levelId",
                    connection))
                {
                    command.Parameters.AddWithValue("@levelId", educationLevelId);

                    using (var reader = command.ExecuteReader())
                    {
                        availableDirections.Clear();
                        comboBoxSpecializations.Items.Clear();

                        while (reader.Read())
                        {
                            var direction = new StudyDirection
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2)
                            };
                            availableDirections.Add(direction);
                            comboBoxSpecializations.Items.Add(direction);
                        }
                    }
                }
            }
            comboBoxSpecializations.DisplayMember = "FullName";
        }

        private void UpdateScoreControls(int educationLevelId)
        {
            panelScores.Controls.Clear();
            panelScores.AutoScroll = true;
            panelScores.Padding = new Padding(10);

            if (educationLevelId == 1 || educationLevelId == 4) // Для СПО (1) и магистратуры (4)
            {
                var scorePanel = new Panel();
                scorePanel.BackColor = Color.White;
                scorePanel.Size = new Size(panelScores.Width - 20, 60);

                Label label = new Label();
                label.Text = educationLevelId == 1 ? "Средний балл аттестата:" : "Средний балл диплома:";
                label.Font = new Font("Montserrat", 10F);
                label.Location = new Point(10, 15);
                label.AutoSize = true;
                scorePanel.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Font = new Font("Montserrat", 10F);
                textBox.Location = new Point(250, 12);
                textBox.Width = 100;
                textBox.Tag = "average_score"; // Уникальный тег
                textBox.Name = "txtAverageScore"; // Явное имя
                scorePanel.Controls.Add(textBox);

                // Добавляем валидацию для среднего балла
                AddValidationToTextBox(textBox, true);

                panelScores.Controls.Add(scorePanel);
            }
            else // Для баллов ЕГЭ
            {
                int yPos = 0;
                foreach (var subject in availableSubjects)
                {
                    var subjectRow = new Panel();
                    subjectRow.BackColor = Color.White;
                    subjectRow.Size = new Size(380, 40);
                    subjectRow.Location = new Point(0, yPos);

                    Label subjectLabel = new Label();
                    subjectLabel.Text = subject.Name + ":";
                    subjectLabel.Font = new Font("Montserrat", 10F);
                    subjectLabel.Location = new Point(0, 10);
                    subjectLabel.AutoSize = true;
                    subjectRow.Controls.Add(subjectLabel);

                    TextBox scoreTextBox = new TextBox();
                    scoreTextBox.Font = new Font("Montserrat", 10F);
                    scoreTextBox.Location = new Point(300, 7);
                    scoreTextBox.Width = 60;
                    scoreTextBox.TextAlign = HorizontalAlignment.Center;
                    scoreTextBox.Tag = subject.Id;
                    subjectRow.Controls.Add(scoreTextBox);

                    scoreTextBox.KeyPress += (sender, e) =>
                    {
                        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    };

                    scoreTextBox.Validating += (sender, e) =>
                    {
                        var tb = sender as TextBox;
                        if (!string.IsNullOrEmpty(tb.Text))
                        {
                            int value;
                            if (!int.TryParse(tb.Text, out value) || value < 1 || value > 100)
                            {
                                tb.BackColor = Color.LightPink;
                                toolTip1.SetToolTip(tb, "Введите число от 1 до 100");
                            }
                            else
                            {
                                tb.BackColor = Color.White;
                                toolTip1.SetToolTip(tb, "");
                            }
                        }
                    };

                    panelScores.Controls.Add(subjectRow);
                    yPos += 40;
                }
            }
        }

        private void AddValidationToTextBox(TextBox textBox, bool isAverageScore = false)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (isAverageScore)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    {
                        e.Handled = true;
                    }

                    // Запрещаем второй разделитель
                    if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                        ((sender as TextBox).Text.Contains('.') || (sender as TextBox).Text.Contains(',')))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) return;

                if (isAverageScore)
                {
                    if (decimal.TryParse(textBox.Text.Replace('.', ','), out decimal value))
                    {
                        var educationLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;
                        if ((educationLevel.Id == 1 && (value < 2.0m || value > 5.0m)) ||
                            (educationLevel.Id == 4 && (value < 0 || value > 100)))
                        {
                            textBox.BackColor = Color.LightPink;
                            toolTip1.SetToolTip(textBox, educationLevel.Id == 1 ?
                                "Средний балл должен быть от 2.0 до 5.0" :
                                "Средний балл должен быть от 0 до 100");
                        }
                        else
                        {
                            textBox.BackColor = Color.White;
                            toolTip1.SetToolTip(textBox, "");
                        }
                    }
                }
                else
                {
                    if (int.TryParse(textBox.Text, out int score))
                    {
                        if (score < 1 || score > 100)
                        {
                            textBox.BackColor = Color.LightPink;
                            toolTip1.SetToolTip(textBox, "Балл ЕГЭ должен быть от 1 до 100");
                        }
                        else
                        {
                            textBox.BackColor = Color.White;
                            toolTip1.SetToolTip(textBox, "");
                        }
                    }
                }
            };
        }

        private void buttonAddSpecialization_Click(object sender, EventArgs e)
        {
            if (comboBoxSpecializations.SelectedItem != null && listBoxSelectedSpecializations.Items.Count < 5)
            {
                var selectedDirection = (StudyDirection)comboBoxSpecializations.SelectedItem;

                if (!listBoxSelectedSpecializations.Items.Cast<StudyDirection>().Any(d => d.Id == selectedDirection.Id))
                {
                    listBoxSelectedSpecializations.Items.Add(selectedDirection);
                }
                else
                {
                    MessageBox.Show("Это направление уже добавлено", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (listBoxSelectedSpecializations.Items.Count >= 5)
            {
                MessageBox.Show("Можно выбрать не более 5 направлений", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonRemoveSpecialization_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedSpecializations.SelectedItem != null)
            {
                listBoxSelectedSpecializations.Items.Remove(listBoxSelectedSpecializations.SelectedItem);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            string generatedPassword = GenerateTemporaryPassword();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int userId = CreateUser(connection, transaction, generatedPassword, 1);
                            int applicationId = CreateApplication(connection, transaction, userId);
                            AddStudyDirections(connection, transaction, applicationId);

                            // Обработка баллов в зависимости от уровня образования
                            var educationLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;
                            if (educationLevel.Id == 1 || educationLevel.Id == 4)
                            {
                                // Для СПО и магистратуры сохраняем средний балл в новую таблицу
                                AddAverageScore(connection, transaction, applicationId, educationLevel.Id);
                            }
                            else
                            {
                                // Для ЕГЭ сохраняем баллы по предметам
                                AddScores(connection, transaction, applicationId);
                            }

                            string destDocumentPath = SaveDocument(applicationId);
                            UpdateDocumentPath(connection, transaction, applicationId, destDocumentPath);

                            transaction.Commit();

                            ShowRegistrationSuccess(generatedPassword);

                            this.Hide();
                            Заявление_абитуриента applicationForm = new Заявление_абитуриента(userId);
                            applicationForm.Show();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            File.AppendAllText("registration_errors.log",
                                $"{DateTime.Now}: Ошибка при регистрации. Error: {ex}\n");
                            MessageBox.Show($"Ошибка при регистрации: {ex.Message}",
                                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int CreateUser(NpgsqlConnection connection, NpgsqlTransaction transaction, string password, int roleId)
        {
            using (var command = new NpgsqlCommand(
                "INSERT INTO users (id_roles, email, pass, fio, pasport, snils, phone, fio_parent, school) " +
                "VALUES (@roleId, @email, @pass, @fio, @pasport, @snils, @phone, @fio_parent, @school) RETURNING id_users",
                connection, transaction))
            {
                command.Parameters.AddWithValue("@roleId", roleId);
                command.Parameters.AddWithValue("@email", textBoxEmail.Text.Trim());
                command.Parameters.AddWithValue("@pass", password);
                command.Parameters.AddWithValue("@fio", textBoxFullName.Text.Trim());
                command.Parameters.AddWithValue("@pasport", textBoxPassport.Text.Trim());
                command.Parameters.AddWithValue("@snils", textBoxSNILS.Text.Trim());
                command.Parameters.AddWithValue("@phone", textBoxPhone.Text.Trim());
                command.Parameters.AddWithValue("@fio_parent", textBoxParentName.Text.Trim());
                command.Parameters.AddWithValue("@school", textBoxSchool.Text.Trim());

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private int CreateApplication(NpgsqlConnection connection, NpgsqlTransaction transaction, int userId)
        {
            var educationLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;

            using (var command = new NpgsqlCommand(
                "INSERT INTO applications (id_users, id_education_level, application_date, id_status, document_path) " +
                "VALUES (@userId, @eduLevelId, NOW(), 1, '') RETURNING id_application",
                connection, transaction))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@eduLevelId", educationLevel.Id);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void AddAverageScore(NpgsqlConnection connection, NpgsqlTransaction transaction, int applicationId, int educationLevelId)
        {
            TextBox avgScoreTextBox = (TextBox)FindControlRecursive(panelScores, "average_score");
            if (avgScoreTextBox == null)
            {
                File.AppendAllText("registration_errors.log", $"{DateTime.Now}: average_score TextBox is null\n");
                return;
            }

            if (string.IsNullOrWhiteSpace(avgScoreTextBox.Text))
            {
                File.AppendAllText("registration_errors.log", $"{DateTime.Now}: average_score TextBox is empty\n");
                return;
            }

            string scoreType = educationLevelId == 1 ? "SPO" : "MAGISTRACY";
            decimal scoreValue;
            if (!decimal.TryParse(avgScoreTextBox.Text.Replace('.', ','), out scoreValue))
            {
                File.AppendAllText("registration_errors.log", $"{DateTime.Now}: Failed to parse average score: {avgScoreTextBox.Text}\n");
                return;
            }

            try
            {
                using (var command = new NpgsqlCommand(
                    "INSERT INTO average_scores (id_application, score_type, score_value) " +
                    "VALUES (@appId, @scoreType, @scoreValue)",
                    connection, transaction))
                {
                    command.Parameters.AddWithValue("@appId", applicationId);
                    command.Parameters.AddWithValue("@scoreType", scoreType);
                    command.Parameters.AddWithValue("@scoreValue", scoreValue);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        File.AppendAllText("registration_errors.log", $"{DateTime.Now}: No rows affected when inserting average score\n");
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("registration_errors.log", $"{DateTime.Now}: Error inserting average score: {ex}\n");
            }
        }


        private void AddStudyDirections(NpgsqlConnection connection, NpgsqlTransaction transaction, int applicationId)
        {
            int priority = 1;
            foreach (StudyDirection direction in listBoxSelectedSpecializations.Items)
            {
                using (var command = new NpgsqlCommand(
                    "INSERT INTO application_study_directions (id_application, id_direction, priority) " +
                    "VALUES (@appId, @dirId, @priority)",
                    connection, transaction))
                {
                    command.Parameters.AddWithValue("@appId", applicationId);
                    command.Parameters.AddWithValue("@dirId", direction.Id);
                    command.Parameters.AddWithValue("@priority", priority++);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddScores(NpgsqlConnection connection, NpgsqlTransaction transaction, int applicationId)
        {
            int savedScoresCount = 0;
            var educationLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;

            foreach (Control control in panelScores.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control panelControl in panel.Controls)
                    {
                        if (panelControl is TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            if (int.TryParse(textBox.Text, out int score) && score >= 1 && score <= 100)
                            {
                                int subjectId = Convert.ToInt32(textBox.Tag);

                                using (var command = new NpgsqlCommand(
                                    "INSERT INTO application_subjects (id_application, id_subject, score) " +
                                    "VALUES (@appId, @subjId, @score)",
                                    connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@appId", applicationId);
                                    command.Parameters.AddWithValue("@subjId", subjectId);
                                    command.Parameters.AddWithValue("@score", score);

                                    command.ExecuteNonQuery();
                                    savedScoresCount++;
                                }
                            }
                        }
                    }
                }
            }

            if (savedScoresCount < 2)
            {
                throw new Exception("Необходимо указать баллы минимум по 2 предметам ЕГЭ");
            }
        }

        private string SaveDocument(int applicationId)
        {
            if (string.IsNullOrEmpty(documentPath))
                return "";

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AdmissionSystem");
            Directory.CreateDirectory(appDataPath);

            string destFileName = $"doc_{applicationId}{Path.GetExtension(documentPath)}";
            string destPath = Path.Combine(appDataPath, destFileName);

            File.Copy(documentPath, destPath, true);

            return destPath;
        }

        private void UpdateDocumentPath(NpgsqlConnection connection, NpgsqlTransaction transaction, int applicationId, string path)
        {
            using (var command = new NpgsqlCommand(
                "UPDATE applications SET document_path = @path WHERE id_application = @appId",
                connection, transaction))
            {
                command.Parameters.AddWithValue("@path", path);
                command.Parameters.AddWithValue("@appId", applicationId);

                command.ExecuteNonQuery();
            }
        }

        private void ShowRegistrationSuccess(string password)
        {
            // Создаем форму для сообщения об успехе
            Form successForm = new Form();
            successForm.Text = "Успешная регистрация";
            successForm.Size = new Size(450, 280);
            successForm.StartPosition = FormStartPosition.CenterScreen;
            successForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            successForm.MaximizeBox = false;
            successForm.MinimizeBox = false;

            // Создаем элементы управления
            Label successLabel = new Label()
            {
                Text = "Регистрация прошла успешно!",
                Font = new Font("Montserrat", 10F),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Поле для email с возможностью копирования
            Label emailTextLabel = new Label()
            {
                Text = "Email:",
                Font = new Font("Montserrat", 10F),
                AutoSize = true,
                Location = new Point(20, 67)
            };

            TextBox emailTextBox = new TextBox()
            {
                Text = textBoxEmail.Text.Trim(),
                Font = new Font("Montserrat", 10F),
                Location = new Point(110, 67),
                Size = new Size(300, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            // Поле для пароля с возможностью копирования
            Label passwordTextLabel = new Label()
            {
                Text = "Пароль:",
                Font = new Font("Montserrat", 10F),
                AutoSize = true,
                Location = new Point(20, 100)
            };

            TextBox passwordTextBox = new TextBox()
            {
                Text = password,
                Font = new Font("Montserrat", 10F),
                Location = new Point(110, 97),
                Size = new Size(300, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            Label warningLabel = new Label()
            {
                Text = "Сохраните эти данные в надежном месте!",
                Font = new Font("Montserrat", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 130),
                ForeColor = Color.DarkRed
            };

            Button okButton = new Button()
            {
                Text = "OK",
                Font = new Font("Montserrat", 10F),
                Size = new Size(80, 30),
                Location = new Point(180, 170),
                DialogResult = DialogResult.OK
            };

            // Общий метод для копирования текста
            void CopyToClipboardWithFeedback(TextBox textBox, string textToCopy)
            {
                try
                {
                    Clipboard.Clear(); // Очищаем буфер перед копированием
                    Clipboard.SetText(textToCopy);

                    Color originalColor = textBox.BackColor;
                    string originalText = textBox.Text;

                    textBox.BackColor = Color.LightGreen;
                    textBox.Text = "Скопировано!";

                    // Таймер для возврата исходного состояния
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Interval = 2000;
                    timer.Tick += (s, ev) =>
                    {
                        textBox.Text = originalText;
                        textBox.BackColor = originalColor;
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                }
                catch (Exception ex)
                {
                    textBox.BackColor = Color.LightPink;
                    textBox.Text = "Ошибка копирования!";

                    // Таймер для возврата исходного состояния при ошибке
                    System.Windows.Forms.Timer errorTimer = new System.Windows.Forms.Timer();
                    errorTimer.Interval = 2000;
                    errorTimer.Tick += (s, ev) =>
                    {
                        textBox.Text = textToCopy;
                        textBox.BackColor = Color.White;
                        errorTimer.Stop();
                        errorTimer.Dispose();
                    };
                    errorTimer.Start();
                }
            }

            // Обработчики кликов
            emailTextBox.Click += (sender, e) => CopyToClipboardWithFeedback(emailTextBox, emailTextBox.Text);
            passwordTextBox.Click += (sender, e) => CopyToClipboardWithFeedback(passwordTextBox, passwordTextBox.Text);

            // Добавляем элементы на форму
            successForm.Controls.Add(successLabel);
            successForm.Controls.Add(emailTextLabel);
            successForm.Controls.Add(emailTextBox);
            successForm.Controls.Add(passwordTextLabel);
            successForm.Controls.Add(passwordTextBox);
            successForm.Controls.Add(warningLabel);
            successForm.Controls.Add(okButton);

            // Показываем форму
            successForm.ShowDialog();
        }

        private string GenerateTemporaryPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        private bool ValidateForm()
        {
            if (comboBoxEducationLevel.SelectedItem == null)
            {
                MessageBox.Show("Выберите уровень образования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxFullName.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPassport.Text))
            {
                MessageBox.Show("Введите паспортные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxSNILS.Text))
            {
                MessageBox.Show("Введите СНИЛС", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                MessageBox.Show("Введите email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxParentName.Text))
            {
                MessageBox.Show("Введите ФИО родителя/представителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxSchool.Text))
            {
                MessageBox.Show("Введите учебное заведение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var educationLevel = (EducationLevel)comboBoxEducationLevel.SelectedItem;

            if (educationLevel.Id == 1 || educationLevel.Id == 4) // СПО или магистратура
            {
                // Измененный поиск TextBox-а
                TextBox avgScoreTextBox = (TextBox)FindControlRecursive(panelScores, "average_score");


                if (avgScoreTextBox == null)
                {
                    MessageBox.Show("Системная ошибка: не найдено поле для ввода среднего балла",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(avgScoreTextBox.Text))
                {
                    MessageBox.Show("Введите средний балл", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Парсинг с учетом культуры
                if (!decimal.TryParse(avgScoreTextBox.Text.Replace('.', ','), out decimal avgScore))
                {
                    MessageBox.Show("Средний балл должен быть числом", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Проверка диапазона
                if (educationLevel.Id == 1 && (avgScore < 2.0m || avgScore > 5.0m))
                {
                    MessageBox.Show("Средний балл аттестата должен быть от 2.0 до 5.0",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (educationLevel.Id == 4 && (avgScore < 0 || avgScore > 100))
                {
                    MessageBox.Show("Средний балл диплома должен быть от 0 до 100",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            
        }
            else
            {
                int filledSubjectsCount = 0;
                List<string> invalidSubjects = new List<string>();

                foreach (Control control in panelScores.Controls)
                {
                    if (control is Panel panel)
                    {
                        foreach (Control panelControl in panel.Controls)
                        {
                            if (panelControl is TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
                            {
                                if (!int.TryParse(textBox.Text, out int score) || score < 1 || score > 100)
                                {
                                    var subjectLabel = panel.Controls.OfType<Label>()
                                        .FirstOrDefault(l => l.Text.EndsWith(":"));
                                    if (subjectLabel != null)
                                    {
                                        invalidSubjects.Add(subjectLabel.Text.Replace(":", "").Trim());
                                    }
                                }
                                else
                                {
                                    filledSubjectsCount++;
                                }
                            }
                        }
                    }
                }

                if (invalidSubjects.Count > 0)
                {
                    MessageBox.Show($"Некорректные баллы ЕГЭ для предметов: {string.Join(", ", invalidSubjects)}\n" +
                                  "Баллы должны быть целыми числами от 1 до 100",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (filledSubjectsCount < 2)
                {
                    MessageBox.Show("Необходимо указать баллы минимум по 2 предметам ЕГЭ",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (listBoxSelectedSpecializations.Items.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одно направление подготовки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(documentPath))
            {
                MessageBox.Show("Загрузите документ об образовании", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private Control FindControlRecursive(Control parent, string tag)
        {
            if (parent == null) return null;

            if (parent.Tag != null && parent.Tag.ToString() == tag)
                return parent;

            foreach (Control child in parent.Controls)
            {
                var found = FindControlRecursive(child, tag);
                if (found != null)
                    return found;
            }

            return null;
        }

        private void buttonUploadDocument_Click_1(object sender, EventArgs e)
        {
            // Обработчик загрузки документа
        }
    }

    public class EducationLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class StudyDirection
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string FullName => $"{Code} {Name}";

        public override string ToString()
        {
            return FullName;
        }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}