using System.Configuration;
using System.Windows.Forms;
using Npgsql;

namespace uchet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ����������� ����������� = new �����������();
            �����������.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxPass.Text;

            string ConnectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                // ������ ��� �������� ���� ������������
                string sql = @"SELECT u.id_users, r.role_name 
                              FROM users u
                              JOIN roles r ON u.id_roles = r.id_roles
                              WHERE u.email = @email AND u.pass = @password";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string role = reader.GetString(1);

                            if (role == "����������")
                            {
                                ���������_����������� applicantForm = new ���������_�����������(userId);
                                applicantForm.Show();
                            }
                            else if (role == "�������� �������� ��������")
                            {
                                // �������� userId � ����������� ����������������_����
                                ����������������_���� adminMenu = new ����������������_����(userId);
                                adminMenu.Show();
                            }
                            else
                            {
                                MessageBox.Show("����������� ���� ������������!");
                                return;
                            }

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("�������� ����� ��� ������!");
                        }
                    }
                }
            }
        }
    }
}