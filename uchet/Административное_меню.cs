using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace uchet
{
    public partial class Административное_меню : Form
    {
        private int currentUserId;
        private DataTable applicationsTable;
        private DataTable directionsTable;
        private DataTable educationLevelsTable;
        private DataTable executorsTable;

        public Административное_меню(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            this.Size = new Size(1366, 768);
            SetupControls();
            LoadData();
        }

        private void SetupControls()
        {
            // Настройка шрифтов и цветов
            this.BackColor = Color.White;
            this.Font = new Font("Montserrat", 10F);

            // Настройка вкладок
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.BackColor = Color.White;

            // Настройка DataGridView
            foreach (DataGridView dgv in new[] { dataGridViewApplications, dataGridViewDirections,
                                              dataGridViewEducationLevels, dataGridViewExecutors,
                                              dataGridViewStatistics })
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.DefaultCellStyle.Font = new Font("Montserrat", 9F);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(38, 48, 69);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.EnableHeadersVisualStyles = false;
                dgv.RowHeadersVisible = false;
            }

            // Настройка кнопок навигации
            foreach (Button btn in new[] { buttonShowApplications, buttonShowDirections,
                                        buttonShowEducationLevels, buttonShowExecutors,
                                        buttonShowStatistics, buttonLogout })
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(38, 48, 69);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Montserrat", 10F, FontStyle.Bold);
                btn.Height = 40;
                btn.Padding = new Padding(5);
            }

            buttonLogout.BackColor = Color.FromArgb(185, 159, 110);

            // Настройка кнопок действий
            foreach (Button btn in new[] { buttonFilter, buttonApprove, buttonReject,
                                        buttonViewDetails, buttonAssignExecutor,
                                        buttonAddComment })
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(38, 48, 69);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Montserrat", 9F);
                btn.Height = 30;
            }

            buttonReject.BackColor = Color.FromArgb(185, 159, 110);

            // Подключение обработчиков событий для кнопок
            buttonAssignExecutor.Click += buttonAssignExecutor_Click;
            buttonAddComment.Click += buttonAddComment_Click;

            // Добавляем панели с кнопками для направлений и уровней образования
            var panelDirections = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var btnAddDirection = new Button
            {
                Text = "Добавить",
                Location = new Point(10, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(38, 48, 69),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnAddDirection.Click += BtnAddDirection_Click;

            var btnEditDirection = new Button
            {
                Text = "Изменить",
                Location = new Point(120, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(38, 48, 69),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnEditDirection.Click += BtnEditDirection_Click;

            var btnDeleteDirection = new Button
            {
                Text = "Удалить",
                Location = new Point(230, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(185, 159, 110),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnDeleteDirection.Click += BtnDeleteDirection_Click;

            panelDirections.Controls.Add(btnAddDirection);
            panelDirections.Controls.Add(btnEditDirection);
            panelDirections.Controls.Add(btnDeleteDirection);
            tabPageDirections.Controls.Add(panelDirections);

            // Панель для кнопок управления уровнями образования
            var panelEducationLevels = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var btnAddEducationLevel = new Button
            {
                Text = "Добавить",
                Location = new Point(10, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(38, 48, 69),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnAddEducationLevel.Click += BtnAddEducationLevel_Click;

            var btnEditEducationLevel = new Button
            {
                Text = "Изменить",
                Location = new Point(120, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(38, 48, 69),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnEditEducationLevel.Click += BtnEditEducationLevel_Click;

            var btnDeleteEducationLevel = new Button
            {
                Text = "Удалить",
                Location = new Point(230, 10),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(185, 159, 110),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F)
            };
            btnDeleteEducationLevel.Click += BtnDeleteEducationLevel_Click;

            panelEducationLevels.Controls.Add(btnAddEducationLevel);
            panelEducationLevels.Controls.Add(btnEditEducationLevel);
            panelEducationLevels.Controls.Add(btnDeleteEducationLevel);
            tabPageEducationLevels.Controls.Add(panelEducationLevels);

            // Уменьшаем размер DataGridView, чтобы кнопки были видны
            dataGridViewDirections.Height -= panelDirections.Height;
            dataGridViewEducationLevels.Height -= panelEducationLevels.Height;
        }

        private void LoadData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Загрузка заявлений с информацией о назначенных исполнителях
                    string applicationsQuery = @"
                SELECT a.id_application, u.fio, el.name_level, 
                       sa.status_name, a.application_date, a.comment,
                       a.document_path, a.average_score,
                       exec.fio as executor_name,
                       aa.assignment_date, aa.completion_date
                FROM applications a
                JOIN users u ON a.id_users = u.id_users
                JOIN education_level el ON a.id_education_level = el.id_education_level
                JOIN status_applications sa ON a.id_status = sa.id_status
                LEFT JOIN application_assignments aa ON a.id_application = aa.id_application
                LEFT JOIN executors e ON aa.id_executor = e.id_executor
                LEFT JOIN users exec ON e.id_user = exec.id_users
                ORDER BY a.application_date DESC";

                    using (var adapter = new NpgsqlDataAdapter(applicationsQuery, connection))
                    {
                        applicationsTable = new DataTable();
                        adapter.Fill(applicationsTable);
                        dataGridViewApplications.DataSource = applicationsTable;
                    }

                    // Загрузка направлений подготовки
                    string directionsQuery = @"
                SELECT sd.id_direction, sd.direction_code, sd.direction_name, 
                       el.name_level as education_level
                FROM study_directions sd
                JOIN education_level el ON sd.id_education_level = el.id_education_level
                ORDER BY sd.direction_code";

                    using (var adapter = new NpgsqlDataAdapter(directionsQuery, connection))
                    {
                        directionsTable = new DataTable();
                        adapter.Fill(directionsTable);
                        dataGridViewDirections.DataSource = directionsTable;
                    }

                    // Загрузка уровней образования
                    string educationLevelsQuery = "SELECT * FROM education_level ORDER BY id_education_level";
                    using (var adapter = new NpgsqlDataAdapter(educationLevelsQuery, connection))
                    {
                        educationLevelsTable = new DataTable();
                        adapter.Fill(educationLevelsTable);
                        dataGridViewEducationLevels.DataSource = educationLevelsTable;
                    }

                    // Загрузка исполнителей (пользователей с ролью 2)
                    string executorsQuery = @"
                SELECT e.id_executor, u.fio, u.email 
                FROM executors e
                JOIN users u ON e.id_user = u.id_users
                WHERE u.id_roles = 2";
                    using (var adapter = new NpgsqlDataAdapter(executorsQuery, connection))
                    {
                        executorsTable = new DataTable();
                        adapter.Fill(executorsTable);
                        dataGridViewExecutors.DataSource = executorsTable;
                    }

                    // Загрузка статистики
                    LoadStatistics(connection);

                    // Настройка столбцов после загрузки данных
                    ConfigureDataGridViewColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAssignExecutor_Click(object sender, EventArgs e)
        {
            if (dataGridViewApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявление для назначения исполнителя", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridViewExecutors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите исполнителя", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int applicationId = (int)dataGridViewApplications.SelectedRows[0].Cells["id_application"].Value;
            int executorId = (int)dataGridViewExecutors.SelectedRows[0].Cells["id_executor"].Value;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем, не назначен ли уже исполнитель
                    using (var checkCommand = new NpgsqlCommand(
                        "SELECT COUNT(*) FROM application_assignments WHERE id_application = @appId", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@appId", applicationId);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            if (MessageBox.Show("Для этой заявки уже назначен исполнитель. Хотите заменить его?",
                                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            // Удаляем предыдущее назначение
                            using (var deleteCommand = new NpgsqlCommand(
                                "DELETE FROM application_assignments WHERE id_application = @appId", connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@appId", applicationId);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Добавляем новое назначение
                    using (var insertCommand = new NpgsqlCommand(
                        "INSERT INTO application_assignments (id_application, id_executor, assignment_date) " +
                        "VALUES (@appId, @execId, NOW())", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@appId", applicationId);
                        insertCommand.Parameters.AddWithValue("@execId", executorId);
                        insertCommand.ExecuteNonQuery();
                    }

                    using (var statusCommand = new NpgsqlCommand(
                        "UPDATE applications SET id_status = " +
                        "(SELECT id_status FROM status_applications WHERE status_name = 'В ДОРАБОТКУ') " +
                        "WHERE id_application = @appId", connection))
                    {
                        statusCommand.Parameters.AddWithValue("@appId", applicationId);
                        statusCommand.ExecuteNonQuery();
                    }

                    // Добавляем запись в историю статусов
                    using (var historyCommand = new NpgsqlCommand(
                        "INSERT INTO application_status_history " +
                        "(id_application, id_status, comment, changed_by_user) " +
                        "VALUES (@appId, (SELECT id_status FROM status_applications WHERE status_name = 'В ДОРАБОТКУ'), " +
                        "'Назначен исполнитель', @userId)", connection))
                    {
                        historyCommand.Parameters.AddWithValue("@appId", applicationId);
                        historyCommand.Parameters.AddWithValue("@userId", currentUserId);
                        historyCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Исполнитель успешно назначен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData(); // Обновляем данные
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка назначения исполнителя: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddComment_Click(object sender, EventArgs e)
        {
            if (dataGridViewApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявление для добавления комментария", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int applicationId = (int)dataGridViewApplications.SelectedRows[0].Cells["id_application"].Value;
            string currentStatus = dataGridViewApplications.SelectedRows[0].Cells["status_name"].Value.ToString();

            string comment = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите комментарий:", "Добавление комментария", "");

            if (!string.IsNullOrEmpty(comment))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        // Добавляем запись в историю статусов
                        using (var command = new NpgsqlCommand(
                            "INSERT INTO application_status_history " +
                            "(id_application, id_status, comment, changed_by_user) " +
                            "VALUES (@appId, (SELECT id_status FROM status_applications WHERE status_name = @status), " +
                            "@comment, @userId)", connection))
                        {
                            command.Parameters.AddWithValue("@appId", applicationId);
                            command.Parameters.AddWithValue("@status", currentStatus);
                            command.Parameters.AddWithValue("@comment", comment);
                            command.Parameters.AddWithValue("@userId", currentUserId);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Комментарий успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData(); // Обновляем данные
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления комментария: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadStatistics(NpgsqlConnection connection)
        {
            // Статистика по выполненным заявкам
            string statsQuery = @"
                WITH completed_apps AS (
                    SELECT 
                        aa.id_application,
                        aa.assignment_date,
                        aa.completion_date,
                        EXTRACT(EPOCH FROM (aa.completion_date - aa.assignment_date))/3600 as hours_spent,
                        el.name_level as education_level,
                        exec.fio as executor_name
                    FROM application_assignments aa
                    JOIN applications a ON aa.id_application = a.id_application
                    JOIN education_level el ON a.id_education_level = el.id_education_level
                    LEFT JOIN executors e ON aa.id_executor = e.id_executor
                    LEFT JOIN users exec ON e.id_user = exec.id_users
                    WHERE aa.completion_date IS NOT NULL
                    AND a.id_status = (SELECT id_status FROM status_applications WHERE status_name = 'ГОТОВО')
                )
                SELECT 
                    COUNT(*) as total_completed,
                    AVG(hours_spent) as avg_hours,
                    MIN(hours_spent) as min_hours,
                    MAX(hours_spent) as max_hours,
                    education_level,
                    executor_name,
                    COUNT(*) as count_by_executor
                FROM completed_apps
                GROUP BY GROUPING SETS ((education_level), (executor_name), ())
                HAVING GROUPING(education_level) = 0 OR GROUPING(executor_name) = 0 OR 
                      (GROUPING(education_level) = 1 AND GROUPING(executor_name) = 1)";

            DataTable statsTable = new DataTable();
            statsTable.Columns.Add("Категория");
            statsTable.Columns.Add("Показатель");
            statsTable.Columns.Add("Значение");

            using (var command = new NpgsqlCommand(statsQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = "";
                        string metric = "";
                        string value = "";

                        // Общая статистика
                        if (reader.IsDBNull(4) && reader.IsDBNull(5))
                        {
                            category = "Общая статистика";
                            int total = reader.GetInt32(0);
                            double avgHours = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                            double minHours = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
                            double maxHours = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);

                            statsTable.Rows.Add(category, "Всего выполнено заявок", total);
                            statsTable.Rows.Add(category, "Среднее время выполнения (часы)", $"{avgHours:F1}");
                            statsTable.Rows.Add(category, "Минимальное время (часы)", $"{minHours:F1}");
                            statsTable.Rows.Add(category, "Максимальное время (часы)", $"{maxHours:F1}");
                        }
                        // Статистика по уровням образования
                        else if (!reader.IsDBNull(4))
                        {
                            category = "По уровню образования";
                            metric = reader.GetString(4);
                            value = $"{reader.GetInt32(6)} заявок";
                            statsTable.Rows.Add(category, metric, value);
                        }
                        // Статистика по исполнителям
                        else if (!reader.IsDBNull(5))
                        {
                            category = "По исполнителям";
                            metric = reader.GetString(5);
                            double avgHours = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                            value = $"{reader.GetInt32(6)} заявок, среднее время: {avgHours:F1} ч.";
                            statsTable.Rows.Add(category, metric, value);
                        }
                    }
                }
            }

            dataGridViewStatistics.DataSource = statsTable;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePickerFilter.Value.Date;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT a.id_application, u.fio, el.name_level, 
                               sa.status_name, a.application_date, a.comment,
                               a.document_path, a.average_score,
                               exec.fio as executor_name,
                               aa.assignment_date, aa.completion_date
                        FROM applications a
                        JOIN users u ON a.id_users = u.id_users
                        JOIN education_level el ON a.id_education_level = el.id_education_level
                        JOIN status_applications sa ON a.id_status = sa.id_status
                        LEFT JOIN application_assignments aa ON a.id_application = aa.id_application
                        LEFT JOIN executors e ON aa.id_executor = e.id_executor
                        LEFT JOIN users exec ON e.id_user = exec.id_users
                        WHERE DATE(a.application_date) = @selectedDate
                        ORDER BY a.application_date DESC";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@selectedDate", selectedDate);

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable filteredTable = new DataTable();
                            adapter.Fill(filteredTable);
                            dataGridViewApplications.DataSource = filteredTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonApprove_Click(object sender, EventArgs e)
        {
            if (dataGridViewApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявление для одобрения", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewApplications.SelectedRows[0];
            int applicationId = (int)selectedRow.Cells["id_application"].Value;
            string documentPath = selectedRow.Cells["document_path"].Value?.ToString();

            if (string.IsNullOrEmpty(documentPath) || !File.Exists(documentPath))
            {
                MessageBox.Show("Документ об образовании не загружен или не найден!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Обновляем completion_date в application_assignments
                    using (var updateAssignmentCommand = new NpgsqlCommand(
                        "UPDATE application_assignments SET completion_date = NOW() " +
                        "WHERE id_application = @appId", connection))
                    {
                        updateAssignmentCommand.Parameters.AddWithValue("@appId", applicationId);
                        int rowsAffected = updateAssignmentCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("Для этой заявки не назначен исполнитель!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Затем меняем статус заявки
                    ChangeApplicationStatus(applicationId, "ГОТОВО", "Заявление проверено и одобрено");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении даты завершения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
            if (dataGridViewApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявление для возврата на доработку", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string comment = Microsoft.VisualBasic.Interaction.InputBox(
                "Укажите причину доработки:", "Комментарий к доработке", "");

            if (!string.IsNullOrEmpty(comment))
            {
                int applicationId = (int)dataGridViewApplications.SelectedRows[0].Cells["id_application"].Value;
                ChangeApplicationStatus(applicationId, "В ДОРАБОТКУ", comment);
            }
        }

        private void ChangeApplicationStatus(int applicationId, string statusName, string comment)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Получаем ID статуса
                    int statusId;
                    using (var command = new NpgsqlCommand(
                        "SELECT id_status FROM status_applications WHERE status_name = @statusName", connection))
                    {
                        command.Parameters.AddWithValue("@statusName", statusName);
                        statusId = (int)command.ExecuteScalar();
                    }

                    // Обновляем статус заявления
                    using (var command = new NpgsqlCommand(
                        "UPDATE applications SET id_status = @statusId, comment = @comment " +
                        "WHERE id_application = @appId", connection))
                    {
                        command.Parameters.AddWithValue("@statusId", statusId);
                        command.Parameters.AddWithValue("@comment", comment);
                        command.Parameters.AddWithValue("@appId", applicationId);
                        command.ExecuteNonQuery();
                    }

                    // Добавляем запись в историю статусов
                    using (var command = new NpgsqlCommand(
                        "INSERT INTO application_status_history " +
                        "(id_application, id_status, comment, changed_by_user) " +
                        "VALUES (@appId, @statusId, @comment, @userId)", connection))
                    {
                        command.Parameters.AddWithValue("@appId", applicationId);
                        command.Parameters.AddWithValue("@statusId", statusId);
                        command.Parameters.AddWithValue("@comment", comment);
                        command.Parameters.AddWithValue("@userId", currentUserId);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Статус заявления успешно изменен на '{statusName}'", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения статуса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявление для просмотра", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int applicationId = (int)dataGridViewApplications.SelectedRows[0].Cells["id_application"].Value;
            string fio = dataGridViewApplications.Rows[dataGridViewApplications.SelectedRows[0].Index].Cells["fio"].Value.ToString();
            string status = dataGridViewApplications.Rows[dataGridViewApplications.SelectedRows[0].Index].Cells["status_name"].Value.ToString();
            string comment = dataGridViewApplications.Rows[dataGridViewApplications.SelectedRows[0].Index].Cells["comment"]?.Value?.ToString() ?? "";

            using (var form = new ApplicationDetailsForm(applicationId, fio, status, comment))
            {
                form.ShowDialog();
            }
        }

        #region Управление направлениями подготовки

        private void BtnAddDirection_Click(object sender, EventArgs e)
        {
            using (var form = new EditDirectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                        using (var connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();

                            using (var command = new NpgsqlCommand(
                                "INSERT INTO study_directions (direction_code, direction_name, id_education_level) " +
                                "VALUES (@code, @name, @levelId)", connection))
                            {
                                command.Parameters.AddWithValue("@code", form.DirectionCode);
                                command.Parameters.AddWithValue("@name", form.DirectionName);
                                command.Parameters.AddWithValue("@levelId", form.EducationLevelId);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Направление успешно добавлено", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка добавления направления: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnEditDirection_Click(object sender, EventArgs e)
        {
            if (dataGridViewDirections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите направление для редактирования", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewDirections.SelectedRows[0];
            int id = (int)selectedRow.Cells["id_direction"].Value;
            string code = selectedRow.Cells["direction_code"].Value.ToString();
            string name = selectedRow.Cells["direction_name"].Value.ToString();
            int levelId = GetEducationLevelId(selectedRow.Cells["education_level"].Value.ToString());

            using (var form = new EditDirectionForm(id, code, name, levelId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                        using (var connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();

                            using (var command = new NpgsqlCommand(
                                "UPDATE study_directions SET direction_code = @code, direction_name = @name, " +
                                "id_education_level = @levelId WHERE id_direction = @id", connection))
                            {
                                command.Parameters.AddWithValue("@code", form.DirectionCode);
                                command.Parameters.AddWithValue("@name", form.DirectionName);
                                command.Parameters.AddWithValue("@levelId", form.EducationLevelId);
                                command.Parameters.AddWithValue("@id", id);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Направление успешно обновлено", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обновления направления: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnDeleteDirection_Click(object sender, EventArgs e)
        {
            if (dataGridViewDirections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите направление для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewDirections.SelectedRows[0].Cells["id_direction"].Value;
            string name = dataGridViewDirections.SelectedRows[0].Cells["direction_name"].Value.ToString();

            if (MessageBox.Show($"Вы уверены, что хотите удалить направление '{name}'?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        using (var command = new NpgsqlCommand(
                            "DELETE FROM study_directions WHERE id_direction = @id", connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Направление успешно удалено", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления направления: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetEducationLevelId(string levelName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "SELECT id_education_level FROM education_level WHERE name_level = @name", connection))
                {
                    command.Parameters.AddWithValue("@name", levelName);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Управление уровнями образования

        private void BtnAddEducationLevel_Click(object sender, EventArgs e)
        {
            string levelName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите название уровня образования:", "Добавление уровня образования", "");

            if (!string.IsNullOrEmpty(levelName))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        using (var command = new NpgsqlCommand(
                            "INSERT INTO education_level (name_level) VALUES (@name)", connection))
                        {
                            command.Parameters.AddWithValue("@name", levelName);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Уровень образования успешно добавлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления уровня образования: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditEducationLevel_Click(object sender, EventArgs e)
        {
            if (dataGridViewEducationLevels.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите уровень образования для редактирования", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewEducationLevels.SelectedRows[0];
            int id = (int)selectedRow.Cells["id_education_level"].Value;
            string currentName = selectedRow.Cells["name_level"].Value.ToString();

            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите новое название уровня образования:", "Редактирование уровня образования", currentName);

            if (!string.IsNullOrEmpty(newName) && newName != currentName)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        using (var command = new NpgsqlCommand(
                            "UPDATE education_level SET name_level = @name WHERE id_education_level = @id", connection))
                        {
                            command.Parameters.AddWithValue("@name", newName);
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Уровень образования успешно обновлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления уровня образования: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDeleteEducationLevel_Click(object sender, EventArgs e)
        {
            if (dataGridViewEducationLevels.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите уровень образования для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewEducationLevels.SelectedRows[0].Cells["id_education_level"].Value;
            string name = dataGridViewEducationLevels.SelectedRows[0].Cells["name_level"].Value.ToString();

            if (MessageBox.Show($"Вы уверены, что хотите удалить уровень образования '{name}'?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        // Проверяем, есть ли связанные направления
                        using (var checkCommand = new NpgsqlCommand(
                            "SELECT COUNT(*) FROM study_directions WHERE id_education_level = @id", connection))
                        {
                            checkCommand.Parameters.AddWithValue("@id", id);
                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Невозможно удалить уровень образования, так как существуют связанные направления!", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        using (var command = new NpgsqlCommand(
                            "DELETE FROM education_level WHERE id_education_level = @id", connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Уровень образования успешно удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления уровня образования: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void buttonShowApplications_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageApplications;
        }

        private void buttonShowDirections_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageDirections;
        }

        private void buttonShowEducationLevels_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageEducationLevels;
        }

        private void buttonShowExecutors_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageExecutors;
        }

        private void buttonShowStatistics_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageStatistics;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConfigureDataGridViewColumns()
        {
            // Настройка столбцов для таблицы заявок
            if (dataGridViewApplications.Columns.Count > 0)
            {
                dataGridViewApplications.Columns["id_application"].HeaderText = "ID заявки";
                dataGridViewApplications.Columns["fio"].HeaderText = "ФИО абитуриента";
                dataGridViewApplications.Columns["name_level"].HeaderText = "Уровень образования";
                dataGridViewApplications.Columns["status_name"].HeaderText = "Статус";
                dataGridViewApplications.Columns["application_date"].HeaderText = "Дата подачи";
                dataGridViewApplications.Columns["comment"].HeaderText = "Комментарий";
                dataGridViewApplications.Columns["document_path"].HeaderText = "Путь к документу";

                // Скрываем столбец average_score
                if (dataGridViewApplications.Columns.Contains("average_score"))
                {
                    dataGridViewApplications.Columns["average_score"].Visible = false;
                }

                dataGridViewApplications.Columns["executor_name"].HeaderText = "Исполнитель";
                dataGridViewApplications.Columns["assignment_date"].HeaderText = "Дата назначения";
                dataGridViewApplications.Columns["completion_date"].HeaderText = "Дата завершения";
            }

            // Остальные настройки столбцов для других таблиц остаются без изменений
            if (dataGridViewDirections.Columns.Count > 0)
            {
                dataGridViewDirections.Columns["id_direction"].HeaderText = "ID направления";
                dataGridViewDirections.Columns["direction_code"].HeaderText = "Код направления";
                dataGridViewDirections.Columns["direction_name"].HeaderText = "Название направления";
                dataGridViewDirections.Columns["education_level"].HeaderText = "Уровень образования";
            }

            if (dataGridViewEducationLevels.Columns.Count > 0)
            {
                dataGridViewEducationLevels.Columns["id_education_level"].HeaderText = "ID уровня";
                dataGridViewEducationLevels.Columns["name_level"].HeaderText = "Название уровня";
            }

            if (dataGridViewExecutors.Columns.Count > 0)
            {
                dataGridViewExecutors.Columns["id_executor"].HeaderText = "ID исполнителя";
                dataGridViewExecutors.Columns["fio"].HeaderText = "ФИО";
                dataGridViewExecutors.Columns["email"].HeaderText = "Email";
            }

            if (dataGridViewStatistics.Columns.Count > 0)
            {
                dataGridViewStatistics.Columns["Категория"].HeaderText = "Категория";
                dataGridViewStatistics.Columns["Показатель"].HeaderText = "Показатель";
                dataGridViewStatistics.Columns["Значение"].HeaderText = "Значение";
            }
        }
        private void dataGridViewApplications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int applicationId = (int)dataGridViewApplications.Rows[e.RowIndex].Cells["id_application"].Value;
                string fio = dataGridViewApplications.Rows[e.RowIndex].Cells["fio"].Value.ToString();
                string status = dataGridViewApplications.Rows[e.RowIndex].Cells["status_name"].Value.ToString();
                string comment = dataGridViewApplications.Rows[e.RowIndex].Cells["comment"]?.Value?.ToString() ?? "";

                using (var form = new ApplicationDetailsForm(applicationId, fio, status, comment))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewApplications.Rows[e.RowIndex].Cells["comment"].Value = form.Comment;
                    }
                }
            }
        }
    }
}