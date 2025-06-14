using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Günessistemioyun
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            buttonAdminPanel.Visible = oyuncubilgi.isAdmin;
            // Dilek/Şikayet butonu her zaman görünür olabilir veya isteğe bağlı olarak ayarlanabilir
            // buttonOneriSikayet.Visible = true; // Örneğin her zaman görünür yapmak için
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowHowToPlay();
        }
        private void ShowHowToPlay()
        {
            MessageBox.Show("Bu oyunda amacınız... 1. seviyede gezegenleri yerlerine  yerleştirme dogru yerleştirirsen  2.seviye başlıyor ve roket ile meteorların dünyaya çarpmasını engelliyosun ve 100,200,300 puanda zorluk seviyesi artıyor . Hazine kutularına basarsan artı 50 puan kazanıyosun .");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProfilSayfası profilSayfası = new ProfilSayfası();
            profilSayfası.Show();
            this.Hide();
        }

        private void buttonAdminPanel_Click(object sender, EventArgs e)
        {
            Form4 adminPanel = new Form4();
            adminPanel.Show();
            this.Hide();
        }

        private void buttonOneriSikayet_Click(object sender, EventArgs e)
        {
            // dileksikayet formunu aç
            dileksikayet oneriSikayetForm = new dileksikayet();
            oneriSikayetForm.ShowDialog(); // ShowDialog ile açmak, form kapanana kadar Form7'yi bekletecektir.
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
