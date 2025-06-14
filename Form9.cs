using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Günessistemioyun
{

    public partial class Form9 : Form
    {
        private bomba bombayonet;

        private Point suruklemeBaslangicNoktasi;
        private bool surukleniyor = false;
        private PictureBox suruklenenNesne = null;
        private Random random = new Random();
        private PictureBox asteroidPictureBox; // Dünya görseli için değişken
        private List<PictureBox> meteorlar = new List<PictureBox>();
        private int meteorHizi = 5; // Başlangıç meteor hızı
        private int puan = 0;
        private Timer meteorOlusturmaTimer = new Timer();
        private PictureBox fuzePictureBox; // Füze görseli için değişken
        private Timer hazineOlusturmaTimer = new Timer();
        private bool hazineAktif = false;
        private int can = 3;
        private PictureBox bombaPictureBox; // Bomba görseli için değişken
        private Timer bombaOlusturmaTimer = new Timer(); // Bomba oluşturma zamanlayıcısı
        private List<PictureBox> bombalar = new List<PictureBox>();
        private List<PictureBox> canKalpListesi = new List<PictureBox>();


        private SecilenGezeegen secilen;

        public Form9(SecilenGezeegen gelenSecim,int gelenindex)
        {
            InitializeComponent();
            // Arka plan resmini ayarlayın (tasarımda da yapılabilir).

            arkaindex = gelenindex;
            secilen = gelenSecim;

            pictureBox3.Image = secilen.Gezegen;


            bombayonet = new bomba(pictureBoxArkaPlan, PuanAzalt);

            // Kalp simgelerinin gösterileceği alanı ayarlayın
            for (int i = 0; i < can; i++)
            {
                PictureBox kalp = new PictureBox
                {
                    Image = Properties.Resources.kalp, // Kalp resminiz
                    Size = new Size(30, 30),
                    Location = new Point(10 + (i * 35), 35), // Canları yatay olarak yerleştir
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                pictureBoxArkaPlan.Controls.Add(kalp);
                canKalpListesi.Add(kalp);
            }

            // Füze oluştur
            fuzePictureBox = new PictureBox
            {
                Image = Properties.Resources.fuze, // Füze resminizin adı
                Size = new Size(70, 70),
                Location = new Point((this.ClientSize.Width - 40) / 2, this.ClientSize.Height - 190),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            fuzePictureBox.MouseDown += Nesne_MouseDown;
            fuzePictureBox.MouseMove += Nesne_MouseMove;
            fuzePictureBox.MouseUp += Nesne_MouseUp;
            pictureBoxArkaPlan.Controls.Add(fuzePictureBox);
            fuzePictureBox.BringToFront();

            // Puan etiketi ayarla
            puanLabel.Text = "Puan: 0";
            puanLabel.Location = new Point(10, 10);
            puanLabel.ForeColor = Color.Red;
            puanLabel.Font = new Font("Arial", 12, FontStyle.Bold);

            // Hazine sandığı oluştur (başlangıçta gizli)
            pictureBox1 = new PictureBox
            {
                Image = Properties.Resources.hazine, // Hazine sandığı resminizin adı
                Size = new Size(30, 30),
                Location = new Point(random.Next(0, this.ClientSize.Width - 30), -30),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = true
            };
            pictureBoxArkaPlan.Controls.Add(pictureBox1);
            pictureBox1.BringToFront();

            // Meteor oluşturma timer'ı ayarla
            meteorOlusturmaTimer.Interval = 1000; // Başlangıçta her saniyede yeni meteor
            meteorOlusturmaTimer.Tick += MeteorOlusturmaTimer_Tick;
            meteorOlusturmaTimer.Start();

            // Meteor hareket timer'ı ayarla
            meteorTimer.Interval = 50; // Her 50 milisaniyede meteorları hareket ettir
            meteorTimer.Tick += MeteorTimer_Tick;
            meteorTimer.Start();


            // Hazine oluşturma timer'ı ayarla
            hazineOlusturmaTimer.Interval = 1500; // Her 1.5 saniyede bir hazine oluşturma olasılığı (daha sık)
            hazineOlusturmaTimer.Tick += HazineOlusturmaTimer_Tick;
            hazineOlusturmaTimer.Start();

            sonucLabel.Visible = false;
            anaMenuButton.Visible = false;
            cikisButton.Visible = false;
            tekrarOynaButton.Visible = false;
            tekrarOynaButton.Click += TekrarOynaButton_Click; // Tekrar Oyna butonu için olay ekle
        }


        private void MeteorOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            // Rastgele bir konumda yeni bir meteor oluştur
            PictureBox yeniMeteor = new PictureBox
            {
                Image = Properties.Resources.meteor, // Kendi meteor resminizin adı
                Size = new Size(30, 30),
                Location = new Point(random.Next(0, this.ClientSize.Width - 30), -30), // Ekranın üstünden başla
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pictureBoxArkaPlan.Controls.Add(yeniMeteor);
            meteorlar.Add(yeniMeteor);
            yeniMeteor.BringToFront();

            // Zorluk seviyesini ayarla
            if (puan >= 200)
            {
                meteorOlusturmaTimer.Interval = 300; // Daha sık meteor
                meteorHizi = 10; // Daha hızlı meteor
            }
            else if (puan >= 100)
            {
                meteorOlusturmaTimer.Interval = 600; // Daha sık meteor
                meteorHizi = 7; // Daha hızlı meteor
            }
            else
            {
                meteorOlusturmaTimer.Interval = 1000; // Başlangıç hızı
                meteorHizi = 5; // Başlangıç sıklığı
            }
        }

        private void HazineOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            if (!hazineAktif && random.Next(0, 25) == 0) // Olasılığı artırıldı
            {
                pictureBox1.Location = new Point(random.Next(0, this.ClientSize.Width - 30), -30);
                pictureBox1.Visible = true;
                hazineAktif = true;
            }

            if (hazineAktif)
            {
                pictureBox1.Top += 10; // Hazine sandığını aşağı indir

                if (pictureBox1.Top > this.ClientSize.Height)
                {
                    pictureBox1.Visible = false; // Görünürlüğü false yapmalısınız ki bir sonraki oluşturmada tekrar yukarıdan gelsin
                    hazineAktif = true;
                }
                else if (suruklenenNesne != null && suruklenenNesne.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    puan += 50; // Hazine sandığı ile çarpışınca 50 puan kazan
                    puanLabel.Text = $"Puan: {puan}";
                    pictureBox1.Visible = false;
                    hazineAktif = true;
                }
            }
        }

        private void MeteorTimer_Tick(object sender, EventArgs e)
        {
            List<PictureBox> yokEdilecekMeteorlar = new List<PictureBox>();

            foreach (PictureBox meteor in meteorlar)
            {
                // Meteorları hareket ettir
                meteor.Top += meteorHizi;

                // Meteor dünyaya çarptıysa
                if (meteor.Bounds.IntersectsWith(pictureBox3.Bounds))
                {
                    yokEdilecekMeteorlar.Add(meteor);
                    can--; // Can kaybı
                    UpdateCan(); // Canları güncelle

                    if (can <= 0)
                    {
                        OyunSonu("Meteor Dünya'ya çarptı!", false);
                        SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
                        return;
                    }
                }
            }

            // Yok edilecek meteorları temizle
            foreach (PictureBox meteor in yokEdilecekMeteorlar)
            {
                meteorlar.Remove(meteor);
                pictureBoxArkaPlan.Controls.Remove(meteor);
                meteor.Dispose();
            }
        }


        private void UpdateCan()
        {

            // Önceki kalp simgelerini kaldır
            foreach (PictureBox kalp in canKalpListesi)
            {
                pictureBoxArkaPlan.Controls.Remove(kalp);
                kalp.Dispose();
            }
            canKalpListesi.Clear();

            // Mevcut can sayısına göre kalp simgelerini tekrar oluştur
            for (int i = 0; i < can; i++)
            {
                PictureBox kalp = new PictureBox
                {
                    Image = Properties.Resources.kalp, // Kalp simgesi
                    Size = new Size(30, 30),
                    Location = new Point(10 + (i * 35), 35),
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                pictureBoxArkaPlan.Controls.Add(kalp);
                canKalpListesi.Add(kalp);
                kalp.BringToFront(); // Kalpler üstte olsun
            }

            // Can 0'a ulaştıysa oyunu bitir
            if (can <= 0)
            {
                OyunSonu("Tüm canlar tükendi!", false);
                SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
            }
        }




        private void Nesne_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                surukleniyor = true;
                suruklenenNesne = sender as PictureBox;
                suruklemeBaslangicNoktasi = new Point(e.X, e.Y);
                suruklenenNesne.Cursor = Cursors.Hand;
            }
        }

        private void Nesne_MouseMove(object sender, MouseEventArgs e)
        {
            if (surukleniyor && suruklenenNesne != null)
            {
                Point yeniKonum = suruklenenNesne.Location;
                yeniKonum.X += e.X - suruklemeBaslangicNoktasi.X;
                yeniKonum.Y += e.Y - suruklemeBaslangicNoktasi.Y;
                suruklenenNesne.Location = yeniKonum;
            }
        }

        private void Nesne_MouseUp(object sender, MouseEventArgs e)
        {
            if (surukleniyor && suruklenenNesne != null)
            {
                surukleniyor = false;
                suruklenenNesne.Cursor = Cursors.Default;

                // Füzeyle meteorları vurma kontrolü
                List<PictureBox> vurulanMeteorlar = new List<PictureBox>();
                foreach (PictureBox meteor in meteorlar)
                {
                    if (suruklenenNesne.Bounds.IntersectsWith(meteor.Bounds))
                    {
                        vurulanMeteorlar.Add(meteor);
                        puan += 10; // Her vuruşta 10 puan kazan
                        puanLabel.Text = $"Puan: {puan}";
                    }
                }

                // Vurulan meteorları listeden ve kontrollerden kaldır
                foreach (PictureBox meteor in vurulanMeteorlar)
                {
                    meteorlar.Remove(meteor);
                    pictureBoxArkaPlan.Controls.Remove(meteor);
                    meteor.Dispose();
                }

                // Füze ile hazine sandığı etkileşimi
                if (hazineAktif && suruklenenNesne.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    puan += 50; // Hazine sandığı ile çarpışınca 50 puan kazan
                    puanLabel.Text = $"Puan: {puan}";
                    pictureBox1.Visible = false;
                    hazineAktif = false;
                }

                suruklenenNesne = null;
            }
        }
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
        private void OyunSonu(string mesaj, bool dunyaKurtuldu)
        {
            meteorOlusturmaTimer.Stop();
            meteorTimer.Stop();
            hazineOlusturmaTimer.Stop();

            // Tüm kontrolleri gizle (etkileşimi kapat)
            foreach (Control control in pictureBoxArkaPlan.Controls)
            {
                control.Visible = false;
            }

            sonucLabel.Text = mesaj + (dunyaKurtuldu ? " Dünya kurtuldu! Toplam Puan: " + puan : " Dünya yok oldu! Toplam Puan: " + puan);
            SkoruKaydet(oyuncubilgi.kullaniciisimG, puan);
            sonucLabel.Visible = true;
            sonucLabel.Location = new Point(this.ClientSize.Width / 2 - sonucLabel.Width / 2, this.ClientSize.Height / 2 - sonucLabel.Height);

           

            cikisButton.Visible = true;
            cikisButton.Location = new Point(this.ClientSize.Width / 2 + 10, this.ClientSize.Height / 2 + 20);
            cikisButton.Text = "Çıkış";
            cikisButton.Click += CikisButton_Click;

            tekrarOynaButton.Visible = true;
            tekrarOynaButton.Location = new Point(this.ClientSize.Width / 2 - tekrarOynaButton.Width / 2, this.ClientSize.Height / 2 + 60);
            tekrarOynaButton.Text = "Tekrar Oyna";

            anaMenuButton.Visible = true;
            anaMenuButton.Location = new Point(this.ClientSize.Width / 2 - anaMenuButton.Width - 10, this.ClientSize.Height / 2 + 20);
            anaMenuButton.Text = "Ana Menü";
            anaMenuButton.Click += anaMenuButton_Click_1;
        }

        private void TekrarOynaButton_Click(object sender, EventArgs e)
        {
            // Oyunu yeniden başlat
            puan = 0;
            can = 3;
            puanLabel.Text = "Puan: 0";
            pictureBox1.Text = "Can: 3";
            meteorHizi = 5;
            meteorOlusturmaTimer.Interval = 1000;
            meteorOlusturmaTimer.Start();
            meteorTimer.Start();
            hazineOlusturmaTimer.Start();

            // Tüm eski meteorları temizle
            foreach (var meteor in meteorlar)
            {
                pictureBoxArkaPlan.Controls.Remove(meteor);
                meteor.Dispose();
            }
            meteorlar.Clear();

            pictureBox1.Visible = false;
            hazineAktif = false;

            // Görselleri ve kontrolleri tekrar göster
            foreach (Control control in pictureBoxArkaPlan.Controls)
            {
                control.Visible = true;
            }

            sonucLabel.Visible = false;
            anaMenuButton.Visible = false;
            cikisButton.Visible = false;
            tekrarOynaButton.Visible = false;

            bombayonet.Start();
            puan = 0;
            puanLabel.Text = "Puan: 0";
        }


        private void anaMenuButton_Click_1(object sender, EventArgs e)
        {
            Form7 anaMenu = new Form7();
            this.Hide();
            anaMenu.Show();
            this.Close();

        }

        private void CikisButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxArkaPlan_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {


        }
        private void PuanAzalt(int miktar)
        {
            puan -= miktar;
            if (puan < 0) puan = 0;
            puanLabel.Text = $"Puan: {puan}";
        }
        public int arkaindex;
        private void Form9_Load(object sender, EventArgs e)
        {
            pictureBox3.Image = secilen.Gezegen;
            switch (arkaindex)
            {
                case 1:
                    pictureBoxArkaPlan.BackgroundImage = Properties.Resources.sat;
                    pictureBoxArkaPlan.BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBoxArkaPlan.BackColor = Color.Transparent;
                    break;
                case 2:
                    pictureBoxArkaPlan.BackgroundImage = Properties.Resources.jup;
                    pictureBoxArkaPlan.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case 3:
                    pictureBoxArkaPlan.BackgroundImage = Properties.Resources.ven;
                    pictureBoxArkaPlan.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case 4:
                    pictureBoxArkaPlan.BackgroundImage = Properties.Resources.bizimgezegen;
                    pictureBoxArkaPlan.BackgroundImageLayout = ImageLayout.Stretch;
                    break;

                default:
                    pictureBoxArkaPlan.BackColor = Color.Black;
                    break;
            }

        }

     
    }
}