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
using System.IO;

namespace Günessistemioyun
{
    public partial class ProfilSayfası : Form
    {
        private readonly string connectionString = @"server=(localdb)\Salih;database=oyun;integrated security=true;";
        AdminPaneliClass admin = new AdminPaneliClass();
        private bool isCurrentUser = false;

        public ProfilSayfası()
        {
            InitializeComponent();
            SetupControls();
            ConfigureForCurrentUser();
        }

        private void ConfigureForCurrentUser()
        {
            // Admin kontrolleri varsayılan olarak gizle
            button2.Visible = false; // Ekle
            button8.Visible = false; // Sil
            button7.Visible = false; // CSV Export
           

            // Sadece kendi bilgilerini göster
            LoadCurrentUserInfo();
        }

        private void LoadCurrentUserInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Username, FullName, pass, e_mail, Numara, Avatar FROM oyun WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", oyuncubilgi.kullaniciisimG);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isCurrentUser = true;
                                textBox1.Text = reader["Username"].ToString();
                                textBox2.Text = reader["pass"].ToString();
                                textBox3.Text = reader["FullName"].ToString();
                                textBox4.Text = reader["e_mail"].ToString();
                                textBox5.Text = reader["Numara"].ToString();

                                if (reader["Avatar"] != DBNull.Value)
                                {
                                    byte[] avatarBytes = (byte[])reader["Avatar"];
                                    using (MemoryStream ms = new MemoryStream(avatarBytes))
                                    {
                                        pbAvatar.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void SetupControls()
        {
            // Button görselleri ve işlevleri ayarla
            button3.Click += button3_Click; // Güncelle butonu
           
            button5.Click += btnChangeAvatar_Click; // Avatar değiştir butonu
            button1.Click += button6_Click; // Çıkış butonu

            // DataGridView ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;

            // TextBox ayarları
            textBox1.ReadOnly = true; // Kullanıcı adı değiştirilemez
            textBox2.UseSystemPasswordChar = true; // Şifre gizli
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!isCurrentUser)
            {
                MessageBox.Show("Sadece kendi bilgilerinizi güncelleyebilirsiniz.");
                return;
            }

            try
            {
                // Güncelleme işlemi için gerekli kontroller
                if (string.IsNullOrWhiteSpace(textBox3.Text) || 
                    string.IsNullOrWhiteSpace(textBox4.Text) || 
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                    return;
                }

                // E-posta formatı kontrolü
                if (!IsValidEmail(textBox4.Text))
                {
                    MessageBox.Show("Lütfen geçerli bir e-posta adresi giriniz.");
                    return;
                }

                // Telefon numarası kontrolü
                if (!IsValidPhoneNumber(textBox5.Text))
                {
                    MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz (örn: 5XX XXX XX XX)");
                    return;
                }

                admin.KullaniciGuncelle(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, pbAvatar.Image);
                MessageBox.Show("Bilgileriniz başarıyla güncellendi.");
                LoadCurrentUserInfo(); // Bilgileri yeniden yükle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Türkiye telefon numarası formatı kontrolü (5XX XXX XX XX)
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^5[0-9]{2}[0-9]{3}[0-9]{2}[0-9]{2}$");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Temizle butonu sadece mevcut kullanıcı bilgilerini yeniden yükler
            LoadCurrentUserInfo();
        }

        private void ProfilSayfası_Load(object sender, EventArgs e)
        {
            LoadUserScores();
        }

        private void LoadUserScores()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Kullanıcının kendi skorları
                    string userQuery = @"SELECT 
                        OyuncuAdi, 
                        Skor, 
                        Tarih,
                        CASE 
                            WHEN Tarih >= DATEADD(day, -1, GETDATE()) THEN 'Günlük'
                            WHEN Tarih >= DATEADD(month, -1, GETDATE()) THEN 'Aylık'
                            ELSE 'Yıllık'
                        END as Periyot
                        FROM SkorTablosu 
                        WHERE OyuncuAdi = @KullaniciAdi 
                        ORDER BY Tarih DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(userQuery, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@KullaniciAdi", oyuncubilgi.kullaniciisimG);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        // İstatistikleri hesapla ve göster
                        CalculateAndDisplayStatistics(dt);
                    }

                    // Global skorlar
                    string globalQuery = @"SELECT TOP 10 
                        OyuncuAdi, 
                        Skor, 
                        Tarih,
                        CASE 
                            WHEN Tarih >= DATEADD(day, -1, GETDATE()) THEN 'Günlük'
                            WHEN Tarih >= DATEADD(month, -1, GETDATE()) THEN 'Aylık'
                            ELSE 'Yıllık'
                        END as Periyot
                        FROM SkorTablosu 
                        ORDER BY Skor DESC";
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(globalQuery, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Skorlar yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void CalculateAndDisplayStatistics(DataTable scores)
        {
            try
            {
                // Günlük, aylık ve yıllık en yüksek skorları hesapla
                var dailyScores = scores.AsEnumerable()
                    .Where(r => Convert.ToDateTime(r["Tarih"]) >= DateTime.Now.AddDays(-1))
                    .Select(r => Convert.ToInt32(r["Skor"]));

                var monthlyScores = scores.AsEnumerable()
                    .Where(r => Convert.ToDateTime(r["Tarih"]) >= DateTime.Now.AddMonths(-1))
                    .Select(r => Convert.ToInt32(r["Skor"]));

                var yearlyScores = scores.AsEnumerable()
                    .Where(r => Convert.ToDateTime(r["Tarih"]) >= DateTime.Now.AddYears(-1))
                    .Select(r => Convert.ToInt32(r["Skor"]));

                // İstatistikleri göster (Label'ları forma eklemeniz gerekiyor)
                lblGunlukEnYuksek.Text = $"Günlük En Yüksek: {(dailyScores.Any() ? dailyScores.Max() : 0)}";
                lblAylikEnYuksek.Text = $"Aylık En Yüksek: {(monthlyScores.Any() ? monthlyScores.Max() : 0)}";
                lblYillikEnYuksek.Text = $"Yıllık En Yüksek: {(yearlyScores.Any() ? yearlyScores.Max() : 0)}";

                // Ortalama skorları hesapla
                lblGunlukOrtalama.Text = $"Günlük Ortalama: {(dailyScores.Any() ? dailyScores.Average() : 0):F1}";
                lblAylikOrtalama.Text = $"Aylık Ortalama: {(monthlyScores.Any() ? monthlyScores.Average() : 0):F1}";
                lblYillikOrtalama.Text = $"Yıllık Ortalama: {(yearlyScores.Any() ? yearlyScores.Average() : 0):F1}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("İstatistikler hesaplanırken hata oluştu: " + ex.Message);
            }
        }

        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            if (!isCurrentUser)
            {
                MessageBox.Show("Sadece kendi avatarınızı değiştirebilirsiniz.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Avatar Seç";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAvatarDegistir_Click(object sender, EventArgs e)
        {
            if (isCurrentUser)
            {
                btnChangeAvatar_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Sadece kendi avatarınızı değiştirebilirsiniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form7().ShowDialog();
            this.Close();

        }
    }
}
