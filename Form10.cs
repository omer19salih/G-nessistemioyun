using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Günessistemioyun
{
    public partial class Form10 : Form
    {
     


        // AudioFileReader ve WaveOutEvent nesneleri ses dosyasını çalmak için kullanılır
        private AudioFileReader audioFileReader;
        private WaveOutEvent waveOutEvent;

        // Sesin çalıp çalmadığını kontrol etmek için kullanılan değişken
        private bool isPlaying;

        // ProgressBar'ın değerini takip etmek için kullanılan değişken
        int progressValue = 0;

        public Form10()
        {
            InitializeComponent();
            // Ses çıkışı için WaveOutEvent nesnesi başlatılır
            waveOutEvent = new WaveOutEvent();
          
        }

        // ProgressBar'a tıklanma olayını ele alan boş event metodu
        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form10_Load(object sender, EventArgs e)
        {
            timer1.Stop();

        }

        public void StartProgres()
        {

            // ProgressBar'ı ayarla
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            timer1.Interval = 50; // Timer'ı 50ms'lik aralıklarla çalışacak şekilde ayarla
            timer1.Start(); // Timer'ı başlat

            // Eğer ses dosyası daha önce başlatılmamışsa, ses dosyasını başlat
            if (audioFileReader == null)
            {
                audioFileReader = new AudioFileReader(@"Resources\muzik1.mp3");
                waveOutEvent.Init(audioFileReader); // Ses çıkışı için ses dosyasını ayarla
            }

            // Eğer ses çalmıyorsa, ses dosyasını çalmaya başla
            if (!isPlaying)
            {
                waveOutEvent.Play(); // Ses çalmaya başla
                isPlaying = true; // Çaldığını belirt
            }
            else
            {
                waveOutEvent.Stop(); // Ses durdurulacaksa durdur
                isPlaying = false; // Çalmadığını belirt
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            progressValue += 2; // ProgressBar'ı artır
            progressBar1.Value = progressValue;

            // ProgressBar yüzde 100'e ulaştığında işlemi sonlandır
            if (progressValue >= 100)
            {
                timer1.Stop(); // Timer'ı durdurq
                progressBar1.Visible = false; // ProgressBar'ı gizle

                // Ses çalıyorsa durdur
                if (isPlaying)
                {
                    waveOutEvent.Stop();
                    isPlaying = false;
                }
                Form9 form9 = new Form9(secim,secimindex);
                form9.Show();
                this.Hide();
            }
        }
        SecilenGezeegen secim = new SecilenGezeegen();
        public int secimindex;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
           // SecilenGezeegen secim = new SecilenGezeegen();
            secim.Gezegen = pictureBox2.Image; // Örneğin PictureBox'tan resmi alıyoruz
            secimindex = 1;
            StartProgres();

          
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
           // SecilenGezeegen secim = new SecilenGezeegen();
            secim.Gezegen = pictureBox3.Image; // Örneğin PictureBox'tan resmi alıyoruz
            secimindex = 2;
            StartProgres();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
           // SecilenGezeegen secim = new SecilenGezeegen();
            secim.Gezegen = pictureBox4.Image; // Örneğin PictureBox'tan resmi alıyoruz
            secimindex = 3;
            StartProgres();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //SecilenGezeegen secim = new SecilenGezeegen();
            secim.Gezegen = pictureBox5.Image; // Örneğin PictureBox'tan resmi alıyoruz
            secimindex = 4;
            StartProgres();

        }
    }
}

