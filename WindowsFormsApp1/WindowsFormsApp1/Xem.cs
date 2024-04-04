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
    public partial class Xem : Form
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
        public Xem()
        {
            InitializeComponent();
        }
        public Xem(Form1 form1, string userID, string userName, string email, string tel)
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
            txtName.ReadOnly = true; // Ngăn người dùng chỉnh sửa

            txtEmail.Text = email;
            txtEmail.ReadOnly = true;

            txtTel.Text = tel;
            txtTel.ReadOnly = true;

            txtPass.Text = passWord;
            txtPass.ReadOnly = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
