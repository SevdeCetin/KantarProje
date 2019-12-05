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
    public partial class FmGiris : Form
    {
        public FmGiris()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void button4_Click(object sender, EventArgs e)
        {
           DateTime tarih = DateTime.Now;

            try
            {
                con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");

                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into DbKayit(Plaka,Sofor,GirisAgirligi,GirisTarihi,KayitEden) values (@Plaka,@Sofor,@GirisAgirligi,@GirisTarihi,@KayitEden)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@Plaka", textBox1.Text);
                komut.Parameters.AddWithValue("@Sofor", textBox2.Text);
                komut.Parameters.AddWithValue("@GirisAgirligi", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@GirisTarihi",tarih.ToString());
                komut.Parameters.AddWithValue("@KayitEden", FmSifre.Kullanici);


               
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
        string port;
        int receiveddata;
        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Start();

            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                port = System.IO.Ports.SerialPort.GetPortNames()[i];
                // comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            try
            {
                serialPort1.PortName = port;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();
            }
            catch (Exception)
            {

                
            }
        }


        private void tarihvesaat()
        {
            label5.Text = DateTime.Now.ToString(); // tarih ve saat
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort1.Write("1");
                 receiveddata = Convert.ToInt16(serialPort1.ReadLine());
                // receiveddata = ((receiveddata * 5000) / 1023) / 10;
                label4.Text = receiveddata.ToString() + "kg";
                textBox3.Text = receiveddata.ToString();
                System.Threading.Thread.Sleep(100);

            }
            catch (Exception ex) { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }
            catch
            {
                MessageBox.Show("Seri Port Kapalı !!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FmCikis gecis = new FmCikis();
            gecis.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FmAnasayfa gecis = new FmAnasayfa();
            gecis.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FmRapor gecis = new FmRapor();
            gecis.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tarihvesaat();
        }
    }
}
