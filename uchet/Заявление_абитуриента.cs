
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Npgsql;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using iTextFont = iTextSharp.text.Font;
using System.Reflection.Emit;

namespace uchet
{
    public partial class Заявление_абитуриента : Form
    {
        private int userId;
        private int applicationId;
        private List<ApplicationStatusHistory> statusHistory = new List<ApplicationStatusHistory>();

        public Заявление_абитуриента(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.Size = new Size(1400, 720);
            SetupControls();
            LoadApplicationData();
        }

        private void SetupControls()
        {
            this.Text = "Мое заявление на поступление";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Font mainFont = new Font("Montserrat", 10);
            Font boldFont = new Font("Montserrat", 10, FontStyle.Bold);
            Font titleFont = new Font("Montserrat", 14, FontStyle.Bold);


            label2.Font = mainFont;
            label3.Font = mainFont;
            label4.Font = mainFont;
            label5.Font = mainFont;
            label6.Font = mainFont;
            label7.Font = mainFont;
            label8.Font = mainFont;

            labelEducationLevel.Font = boldFont;
            labelApplicationDate.Font = boldFont;
            labelStatus.Font = boldFont;
            labelAverageScore.Font = boldFont;

            listBoxDirections.Font = mainFont;
            textBoxComments.Font = mainFont;

            buttonPrintApplication.BackColor = Color.FromArgb(38, 48, 69);
            buttonPrintApplication.ForeColor = Color.White;
            buttonPrintApplication.FlatStyle = FlatStyle.Flat;
            buttonPrintApplication.Font = mainFont;

            buttonRefresh.BackColor = Color.FromArgb(185, 159, 110);
            buttonRefresh.ForeColor = Color.White;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.Font = mainFont;

            dataGridViewScores.Font = mainFont;
            dataGridViewScores.DefaultCellStyle.Font = mainFont;
            dataGridViewScores.ColumnHeadersDefaultCellStyle.Font = boldFont;
            dataGridViewScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewScores.ReadOnly = true;
        }

