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
    public partial class QuanLyBanHang : Form
    {
        SqlConnection sqlconn=new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public QuanLyBanHang()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            sqlconn.Open();
            string knoi = "select *from QLBanHang";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(knoi,sqlconn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                comboBox1.Text = row.Cells["TenHang"].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32(row.Cells["SoLuong"].Value.ToString());
                textBox2.Text = row.Cells["DonGia"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string xoadata = "delete from QLBanHang";
            SqlCommand cmd=new SqlCommand(xoadata,sqlconn);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Bút":
                    {
                        textBox2.Text = "1000";
                        break;
                    }
                case "Sách":
                    {
                        textBox2.Text = "2000";
                        break;
                    }
                case "Vở":
                    {
                        textBox2.Text = "3000";
                        break;
                    }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string getstt = "select isnull(max(STT),0) from QLBanHang";
            SqlCommand cmdmaxstt = new SqlCommand(getstt,sqlconn);
            int maxstt= Convert.ToInt32(cmdmaxstt.ExecuteScalar());
            if (maxstt == 0)
            {
                maxstt = 1;
            }
            else
            {
                maxstt++;
            }
            string them = "insert into QLBanHang values(@STT,@TenHang,@SoLuong,@DonGia,@ThanhTien)";
            SqlCommand cmd =new SqlCommand(them,sqlconn);
            cmd.Parameters.AddWithValue("@STT",maxstt);
            cmd.Parameters.AddWithValue("@TenHang",comboBox1.Text);
            cmd.Parameters.AddWithValue("@SoLuong", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@DonGia", Convert.ToInt32(textBox2.Text));
            int thanhtien= Convert.ToInt32(numericUpDown1.Value) * Convert.ToInt32(textBox2.Text);
            cmd.Parameters.AddWithValue("@ThanhTien",thanhtien);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            comboBox1.Text = "";
            numericUpDown1.Value = 0;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            sqlconn.Open();
            string xoa = "delete from QLBanHang where STT=@STT";
            SqlCommand cmd =new SqlCommand(xoa,sqlconn);
            int stt;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            stt = Convert.ToInt32(row.Cells["STT"].Value);
            
            cmd.Parameters.AddWithValue("@STT", stt);
            cmd.ExecuteNonQuery ();
            sqlconn.Close();
            LoadData();
            comboBox1.Text = "";
            numericUpDown1.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlconn.Open ();
            string sum = "Select isnull(sum(ThanhTien),0 )as tongtien from QLBanHang";
            SqlCommand cmd=new SqlCommand(sum,sqlconn);
            int tongtien= Convert.ToInt32(cmd.ExecuteScalar());
            textBox3.Text=tongtien.ToString();
            cmd.ExecuteNonQuery () ;
            sqlconn.Close();
            

        }
    }
}
