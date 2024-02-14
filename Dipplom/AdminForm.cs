using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dipplom
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class AdminForm : Form
    {

        ConnectionClass database = new ConnectionClass();

        int SelectedRow;

        public AdminForm()
        {
            InitializeComponent();
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Логин", "Логин");
            dataGridView1.Columns.Add("Пароль", "Пароль");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void UpdateDb()
        {
            database.openconnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existed)
                    continue;

                if(rowState == RowState.Deleted)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var deletequery = $"delete from Рабочий where id = '{id}'";

                    var command = new SqlCommand(deletequery, database.GetSqlConnection());
                    command.ExecuteNonQuery();
                }
            }
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string querystring = $"select * from Рабочий";
            SqlCommand command = new SqlCommand(querystring, database.GetSqlConnection());
           
            database.openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;

            if (e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[SelectedRow];
                textBoxId.Text = row.Cells[0].Value.ToString();
                textBoxLogin.Text = row.Cells[1].Value.ToString();
                textBoxPassword.Text = row.Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.openconnection();

            var id = textBoxId.Text;
            var log = textBoxLogin.Text;
            var pass = textBoxPassword.Text;

            if (textBoxId.Text != "" && textBoxLogin.Text != "" && textBoxPassword.Text != "")
            {
                var addquery = $"insert into Рабочий (id, Логин, Пароль) values ('{id}','{log}','{pass}')";

                var command = new SqlCommand(addquery, database.GetSqlConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Обновите данные", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateDb();
        }
    }
}
