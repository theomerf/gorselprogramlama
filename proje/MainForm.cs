using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;

namespace proje
{
    public partial class MainForm : Form
    {
        public string kAdi;
        public bool girisy = false;
        
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public class Kullanici
        {
            public string KullaniciAdi { get; set; }
            public string Sifre { get; set; }

            public Kullanici(string kullaniciAdi, string sifre)
            {
                KullaniciAdi = kullaniciAdi;
                Sifre = sifre;
            }
        }



        public void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "TýklAl v1.02";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size(1500, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon("shopicon.ico");
            this.BackgroundImage = Image.FromFile("arkaplan.jpeg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            if (girisy)
            {
                Label kulAdi = new Label();
                kulAdi.Bounds = new Rectangle(1200, 35, 250, 50);
                kulAdi.BorderStyle= BorderStyle.FixedSingle;
                kulAdi.Font = new Font("Ariel", 12, FontStyle.Bold);
                kulAdi.BackColor = Color.White;
                kulAdi.TextAlign = ContentAlignment.MiddleCenter;
                kulAdi.Text = "Kullanýcý Adý:   " + kAdi;
                this.Controls.Add(kulAdi);

            }
            else
            {
                Button giris = new Button();
                giris.Bounds = new Rectangle(1350, 100, 100, 50);
                giris.Text = "Giriþ Yap";
                giris.Font = new Font("Arial", 10, FontStyle.Bold);
                giris.BackColor = Color.White;
                giris.Click += new EventHandler(GirisButton_Click);
                this.Controls.Add(giris);

                Button kayit = new Button();
                kayit.Bounds = new Rectangle(1350, 160, 100, 50);
                kayit.Text = "Kayýt Ol";
                kayit.BackColor = Color.White;
                kayit.Font = new Font("Arial", 10, FontStyle.Bold);
                kayit.Click += new EventHandler(KayitButton_Click);
                this.Controls.Add(kayit);
            }          
            
                // Diðer form öðeleri
                Button yemek = new Button();
                yemek.Bounds = new Rectangle(500, 390, 200, 100);
                yemek.Text = "Yemek Al";
                yemek.Font = new Font("Arial", 15, FontStyle.Bold);
                yemek.BackColor = Color.Turquoise;
                yemek.FlatStyle = FlatStyle.Flat;
                yemek.FlatAppearance.BorderColor = Color.Turquoise;
                yemek.FlatAppearance.BorderSize = 2;

                GraphicsPath dairePath1 = new GraphicsPath();
                dairePath1.AddEllipse(0, 0, yemek.Width, yemek.Height);
                yemek.Region = new Region(dairePath1);
                yemek.Click += new EventHandler(YemekButton_Click);

                this.Controls.Add(yemek);


                Button urun = new Button();
                urun.Bounds = new Rectangle(800, 390, 200, 100);
                urun.Text = "Ürün Al";
                urun.BackColor = Color.Turquoise;
                urun.Font = new Font("Arial", 15, FontStyle.Bold);
                urun.FlatStyle = FlatStyle.Flat;
                urun.FlatAppearance.BorderColor = Color.Turquoise;
                urun.FlatAppearance.BorderSize = 2;

                GraphicsPath dairePath2 = new GraphicsPath();
                dairePath2.AddEllipse(0, 0, urun.Width, urun.Height);
                urun.Region = new Region(dairePath2);
                urun.Click += new EventHandler(UrunButton_Click);

                this.Controls.Add(urun);
            

            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = Image.FromFile("logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Size = new Size(512, 117);
            pictureBox1.BackColor = Color.Transparent;            
            pictureBox1.Location = new Point(510, 100);
            this.Controls.Add(pictureBox1);


            Label label1 = new Label();
            label1.Text = "Ne Sipariþ Vermek Ýstersiniz?";
            label1.BackColor = Color.Transparent;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Arial", 14, FontStyle.Bold);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Bounds = new Rectangle(565, 220, 400, 30);
            this.Controls.Add(label1);

        
        }
        private void EkleButton_Click(object sender, EventArgs e)
        {
            EkleForm ekleForm = new EkleForm(this);
            ekleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = ekleForm.ShowDialog();
        }

        private void UEkleButton_Click(object sender, EventArgs e)
        {
            UEkleForm ekleForm = new UEkleForm(this);
            ekleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = ekleForm.ShowDialog();
        }

        private void GirisButton_Click(object sender, EventArgs e)
        {
            GirisForm girisForm = new GirisForm(this);
            girisForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = girisForm.ShowDialog();
        }

        private void KayitButton_Click(object sender, EventArgs e)
        {
            KayitForm kayitForm = new KayitForm(this);
            kayitForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = kayitForm.ShowDialog();
        }

        public void SetKullaniciAdi(string kullaniciAdi)
        {
            this.kAdi = kullaniciAdi;
            this.girisy = true; // Kullanýcýnýn giriþ yaptýðýný belirtmek için
        }

        private void YemekButton_Click(object sender, EventArgs e)
        {
            if (girisy)
            {
                Marka yemekForm = new Marka("yemek",this);
                yemekForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = yemekForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen önce giriþ yapýn!");
            }

        }

        private void UrunButton_Click(object sender, EventArgs e)
        {
            if (girisy)
            {
                Marka urunForm = new Marka("urun",this);
                urunForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = urunForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen önce giriþ yapýn!");
            }

        }

    }
}
