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

namespace WindowsFormsApp1
{
    public partial class Sua : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string chuoiketnoi = @"Data Source=DESKTOP-3MPCM6R\HUUCUONG;Initial Catalog=test;Integrated Security=True;Encrypt=False";
        private Form1 form1;
        public string userID;
        public string userName;
        public string tel;
        public string email;
        public string passWord;
        public string conFirmPassWord;
        public Sua()
        {
            InitializeComponent();
        }
       
        public Sua(Form1 form1,string userID, string userName, string email, string tel)
        {
            InitializeComponent();
            this.form1 = form1;
            this.userID = userID;
            this.userName = userName;
            this.email = email;
            this.tel = tel;
            loadData();
            connection = new SqlConnection(chuoiketnoi);
            connection.Open();
        }
        public void loadData()
        {
            
            txtName.Text = userName;
            txtEmail.Text = email;
            txtTel.Text = tel;
            txtPass.Text = passWord;
        }
        private void btnNhapTiep_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            userName = txtName.Text;
            tel = txtTel.Text;
            email = txtEmail.Text;
            passWord = txtPass.Text;


            command = connection.CreateCommand();
            command.CommandText = command.CommandText = 
            "UPDATE Test SET UserName = '" + userName + "', Password = '" + passWord + "', Email = '" + email + "', Tel = '" + tel + "' WHERE UserID = '" + userID + "'";
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Dữ liệu đã được cập nhật thành công.");
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được cập nhật.");
            }
            form1.loadData();
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
