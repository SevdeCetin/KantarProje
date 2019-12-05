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
    public partial class FmAnasayfa : Form
    {
        public FmAnasayfa()
        {
            InitializeComponent();
        }

        public static FmAnasayfa Current;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private void Form2_Load(object sender, EventArgs e)
        {

            timer2.Start();

            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
        //    cmd = new SqlCommand();
            con.Open();
         
            string kayit = "SELECT Plaka,Sofor,GirisTarihi,CikisTarihi,KayitEden,UrunAgirligi from DbKayit";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, con);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
         
            /*içerdeki araç sayısı*/
            
            cmd = new SqlCommand("SELECT COUNT(KayitId) FROM DbKayit");
            cmd.Connection = con;
            string aracsayisi = cmd.ExecuteScalar().ToString();
            con.Close();
            label1.Text = "İçerdeki araç sayısı:"+aracsayisi;
            con.Close();
            
        }

        private void tarihvesaat()
        {
            Tamzaman.Text = DateTime.Now.ToString(); // tarih ve saat
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FmGiris gecis = new FmGiris();
            gecis.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FmCikis gecis = new FmCikis();
            gecis.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FmRapor gecis = new FmRapor();
            gecis.Show();
            this.Hide();
        }

        private void Tamzaman_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tarihvesaat();
        }
    }
}
