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
    public partial class QuanLyBanHoaQua : Form
    {
        SqlConnection sqlconn =new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public QuanLyBanHoaQua()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            sqlconn.Open();
            string knoi = "select *from QLBanHoaQua";
            SqlDataAdapter adapter = new SqlDataAdapter(knoi,sqlconn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            textBox2.Text=Tongtien().ToString();
        }
        private int Tongtien()
        {
            sqlconn.Open();
            string sumthanhtien = "select isnull(Sum(ThanhTien),0) from QLBanHoaQua";
            SqlCommand cmd = new SqlCommand(sumthanhtien,sqlconn);
            int tongtien= Convert.ToInt32(cmd.ExecuteScalar());
            sqlconn.Close();
            return tongtien;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Chuối":
                    {
                        textBox1.Text = "1000";
                        break;
                    }
                case "Táo":
                    {
                        textBox1.Text = "2000";
                        break;
                    }
                case "Nho":
                    {
                        textBox1.Text = "3000";
                        break;
                    }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string getstt = "select isnull(max(STT),0) from QLBanHoaQua";
            SqlCommand stt = new SqlCommand(getstt,sqlconn);
            int STT =Convert.ToInt32( stt.ExecuteScalar());
            string them = "insert into QLBanHoaQua values(@STT,@TenSanPham,@DonGia,@SoLuong,@ThanhTien)";
            SqlCommand cmd =new SqlCommand(them,sqlconn);
            if (STT ==0)
            {
                STT = 1;
            }
            else
            {
                STT++;
            }
            cmd.Parameters.AddWithValue("@STT", STT);
            
            cmd.Parameters.AddWithValue("@TenSanPham", comboBox1.Text);
            cmd.Parameters.AddWithValue("@DonGia", textBox1.Text);
            cmd.Parameters.AddWithValue("@SoLuong", numericUpDown1.Value);
            int dongia =Convert.ToInt32(textBox1.Text);
            int soluong =Convert.ToInt32(numericUpDown1.Value);
            int thanhtien = dongia * soluong;
            cmd.Parameters.AddWithValue("@ThanhTien", thanhtien);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            
            LoadData();
            
            
            
            comboBox1.Text = "";
            textBox1.Clear();
            numericUpDown1.Value= 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                comboBox1.Text = row.Cells["TenSanPham"].Value.ToString();
                textBox1.Text = row.Cells["DonGia"].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32( row.Cells["SoLuong"].Value);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string xoa = "delete from QLBanHoaQua where STT=@STT";
            SqlCommand  cmd= new SqlCommand(xoa,sqlconn);
            int stt;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            stt = Convert.ToInt32(row.Cells["STT"].Value) ;
            cmd.Parameters.AddWithValue("@STT", stt);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            comboBox1.Text = "";
            textBox1.Clear();
            numericUpDown1.Value = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //su dung tryparse đe khong cho nguoi dung nhap chu
            int tienvao;
            if (int.TryParse(textBox3.Text, out tienvao))
            {
                textBox4.Text = (tienvao - Tongtien()).ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int tienvao= Convert.ToInt32(textBox3.Text);
            if (tienvao< Tongtien())
            {
                MessageBox.Show("Số tiền không đủ");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string clear ="delete from QLBanHoaQua";
            SqlCommand cmd = new SqlCommand(clear,sqlconn);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            
            
        }
    }
}
