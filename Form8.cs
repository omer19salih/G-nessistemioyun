using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
namespace Günessistemioyun
{

    
    public partial class Form8 : Form
    {

        private AudioFileReader audioFileReader;
        private WaveOutEvent waveOutEvent;
        private bool isPlaying;

        private List<Soru> sorular;
        private int mevcutSoruIndex = 0;
        private int dogruCevapSayisi = 0;
        private int canHakki = 3; // Başlangıç can hakkı
        private Timer soruZamanlayici;
        private int kalanSure = 10;
        // Skor tabloları için (basit bir bellek içi çözüm)
        private List<int> gunlukSkorlar = new List<int>();
        private List<int> haftalikSkorlar = new List<int>();
        private List<int> aylikSkorlar = new List<int>();
        private List<int> yillikSkorlar = new List<int>();

        public Form8()
        {
            InitializeComponent();
            SorulariYukle();
            SonrakiSoru();
            GuncelleCanHakki();
            // Timer ayarları
            soruZamanlayici = new Timer();
            soruZamanlayici.Interval = 1000; // 1 saniye
            soruZamanlayici.Tick += SoruZamanlayici_Tick;
            waveOutEvent = new WaveOutEvent();
        }

        private void SorulariYukle()
        {
            sorular = new List<Soru>()
            {
        new Soru { Metin = "Güneş'e en yakın gezegen hangisidir?", Cevaplar = new string[] { "Merkür", "Venüs", "Dünya", "Mars" }, DogruCevapIndex = 0,Resources  = "merkür.jpeg" },
        new Soru { Metin = "Güneş Sistemi'ndeki en büyük gezegen hangisidir?", Cevaplar = new string[] { "Jüpiter", "Satürn", "Uranüs", "Neptün" }, DogruCevapIndex = 0, Resources = "Jüpiter.jpeg" },
        new Soru { Metin = "Dünya'nın kaç tane doğal uydusu vardır?", Cevaplar = new string[] { "0", "1", "2", "3" }, DogruCevapIndex = 1, Resources = "ay.png" },
        new Soru { Metin = "Hangi gezegen 'Kızıl Gezegen' olarak bilinir?", Cevaplar = new string[] { "Mars", "Venüs", "Merkür", "Jüpiter" }, DogruCevapIndex = 0, Resources = "mars.jpeg" },
        new Soru { Metin = "Hangi gezegenin belirgin halkaları vardır?", Cevaplar = new string[] { "Satürn", "Jüpiter", "Uranüs", "Neptün" }, DogruCevapIndex = 0, Resources = "Satürn.png" },
        new Soru { Metin = "Hangi gezegen yan yatmış bir şekilde döner?", Cevaplar = new string[] { "Uranüs", "Neptün", "Satürn", "Jüpiter" }, DogruCevapIndex = 0, Resources = "Uranüs.jpeg" },
        new Soru { Metin = "Güneş Sistemi'nin en dışındaki büyük gezegen hangisidir?", Cevaplar = new string[] { "Neptün", "Uranüs", "Satürn", "Jüpiter" }, DogruCevapIndex = 0, Resources = "Neptün.jpeg" },
        new Soru { Metin = "Güneş Sistemi'ndeki en küçük gezegen hangisidir?", Cevaplar = new string[] { "Merkür", "Mars", "Venüs", "Dünya" }, DogruCevapIndex = 0, Resources = "merkür.jpeg" }
        // Daha fazla soru ve resim dosyası eklenebilir...
             };
        }

