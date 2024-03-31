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
    public partial class QuanLySinhVien : Form
    {
        SqlConnection sqlconn=new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public QuanLySinhVien()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            sqlconn.Open();
            string ketnoi = "select *from QLSinhVien";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(ketnoi,sqlconn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaSV"].Value.ToString();
                textBox2.Text = row.Cells["HoTen"].Value.ToString();
                dateTimePicker1.Text = row.Cells["NgaySinh"].Value.ToString();
                if (row.Cells["GioiTinh"].Value.ToString()=="Nam")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                    radioButton1.Checked=false;
                }
                comboBox1.Text = row.Cells["NoiSinh"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string them = "insert into QLSinhVien values(@MaSV,@HoTen,@NgaySinh,@GioiTinh,@NoiSinh)";
            SqlCommand cmd=new SqlCommand(them,sqlconn);
            cmd.Parameters.AddWithValue("@MaSV", textBox1.Text);
            cmd.Parameters.AddWithValue("@HoTen", textBox2.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", dateTimePicker1.Value);
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
            cmd.Parameters.AddWithValue("@NoiSinh", comboBox1.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            radioButton1.Checked=false ;
            radioButton2.Checked=false ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sua = "update QLSinhVien set HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,NoiSinh=@NoiSinh where MaSV=@MaSV";
            SqlCommand cmd=new SqlCommand(sua,sqlconn);
            cmd.Parameters.AddWithValue("@MaSV", textBox1.Text);
            cmd.Parameters.AddWithValue("@HoTen", textBox2.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", dateTimePicker1.Value);
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
            cmd.Parameters.AddWithValue("@NoiSinh", comboBox1.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string xoa = "delete from QLSinhVien where MaSV=@MaSV";
            SqlCommand cmd =new SqlCommand(xoa,sqlconn);
            cmd.Parameters.AddWithValue("@MaSv", textBox1.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TimKiemSinhVien search =new TimKiemSinhVien();
            search.Show();
        }
    }
}
