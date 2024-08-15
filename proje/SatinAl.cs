using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace proje
{
    public partial class SatinAl : Form
    {
        public MainForm anaForm;
        private string urunAdi;
        private int urunFiyat;
        private string urunResimYolu;
        private int urunAdeti = 1;
        private Label miktarLabel;
        private RichTextBox adresTextBox;
        private Label urunFiyatLabel; // Ürün fiyat label'ını sınıf seviyesine taşıdık
        string connectionString = "server=34.38.166.127;port=3306;database=mydb;user=user;password=user123;";

        public SatinAl(string urunAdi, int urunFiyat, string urunResimYolu, MainForm form1)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.urunAdi = urunAdi;
            this.urunFiyat = urunFiyat;
            this.urunResimYolu = urunResimYolu;
            anaForm = form1;
        }

        private void SatinAl_Load(object sender, EventArgs e)
        {
            this.Text = "Satın Al";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(800, 600);
            this.BackgroundImage = Image.FromFile("satinalim.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = new Icon("shopicon.ico");

            // "Satın Al" başlığı
            Label baslikLabel = new Label();
            baslikLabel.Text = $"{urunAdi} Satın Al";
            baslikLabel.Font = new Font("Arial", 15, FontStyle.Bold);
            baslikLabel.Size = new Size(300, 50);
            baslikLabel.BackColor = Color.Transparent;
            baslikLabel.Location = new Point(300, 50);
            baslikLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(baslikLabel);

            // Ürün fotoğrafı
            PictureBox urunResim = new PictureBox();
            urunResim.Image = Image.FromFile(urunResimYolu);
            urunResim.Size = new Size(150, 150);
            urunResim.BackColor = Color.Transparent;
            urunResim.SizeMode = PictureBoxSizeMode.Zoom;
            urunResim.Location = new Point(50, 50);
            this.Controls.Add(urunResim);

            // Ürün fiyatı
            urunFiyatLabel = new Label();
            urunFiyatLabel.Text = "Fiyat: " + urunFiyat + " ₺";
            urunFiyatLabel.Font = new Font("Arial", 14);
            urunFiyatLabel.BackColor = Color.Transparent;
            urunFiyatLabel.Size = new Size(200, 50);
            urunFiyatLabel.Location = new Point(350, 150);
            urunFiyatLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(urunFiyatLabel);

            // Ürün adeti artırma ve azaltma
            Button azaltButton = new Button();
            azaltButton.Text = "-";
            azaltButton.Font = new Font("Arial", 16, FontStyle.Bold);
            azaltButton.Size = new Size(50, 50);
            azaltButton.Location = new Point(300, 250);
            azaltButton.Click += AzaltButton_Click;
            this.Controls.Add(azaltButton);

            miktarLabel = new Label();
            miktarLabel.Text = urunAdeti.ToString();
            miktarLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            miktarLabel.Size = new Size(50, 50);
            miktarLabel.Location = new Point(400, 250);
            miktarLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(miktarLabel);

            Button artirButton = new Button();
            artirButton.Text = "+";
            artirButton.Font = new Font("Arial", 16, FontStyle.Bold);
            artirButton.Size = new Size(50, 50);
            artirButton.Location = new Point(500, 250);
            artirButton.Click += ArtirButton_Click;
            this.Controls.Add(artirButton);

            // Adres girişi
            Label adresLabel = new Label();
            adresLabel.Text = "Adres:";
            adresLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            adresLabel.Size = new Size(80, 30);
            adresLabel.BackColor = Color.Transparent;
            adresLabel.Location = new Point(200, 350);
            adresLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(adresLabel);

            adresTextBox = new RichTextBox();
            adresTextBox.Multiline = true;
            adresTextBox.Size = new Size(400, 100);
            adresTextBox.Location = new Point(300, 350);
            adresTextBox.BackColor = Color.LightGray;
            adresTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(adresTextBox);

            // Siparişi Tamamla butonu
            Button siparisTamamlaButton = new Button();
            siparisTamamlaButton.Text = "Siparişi Tamamla";
            siparisTamamlaButton.Font = new Font("Arial", 14, FontStyle.Bold);
            siparisTamamlaButton.Size = new Size(300, 50);
            siparisTamamlaButton.Location = new Point(350, 500);
            siparisTamamlaButton.BackColor = Color.Turquoise;    
            siparisTamamlaButton.FlatStyle = FlatStyle.Flat;
            siparisTamamlaButton.FlatAppearance.BorderColor = Color.Turquoise;
            siparisTamamlaButton.FlatAppearance.BorderSize = 2;
            siparisTamamlaButton.Click += SiparisTamamlaButton_Click;
            this.Controls.Add(siparisTamamlaButton);
        }

        private void AzaltButton_Click(object sender, EventArgs e)
        {
            if (urunAdeti > 1)
            {
                urunAdeti--;
                miktarLabel.Text = urunAdeti.ToString();
                UpdateFiyatLabel();
            }
        }

        private void ArtirButton_Click(object sender, EventArgs e)
        {
            urunAdeti++;
            miktarLabel.Text = urunAdeti.ToString();
            UpdateFiyatLabel();
        }

        private void UpdateFiyatLabel()
        {
            urunFiyatLabel.Text = "Fiyat: " + (urunFiyat * urunAdeti) + " ₺";
        }

        private void SiparisTamamlaButton_Click(object sender, EventArgs e)
        {
            string adres = adresTextBox.Text;
            if (string.IsNullOrEmpty(adres))
            {
                MessageBox.Show("Lütfen bir adres girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConnectAndQuery(urunAdi, urunAdeti, urunFiyat * urunAdeti, adres);
            MessageBox.Show($"Siparişiniz başarıyla oluşturuldu!\n\nÜrün: {urunAdi}\nAdet: {urunAdeti}\nFiyat: {urunFiyat * urunAdeti} ₺\nAdres: {adres}", "Sipariş Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        public void ConnectAndQuery(string urunAdi, int urunAdeti, int tutar, string adres)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    int musteriId = GetMusteriId();
                    DateTime dateTime = DateTime.Now;
                    // Bağlantıyı aç
                    conn.Open();

                    // Sorgu oluştur
                    string query = "insert into satinalim(musteri_id,urun_ad,miktar,tutar,adres,tarih) values(@musteriId,@urunAdi,@urunAdeti,@tutar,@adres,@dateTime);";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@musteriId", musteriId);
                    cmd.Parameters.AddWithValue("@urunAdi", urunAdi );
                    cmd.Parameters.AddWithValue("@urunAdeti", urunAdeti);
                    cmd.Parameters.AddWithValue("@tutar", tutar);
                    cmd.Parameters.AddWithValue("@adres", adres);
                    cmd.Parameters.AddWithValue("@dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));


                    // Sorguyu çalıştır
                    cmd.ExecuteNonQuery(); // DataReader yerine ExecuteNonQuery kullanılmalı çünkü bu bir ekleme işlemi
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetMusteriId()
        {
            string kullaniciAdi = anaForm.kAdi;
            int musteriId = -1;
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT musteri_id FROM musteri WHERE kullanici_adi = @KullaniciAdi";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        musteriId = reader.GetInt32("musteri_id");
                    }
                }
            }
            return musteriId;
        }
    }
}
