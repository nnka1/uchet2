using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Text;

namespace uchet
{
    public partial class ApplicationDetailsForm : Form
    {
        private int applicationId;
        private TextBox textBoxComment;

        public string Comment { get; private set; }

        public ApplicationDetailsForm(int applicationId, string fio, string? status, string? comment)
        {
            this.applicationId = applicationId;
            this.Comment = comment ?? string.Empty;
            InitializeComponents(applicationId, fio, status ?? "Не указан", this.Comment);
            LoadApplicationDetails(applicationId);
        }

        private void InitializeComponents(int applicationId, string fio, string status, string comment)
        {
            this.Text = $"Детали заявления #{applicationId}";
            this.Size = new Size(800, 750);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Font mainFont = new Font("Montserrat", 10);
            Font boldFont = new Font("Montserrat", 10, FontStyle.Bold);

            // Основные элементы
            Label labelFio = new Label();
            labelFio.Text = $"Абитуриент: {fio}";
            labelFio.Font = boldFont;
            labelFio.Location = new Point(20, 20);
            labelFio.AutoSize = true;
            this.Controls.Add(labelFio);

            Label labelStatus = new Label();
            labelStatus.Text = $"Статус: {status}";
            labelStatus.Font = boldFont;
            labelStatus.Location = new Point(20, 50);
            labelStatus.AutoSize = true;
            this.Controls.Add(labelStatus);

            // Комментарий (редактируемый)
            Label labelComment = new Label();
            labelComment.Text = "Комментарий:";
            labelComment.Font = mainFont;
            labelComment.Location = new Point(20, 80);
            labelComment.AutoSize = true;
            this.Controls.Add(labelComment);

            textBoxComment = new TextBox();
            textBoxComment.Text = comment;
            textBoxComment.Font = mainFont;
            textBoxComment.Multiline = true;
            textBoxComment.ReadOnly = false;
            textBoxComment.ScrollBars = ScrollBars.Vertical;
            textBoxComment.Location = new Point(20, 110);
            textBoxComment.Size = new Size(740, 60);
            this.Controls.Add(textBoxComment);

            // Направления подготовки
            Label labelDirections = new Label();
            labelDirections.Text = "Направления подготовки:";
            labelDirections.Font = boldFont;
            labelDirections.Location = new Point(20, 180);
            labelDirections.AutoSize = true;
            this.Controls.Add(labelDirections);

            ListBox listBoxDirections = new ListBox();
            listBoxDirections.Font = mainFont;
            listBoxDirections.Location = new Point(20, 210);
            listBoxDirections.Size = new Size(740, 120);
            this.Controls.Add(listBoxDirections);

            // Результаты ЕГЭ
            Label labelScores = new Label();
            labelScores.Text = "Результаты ЕГЭ:";
            labelScores.Font = boldFont;
            labelScores.Location = new Point(20, 340);
            labelScores.AutoSize = true;
            this.Controls.Add(labelScores);

            DataGridView dataGridViewScores = new DataGridView();
            dataGridViewScores.Font = mainFont;
            dataGridViewScores.DefaultCellStyle.Font = mainFont;
            dataGridViewScores.ColumnHeadersDefaultCellStyle.Font = boldFont;
            dataGridViewScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewScores.ReadOnly = true;
            dataGridViewScores.Location = new Point(20, 370);
            dataGridViewScores.Size = new Size(740, 120);
            this.Controls.Add(dataGridViewScores);

            // Комментарии исполнителя
            Label labelExecutorComments = new Label();
            labelExecutorComments.Text = "Комментарии исполнителя:";
            labelExecutorComments.Font = boldFont;
            labelExecutorComments.Location = new Point(20, 500);
            labelExecutorComments.AutoSize = true;
            this.Controls.Add(labelExecutorComments);

            TextBox textBoxExecutorComments = new TextBox();
            textBoxExecutorComments.Font = mainFont;
            textBoxExecutorComments.Multiline = true;
            textBoxExecutorComments.ScrollBars = ScrollBars.Vertical;
            textBoxExecutorComments.Location = new Point(20, 530);
            textBoxExecutorComments.Size = new Size(740, 60);
            textBoxExecutorComments.ReadOnly = true;
            this.Controls.Add(textBoxExecutorComments);

            // Кнопка Сохранить
            Button buttonSave = new Button();
            buttonSave.Text = "Сохранить";
            buttonSave.Font = mainFont;
            buttonSave.BackColor = Color.FromArgb(38, 48, 69);
            buttonSave.ForeColor = Color.White;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Location = new Point(610, 600);
            buttonSave.Size = new Size(150, 35);
            buttonSave.Click += ButtonSave_Click;
            this.Controls.Add(buttonSave);

            // Сохраняем ссылки на элементы для дальнейшего использования
            this.Tag = new Tuple<ListBox, DataGridView, TextBox>(
                listBoxDirections, dataGridViewScores, textBoxExecutorComments);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.Comment = textBoxComment.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadApplicationDetails(int applicationId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Получаем сохраненные элементы управления
                    var controls = (Tuple<ListBox, DataGridView, TextBox>)this.Tag;
                    ListBox listBoxDirections = controls.Item1;
                    DataGridView dataGridViewScores = controls.Item2;
                    TextBox textBoxExecutorComments = controls.Item3;

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
                            while (reader.Read())
                            {
                                string directionInfo = $"{reader.GetString(0)} {reader.GetString(1)} (приоритет: {reader.GetInt32(2)})";
                                listBoxDirections.Items.Add(directionInfo);
                            }
                        }
                    }

                    // Загрузка баллов ЕГЭ
                    using (var command = new NpgsqlCommand(
                        "SELECT s.subject_name, asub.score " +
                        "FROM application_subjects asub " +
                        "JOIN subjects s ON asub.id_subject = s.id_subject " +
                        "WHERE asub.id_application = @appId", connection))
                    {
                        command.Parameters.AddWithValue("@appId", applicationId);

                        DataTable scoresTable = new DataTable();
                        scoresTable.Columns.Add("Предмет");
                        scoresTable.Columns.Add("Балл");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                scoresTable.Rows.Add(reader.GetString(0), reader.GetInt32(1));
                            }
                        }

                        dataGridViewScores.DataSource = scoresTable;
                    }

                    // Загрузка комментариев исполнителя
                    using (var command = new NpgsqlCommand(
                        "SELECT u.fio, h.comment, h.change_date " +
                        "FROM application_status_history h " +
                        "JOIN users u ON h.changed_by_user = u.id_users " +
                        "JOIN executors e ON u.id_users = e.id_user " +
                        "WHERE h.id_application = @appId AND h.comment IS NOT NULL " +
                        "ORDER BY h.change_date DESC", connection))
                    {
                        command.Parameters.AddWithValue("@appId", applicationId);

                        StringBuilder comments = new StringBuilder();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comments.AppendLine($"{reader.GetString(0)} ({reader.GetDateTime(2)}):");
                                comments.AppendLine(reader.GetString(1));
                                comments.AppendLine();
                            }
                        }

                        textBoxExecutorComments.Text = comments.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей заявления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}