using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class EkleForm : Form
    {
        private MainForm anaForm;

        public EkleForm(MainForm form1)
        {
            InitializeComponent();
            anaForm = form1;
        }

        private void EkleForm_Load(object sender, EventArgs e)
        {
            TextBox yemekAd = new TextBox();
            TextBox yemekFiyat = new TextBox();

            Controls.Add(yemekAd);
            Controls.Add(yemekFiyat);
















            PictureBox backg = new PictureBox();
            string imagePath = "background.jpg";
            backg.BackgroundImage = Image.FromFile(imagePath);
            backg.Size = this.ClientSize;
            backg.BackgroundImageLayout = ImageLayout.Stretch;
            this.Controls.Add(backg);
            backg.SendToBack();
        }

    }
}
