﻿using System;
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
    public partial class Form1 : Form
    {
        Database database = new DataBase();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var LoginUser = textBoxLogin.Text;
            var PasswordUser = textBoxPassword.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, Логин, Пароль, accesslevel from Пользователь where Логин = '{LoginUser}' and Пароль = '{PasswordUser}'";

            SqlCommand command = new SqlCommand(querystring, database.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserForm UserForm = new UserForm();
                this.Hide();
                UserForm.ShowDialog();
                this.Show();

            }
            else
            { MessageBox.Show("Такого аккаунта не существует!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}