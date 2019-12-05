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

namespace kantarproje
{
    public partial class FmSifre : Form
    {
        public FmSifre()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public static string Kullanici;
        private void button1_Click(object sender, EventArgs e)
        {
            
            string user = textBox1.Text;
            string pass = textBox2.Text;
            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TbLogin where Kullanici='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               /* MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.");*/
                Kullanici = textBox1.Text;
                FmAnasayfa gecis = new FmAnasayfa();
                gecis.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            con.Close();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
        private void tarihvesaat()
        {
            label3.Text = DateTime.Now.ToString(); // tarih ve saat
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tarihvesaat();

        }
    }
}
