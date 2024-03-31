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
    public partial class QuanLyTruyenTranh : Form
    {
        SqlConnection sqlconn=new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public QuanLyTruyenTranh()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            sqlconn.Open();
            string knoi = "Select *from QLTruyenTranh";
            SqlDataAdapter adapter = new SqlDataAdapter(knoi,sqlconn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string sdt = textBox2.Text;
            foreach(char c in sdt)
            {
                if (!char.IsDigit(c))
                {
                    errorProvider1.SetError(textBox2, "Chi duoc nhap so");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text) 
            {
                case "Conan":
                    {
                        textBox3.Text = "1000";
                        break;
                    }
                case "Dragonball":
                    {
                        textBox3.Text = "2000";
                        break;
                    }
                case "Doraemon":
                    {
                        textBox3.Text = "3000";
                        break;
                    }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["TenKhach"].Value.ToString();
                textBox2.Text = row.Cells["SDT"].Value.ToString();
                comboBox1.Text = row.Cells["TenTruyen"].Value.ToString();
                dateTimePicker1.Text = row.Cells["NgayMuon"].Value.ToString();
                dateTimePicker2.Text = row.Cells["NgayTra"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string getstt = "select isnull(max(STT),1) from QLTruyenTranh";
            SqlCommand cmdSTT= new SqlCommand(getstt,sqlconn);
            int stt=Convert.ToInt32(cmdSTT.ExecuteScalar());
            string muon = "insert into QLTruyenTranh values(@STT,@TenKhach,@SDT,@TenTruyen,@NgayMuon,@NgayTra,@ThanhTien,@Ghichu)";
            SqlCommand cmd=new SqlCommand(muon,sqlconn);
            cmd.Parameters.AddWithValue("@STT", stt);
            cmd.Parameters.AddWithValue("@TenKhach",textBox1.Text);
            cmd.Parameters.AddWithValue("@SDT", textBox2.Text);
            cmd.Parameters.AddWithValue("@TenTruyen", comboBox1.Text);
            cmd.Parameters.AddWithValue("@NgayMuon", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@NgayTra", DBNull.Value);
            cmd.Parameters.AddWithValue("@ThanhTien", DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", "Chưa trả");
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string tra = "update QLTruyenTranh set NgayTra=@NgayTra,ThanhTien=@ThanhTien,GhiChu=@GhiChu where TenKhach=@TenKhach";
            SqlCommand cmd=new SqlCommand(tra,sqlconn);
            
            cmd.Parameters.AddWithValue("@NgayTra", dateTimePicker2.Value);
            TimeSpan ngay = (dateTimePicker2.Value-dateTimePicker1.Value);
            int songay = ngay.Days;
            int thanhtien =  songay * (Convert.ToInt32(textBox3.Text));
            cmd.Parameters.AddWithValue("@ThanhTien", thanhtien);
            cmd.Parameters.AddWithValue("@GhiChu", "Da tra");
            cmd.Parameters.AddWithValue("@TenKhach", textBox1.Text);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
        }
    }
}
