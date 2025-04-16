using Npgsql;
using System.Configuration;
using System.Data;

namespace uchet
{
    public partial class EditDirectionForm : Form
    {
        public string DirectionCode { get; private set; }
        public string DirectionName { get; private set; }
        public int EducationLevelId { get; private set; }

        private ComboBox comboBoxEducationLevels;
        private TextBox textBoxCode;
        private TextBox textBoxName;

        public EditDirectionForm() : this(0, "", "", 0) { }

        public EditDirectionForm(int id, string code, string name, int levelId)
        {
            InitializeComponents(id, code, name, levelId);
            LoadEducationLevels();
        }

        private void InitializeComponents(int id, string code, string name, int levelId)
        {
            this.Text = id == 0 ? "Добавление направления" : "Редактирование направления";
            this.Size = new Size(400, 250); // Увеличили высоту формы
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.75F);

            // Элементы формы
            Label labelCode = new Label
            {
                Text = "Код направления:",
                Location = new Point(20, 20),
                ForeColor = Color.FromArgb(52, 73, 94) // Темно-синий цвет текста
            };

            textBoxCode = new TextBox
            {
                Text = code,
                Location = new Point(150, 20),
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label labelName = new Label
            {
                Text = "Название:",
                Location = new Point(20, 60),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            textBoxName = new TextBox
            {
                Text = name,
                Location = new Point(150, 60),
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label labelLevel = new Label
            {
                Text = "Уровень образования:",
                Location = new Point(20, 100),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            comboBoxEducationLevels = new ComboBox
            {
                Location = new Point(150, 100),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };

            // Панель для кнопок
            Panel panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            Button buttonOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(150, 10),
                BackColor = Color.FromArgb(185, 159, 110), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                Height = 30
            };
            buttonOk.Click += ButtonOk_Click;

            Button buttonCancel = new Button
            {
                Text = "Отмена",
                DialogResult = DialogResult.Cancel,
                Location = new Point(260, 10),
                BackColor = Color.FromArgb(19, 34, 67), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                Height = 30
            };

            panelButtons.Controls.Add(buttonOk);
            panelButtons.Controls.Add(buttonCancel);

            this.Controls.Add(labelCode);
            this.Controls.Add(textBoxCode);
            this.Controls.Add(labelName);
            this.Controls.Add(textBoxName);
            this.Controls.Add(labelLevel);
            this.Controls.Add(comboBoxEducationLevels);
            this.Controls.Add(panelButtons);
        }

        private void LoadEducationLevels()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_education_level, name_level FROM education_level ORDER BY name_level";
                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        comboBoxEducationLevels.DataSource = table;
                        comboBoxEducationLevels.DisplayMember = "name_level";
                        comboBoxEducationLevels.ValueMember = "id_education_level";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки уровней образования: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCode.Text))
            {
                MessageBox.Show("Введите код направления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введите название направления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxEducationLevels.SelectedValue == null)
            {
                MessageBox.Show("Выберите уровень образования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DirectionCode = textBoxCode.Text.Trim();
            DirectionName = textBoxName.Text.Trim();
            EducationLevelId = (int)comboBoxEducationLevels.SelectedValue;
        }
    }
}