using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class QuanLyQuanNet : Form
    {
        public QuanLyQuanNet()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //bao loi chi nhap so
            TextBox tb = (TextBox)sender;
            string text = tb.Text;
            foreach(char c in text)
            {
                if (!char.IsDigit(c))
                {
                    MessageBox.Show("Vui lòng chỉ nhập số");
                    tb.Text = "";
                    return;
                }
            }
        }
        private int TongTien(TextBox tb1,TextBox tb2)
        {
            int giochoi= Convert.ToInt32(tb2.Text) - Convert.ToInt32(tb1.Text);
            return giochoi * 5000;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            if (btn.BackColor == Color.Red)
            {
                MessageBox.Show("Máy này đang được dùng vui lòng chọn máy khác.");
            }
            else
            {
                btn.BackColor = Color.Red;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string so = txt.Name;
            int sotxt=0;
            foreach(char c in so)
            {
                if (char.IsDigit(c))
                {
                    sotxt=Convert.ToInt32(c);
                }
            }
            string name1 = "texBox"+(sotxt - 2).ToString();
            string name2 = "texBox"+(sotxt - 1).ToString();
            TextBox txt1=new TextBox();
            TextBox txt2=new TextBox();
            txt1.Name = name1;
            txt2.Name = name2;
            txt.Text = TongTien(txt1,txt2).ToString();
            
        }
    }
}
