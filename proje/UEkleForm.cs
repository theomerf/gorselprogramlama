using MySql.Data.MySqlClient;


namespace proje
{
    public partial class UEkleForm : Form
    {
        private MainForm anaForm;
        private UrunForm urunForm;

        string connectionString = "server=34.38.166.127;port=3306;database=mydb;user=user;password=user123;";

        public UEkleForm(MainForm form1)
        {
            InitializeComponent();
            anaForm = form1;
        }

        private void UEkleForm_Load(object sender, EventArgs e)
        {

            PictureBox backg = new PictureBox
            {
                Size = this.ClientSize,
                BackgroundImage = Image.FromFile("background.jpg"),
                BackgroundImageLayout = ImageLayout.Stretch
            };
            this.Controls.Add(backg);
            backg.SendToBack();  // Arka planı geri gönder

            Button ekle = new Button
            {
                Text = "Ekle",
                Location = new Point(100, 100),  // Buton konumu
                Size = new Size(100, 50)         // Buton boyutu
            };
            ekle.Click += new EventHandler(EkleButton_Click);
            this.Controls.Add(ekle);
            ekle.BringToFront();  // Butonu en öne getir

            TextBox urunAd = new TextBox
            {
                Location = new Point(10, 70),
                Width = 200
            };
            this.Controls.Add(urunAd);  // Ürün adı TextBox'ını forma ekle
        }

        public bool ConnectAndQuery(string kullaniciAdi, string sifre)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    cmd.Parameters.AddWithValue("@sifre", sifre);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
        }

        public void EkleButton_Click(object sender, EventArgs e)
        {
            //ConnectAndQuery();
        }

    }
}
