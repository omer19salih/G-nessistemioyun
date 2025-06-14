using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Günessistemioyun
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            // TabControl oluştur
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Hakkımızda TabPage
            TabPage hakkimizdaTabPage = new TabPage("Hakkımızda");
            RichTextBox hakkimizdaRichTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Text = "Bu uygulama, Güneş Sistemi hakkında bilgi edinmek ve eğlenmek için geliştirilmiştir.\n\nGeliştirici: [Salih Ömer Uyar]\n\nİletişim: [Salihomeruyar@gmail.com]",
                ReadOnly = true
            };
            hakkimizdaTabPage.Controls.Add(hakkimizdaRichTextBox);

            // Yardım TabPage
            TabPage yardimTabPage = new TabPage("Yardım");
            RichTextBox yardimRichTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Text = "Oyun nasıl oynanır:\n\n1. Gezegenleri seçin.\n2. Bilgileri okuyun.\n3. Testleri çözün.\n\nSorularınız için bizimle iletişime geçin.",
                ReadOnly = true
            };
            yardimTabPage.Controls.Add(yardimRichTextBox);

            // TabControl'e ekle
            tabControl.TabPages.Add(hakkimizdaTabPage);
            tabControl.TabPages.Add(yardimTabPage);

            this.Controls.Add(tabControl);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void Form6_Load(object sender, EventArgs e)
        {

        }
        private void Form6_Load_1(object sender, EventArgs e)
        {

        }

        private void mesajTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}