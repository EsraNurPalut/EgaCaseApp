using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CaseEga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static SqlConnection baglanti = new SqlConnection();
        static SqlCommand gonder = new SqlCommand();

        public static void Kaydet(Musteri musteris)
        {
            baglanti.ConnectionString = @"data source=LAPTOP-1M6OMN39; initial catalog=EgaCase; integrated security=true;";
            gonder.Connection = baglanti;
            gonder.CommandText = "INSERT INTO Musteriler (Ad,Soyad,BabaAdi,Tutar,TCKN,SGKKOD) VALUES ('" + musteris.Ad + "','" + musteris.Soyad + "','" + musteris.BabaAdi + "','" + musteris.Tutar + "','" + musteris.TCKN + "','" + musteris.SGKKOD + "')";
            baglanti.Open();
            gonder.ExecuteNonQuery();
            baglanti.Close();
        }

        //veritabanı kaydet butonu
        private void button1_Click(object sender, EventArgs e)
        { 
            string[] xmlNames = Directory.GetFiles(@"C:\Users\nures\Desktop\EGA\", "*.xml");
            string xmlName = "";
            foreach (var item in xmlNames)
            {
                if (item.Contains("test"))
                {
                    xmlName = item;
                }
            }
            FileStream file = System.IO.File.Open(xmlName, FileMode.Open, FileAccess.ReadWrite);
            XmlDocument doc = new XmlDocument();
            doc.Load(file); 
            XmlElement tempElement;
            XmlElement xmlElement = doc.DocumentElement;  
            XmlElement SGKKOD, TCKN, Soyad, Ad, BabaAdi, Tutar;
            for (int i = 0; i < xmlElement.GetElementsByTagName("SGKIstek").Count; i++)
            {
                tempElement = (XmlElement)xmlElement.GetElementsByTagName("SGKIstek").Item(i);
                SGKKOD = (XmlElement)tempElement.GetElementsByTagName("SGKKOD").Item(0);
                TCKN = (XmlElement)tempElement.GetElementsByTagName("TCKN").Item(0);
                Soyad = (XmlElement)tempElement.GetElementsByTagName("Soyad").Item(0);
                Ad = (XmlElement)tempElement.GetElementsByTagName("Ad").Item(0);
                BabaAdi = (XmlElement)tempElement.GetElementsByTagName("BabaAdi").Item(0);
                Tutar = (XmlElement)tempElement.GetElementsByTagName("Tutar").Item(0);

                Musteri musteri = new Musteri();
                musteri.SGKKOD = SGKKOD.InnerXml.ToString();
                musteri.TCKN = TCKN.InnerXml.ToString();
                musteri.Soyad = Soyad.InnerXml.ToString();
                musteri.Ad = Ad.InnerXml.ToString();
                musteri.BabaAdi = BabaAdi.InnerXml.ToString();
                musteri.Tutar = Tutar.InnerXml.ToString();
                Kaydet(musteri);
            }
            groupBox1.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
