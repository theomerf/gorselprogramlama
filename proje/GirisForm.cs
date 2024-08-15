using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace proje
{
    public partial class GirisForm : Form
    {
        private MainForm anaForm;
        public static bool girisy = false;
        private TextBox kullaniciAdiTextBox;
        private TextBox sifreTextBox;
        string connectionString = "server=34.38.166.127;port=3306;database=mydb;user=user;password=user123;";
        public GirisForm(MainForm form1)
        {
            InitializeComponent();
            this.DoubleBuffered = true;         
            anaForm = form1;
        }

        

        private void GirisForm_Load(object sender, EventArgs e)
        {
          
            this.Text = "TıklAl v1.02 - Giriş Menüsü";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile("userpanelbg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = new Icon("shopicon.ico");

            // Üst uyarı etiketi
            Label uyarıLabel = new Label();
            uyarıLabel.Text = "Lütfen kullanıcı adı ve şifrenizi giriniz";
            uyarıLabel.Size = new System.Drawing.Size(400, 30);
            uyarıLabel.BackColor = Color.Transparent;
            uyarıLabel.Location = new System.Drawing.Point(200, 150);
            uyarıLabel.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            uyarıLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(uyarıLabel);

            // Kullanıcı adı etiketi
            Label kullaniciAdiLabel = new Label();
            kullaniciAdiLabel.Text = "Kullanıcı Adı:";
            kullaniciAdiLabel.BackColor = Color.Transparent;
            kullaniciAdiLabel.Size = new System.Drawing.Size(100, 30);
            kullaniciAdiLabel.Location = new System.Drawing.Point(200, 200);
            kullaniciAdiLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            kullaniciAdiLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(kullaniciAdiLabel);

            kullaniciAdiTextBox = new TextBox();
            kullaniciAdiTextBox.Size = new System.Drawing.Size(200, 30);
            kullaniciAdiTextBox.Location = new System.Drawing.Point(325, 200);
            kullaniciAdiTextBox.BackColor = System.Drawing.Color.LightGray;
            kullaniciAdiTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(kullaniciAdiTextBox);

            // Şifre etiketi
            Label sifreLabel = new Label();
            sifreLabel.Text = "Şifre:";
            sifreLabel.BackColor=Color.Transparent;
            sifreLabel.Size = new System.Drawing.Size(100, 30);
            sifreLabel.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            sifreLabel.Location = new System.Drawing.Point(200, 250);
            sifreLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(sifreLabel);

            sifreTextBox = new TextBox();
            sifreTextBox.Size = new System.Drawing.Size(200, 30);
            sifreTextBox.Location = new System.Drawing.Point(325, 250);
            sifreTextBox.BackColor = System.Drawing.Color.LightGray;
            sifreTextBox.BorderStyle = BorderStyle.FixedSingle;
            sifreTextBox.PasswordChar = '*'; // Şifrenin görünmemesi için
            this.Controls.Add(sifreTextBox);

            // Giriş yap butonu
            Button girisButton = new Button();
            girisButton.Text = "Giriş Yap";
            girisButton.Size = new System.Drawing.Size(100, 40);
            girisButton.Location = new System.Drawing.Point(350, 300);
            girisButton.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            girisButton.BackColor = System.Drawing.Color.Turquoise;
            girisButton.FlatStyle = FlatStyle.Flat;
            girisButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            girisButton.FlatAppearance.BorderSize = 2;
            girisButton.Click += girisButton_Click; // Giriş butonuna tıklandığında işlem yapmak için
            this.Controls.Add(girisButton);

            // Arka plan resmi
            
        }

        // GirisForm.cs

        private void girisButton_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = kullaniciAdiTextBox.Text;
            string sifre = sifreTextBox.Text;

            // Kullanıcı adı ve şifre kontrolü
            if (ConnectAndQuery(kullaniciAdi, sifre))
            {
                MessageBox.Show("Hoşgeldiniz, " + kullaniciAdi);
                anaForm.SetKullaniciAdi(kullaniciAdi);
                this.Close();  // Giriş formunu kapat
                anaForm.Controls.Clear();
                anaForm.Form1_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public bool ConnectAndQuery(string kullaniciAdi, string sifre)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM musteri WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    cmd.Parameters.AddWithValue("@sifre", sifre);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    string query_ = "update musteri set giris_tarih = @dateTime where kullanici_adi = @kullaniciAdi";
                    MySqlCommand cmd_ = new MySqlCommand(query_, conn);
                    cmd_.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    cmd_.Parameters.AddWithValue("@dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd_.ExecuteNonQuery(); 

                    return result > 0;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
        }
    }
}
