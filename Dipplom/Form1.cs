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
    public partial class UserLogin : Form
    {
        ConnectionClass database = new ConnectionClass();
        public UserLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Берём значения из полей логина и пароля
            var LoginUser = textBoxLogin.Text;
            var PasswordUser = textBoxPassword.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            //Запрос к данным списка пользователей

            string querystring = $"select id, Логин, Пароль from Пользователь where Логин = '{LoginUser}' and Пароль = '{PasswordUser}'";

            SqlCommand command = new SqlCommand(querystring, database.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            //Проверка на наличие существующего аккаунта 

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserForm UserForm = new UserForm();
                this.Hide();
                UserForm.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WorkerLoginForm UserForm = new WorkerLoginForm();
            this.Hide();
            UserForm.ShowDialog();
            this.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminLoginForm UserForm = new AdminLoginForm();
            this.Hide();
            UserForm.ShowDialog();
            this.Show();
        }
    }
}