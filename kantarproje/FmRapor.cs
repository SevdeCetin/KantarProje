using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;


namespace kantarproje
{
    public partial class FmRapor : Form
    {
        public FmRapor()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        System.Data.DataTable tablo;
        void griddoldur()
        {
            /*araç sayısı*/
            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
            da = new SqlDataAdapter("Select KayitId,PLaka,Sofor,GirisTarihi,KayitEden,GirisAgirligi,CikisAgirligi,UrunAgirligi from DbKayit ", con);
            ds = new DataSet(); 

            /*comboboxa plaka getirme*/
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM DbKayit";
            komut.Connection = con;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            /*araç sayısı*/
            con.Open();
            da.Fill(ds, "DbKayit");
            dataGridView1.DataSource = ds.Tables["DbKayit"];
            /*comboboxa plaka getirme*/


            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Plaka"]);
            }

            con.Close();
        }

        private void FmRapor_Load(object sender, EventArgs e)
        {
            timer1.Start();

            griddoldur();
            con = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DbKantar;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("Select *From DbKayit", con);
            tablo = new System.Data.DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            con.Close();

            /*içerdeki araç sayısı*/

            cmd = new SqlCommand("SELECT COUNT(KayitId) FROM DbKayit");
            cmd.Connection = con;
            con.Open();
            string aracsayisi = cmd.ExecuteScalar().ToString();
            con.Close();
            label2.Text = "İçerdeki araç sayısı:" + aracsayisi;
            con.Close();
        }

        private void tarihvesaat()
        {
            label4.Text = DateTime.Now.ToString(); // tarih ve saat
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*con.Open();
            cmd = new SqlCommand("SELECT COUNT(KayitId) FROM DbKayit");
            cmd.Connection = con;
            string aracsayisi = cmd.ExecuteScalar().ToString();
            con.Close();
            
            listBox1.Items.Add("İçerideki Araç Sayısı: "+aracsayisi);*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void button3_Click(object sender, EventArgs e)
        {
            DataView dv = tablo.DefaultView;
            dv.RowFilter = string.Format("GirisTarihi > '{0}' AND GirisTarihi <= '{1}'", dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = dv;
        }

       private void excel_Click(object sender, EventArgs e)
        {
            /*Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
            uyg.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook DbKantar= uyg.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)DbKantar.Sheets[1];
            for (int i = 0; i < tablo.Columns.Count; i++)
            {
                Range myRange = ((Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1]);
                myRange.Value2 = tablo.Columns[i].DataType;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = dataGridView1[i, j].Value;
                }
            }*/
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int StartCol = 1;
            int StartRow = 1;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView1.Columns[j].HeaderText;
            }
            StartRow++;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                    myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    myRange.Select();


                }
            }
        }
            DataSet dsa;
        SqlDataAdapter sda;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select *From DbKayit where Plaka='" + comboBox1.SelectedItem.ToString() + "'", con);
            tablo = new System.Data.DataTable();
            da.Fill(tablo);
            dataGridView2.DataSource = tablo;
            con.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FmAnasayfa gecis = new FmAnasayfa();
            gecis.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FmGiris gecis = new FmGiris();
            gecis.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FmCikis gecis = new FmCikis();
            gecis.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tarihvesaat();
        }
    }
}
