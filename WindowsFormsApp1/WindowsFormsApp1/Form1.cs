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
using System.Drawing.Imaging;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Form1 form1;
        string chuoiketnoi = @"Data Source=DESKTOP-3MPCM6R\HUUCUONG;Initial Catalog=test;Integrated Security=True;Encrypt=False";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public string userID;
        public string userName;
        public string tel;
        public string email;
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

        }
        public Form1( Form1 form1)
        {
            InitializeComponent();
            this.form1= form1;
        }      

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(chuoiketnoi);
            connection.Open();
            loadData();
           // hienthi();
        }
      
       public void loadData()
        {
            command=connection.CreateCommand();
            command.CommandText = @"Select userID, userName, email, tel  FROM test";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);    
            dataGridView1.DataSource = table;
            dataGridView1.ClearSelection();
        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnThucHien, new Point(50,50));
        }
        
        private void thêmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                  
            Them frmThem = new Them(this);
            frmThem.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            userID = dataGridView1.Rows[i].Cells[0].Value.ToString();
            userName = dataGridView1.Rows[i].Cells[1].Value.ToString();
            email = dataGridView1.Rows[i].Cells[2].Value.ToString();
            tel = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void sửaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (userID == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng trong bảng.");
            }
            else
            {
                Sua frmSua = new Sua(this,userID, userName, email, tel);
                frmSua.ShowDialog();
            }
        }

        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {


            if (userID == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    command = connection.CreateCommand();
                    command.CommandText = command.CommandText = "delete from Test where userid='" + userID + "'";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công.");
                    loadData();
                }
            }
            
            }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userID == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng trong bảng.");
            }
            else
            {
                Xem frmXem = new Xem(this, userID, userName, email, tel);
                frmXem.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }
    }
}
