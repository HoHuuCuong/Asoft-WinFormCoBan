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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class Them : Form
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
        public Them()
        {
            InitializeComponent();
        
        }
        public Them(Form1 form1)
        {
           
            InitializeComponent();
            // Gán tham chiếu của form chính
            this.form1 = form1;
            connection = new SqlConnection(chuoiketnoi);
            connection.Open();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtTel.Clear();
            txtEmail.Clear();
            txtPass.Clear();
            txtConfirmPass.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            userID = txtID.Text;
            userName = txtName.Text;
            tel =  txtTel.Text;
            email = txtEmail.Text;
            passWord= txtPass.Text;
            conFirmPassWord= txtConfirmPass.Text;

            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("Vui lòng nhập mã người dùng.");
                return;
            }

            // Kiểm tra trùng mã người dùng trong cơ sở dữ liệu (giả sử có hàm kiểm tra trong database)
            if (CheckUserID(userID))
            {
                MessageBox.Show("Mã người dùng đã tồn tại. Vui lòng chọn mã khác.");
                return;
            }

            // Kiểm tra bắt buộc nhập Tên người dùng
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.");
                return;
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu trùng nhau
            if (!string.IsNullOrEmpty(passWord) && !string.IsNullOrEmpty(conFirmPassWord))
            {
                if (passWord != conFirmPassWord)
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.");
                    return;
                }
            }
            else if (string.IsNullOrEmpty(passWord) && !string.IsNullOrEmpty(conFirmPassWord))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.");
                return;
            }
            else if (!string.IsNullOrEmpty(passWord) && string.IsNullOrEmpty(conFirmPassWord))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu.");
                return;
            }

            // Kiểm tra và xử lý email hợp lệ
            if (!string.IsNullOrEmpty(email))
            {
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    return;
                }
            }


           
            command =connection.CreateCommand(); 
            command.CommandText= 
            "insert into Test values('" + userID + "', '"+ userName + "','"+ passWord + "','"+ email + "','"+ tel + "',0)";
            int rowsAffected = command.ExecuteNonQuery();
            form1.loadData();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm thành công.");
            }
            else
            {
                MessageBox.Show("Thêm không thành công.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckUserID(string userID)
        {
            bool check = false;
                string query = "SELECT COUNT(*) FROM Test WHERE UserID = @UserID";
                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu truy vấn
                    command.Parameters.AddWithValue("@UserID", userID);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = (int)command.ExecuteScalar();

                    // Kiểm tra nếu có tồn tại mã người dùng trong cơ sở dữ liệu
                    if (count > 0)
                    {
                    check = true;
                    }
                
            }

            return check;
        }


    }
}
