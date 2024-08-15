using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace proje
{
    public partial class Marka : Form
    {
        private string type;
        private MainForm anaForm;

        public Marka(string type,MainForm form1)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.type = type;
            this.anaForm = form1;
        }

        private void Marka_Load(object sender, EventArgs e)
        {
            this.Text = type == "urun" ? "Ürün Al" : "Yemek Al";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(800, 600);           
            this.Icon = new Icon("shopicon.ico");


            string[] markalar;
            string[] resimler;


            if (type == "urun")
            {
                this.BackgroundImage = Image.FromFile("shopping.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
                markalar = new string[] { "Teknoloji", "Kozmetik", "Spor", "Giyim", "Mobilya" };
                resimler = new string[] { "techonology.png", "products.png", "sports.png", "clothes.png", "furniture.png" };
            }
            else
            {
                this.BackgroundImage = Image.FromFile("foods.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
                markalar = new string[] { "Graburger", "Bursa Sofrası16", "Tatlıcı Egemen Usta", "Kebapçı Ahmet", "Baldwin Döner" };
                resimler = new string[] { "fast-food.png", "etdoner.png", "baklavaci.png", "kebab.png", "wrap.png" };
            }


            for (int i = 0; i < markalar.Length; i++)
            {
                if (type == "urun")
                {
                    Button markaButton = new Button();
                    markaButton.Text = markalar[i];                    
                    markaButton.Size = new Size(100, 50);
                    markaButton.BackColor = Color.Turquoise;
                    markaButton.Font = new Font("Arial", 10.5f);
                    markaButton.Location = new Point(250, 70 + i * 110);
                    markaButton.Click += MarkaButton_Click;
                    this.Controls.Add(markaButton);

                    PictureBox markaResim = new PictureBox();
                    markaResim.Image = Image.FromFile(resimler[i]);
                    markaResim.Size = new Size(85, 85);
                    markaResim.Location = new Point(125, 50 + i * 110);
                    markaResim.BackColor = Color.Transparent;
                    markaResim.SizeMode = PictureBoxSizeMode.Zoom;
                    this.Controls.Add(markaResim);
                }
                else
                {
                    Button restButton = new Button();
                    restButton.Text = markalar[i];
                    restButton.Size = new Size(100, 50);
                    restButton.BackColor = Color.Turquoise;
                    restButton.Font = new Font("Arial", 10f);
                    restButton.Location = new Point(520, 70 + i * 110);
                    restButton.Click += RestButton_Click;
                    this.Controls.Add(restButton);

                    PictureBox markaResim = new PictureBox();
                    markaResim.Image = Image.FromFile(resimler[i]);
                    markaResim.Size = new Size(85, 85);
                    markaResim.Location = new Point(675, 50 + i * 110);
                    markaResim.BackColor = Color.Transparent;
                    markaResim.SizeMode = PictureBoxSizeMode.Zoom;
                    this.Controls.Add(markaResim);
                }


               

            }

        }
        private void MarkaButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedMarka = clickedButton.Text;
            UrunForm urunForm = new UrunForm(selectedMarka,anaForm);
            urunForm.ShowDialog();
        }
        private void RestButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedRest = clickedButton.Text;
            YemekForm yemekForm = new YemekForm(selectedRest,anaForm);
            yemekForm.ShowDialog();
        }
    }
}
