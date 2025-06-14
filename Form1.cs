using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;

namespace Günessistemioyun
{
    public partial class Form1 : Form
    {

       
        private AudioFileReader audioFileReader;
        private WaveOutEvent waveOutEvent;
        private bool isPlaying;

        public static SqlConnection connection = new SqlConnection(@"server=(localdb)\Salih;database=oyun;integrated security=true;");
        private ToolTip toolTip1 = new ToolTip();
        public Form1()
        {
            InitializeComponent();
            waveOutEvent = new WaveOutEvent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Eğer "Beni Hatırla" daha önce işaretlenmişse, kullanıcı bilgilerini doldur
            if (Properties.Settings.Default.RememberMe)
            {
                textBox1.Text = Properties.Settings.Default.Username;
                textBox2.Text = Properties.Settings.Default.Password;
                textBox2.PasswordChar = '*';
                checkBox1.Checked = true;
            }
            // ToolTip'leri ayarla
            toolTip1.SetToolTip(textBox1, "Kullanıcı adınızı girin.");
            toolTip1.SetToolTip(textBox2, "Şifrenizi girin.");
            toolTip1.SetToolTip(button1, "Giriş yap.");
            toolTip1.SetToolTip(linkLabel2, "Kayıt ol.");
            toolTip1.SetToolTip(checkBox1, "Bilgileri hatırla.");
          
            toolTip1.SetToolTip(linkLabel1, "Şifremi unuttum.");

            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Kullanıcı İsmi")
                textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = "Kullanıcı İsmi";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Şifre")
            {
                textBox2.Text = "";
                textBox2.PasswordChar = '*';
            }
        }

        char? none = null;
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Şifre";
                textBox2.PasswordChar = Convert.ToChar(none);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Kullanıcı_İsmi = textBox1.Text.Trim();
            string Şifre = textBox2.Text.Trim();

            if (Kullanıcı_İsmi == "Kullanıcı İsmi" || Şifre == "Şifre" || Kullanıcı_İsmi == "" || Şifre == "")
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    connection.Open();
                    string query = "SELECT username, pass, IsAdmin FROM oyun WHERE username = @username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", Kullanıcı_İsmi);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string dbPassword = reader["pass"].ToString().TrimEnd();
                        bool isAdmin = false;
                        if (reader["IsAdmin"] != DBNull.Value)
                        {
                            isAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        }

                        if (Şifre == dbPassword)
                        {
                            MessageBox.Show("Başarıyla giriş yaptınız!", "Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            oyuncubilgi.kullaniciisimG = Kullanıcı_İsmi;
                            oyuncubilgi.isAdmin = isAdmin;
                            // Eğer "Beni Hatırla" işaretliyse bilgileri kaydet
                            if (checkBox1.Checked)
                            {
                                Properties.Settings.Default.Username = Kullanıcı_İsmi;
                                Properties.Settings.Default.Password = Şifre;
                                Properties.Settings.Default.RememberMe = true;
                            }
                            else
                            {
                                Properties.Settings.Default.Username = "";
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.RememberMe = false;
                            }
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MessageBox.Show("Şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Beni Hatırla seçildiğinde bir işlem yapmaya gerek yok, giriş yaparken zaten kontrol ediliyor.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Form4'ü oluştur
            Form4 form4 = new Form4();

            // Form4'ü aç
            form4.Show();

            // Mevcut formu kapatmak isterseniz (isteğe bağlı):
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Form5().ShowDialog();
            this.Hide();
        }

       

        private void hakımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(); // Create an instance of Form6
            form6.Show(); // Show Form6

        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(); // Create an instance of Form6
            form6.Show(); // Show Form6
        }

        private void bizeUlaşınToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(); // Create an instance of Form6
            form6.Show(); // Show Form6
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                waveOutEvent.Stop();
                isPlaying = false;
            }

        }
    }
}