        private bool ValidateScores(string educationLevel, DataGridView scoresGrid, string averageScore)
        {
            if (educationLevel.ToLower().Contains("спо"))
            {
                if (decimal.TryParse(averageScore, out decimal score))
                {
                    if (score > 5)
                    {
                        MessageBox.Show("Средний балл аттестата не может превышать 5", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (educationLevel.ToLower().Contains("магистратура"))
            {
                if (decimal.TryParse(averageScore, out decimal score))
                {
                    if (score > 100)
                    {
                        MessageBox.Show("Средний балл диплома не может превышать 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else // Бакалавриат/Специалитет
            {
                foreach (DataGridViewRow row in scoresGrid.Rows)
                {
                    if (!row.IsNewRow && row.Cells[1].Value != null)
                    {
                        if (int.TryParse(row.Cells[1].Value.ToString(), out int score))
                        {
                            if (score > 100)
                            {
                                MessageBox.Show("Баллы ЕГЭ не могут превышать 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private string CalculateAverageScore()
        {
            string educationLevel = labelEducationLevel.Text.ToLower();

            if (educationLevel.Contains("спо") || educationLevel.Contains("магистратура"))
            {
                if (!string.IsNullOrEmpty(labelAverageScore.Text) && labelAverageScore.Text != "Нет данных")
                {
                    return labelAverageScore.Text;
                }
                return "Нет данных";
            }
            else // Бакалавриат/Специалитет
            {
                if (dataGridViewScores.Rows.Count == 0)
                    return "Нет данных о баллах ЕГЭ";

                int sum = 0;
                int count = 0;

                foreach (DataGridViewRow row in dataGridViewScores.Rows)
                {
                    if (!row.IsNewRow && row.Cells[1].Value != null)
                    {
                        if (int.TryParse(row.Cells[1].Value.ToString(), out int score))
                        {
                            sum += score;
                            count++;
                        }
                    }
                }

                return count > 0 ? (sum / count).ToString() : "Нет данных о баллах ЕГЭ";
            }
        }

        private void LoadApplicationData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Загрузка основного заявления
                    using (var command = new NpgsqlCommand(
                        "SELECT a.id_application, el.name_level, a.application_date, sa.status_name, " +
                        "a.document_path, a.comment " +  // Removed average_score here
                        "FROM applications a " +
                        "JOIN education_level el ON a.id_education_level = el.id_education_level " +
                        "JOIN status_applications sa ON a.id_status = sa.id_status " +
                        "WHERE a.id_users = @userId", connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                applicationId = reader.GetInt32(0);
                                labelEducationLevel.Text = reader.GetString(1);
                                labelApplicationDate.Text = reader.GetDateTime(2).ToString("dd.MM.yyyy");
                                labelStatus.Text = reader.GetString(3);

                                //Average score will be loaded from the average_scores table now
                                if (!reader.IsDBNull(5))
                                    textBoxComments.Text = reader.GetString(5);
                            }
                        }
                    }

                    // Load average score from average_scores table
                    string educationLevelLower = labelEducationLevel.Text.ToLower();
                    if (educationLevelLower.Contains("спо") || educationLevelLower.Contains("магистратура"))
                    {
                        using (var command = new NpgsqlCommand(
                            "SELECT score_value FROM average_scores " +
                            "WHERE id_application = @appId AND score_type = @scoreType", connection))
                        {
                            command.Parameters.AddWithValue("@appId", applicationId);
                            command.Parameters.AddWithValue("@scoreType", educationLevelLower.Contains("спо") ? "SPO" : "MAGISTRACY");

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    labelAverageScore.Text = reader.GetDecimal(0).ToString();
                                }
                                else
                                {
                                    labelAverageScore.Text = "Нет данных";
                                }
                            }
                        }
                    }
                    else
                    {
                        labelAverageScore.Text = "Не требуется";  //For Bachelor or Specialist's degree
                    }



                    // Загрузка направлений подготовки
                    using (var command = new NpgsqlCommand(
                        "SELECT sd.direction_code, sd.direction_name, asd.priority " +
                        "FROM application_study_directions asd " +
                        "JOIN study_directions sd ON asd.id_direction = sd.id_direction " +
                        "WHERE asd.id_application = @appId " +
                        "ORDER BY asd.priority", connection))
                    {
                        command.Parameters.AddWithValue("@appId", applicationId);

                        using (var reader = command.ExecuteReader())
                        {
                            listBoxDirections.Items.Clear();
                            while (reader.Read())
                            {
                                string directionInfo = $"{reader.GetString(0)} {reader.GetString(1)} (приоритет: {reader.GetInt32(2)})";
                                listBoxDirections.Items.Add(directionInfo);
                            }
                        }
                    }

                    // Загрузка баллов ЕГЭ (только для бакалавриата/специалитета)
                    if (educationLevelLower.Contains("бакалавр") || educationLevelLower.Contains("специалитет"))
                    {
                        using (var command = new NpgsqlCommand(
                            "SELECT s.subject_name, asub.score " +
                            "FROM application_subjects asub " +
                            "JOIN subjects s ON asub.id_subject = s.id_subject " +
                            "WHERE asub.id_application = @appId", connection))
                        {
                            command.Parameters.AddWithValue("@appId", applicationId);

                            using (var reader = command.ExecuteReader())
                            {
                                dataGridViewScores.Rows.Clear();
                                bool hasData = false;

                                while (reader.Read())
                                {
                                    dataGridViewScores.Rows.Add(reader.GetString(0), reader.GetInt32(1));
                                    hasData = true;
                                }

                                if (hasData)
                                {
                                    labelAverageScore.Text = CalculateAverageScore();
                                }
                                else
                                {
                                    dataGridViewScores.Rows.Add("Данные не найдены в базе", "");
                                }
                            }
                        }
                    }
                    else
                    {
                        // Для СПО и магистратуры скрываем таблицу с баллами ЕГЭ
                        dataGridViewScores.Visible = false;
                        label7.Visible = false; // Скрываем заголовок "Результаты ЕГЭ"
                    }

                    // Загрузка истории статусов
                    using (var command = new NpgsqlCommand(
                        "SELECT ash.change_date, sa.status_name, ash.comment, u.fio " +
                        "FROM application_status_history ash " +
                        "JOIN status_applications sa ON ash.id_status = sa.id_status " +
                        "LEFT JOIN users u ON ash.changed_by_user = u.id_users " +
                        "WHERE ash.id_application = @appId " +
                        "ORDER BY ash.change_date DESC", connection))
                    {
                        command.Parameters.AddWithValue("@appId", applicationId);

                        using (var reader = command.ExecuteReader())
                        {
                            statusHistory.Clear();
                            while (reader.Read())
                            {
                                statusHistory.Add(new ApplicationStatusHistory
                                {
                                    ChangeDate = reader.GetDateTime(0),
                                    Status = reader.GetString(1),
                                    Comment = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    ChangedBy = reader.IsDBNull(3) ? "Система" : reader.GetString(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                File.AppendAllText("application_load_errors.log",
                    $"{DateTime.Now}: Ошибка загрузки заявления. UserId: {userId}. Error: {ex}\n");
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadApplicationData();
            MessageBox.Show($"Данные обновлены. Средний балл: {labelAverageScore.Text}",
                          "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonPrintApplication_Click(object sender, EventArgs e)
        {
            if (listBoxDirections.Items.Count == 0)
            {
                MessageBox.Show("Нет данных о направлениях подготовки", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = $"Заявление_на_поступление_{DateTime.Now:ddMMyyyy}.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    if (File.Exists(saveFileDialog.FileName))
                    {
                        File.Delete(saveFileDialog.FileName);
                    }

                    Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));

                    document.Open();

                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 12);
                    iTextSharp.text.Font boldFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);

                    Paragraph title = new Paragraph("ЗАЯВЛЕНИЕ", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 25;
                    document.Add(title);

                    string fio = GetUserFullName();
                    if (string.IsNullOrEmpty(fio)) fio = "[ФИО не указано]";

                    Paragraph mainText = new Paragraph($"Я, {fio}, подал(а) документы в ВГТУ", normalFont);
                    mainText.SpacingAfter = 20;
                    document.Add(mainText);

                    Paragraph educationLevel = new Paragraph($"Уровень образования: {labelEducationLevel.Text}", normalFont);
                    educationLevel.SpacingAfter = 10;
                    document.Add(educationLevel);

                    if (labelEducationLevel.Text.Contains("Бакалавриат") || labelEducationLevel.Text.Contains("Специалитет"))
                    {
                        Paragraph averageScore = new Paragraph($"Средний балл ЕГЭ: {labelAverageScore.Text}", normalFont);
                        averageScore.SpacingAfter = 10;
                        document.Add(averageScore);
                    }
                    else if (labelEducationLevel.Text.Contains("СПО"))
                    {
                        Paragraph averageScore = new Paragraph($"Средний балл аттестата: {labelAverageScore.Text}", normalFont);
                        averageScore.SpacingAfter = 10;
                        document.Add(averageScore);
                    }
                    else if (labelEducationLevel.Text.Contains("Магистратура"))
                    {
                        Paragraph averageScore = new Paragraph($"Средний балл диплома: {labelAverageScore.Text}", normalFont);
                        averageScore.SpacingAfter = 10;
                        document.Add(averageScore);
                    }

                    Paragraph directionsHeader = new Paragraph("На направления подготовки:", boldFont);
                    directionsHeader.SpacingAfter = 15;
                    document.Add(directionsHeader);

                    foreach (var item in listBoxDirections.Items)
                    {
                        string direction = item.ToString();
                        direction = direction.Replace(" (приоритет: ", " - приоритет ").Replace(")", "");
                        Paragraph directionItem = new Paragraph($"- {direction}", normalFont);
                        directionItem.IndentationLeft = 20;
                        directionItem.SpacingAfter = 5;
                        document.Add(directionItem);
                    }

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    Paragraph dateParagraph = new Paragraph($"Дата: {DateTime.Now.ToString("dd.MM.yyyy")}", normalFont);
                    dateParagraph.Alignment = Element.ALIGN_RIGHT;
                    document.Add(dateParagraph);

                    document.Close();

                    MessageBox.Show("Заявление успешно сохранено в формате PDF", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetUserFullName()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "SELECT fio FROM users WHERE id_users = @userId", connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    return command.ExecuteScalar()?.ToString() ?? "";
                }
            }
        }

        private void Заявление_абитуриента_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void buttonStatusHistory_Click(object sender, EventArgs e)
        {
            StatusHistoryForm historyForm = new StatusHistoryForm(statusHistory);
            historyForm.ShowDialog();
        }

        private void Заявление_абитуриента_Load(object sender, EventArgs e)
        {

        }
    }

    public class ApplicationStatusHistory
    {
        public DateTime ChangeDate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string ChangedBy { get; set; }
    }

    public class StatusHistoryForm : Form
    {
        public StatusHistoryForm(List<ApplicationStatusHistory> history)
        {
            InitializeComponents(history);
        }

        private void InitializeComponents(List<ApplicationStatusHistory> history)
        {
            this.Text = "История изменения статусов";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            DataGridView grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Дата изменения",
                DataPropertyName = "ChangeDate",
                Width = 150
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Статус",
                DataPropertyName = "Status",
                Width = 120
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Комментарий",
                DataPropertyName = "Comment",
                Width = 200
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Изменено",
                DataPropertyName = "ChangedBy",
                Width = 120
            });

            var bindingSource = new BindingSource();
            bindingSource.DataSource = history;
            grid.DataSource = bindingSource;

            this.Controls.Add(grid);
        }
    }
}
