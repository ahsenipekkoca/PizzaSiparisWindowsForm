using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeAdamSinav
{
    //temizle deyince combobox içleri aynen kalıyor onu 0lamak lazım!! 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbPizzaBoyu.Items.Add("Küçük"); //her zaman bu değerlere ulaşabilmek için buraya ekledim.
            cmbPizzaBoyu.Items.Add("Orta");
            cmbPizzaBoyu.Items.Add("Büyük");
            cmbIcecek.Items.Add("Coca Cola");
            cmbIcecek.Items.Add("Fanta");
            cmbIcecek.Items.Add("Sprite");
            

        }
            
        private void Form1_Load(object sender, EventArgs e)
        {

            


            btnSiparisAl.Enabled = true; // form açıldığında sipariş al butonu aktif
            btnSiparisleriTemizle.Enabled = false; // form açıldığında sipariş temizle butonu pasif
            btnTemizle.Enabled = false; // form açıldığında temizle butonu pasif

        }

        private void btnSiparisAl_Click(object sender, EventArgs e)
        {
            long telefonNo = 0;
            telefonNo = txtTelefon.TextLength;
            btnSiparisAl.Enabled = false; // sipariş al'a tıkladığımızda sipariş al butonu pasif
            btnSiparisleriTemizle.Enabled = true; // sipariş al'a tıkladığımızda sipariş temizle butonu aktif
            btnTemizle.Enabled = true; // sipariş al'a tıkladığımızda temizle butonu aktif

            Musteri m = new Musteri(); // yeni müşteri oluşturuyoruz.
            m.AdSoyad = txtAdSoyad.Text;
            m.TelefonNumarasi = txtTelefon.Text;
            m.Adres = txtAdres.Text; // müşteri bilgileri aldık.

            if(m.AdSoyad=="" || m.Adres=="" || m.TelefonNumarasi=="") //boş olmama kontrollerini sağlıyoruz bu döngüde.
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız...");
                TemizleButonu();
            }
            else if(cmbPizzaBoyu.Text=="" || cmbIcecek.Text == "") 
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız...");
                TemizleButonu();
            }
            else if ( nudPizzaAdet.Value<=0) //pizza adedi 0 veya daha az olamaz
            {
                MessageBox.Show("Pizza Adedi 0 olamaz!!!");
                TemizleButonu();
            }
            else if(telefonNo!=11 ) // telefon numarası 11 haneli olsun istedim 
            {
                MessageBox.Show("Lütfen Telefon numarasını doğru giriniz...!");
            }
            else
            {

                lbxAdSoyad.Items.Add(m.AdSoyad);
                lbxAdres.Items.Add(m.Adres);
                if (telefonNo is long)
                {
                    lbxTelefon.Items.Add(m.TelefonNumarasi);
                }
                else
                    MessageBox.Show("Telefon numarası giriniz.");
                lbxPizzaBoyAdet.Items.Add(cmbPizzaBoyu.Text + " boy pizza" + nudPizzaAdet.Value + " adet");
                lbxIcecekAdet.Items.Add(cmbIcecek.Text + nudIcecekAdet.Value + " adet");
                Ucretlendirme();
                Malzeme();
            }
            
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            TemizleButonu();
            btnSiparisAl.Enabled = true; //temizle butonuna basınca sipariş al aktif
            btnSiparisleriTemizle.Enabled = true; //temizle butonuna basınca siparişleri temizle aktif
            btnTemizle.Enabled = false; //temizle butonuna basınca temizle aktif
        }

        private void btnSiparisleriTemizle_Click(object sender, EventArgs e)
        {
            SiparisiTemizleButonu(); 
            btnSiparisAl.Enabled = true; // sipariş temizle butonuna basınca sipariş al aktif
            btnSiparisleriTemizle.Enabled = false; // sipariş temizle butonuna basınca sipariş temizle pasif
            btnTemizle.Enabled = false; //sipariş temizle butonuna basınca temizle pasif
        }
      
        public void Ucretlendirme()
        {
            int toplam = 0;
            
                switch (cmbPizzaBoyu.Text)
                {
                    case "Küçük":
                        toplam += Convert.ToInt32(10 * nudPizzaAdet.Value);
                        break;
                    case "Orta":
                        toplam += Convert.ToInt32(15 * nudPizzaAdet.Value);
                        break;
                    case "Büyük":
                        toplam += Convert.ToInt32(20 * nudPizzaAdet.Value);
                        break;
                    default:
                        MessageBox.Show("Lütfen pizza boyu seçiniz...");
                        TemizleButonu();

                        break;

                }
                switch (cmbIcecek.Text)
                {
                    case "Coca Cola":
                        toplam += Convert.ToInt32(5 * nudIcecekAdet.Value);
                        break;
                    case "Fanta":
                        toplam += Convert.ToInt32(3 * nudIcecekAdet.Value);
                        break;
                    case "Sprite":
                        toplam += Convert.ToInt32(3 * nudIcecekAdet.Value);
                        break;
                    default:
                        MessageBox.Show("Lütfen içecek seçiniz...");
                        TemizleButonu();

                        break;
                }

            
            
            lbxUcret.Items.Add(toplam+" TL ");
        }
        public void Malzeme() //burada malzeme kontrolü yapıyoruz.
        {
            string malzemeler = "";
           
            if (chkKasar.Checked)
                malzemeler += "Kaşar";
            if (chkMantar.Checked)
                malzemeler += " - " + "Mantar";
            if (chkMisir.Checked)
                malzemeler += " - " + "Mısır";
            if (chkSebze.Checked)
                malzemeler += " - " + "Sebze";
            if (chkSogan.Checked)
                malzemeler += " - " + "Soğan";
            if (chkSosis.Checked)
                malzemeler += " - " + "Sosis";
            if (chkSucuk.Checked)
                malzemeler += " - " + "Sucuk";
            if (chkZeytin.Checked)
                malzemeler += " - " + "Zeytin";

            if (malzemeler=="") //malzemelere değer girilmemişse sipariş alamasın. Çünkü pizza için malzeme gerek :) 
            {
                MessageBox.Show("Lütfen en az bir malzeme giriniz...");
                btnSiparisAl.Enabled = true;
                TemizleButonu();

            }
            else
            lbxMalzemeler.Items.Add(malzemeler);
        }   
          public void TemizleButonu() //Malzemeler groupboxundaki elemanları temizleme.
        {
            txtAdres.Clear();
            txtAdSoyad.Clear();
            txtTelefon.Clear();
           
            nudIcecekAdet.Value = 0;
            nudPizzaAdet.Value = 0;

            cmbPizzaBoyu.Text = null;     
            cmbIcecek.Text =null;               
       
            chkKasar.Checked = false;
            chkMantar.Checked = false;
            chkMisir.Checked = false;
            chkSebze.Checked = false;
            chkSogan.Checked = false;
            chkSosis.Checked = false;
            chkSucuk.Checked = false;
            chkZeytin.Checked = false;
        }
          public void SiparisiTemizleButonu() // Listboxu temizleme.
        {
            lbxAdSoyad.Items.Clear();
            lbxTelefon.Items.Clear();
            lbxAdres.Items.Clear();
            lbxIcecekAdet.Items.Clear();
            lbxPizzaBoyAdet.Items.Clear();
            lbxMalzemeler.Items.Clear();
            lbxUcret.Items.Clear();
        }
    }
    
}

