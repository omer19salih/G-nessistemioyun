using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using NAudio.Wave;

namespace Günessistemioyun
{
    public partial class animasyongiris : Form
    {
        // AudioFileReader ve WaveOutEvent nesneleri ses dosyasını çalmak için kullanılır
        private AudioFileReader audioFileReader;
        private WaveOutEvent waveOutEvent;

        // Sesin çalıp çalmadığını kontrol etmek için kullanılan değişken
        private bool isPlaying;

        // ProgressBar'ın değerini takip etmek için kullanılan değişken
        int progressValue = 0;

        public animasyongiris()
        {
            InitializeComponent();
            // Ses çıkışı için WaveOutEvent nesnesi başlatılır
            waveOutEvent = new WaveOutEvent();
        }

        // ProgressBar'a tıklanma olayını ele alan boş event metodu
        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        // Timer her tick olduğunda ProgressBar'ı güncelleyen metod
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressValue += 2; // ProgressBar'ı artır
            progressBar1.Value = progressValue;

            // ProgressBar yüzde 100'e ulaştığında işlemi sonlandır
            if (progressValue >= 100)
            {
                timer1.Stop(); // Timer'ı durdur
                progressBar1.Visible = false; // ProgressBar'ı gizle
                Form1 form1 = new Form1(); // Ana formu oluştur
                form1.Show(); // Ana formu göster
                this.Hide(); // Bu formu gizle

                // Ses çalıyorsa durdur
                if (isPlaying)
                {
                    waveOutEvent.Stop();
                    isPlaying = false;
                }
            }
        }

        // Form yüklendiğinde çalışacak metod
        private void animasyongiris_Load_1(object sender, EventArgs e)
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
    }
}
