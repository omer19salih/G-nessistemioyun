using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Günessistemioyun
{
    public partial class Form11 : Form
    {
        int puan = 0;
        int can = 3;
        int zaman = 120;
        
        private bool sürükleniyor = false;
        private Point mouseOffset;
        private PictureBox seçiliGezegen = null;

        // Ses oynatıcıları
        private SoundPlayer dogruSes;
        private SoundPlayer yanlisSes;

        private WaveOutEvent currentAudioDevice = null;
        private AudioFileReader currentAudioFile = null;
        private bool soundSaturnPlay, marsPlay, jupiterPlay, merPlay, dünPlay;

        public Form11()
        {
            InitializeComponent();
            timer1.Start();

            // Form ayarları
            this.DoubleBuffered = true;

            // Ses dosyalarını yükle
            try
            {
                dogruSes = new SoundPlayer(Properties.Resources.dogru);
                yanlisSes = new SoundPlayer(Properties.Resources.yanlıs);

                // Sesleri önceden yükle
                dogruSes.LoadAsync();
                yanlisSes.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ses dosyaları yüklenemedi: " + ex.Message);
            }

            // Form kapatılırken sesleri temizle
            this.FormClosing += (s, e) =>
            {
                if (dogruSes != null)
                {
                    dogruSes.Dispose();
                    dogruSes = null;
                }
                if (yanlisSes != null)
                {
                    yanlisSes.Dispose();
                    yanlisSes = null;
                }
            };

            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Tag != null && c.Name.StartsWith("hedef"))
                {
                    PictureBox hedef = (PictureBox)c;
                    hedef.AllowDrop = true;
                }
                if (c is PictureBox && c.Tag != null && c.Name.StartsWith("gezegen"))
                {
                    PictureBox gezegen = (PictureBox)c;
                    gezegen.MouseDown += Gezegen_MouseDown;
                    gezegen.MouseMove += Gezegen_MouseMove;
                    gezegen.MouseUp += Gezegen_MouseUp;
                }
            }

            GuncelleBilgiler();
        }

        private void Gezegen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                seçiliGezegen = sender as PictureBox;
                if (seçiliGezegen != null)
                {
                    sürükleniyor = true;
                    seçiliGezegen.BringToFront();
                    mouseOffset = new Point(e.X, e.Y);
                    Cursor = Cursors.Hand;
                }
            }
        }

        private void Gezegen_MouseMove(object sender, MouseEventArgs e)
        {
            if (sürükleniyor && seçiliGezegen != null)
            {
                Point yeniKonum = this.PointToClient(Cursor.Position);
                seçiliGezegen.Location = new Point(
                    yeniKonum.X - mouseOffset.X,
                    yeniKonum.Y - mouseOffset.Y
                );
            }
        }

        private void Gezegen_MouseUp(object sender, MouseEventArgs e)
        {
            if (sürükleniyor && seçiliGezegen != null)
            {
                sürükleniyor = false;
                Cursor = Cursors.Default;

                // Hedef kontrolü
                Point gezegenMerkez = new Point(
                    seçiliGezegen.Location.X + seçiliGezegen.Width / 2,
                    seçiliGezegen.Location.Y + seçiliGezegen.Height / 2
                );

                foreach (Control c in this.Controls)
                {
                    if (c is PictureBox && c.Tag != null && c.Name.StartsWith("hedef"))
                    {
                        PictureBox hedef = c as PictureBox;
                        if (hedef.Visible && HedefteGezegen(hedef, gezegenMerkez))
                        {
                            if (hedef.Tag.ToString() == seçiliGezegen.Tag.ToString())
                            {
                                // Doğru eşleşme
                                seçiliGezegen.Location = hedef.Location;
                                seçiliGezegen.Enabled = false;
                                seçiliGezegen.BackColor = Color.Transparent;
                                puan += 10;
                                hedef.Visible = false;
                                
                                // Doğru sesi çal
                                try
                                {
                                    dogruSes?.Play();
                                }
                                catch { }
                            }
                            else
                            {
                                // Yanlış eşleşme
                                can--;
                                // Yanlış sesi çal
                                try
                                {
                                    yanlisSes?.Play();
                                }
                                catch { }
                                MessageBox.Show("Yanlış gezegen yörüngesi!");
                            }
                            break;
                        }
                    }
                }

                GuncelleBilgiler();
                OyunKontrol();
            }
            seçiliGezegen = null;
        }

        private bool HedefteGezegen(PictureBox hedef, Point gezegenMerkez)
        {
            Rectangle hedefAlan = new Rectangle(
                hedef.Location.X - 20,
                hedef.Location.Y - 20,
                hedef.Width + 40,
                hedef.Height + 40
            );
            return hedefAlan.Contains(gezegenMerkez);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            zaman--;
            GuncelleBilgiler();

            if (zaman <= 0)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Süre bitti! Puan: " + puan + "\nTekrar oynamak ister misiniz?", "Süre Bitti", MessageBoxButtons.YesNo);
                SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
                if (result == DialogResult.Yes)
                {
                    Form11 yeniForm = new Form11();
                    yeniForm.Show();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void GuncelleBilgiler()
        {
            labelPuan.Text = "Puan: " + puan;
            labelZaman.Text = "Zaman: " + zaman + " sn";

            kalp1.Visible = can >= 1;
            kalp2.Visible = can >= 2;
            kalp3.Visible = can >= 3;
        }

        private void OyunKontrol()
        {
            if (can <= 0)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Canların bitti! Oyun bitti. Tekrar oynamak ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
                if (result == DialogResult.Yes)
                {
                    Form11 yeniForm = new Form11();
                    yeniForm.Show();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
                return;
            }

            bool hepsiDogru = true;
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Name.StartsWith("gezegen") && c.Enabled == true)
                {
                    hepsiDogru = false;
                    break;
                }
            }

            if (hepsiDogru)
            {
                timer1.Stop();
                MessageBox.Show("Tebrikler! Tüm gezegenleri doğru yerleştirdin.");
                SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
                Form16 form16 = new Form16();
                form16.Show();
                this.Close();
            }
        }

        private void labelZaman_Click(object sender, EventArgs e) { }

        private void Form11_Load(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private readonly string connectionString = @"server=(localdb)\Salih;database=oyun;integrated security=true;";

        private void SkoruKaydet(string oyuncuAdi, int skor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SkorTablosu (OyuncuAdi, Skor, Tarih) VALUES (@OyuncuAdi, @Skor, @Tarih)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);
                cmd.Parameters.AddWithValue("@Skor", skor);
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void StopCurrentAudio()
        {
            if (currentAudioDevice != null)
            {
                try
                {
                    currentAudioDevice.Stop();
                    currentAudioDevice.Dispose();
                }
                catch { }
                currentAudioDevice = null;
            }
            if (currentAudioFile != null)
            {
                try
                {
                    currentAudioFile.Dispose();
                }
                catch { }
                currentAudioFile = null;
            }
        }

        private void SesCalAsync(string dosyaYolu)
        {
            try
            {
                // Stop any currently playing sound
                StopCurrentAudio();

                // Reset all play flags
                soundSaturnPlay = false;
                marsPlay = false;
                jupiterPlay = false;
                merPlay = false;
                dünPlay = false;

                // Create new audio resources
                currentAudioFile = new AudioFileReader(dosyaYolu);
                currentAudioDevice = new WaveOutEvent();
                
                // Set up event handler for playback stopped
                currentAudioDevice.PlaybackStopped += (s, e) =>
                {
                    StopCurrentAudio();
                };

                // Initialize and play
                currentAudioDevice.Init(currentAudioFile);
                currentAudioDevice.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ses çalma hatası: " + ex.Message);
                StopCurrentAudio();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopCurrentAudio();
            base.OnFormClosing(e);
        }

        private void gezegen5_Click(object sender, EventArgs e)
        {
            if (currentAudioDevice?.PlaybackState != PlaybackState.Playing)
            {
                SesCalAsync(@"Resources\Satürn Saturn Yüzeyi.mp3");
                soundSaturnPlay = true;
            }
        }

        private void gezegen3_Click(object sender, EventArgs e)
        {
            if (currentAudioDevice?.PlaybackState != PlaybackState.Playing)
            {
                SesCalAsync(@"Resources\Merkür Mercury Güneş.mp3");
                merPlay = true;
            }
        }

        private void gezegen1_Click(object sender, EventArgs e)
        {
            if (currentAudioDevice?.PlaybackState != PlaybackState.Playing)
            {
                SesCalAsync(@"Resources\Dünya Earth Hayat ba.mp3");
                dünPlay = true;
            }
        }

        private void gezegen4_Click(object sender, EventArgs e)
        {
            if (currentAudioDevice?.PlaybackState != PlaybackState.Playing)
            {
                SesCalAsync(@"Resources\Jüpiter Jupiter Güne.mp3");
                jupiterPlay = true;
            }
        }

        private void gezegen2_Click(object sender, EventArgs e)
        {
            if (currentAudioDevice?.PlaybackState != PlaybackState.Playing)
            {
                SesCalAsync(@"Resources\Mars Kızıl Gezegen o.mp3");
                marsPlay = true;
            }
        }
    }

    // Farenin hareketini sürekli izleyebilmek için:
    public class MouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                Application.DoEvents(); // Her hareketi yakala
            }
            return false;
        }
    }
}

