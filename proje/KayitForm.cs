using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace proje
{
    public partial class KayitForm : Form
    {
        private MainForm anaForm;
        private TextBox kullaniciAdiTextBox;
        private TextBox sifreTextBox;
        private TextBox adTextBox;
        private TextBox soyadTextBox;

        string connectionString = "server=34.38.166.127;port=3306;database=mydb;user=user;password=user123;";
        public KayitForm(MainForm form1)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            anaForm = form1;
        }

        private void KayitForm_Load(object sender, EventArgs e)
        {
            this.Text = "TıklAl v1.02 - Kayıt Menüsü";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile("userpanelbg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = new Icon("shopicon.ico");

            // Üst uyarı etiketi
            Label uyarıLabel = new Label();
            uyarıLabel.Text = "Lütfen kullanıcı adı ve şifrenizi giriniz";
            uyarıLabel.BackColor = Color.Transparent;
            uyarıLabel.Size = new System.Drawing.Size(400, 30);
            uyarıLabel.Location = new System.Drawing.Point(200, 75);
            uyarıLabel.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            uyarıLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(uyarıLabel);

            // İsim Label
            Label isimLabel = new Label();
            isimLabel.Text = "İsim:";
            isimLabel.BackColor = Color.Transparent;
            isimLabel.Size = new System.Drawing.Size(100, 30);
            isimLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            isimLabel.Location = new System.Drawing.Point(200, 125);
            isimLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(isimLabel);

            // İsim TextBox          
            adTextBox = new TextBox();            
            adTextBox.Size = new System.Drawing.Size(200, 30);
            adTextBox.Location = new System.Drawing.Point(325, 125);
            adTextBox.BackColor = System.Drawing.Color.LightGray;
            adTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(adTextBox);

            // Soyad Label
            Label soyisimLabel = new Label();
            soyisimLabel.BackColor=Color.Transparent;
            soyisimLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            soyisimLabel.Text = "Soyisim:";
            soyisimLabel.Size = new System.Drawing.Size(100, 30);
            soyisimLabel.Location = new System.Drawing.Point(200, 175);
            soyisimLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(soyisimLabel);

            // Soyad TextBox
            soyadTextBox = new TextBox();
            soyadTextBox.Size = new System.Drawing.Size(200, 30);
            soyadTextBox.Location = new System.Drawing.Point(325, 175);
            soyadTextBox.BackColor = System.Drawing.Color.LightGray;
            soyadTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(soyadTextBox);

            // Kullanıcı adı etiketi
            Label kullaniciAdiLabel = new Label();
            kullaniciAdiLabel.Text = "Kullanıcı Adı:";
            kullaniciAdiLabel.BackColor= Color.Transparent;
            kullaniciAdiLabel.Size = new System.Drawing.Size(100, 30);
            kullaniciAdiLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            kullaniciAdiLabel.Location = new System.Drawing.Point(200, 225);
            kullaniciAdiLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(kullaniciAdiLabel);

            // Kullanıcı adı TextBox
            kullaniciAdiTextBox = new TextBox();
            kullaniciAdiTextBox.Size = new System.Drawing.Size(200, 30);
            kullaniciAdiTextBox.Location = new System.Drawing.Point(325, 225);
            kullaniciAdiTextBox.BackColor = System.Drawing.Color.LightGray;
            kullaniciAdiTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(kullaniciAdiTextBox);

            // Şifre etiketi
            Label sifreLabel = new Label();
            sifreLabel.Text = "Şifre:";
            sifreLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            sifreLabel.BackColor = Color.Transparent;
            sifreLabel.Size = new System.Drawing.Size(100, 30);
            sifreLabel.Location = new System.Drawing.Point(200, 275);
            sifreLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(sifreLabel);

            // Şifre TextBox
            sifreTextBox = new TextBox();
            sifreTextBox.Size = new System.Drawing.Size(200, 30);
            sifreTextBox.Location = new System.Drawing.Point(325, 275);
            sifreTextBox.BackColor = System.Drawing.Color.LightGray;
            sifreTextBox.BorderStyle = BorderStyle.FixedSingle;
            sifreTextBox.PasswordChar = '*'; // Şifrenin görünmemesi için
            this.Controls.Add(sifreTextBox);

            // Kayıt ol butonu
            Button kayitButton = new Button();
            kayitButton.Text = "Kayıt Ol";
            kayitButton.BackColor = Color.Transparent;
            kayitButton.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            kayitButton.Size = new System.Drawing.Size(100, 40);
            kayitButton.Location = new System.Drawing.Point(350, 325);
            kayitButton.BackColor = System.Drawing.Color.Turquoise;
            kayitButton.FlatStyle = FlatStyle.Flat;
            kayitButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            kayitButton.FlatAppearance.BorderSize = 2;
            kayitButton.Click += KayitButton_Click;
            this.Controls.Add(kayitButton);
        }

        private void KayitButton_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = kullaniciAdiTextBox.Text;
            string sifre = sifreTextBox.Text;
            string ad = adTextBox.Text;
            string soyad = soyadTextBox.Text;

            // Kullanıcı adı ve şifre boş olamaz
            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifreyi boş bırakmayın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConnectAndQuery(ad, soyad, kullaniciAdi, sifre);
        }

        public void ConnectAndQuery(string ad, string soyad, string kullaniciAdi, string sifre)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    // Bağlantıyı aç
                    conn.Open();

                    // Sorgu oluştur
                    string query = "INSERT INTO musteri(musteri_ad, musteri_soyad, kullanici_adi, sifre,kayit_tarih) VALUES (@ad, @soyad, @kullaniciAdi, @sifre,@dateTime);";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    cmd.Parameters.AddWithValue("@sifre", sifre);
                    cmd.Parameters.AddWithValue("@dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));


                    MessageBox.Show("Başarıyla kayıt oldunuz!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                    // Sorguyu çalıştır
                    cmd.ExecuteNonQuery(); // DataReader yerine ExecuteNonQuery kullanılmalı çünkü bu bir ekleme işlemi
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
