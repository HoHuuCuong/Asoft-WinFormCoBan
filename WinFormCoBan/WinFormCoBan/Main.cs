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

namespace WinFormCoBan
{
    public partial class Main : Form
    {
        Main main;
        string chuoiketnoi = @"Data Source=DESKTOP-3MPCM6R\HUUCUONG;Initial Catalog=test;Integrated Security=True;Encrypt=False";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public string userID;
        public string userName;
        public string tel;
        public string email;
        public string pass;
        public string flag;
        public Main()
        {
            InitializeComponent();
            connection = new SqlConnection(chuoiketnoi);
            connection.Open();
            loadData();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        public Main(Main main)
        {
            InitializeComponent();
           this.main = main;
        }

        public void loadData()
        {
            command = connection.CreateCommand();
            command.CommandText = @"Select userID, userName, email, tel  FROM test";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.ClearSelection();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnThucHien, new Point(50, 50));

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            userID = dataGridView1.Rows[i].Cells[0].Value.ToString();
            userName = dataGridView1.Rows[i].Cells[1].Value.ToString();
            email = dataGridView1.Rows[i].Cells[2].Value.ToString();
            tel = dataGridView1.Rows[i].Cells[3].Value.ToString();

            string sqlQuery = "SELECT Password FROM Test WHERE UserID = @userID";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {   
                command.Parameters.AddWithValue("@userID", userID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                         pass = reader["Password"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng có userID " + userID);
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void thêmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            flag="them";
            ThucHien thucHien = new ThucHien(this, userID, userName, email, tel,pass,flag);
            thucHien.ShowDialog();
        }

        private void sửaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (userID == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng trong bảng.");
            }
            else
            {
                flag = "sua";
                ThucHien thucHien = new ThucHien(this, userID, userName, email, tel,pass, flag);
                thucHien.ShowDialog();
            }
           
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userID == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng trong bảng.");
            }
            else
            {
                flag = "xem";
                ThucHien thucHien = new ThucHien(this, userID, userName, email, tel, pass, flag);
                thucHien.ShowDialog();
            }
        }
    }
}