        private void SonrakiSoru()
        {
            if (mevcutSoruIndex < sorular.Count)
            {
                Soru mevcutSoru = sorular[mevcutSoruIndex];
                soruMetniLabel.Text = mevcutSoru.Metin;

                // Cevap butonlarının metinlerini güncelle
                if (mevcutSoru.Cevaplar.Length > 0) cevapButton1.Text = mevcutSoru.Cevaplar[0];
                if (mevcutSoru.Cevaplar.Length > 1) cevapButton2.Text = mevcutSoru.Cevaplar[1];
                if (mevcutSoru.Cevaplar.Length > 2) cevapButton3.Text = mevcutSoru.Cevaplar[2];
                if (mevcutSoru.Cevaplar.Length > 3) cevapButton4.Text = mevcutSoru.Cevaplar[3];

                // Resmi yükle
                try
                {
                    string imagePath = $"C:\\Users\\salih ömer\\source\\repos\\Günessistemioyun\\Resources\\{mevcutSoru.Resources}";
                    if (File.Exists(imagePath))
                    {
                        pictureBoxSoruResmi.Image = Image.FromFile(imagePath);
                        pictureBoxSoruResmi.SizeMode = PictureBoxSizeMode.CenterImage; // Resmin boyutunu PictureBox'a sığdır
                    }
                    else
                    {
                        MessageBox.Show("Resim dosyası bulunamadı.", "Hata");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Resim yüklenirken bir hata oluştu: {ex.Message}", "Hata");
                }

                // Mevcut soru indeksini arttır
                mevcutSoruIndex++;

                // Kalan süreyi sıfırla ve timer'ı başlat
                kalanSure = 10;
                sureLabel.Text = $"Kalan Süre: {kalanSure} sn";

                // Timer'ı sıfırlayıp başlat
                if (soruZamanlayici != null)
                {
                    soruZamanlayici.Stop();
                    soruZamanlayici.Start();
                }
               
            }
            else
            {
                // Tüm sorular bitti, sonuçları göster
                OyunBitti();
            }
        }


        private void SoruZamanlayici_Tick(object sender, EventArgs e)
        {
            kalanSure--;
            sureLabel.Text = $"Kalan Süre: {kalanSure} sn";

            if (kalanSure == 0)
            {
                soruZamanlayici.Stop();
                MessageBox.Show("Süre doldu! Bu soruyu yanlış sayıyoruz.", "Süre Bitti");
                canHakki--;
                GuncelleCanHakki();

                if (canHakki == 0)
                {
                    OyunBitti();
                }
                else
                {
                    SonrakiSoru();
                }
            }
        }






        private void CevapKontrol(int secilenCevapIndex)
        {
            soruZamanlayici.Stop();
            if (secilenCevapIndex == sorular[mevcutSoruIndex - 1].DogruCevapIndex)
            {
                dogruCevapSayisi++;
                MessageBox.Show("Doğru!", "Sonuç");
            }
            else
            {
                canHakki--;
                GuncelleCanHakki();
                MessageBox.Show($"Yanlış! Doğru cevap: {sorular[mevcutSoruIndex - 1].Cevaplar[sorular[mevcutSoruIndex - 1].DogruCevapIndex]}", "Sonuç");

                if (canHakki == 0)
                {
                    OyunBitti();
                    return;
                }
            }

            SonrakiSoru();
        }

        private void GuncelleCanHakki()
        {
            // Tüm PictureBox'ları gizle
            heartPictureBox1.Visible = false;
            heartPictureBox2.Visible = false;
            heartPictureBox3.Visible = false;

            // Mevcut can hakkı sayısına göre PictureBox'ları göster
            switch (canHakki)
            {
                case 3:
                    heartPictureBox1.Visible = true;
                    heartPictureBox2.Visible = true;
                    heartPictureBox3.Visible = true;
                    break;
                case 2:
                    heartPictureBox1.Visible = true;
                    heartPictureBox2.Visible = true;
                    break;
                case 1:
                    heartPictureBox1.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void OyunBitti()
        {
            MessageBox.Show($"Oyun Bitti! Doğru cevap sayısı: {dogruCevapSayisi}", "Oyun Sonu");

            // Skorları kaydet
            int skor = dogruCevapSayisi;
            KaydetSkor(skor);

            // 2. seviyeye geçiş kontrolü (örneğin belirli bir doğru cevap sayısına göre)
            if (dogruCevapSayisi >= 3) // Örnek eşik
            {
                MessageBox.Show("Tebrikler! 2. Seviyeye geçtiniz!", "Seviye Atlama");
                // Burada 2. seviye için yeni bir form veya içerik yüklenebilir.
                // Mevcut formu kapatıp 2. seviye formunu açabilirsiniz.
                 this.Close();
                 Form level2Form = new Form10(); // Örneğin Form9 2. seviye formu olsun
                 level2Form.Show();
            }
            else
            {
                MessageBox.Show("2. Seviyeye geçmek için daha çok doğru cevap vermelisiniz.", "Seviye Atlanamadı");
                // Oyunu yeniden başlatma veya çıkış yapma seçenekleri sun
                DialogResult result = MessageBox.Show("Oyunu yeniden başlatmak ister misiniz?", "Yeniden Başlat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Oyunu yeniden başlat
                    mevcutSoruIndex = 0;
                    dogruCevapSayisi = 0;
                    canHakki = 3;
                    GuncelleCanHakki();
                    SorulariYukle(); // Soruları yeniden yükle (isteğe bağlı)
                    SonrakiSoru();
                }
                else
                {
                    
                    this.Close();
                    Form level2Form = new Form10(); // Örneğin Form9 2. seviye formu olsun
                    level2Form.Show();
                }
            }
        }

        private void KaydetSkor(int skor)
        {
            DateTime bugun = DateTime.Now;
            gunlukSkorlar.Add(skor);

            // Haftalık skor
            if (haftalikSkorlar.Count == 0 || DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                haftalikSkorlar.Clear();
            }
            haftalikSkorlar.Add(skor);

            // Aylık skor
            if (aylikSkorlar.Count == 0 || DateTime.Now.Day == 1)
            {
                aylikSkorlar.Clear();
            }
            aylikSkorlar.Add(skor);

            // Yıllık skor
            if (yillikSkorlar.Count == 0 || DateTime.Now.DayOfYear == 1)
            {
                yillikSkorlar.Clear();
            }
            yillikSkorlar.Add(skor);

            // Skor tablolarını güncelle (arayüzde gösterim için)
            GuncelleSkorTablolari();
        }

        private void GuncelleSkorTablolari()
        {
            // Bu kısımda Label veya DataGridView kontrolleri kullanarak skor tablolarını görselleştirebilirsiniz.
            // Örneğin:
            // gunlukSkorListBox.DataSource = gunlukSkorlar.OrderByDescending(s => s).ToList();
            // ... diğer skor tabloları için de benzer işlemler.
        }

        private void cevapButton1_Click_1(object sender, EventArgs e)
        {
            CevapKontrol(0);
        }
        private void cevapButton2_Click_1(object sender, EventArgs e)
        {
            CevapKontrol(1);
        }


        private void cevapButton3_Click_1(object sender, EventArgs e)
        {
            CevapKontrol(2);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            CevapKontrol(3);
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // Zamanlayıcıyı burada başlatıyoruz, böylece form tamamen yüklendikten sonra başlar.
            if (soruZamanlayici != null)
            {
                soruZamanlayici.Start();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (audioFileReader == null)
            {
                audioFileReader = new AudioFileReader(@"Resources\muzik1.mp3");
                waveOutEvent.Init(audioFileReader);
            }

            if (!isPlaying)
            {
                waveOutEvent.Play();
                isPlaying = true;
            }
            else
            {
                waveOutEvent.Stop();
                isPlaying = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                waveOutEvent.Stop();
                isPlaying = false;
            }
        }
    }
   

}