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
    public partial class FmCikis : Form
    {
        public FmCikis()
        {
            InitializeComponent();
        }

       

        SqlConnection con;
        SqlCommand cmd;


        string port;
        int receiveddata1;
        private void FmCikis_Load(object sender, EventArgs e)
        {

            timer1.Start();

            kisiGetir();
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

            //----------------------//


            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
            //    cmd = new SqlCommand();
            con.Open();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort1.Write("1");
                receiveddata1 = Convert.ToInt16(serialPort1.ReadLine());
                // receiveddata = ((receiveddata * 5000) / 1023) / 10;
                label6.Text = receiveddata1.ToString() + "kg";
                textBox4.Text = receiveddata1.ToString();
                
                System.Threading.Thread.Sleep(100);
                int urun = receiveddata1 - (Convert.ToInt32(textBox3.Text));
                textBox5.Text = urun.ToString();

            }
            catch (Exception ex)
            {

            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            FmAnasayfa gecis = new FmAnasayfa();
            gecis.Show();
            this.Hide();
        }
        void kisiGetir()
        {
            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
            //    cmd = new SqlCommand();
            con.Open();

            string kayit = "SELECT KayitId,Plaka,Sofor,GirisTarihi,CikisTarihi,GirisAgirligi,KayitEden from DbKayit where UrunAgirligi is null  ";
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
            con.Close();

        }

        

           

       
        private void button4_Click(object sender, EventArgs e)
        {
            DateTime tarih = DateTime.Now;
            try
            {
                // Bağlantı açıldığında çalışacak sql sorgusu için cmd nesnesi oluşturulur:     
            //    SqlCommand cmd = new SqlCommand("UPDATE kisiler SET Plaka=@Plaka,Sofor=@Sofor,GirisAgirligi=@girisAgirligi,CikisAgirligi=@cikisAgirligi,UrunAgirligi=@urunAgirligi WHERE KayitId=@id ", con);
                SqlCommand cmd = new SqlCommand("UPDATE DbKayit SET CikisAgirligi=@CikisAgirligi,CikisTarihi=@CikisTarihi,UrunAgirligi=@UrunAgirligi WHERE KayitId=@id ", con);

                // Fare ile seçilmiş satırın değeri @id'ye aktarılır:      
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
                // TextBox'lardan alınan bilgiler etiketlere, oradan da sorguya gönderilir:     
                // cmd.Parameters.AddWithValue("@Plaka", textBox1.Text);
                // cmd.Parameters.AddWithValue("@Sofor", textBox2.Text);
                //    cmd.Parameters.AddWithValue("@GirisAgirligi", textBox3.Text);
                cmd.Parameters.AddWithValue("@CikisTarihi", tarih.ToString());
                cmd.Parameters.AddWithValue("@CikisAgirligi", Convert.ToInt32(textBox4.Text));
                cmd.Parameters.AddWithValue("@UrunAgirligi",Convert.ToInt32(textBox5.Text));
                // Bağlantı kapalı ise açılır:  
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // Sorgu çalıştırılır:   
                cmd.ExecuteNonQuery();
                // Bağlantı kapatılır:   
                con.Close();
                // kisiGetir fonksiyonu ile tablonun son hali getirilir:  
               // kisiGetir();
                // Güncellendi mesajı gösterilir:  
                MessageBox.Show("Güncellendi.");
                kisiGetir();
            }
            // Bir yerde hata varsa catch ile yakalanır ve mesaj verilir:      
            catch (SqlException)
            {
                MessageBox.Show("Hata olustu!");
            } 
           

         }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox1.Text= dataGridView1.CurrentRow.Cells["Plaka"].Value.ToString();
           textBox2.Text = dataGridView1.CurrentRow.Cells["Sofor"].Value.ToString();
           textBox3.Text = dataGridView1.CurrentRow.Cells["GirisAgirligi"].Value.ToString();

           


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FmGiris gecis = new FmGiris();
            gecis.Show();
            this.Hide();
        }
        private void tarihvesaat()
        {
            label7.Text = DateTime.Now.ToString(); // tarih ve saat
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FmRapor gecis = new FmRapor();
            gecis.Show();
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            tarihvesaat();
        }
    }
}
