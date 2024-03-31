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
    public partial class TimKiemSinhVien : Form
    {
        SqlConnection sqlconn = new SqlConnection(@"Data Source=BACH\SQLEXPRESS;Initial Catalog=winform;Integrated Security=True");
        public TimKiemSinhVien()
        {
            InitializeComponent();
            
        }

        private void LoadData(string name,string values)
        {
            sqlconn.Open();
            string show = "select *from QLSinhVien where "+name+"="+"'"+values+"'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(show,sqlconn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();

        }
        string sex;
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1 != null)
            {
                LoadData("MaSV", textBox1.Text);
            }
            else if (textBox2 != null)
            {
                LoadData("HoTen", textBox2.Text);
            }
            else
            {
                if (radioButton1.Checked)
                {
                    sex = "Nam";

                }
                else { sex = "Nữ"; }
                LoadData("GioiTinh", sex);
            }
        }
    }
}
