using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace proje
{
    public partial class YemekForm : Form
    {
        private MainForm anaForm;
        private int sayfa = 0;
        private string restAdi;
        private int urunlerSayfa = 3; // Her sayfada en fazla 9 ürün olacak
        private List<Urun> urunler; // Ürünleri tutacak liste
        private Button oncekiButton;  
        private Button sonrakiButton;

        public YemekForm(string restAdi, MainForm anaForm)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.restAdi = restAdi;
            this.anaForm = anaForm;



            // Restoran adına göre ürünleri tanımla
            if (restAdi == "Graburger")
            {
                urunler = new List<Urun>
                {
                    new Urun("Hamburger", 150, "hamburger.png"),
                    new Urun("Pizza", 200, "pizza.png"),
                    new Urun("Sushi", 170, "sushi.png"),
                    new Urun("Hot-Dog", 50, "hotdog.png"),
                };
            }
            else if (restAdi == "Bursa Sofrası16")
            {
                urunler = new List<Urun>
                {
                    new Urun("Lahmacun", 120, "lahmacun.png"),
                    new Urun("İskender", 250, "iskender.png"),
                    new Urun("Pideli Köfte", 230, "pidelikofte.png"),
                };
            }
            else if (restAdi == "Tatlıcı Egemen Usta")
            {
                urunler = new List<Urun>
                {
                    new Urun("Baklava", 120, "baklava.png"),
                    new Urun("Künefe", 110, "kunefe.png"),
                    new Urun("Fırın Sütlaç", 80, "sutlac.png"),
                };
            }
            else if (restAdi == "Kebapçı Ahmet")
            {
                urunler = new List<Urun>
                {
                    new Urun("Adana Kebap", 220, "adana.png"),
                    new Urun("Çiğköfte Dürüm", 70, "cigkofte.png"),
                    new Urun("Mantı", 140, "manti.png"),
                    new Urun("Izgara Çipura", 170, "izgaracipura.png"),
                };
            }
            else if (restAdi == "Baldwin Döner")
            {
                urunler = new List<Urun>
                {
                    new Urun("Tavuk Döner", 90, "doner.png"),
                    new Urun("Zurna Dürüm", 150, "zurna.png"),
                };
            }

        } 
        private void YemekForm_Load(object sender, EventArgs e)
        {
            this.Text = "TıklAl v1.02 - Yemek Menüsü";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(1300, 650);
            this.BackgroundImage = Image.FromFile("satinalim.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = new Icon("shopicon.ico");
            this.DoubleBuffered = true;

            Label baslikLabel1 = new Label();
            baslikLabel1.Text = restAdi;
            baslikLabel1.BackColor = Color.Turquoise;
            baslikLabel1.BorderStyle = BorderStyle.FixedSingle;
            baslikLabel1.TextAlign = ContentAlignment.MiddleCenter;
            baslikLabel1.Font = new Font("Arial", 14, FontStyle.Bold);
            baslikLabel1.Size = new Size(450, 30);
            baslikLabel1.Location = new Point(425, 20);
            this.Controls.Add(baslikLabel1);

            Label baslikLabel = new Label();
            baslikLabel.Text = "Lütfen almak istediğiniz yemeği seçin";
            baslikLabel.BackColor = Color.Turquoise;
            baslikLabel.BorderStyle = BorderStyle.FixedSingle;
            baslikLabel.TextAlign = ContentAlignment.MiddleCenter;
            baslikLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            baslikLabel.Size = new Size(450, 30);
            baslikLabel.Location = new Point(425, 50);
            this.Controls.Add(baslikLabel);

            DoubleBufferedPanel urunPanel = new DoubleBufferedPanel();
            urunPanel.Size = new Size(1200, 500);
            urunPanel.BackColor = Color.Transparent;
            urunPanel.Location = new Point(50, 100);
            urunPanel.AutoScroll = true;
            this.Controls.Add(urunPanel);

            int baslangicIndex = sayfa * urunlerSayfa;
            int bitisIndex = Math.Min(baslangicIndex + urunlerSayfa, urunler.Count);

            for (int i = baslangicIndex; i < bitisIndex; i++)
            {
                Urun urunBilgi = urunler[i];

                Panel urun = new Panel();
                urun.Size = new Size(275, 300);
                urun.Location = new Point(75 + ((i - baslangicIndex) % 3) * (urun.Width + 100), 100 + ((i - baslangicIndex) / 3) * (urun.Height + 50));
                urun.BackColor = Color.LightGray;
                urunPanel.Controls.Add(urun);

                PictureBox urunResim = new PictureBox();
                urunResim.Image = Image.FromFile(urunBilgi.ResimYolu);
                urunResim.Size = new Size(150, 150);
                urunResim.SizeMode = PictureBoxSizeMode.Zoom;
                urunResim.Location = new Point((urun.Width - urunResim.Width) / 2, 10);
                urun.Controls.Add(urunResim);

                Label urunAdiLabel = new Label();
                urunAdiLabel.Text = urunBilgi.Ad;
                urunAdiLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                urunAdiLabel.TextAlign = ContentAlignment.MiddleCenter;
                urunAdiLabel.AutoSize = false;
                urunAdiLabel.Size = new Size(urun.Width, 25);
                urunAdiLabel.Location = new Point(0, urunResim.Bottom + 10);
                urun.Controls.Add(urunAdiLabel);

                Label urunFiyatLabel = new Label();
                urunFiyatLabel.Text = "Fiyat: " + urunBilgi.Fiyat + " ₺";
                urunFiyatLabel.Font = new Font("Arial", 10);
                urunFiyatLabel.TextAlign = ContentAlignment.MiddleCenter;
                urunFiyatLabel.AutoSize = false;
                urunFiyatLabel.Size = new Size(urun.Width, 20);
                urunFiyatLabel.Location = new Point(0, urunAdiLabel.Bottom + 10);
                urun.Controls.Add(urunFiyatLabel);

                Button satinAlButton = new Button();
                satinAlButton.Text = "Satın Al";
                satinAlButton.BackColor = Color.Turquoise;
                satinAlButton.Size = new Size(100, 30);
                satinAlButton.Tag = urunBilgi;
                satinAlButton.Click += SatinAlButton_Click;
                satinAlButton.Location = new Point((urun.Width - satinAlButton.Width) / 2, urunFiyatLabel.Bottom + 10);
                urun.Controls.Add(satinAlButton);
            }

            oncekiButton = new Button();
            oncekiButton.Text = "Önceki";
            oncekiButton.Size = new Size(100, 30);
            oncekiButton.Location = new Point(500, 610);
            oncekiButton.Click += OncekiButton_Click;
            this.Controls.Add(oncekiButton);

            sonrakiButton = new Button();
            sonrakiButton.Text = "Sonraki";
            sonrakiButton.Size = new Size(100, 30);
            sonrakiButton.Location = new Point(700, 610);
            sonrakiButton.Click += SonrakiButton_Click;
            this.Controls.Add(sonrakiButton);

            if (oncekiButton == null)
            {
                oncekiButton = new Button();
                oncekiButton.Text = "Önceki";
                oncekiButton.Size = new Size(100, 30);
                oncekiButton.Location = new Point(500, 610);
                oncekiButton.Click += OncekiButton_Click;
                this.Controls.Add(oncekiButton);
            }

            if (sonrakiButton == null)
            {
                sonrakiButton = new Button();
                sonrakiButton.Text = "Sonraki";
                sonrakiButton.Size = new Size(100, 30);
                sonrakiButton.Location = new Point(700, 610);
                sonrakiButton.Click += SonrakiButton_Click;
                this.Controls.Add(sonrakiButton);
            }

            UpdateNavigationButtons();
        }

        private void OncekiButton_Click(object sender, EventArgs e)
        {
            sayfa = Math.Max(0, sayfa - 1);
            Yenile();
        }

        private void SonrakiButton_Click(object sender, EventArgs e)
        {
            sayfa = Math.Min((int)Math.Ceiling((double)urunler.Count / urunlerSayfa) - 1, sayfa + 1);
            Yenile();
        }

        private void SatinAlButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Urun urun = button.Tag as Urun;
            SatinAl satinAl = new SatinAl(urun.Ad, urun.Fiyat, urun.ResimYolu,anaForm);
            satinAl.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = satinAl.ShowDialog();
        }

        private void Yenile()
        {
            this.Controls.Clear();
            YemekForm_Load(this, EventArgs.Empty);
        }

        private void UpdateNavigationButtons()
{
        oncekiButton.Enabled = sayfa > 0;  // Enable "Previous" button only if not on the first page.
        sonrakiButton.Enabled = sayfa < (int)Math.Ceiling((double)urunler.Count / urunlerSayfa) - 1;  // Enable "Next" button only if not on the last page.
}
    }

    public class Urun
    {
        public string Ad { get; set; }
        public int Fiyat { get; set; }
        public string ResimYolu { get; set; }

        public Urun(string ad, int fiyat, string resimYolu)
        {
            Ad = ad;
            Fiyat = fiyat;
            ResimYolu = resimYolu;
        }
    }
}
