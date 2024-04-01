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
namespace WindowsForm
{
    public partial class QuanLyKhachSan : Form
    {
        SqlConnection sqlconn =new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public QuanLyKhachSan()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            sqlconn.Open();
            string knoi = "select *from QLKhachSan";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(knoi,sqlconn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["TenKhach"].Value.ToString();
                string sex;
                sex = Convert.ToString(row.Cells["GioiTinh"].Value);
                if(sex=="Nam")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                comboBox1.Text = row.Cells["LoaiPhong"].Value.ToString();
                textBox2.Text = row.Cells["SoPhongCanThue"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string getmaxstt = "select isnull(Max(Stt),0) from QLKhachSan";
            SqlCommand getmax=new SqlCommand(getmaxstt,sqlconn);
            int stt= Convert.ToInt32(getmax.ExecuteScalar());
            if(stt==0)
            {
                stt = 1;
            }
            else
            {
                stt++;
            }
            string them = "insert into QLKhachSan values(@Stt,@TenKhach,@GioiTinh,@LoaiPhong,@SoPhongCanThue)";
            SqlCommand cmd =new SqlCommand(them,sqlconn);
            cmd.Parameters.AddWithValue("@Stt", stt);
            cmd.Parameters.AddWithValue("@TenKhach", textBox1.Text);
            string sex;
            if (radioButton1.Checked)
            {
                sex = "Nam";
            }
            else
            {
                sex = "Nữ";
            }
            cmd.Parameters.AddWithValue("@GioiTinh", sex);
            cmd.Parameters.AddWithValue("@LoaiPhong", comboBox1.Text);
            cmd.Parameters.AddWithValue("@SoPhongCanThue",textBox2.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Items.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string xoa = "delete from QLKhachSan where TenKhach=@TenKhach";
            SqlCommand cmd =new SqlCommand(xoa,sqlconn);
            cmd.Parameters.AddWithValue("@TenKhach", textBox1.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Items.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sua = "update QLKhachSan set TenKhach=@TenKhach,GioiTinh=@GioiTinh,LoaiPhong=@LoaiPhong,SoPhongCanThue=@SoPhongCanThue where Stt=@Stt";
            SqlCommand cmd =new SqlCommand(sua,sqlconn);
            int stt;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            stt = Convert.ToInt32(row.Cells["Stt"].Value);
            cmd.Parameters.AddWithValue("@Stt", stt);
            cmd.Parameters.AddWithValue("@TenKhach", textBox1.Text);
            string sex;
            if (radioButton1.Checked)
            {
                sex = "Nam";
            }
            else
            {
                sex = "Nữ";
            }
            cmd.Parameters.AddWithValue("@GioiTinh", sex);
            cmd.Parameters.AddWithValue("@LoaiPhong", comboBox1.Text);
            cmd.Parameters.AddWithValue("@SoPhongCanThue", textBox2.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Items.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string name =textBox1.Text;
            string timkiem = "select *from QLKhachSan where TenKhach ='"+name+"'";
            SqlDataAdapter adapter = new SqlDataAdapter(timkiem,sqlconn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }
    }
}
