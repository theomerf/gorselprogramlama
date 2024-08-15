using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace proje
{
    public partial class UrunForm : Form
    {
        private MainForm anaForm;
        private string markaAdi;
        private int sayfa = 0;
        private int urunlerSayfa = 3; // Her sayfada en fazla 3 ürün olacak
        private List<DUrun> urunler;
        private Button oncekiButton;  // Declare these buttons at the class level so they can be accessed in multiple methods.
        private Button sonrakiButton;
        public UrunForm(string markaAdi, MainForm form1)
        {
            InitializeComponent();
            this.markaAdi = markaAdi;
            this.Icon = new Icon("shopicon.ico");
            this.DoubleBuffered = true;
            this.anaForm = form1;

            if (markaAdi == "Teknoloji")
            {
                urunler = new List<DUrun>
                {
                new DUrun("İphone 15", 49000, "telefon.png"),
                new DUrun("Samsung UHD TV", 24400, "tv.png"),
                new DUrun("Apple Watch", 10000, "watch.png"),
                new DUrun("Airpods Pro",9350,"airpods.png"),
                new DUrun("JBL T500BT", 1600, "jbl.png"),
                new DUrun("Macbook Pro", 54000, "macbook.png"),
                new DUrun("Monster Oyuncu Bilgisayarı", 32000, "monster.png"),
                new DUrun("Philips Airfryer", 5600, "airfryer.png"),
                new DUrun("Vestel Tost Makinesi", 3000, "tostmakinesi.png"),
                new DUrun("SteelSeries Klavye", 2350, "keyboard.png"),
                new DUrun("Razer Mouse", 1800, "mouse.png"),
                new DUrun("PlayStation 5", 23500, "ps5.png"),
                };
            }

            if (markaAdi == "Kozmetik")
            {
                urunler = new List<DUrun>
                {
                new DUrun("Yves Rocher Krem", 380, "krem.png"),
                new DUrun("Head&&Shoulders Şampuan", 120, "shampoo.png"),
                new DUrun("Axe Black Deodorant ", 150, "deodorant.png"),
                };
            };

            if (markaAdi == "Spor")
            {
                urunler = new List<DUrun>
                {
                new DUrun("Nike Futbol Topu ", 800, "ball.png"),
                new DUrun("Chelsea Forma", 1100, "jersey.png"),
                new DUrun("Tenis Raketi", 1900, "racket.png"),
                new DUrun("Nike Koşu Ayakkabısı", 2750, "sneaker.png"),
                new DUrun("Vmax Protein Tozu", 1500, "toz.png"),
                new DUrun("Dağ Bisikleti", 10000, "bisiklet.png"),
                };
            };

            if (markaAdi == "Giyim")
            {
                urunler = new List<DUrun>
                {
                new DUrun("T-Shirt", 300, "tshirt.png"),
                new DUrun("Şort", 350, "short.png"),
                new DUrun("Gömlek", 600, "gomlek.png"),
                new DUrun("Elbise", 800, "dress.png"),
                new DUrun("Pantolon", 750, "pantolon.png"),
                new DUrun("Kapşonlu Polar Mont", 2200, "mont.png"),
                };
            };

            if (markaAdi == "Mobilya")
            {
                urunler = new List<DUrun>
                {
                new DUrun("L Koltuk", 16000, "koltuk.png"),
                new DUrun("Dolap", 12250, "dolap.png"),
                new DUrun("Çift Kişilik Yatak", 14500, "yatak.png"),
                };
            };

        }

        private void UrunForm_Load(object sender, EventArgs e)
        {
            this.Text = "TıklAl v1.02 - Ürün Menüsü";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(1300, 650);
            this.BackgroundImage = Image.FromFile("userpanelbg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Label baslikLabel1 = new Label();
            baslikLabel1.Text = markaAdi;
            baslikLabel1.BackColor = Color.Turquoise;
            baslikLabel1.BorderStyle = BorderStyle.FixedSingle;
            baslikLabel1.TextAlign = ContentAlignment.MiddleCenter;
            baslikLabel1.Font = new Font("Arial", 14, FontStyle.Bold);
            baslikLabel1.Size = new Size(450, 30);
            baslikLabel1.Location = new Point(425, 20);
            this.Controls.Add(baslikLabel1);

            // Ürün menüsü başlık label'ı
            Label baslikLabel = new Label();
            baslikLabel.Text = "Lütfen almak istediğiniz ürünü seçin";
            baslikLabel.BackColor = Color.Turquoise;
            baslikLabel.BorderStyle = BorderStyle.FixedSingle;
            baslikLabel.TextAlign = ContentAlignment.MiddleCenter;
            baslikLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            baslikLabel.Size = new Size(450, 30);
            baslikLabel.Location = new Point(425, 50);
            this.Controls.Add(baslikLabel);

            // Ürünlerin gösterileceği panel
            DoubleBufferedPanel urunPanel = new DoubleBufferedPanel();
            urunPanel.Size = new Size(1200, 500);
            urunPanel.BackColor = Color.Transparent;
            urunPanel.Location = new Point(50, 100);
            urunPanel.AutoScroll = true; // Kaydırma özelliğini ekledik
            this.Controls.Add(urunPanel);

            // Sayfadaki ürünlerin başlangıç ve bitiş indeksleri
            int baslangicIndex = sayfa * urunlerSayfa;
            int bitisIndex = Math.Min(baslangicIndex + urunlerSayfa, urunler.Count);

            // Sayfadaki her ürün için panel oluştur
            for (int i = baslangicIndex; i < bitisIndex; i++)
            {
                DUrun urunBilgi = urunler[i];

                // Her bir ürün için panel oluştur
                Panel urun = new Panel();
                urun.Size = new Size(275, 300); // Panel boyutunu güncelledik
                urun.Location = new Point(75 + ((i - baslangicIndex) % 3) * (urun.Width + 100), 100 + ((i - baslangicIndex) / 3) * (urun.Height + 50));
                urun.BackColor = Color.LightGray;
                urunPanel.Controls.Add(urun);

                // Ürün fotoğrafı
                PictureBox urunResim = new PictureBox();
                urunResim.Image = Image.FromFile(urunBilgi.ResimYolu);
                urunResim.Size = new Size(150, 150);
                urunResim.SizeMode = PictureBoxSizeMode.Zoom;
                urunResim.Location = new Point((urun.Width - urunResim.Width) / 2, 10);
                urun.Controls.Add(urunResim);

                // Ürün adı label'ı
                Label urunAdiLabel = new Label();
                urunAdiLabel.Text = urunBilgi.Ad;
                urunAdiLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                urunAdiLabel.TextAlign = ContentAlignment.MiddleCenter;
                urunAdiLabel.AutoSize = false;
                urunAdiLabel.Size = new Size(urun.Width, 25); // Sabit genişlik ve yükseklik veriyoruz
                urunAdiLabel.Location = new Point(0, urunResim.Bottom + 10); // Ortalıyoruz
                urun.Controls.Add(urunAdiLabel);

                // Ürün fiyatı label'ı
                Label urunFiyatLabel = new Label();
                urunFiyatLabel.Text = "Fiyat: " + urunBilgi.Fiyat + " ₺";
                urunFiyatLabel.Font = new Font("Arial", 10);
                urunFiyatLabel.AutoSize = true;
                urunFiyatLabel.Location = new Point(100, 225);
                urun.Controls.Add(urunFiyatLabel);

                // Satın al butonu
                Button satinAlButton = new Button();
                satinAlButton.Text = "Satın Al";
                satinAlButton.BackColor = Color.Turquoise;
                satinAlButton.Click += SatinAlButton_Click;                
                satinAlButton.Tag = urunBilgi;
                satinAlButton.Size = new Size(100, 30);
                satinAlButton.Location = new Point(87, 270);
                urun.Controls.Add(satinAlButton);
            }

            // Önceki ve sonraki listeye geçme butonları
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

        private void UpdateNavigationButtons()
        {
            oncekiButton.Enabled = sayfa > 0;  // Enable "Previous" button only if not on the first page.
            sonrakiButton.Enabled = sayfa < (int)Math.Ceiling((double)urunler.Count / urunlerSayfa) - 1;  // Enable "Next" button only if not on the last page.
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
            if (sender is Button button && button.Tag is DUrun urun)
            {
                SatinAl satinAl = new SatinAl(urun.Ad, urun.Fiyat, urun.ResimYolu,anaForm);
                satinAl.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = satinAl.ShowDialog();
            }
        }

        private void Yenile()
        {
            this.Controls.Clear();
            UrunForm_Load(this, EventArgs.Empty);
        }
    }

    public class DUrun
    {
        public string Ad { get; set; }
        public int Fiyat { get; set; }
        public string ResimYolu { get; set; }

        public DUrun(string adi, int fiyat, string resimYolu)
        {
            Ad = adi;
            Fiyat = fiyat;
            ResimYolu = resimYolu;
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }
    }
}
