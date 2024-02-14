using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dipplom
{
    public partial class AdminLoginForm : Form
    {
        ConnectionClass database = new ConnectionClass();

        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Берём значения из полей логина и пароля
            var LoginUser = textBoxLogin.Text;
            var PasswordUser = textBoxPassword.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            //Запрос к данным списка пользователей

            string querystring = $"select id, Логин, Пароль from Админ where Логин = '{LoginUser}' and Пароль = '{PasswordUser}'";

            SqlCommand command = new SqlCommand(querystring, database.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            //Проверка на наличие существующего аккаунта 

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AdminForm AdminLoginForm = new AdminForm();
                this.Hide();
                AdminLoginForm.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
