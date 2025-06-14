using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Günessistemioyun
{
    public partial class Form4 : Form
    {
        private TabControl tabControl;
        private TabPage tabKullanicilar;
        private TabPage tabMesajlar;
        private DataGridView dgvMesajlar;
        private TextBox txtCevap;
        private Button btnCevapGonder;
        private int seciliMesajId = -1;

        public Form4()
        {
            InitializeComponent();
            InitializeTabs();
            LoadMesajlar();
        }

        private void InitializeTabs()
        {
            // TabControl oluştur
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Kullanıcılar sekmesi
            tabKullanicilar = new TabPage("Kullanıcılar");
            tabKullanicilar.Controls.Add(dataGridView1);
            tabKullanicilar.Controls.Add(textBox1);
            tabKullanicilar.Controls.Add(textBox2);
            tabKullanicilar.Controls.Add(textBox3);
            tabKullanicilar.Controls.Add(textBox4);
            tabKullanicilar.Controls.Add(textBox5);
            tabKullanicilar.Controls.Add(pbAvatar);
            tabKullanicilar.Controls.Add(button1);
            tabKullanicilar.Controls.Add(button2);
            tabKullanicilar.Controls.Add(button3);
            tabKullanicilar.Controls.Add(button4);
            tabKullanicilar.Controls.Add(button5);
            tabKullanicilar.Controls.Add(button6);
            tabKullanicilar.Controls.Add(button7);
            tabKullanicilar.Controls.Add(btnYetkiVer);
            tabKullanicilar.Controls.Add(btnEngelle);

            // Mesajlar sekmesi
            tabMesajlar = new TabPage("Mesajlar");
            InitializeMesajlarTab();

            // Tabları ekle
            tabControl.TabPages.Add(tabKullanicilar);
            tabControl.TabPages.Add(tabMesajlar);

            // TabControl'ü forma ekle
            this.Controls.Add(tabControl);
        }

        private void InitializeMesajlarTab()
        {
            // DataGridView oluştur
            dgvMesajlar = new DataGridView();
            dgvMesajlar.Dock = DockStyle.Fill;
            dgvMesajlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMesajlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMesajlar.MultiSelect = false;
            dgvMesajlar.AllowUserToAddRows = false;
            dgvMesajlar.AllowUserToDeleteRows = false;
            dgvMesajlar.ReadOnly = true;
            dgvMesajlar.CellClick += DgvMesajlar_CellClick;

            // Cevap paneli
            Panel cevapPanel = new Panel();
            cevapPanel.Dock = DockStyle.Bottom;
            cevapPanel.Height = 100;

            // Cevap TextBox
            txtCevap = new TextBox();
            txtCevap.Multiline = true;
            txtCevap.Dock = DockStyle.Fill;
            txtCevap.Enabled = false;

            // Cevap gönder butonu
            btnCevapGonder = new Button();
            btnCevapGonder.Text = "Cevap Gönder";
            btnCevapGonder.Dock = DockStyle.Bottom;
            btnCevapGonder.Height = 30;
            btnCevapGonder.Enabled = false;
            btnCevapGonder.Click += BtnCevapGonder_Click;

            // Panel'e kontrolleri ekle
            cevapPanel.Controls.Add(txtCevap);
            cevapPanel.Controls.Add(btnCevapGonder);

            // Tab'a kontrolleri ekle
            tabMesajlar.Controls.Add(dgvMesajlar);
            tabMesajlar.Controls.Add(cevapPanel);
        }

        private void LoadMesajlar()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT ID, GonderenKullanici, GonderenEmail, Mesaj, Tarih, 
                                   Cevap, CevapTarihi, Cevaplandi 
                                   FROM Mesajlar 
                                   ORDER BY Tarih DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvMesajlar.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesajlar yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void DgvMesajlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMesajlar.Rows[e.RowIndex];
                seciliMesajId = Convert.ToInt32(row.Cells["ID"].Value);
                bool cevaplandi = Convert.ToBoolean(row.Cells["Cevaplandi"].Value);

                txtCevap.Enabled = !cevaplandi;
                btnCevapGonder.Enabled = !cevaplandi;

                if (cevaplandi)
                {
                    txtCevap.Text = row.Cells["Cevap"].Value?.ToString() ?? "";
                }
                else
                {
                    txtCevap.Text = "";
                }
            }
        }

        private void BtnCevapGonder_Click(object sender, EventArgs e)
        {
            if (seciliMesajId == -1)
            {
                MessageBox.Show("Lütfen bir mesaj seçin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCevap.Text))
            {
                MessageBox.Show("Lütfen bir cevap yazın.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Mesajı güncelle
                    string updateQuery = @"UPDATE Mesajlar 
                                         SET Cevap = @cevap, 
                                             CevapTarihi = @cevapTarihi, 
                                             Cevaplandi = 1 
                                         WHERE ID = @id";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cevap", txtCevap.Text);
                        command.Parameters.AddWithValue("@cevapTarihi", DateTime.Now);
                        command.Parameters.AddWithValue("@id", seciliMesajId);
                        command.ExecuteNonQuery();
                    }

                    // Kullanıcının email adresini al
                    string emailQuery = "SELECT GonderenEmail FROM Mesajlar WHERE ID = @id";
                    string gonderenEmail;
                    using (SqlCommand command = new SqlCommand(emailQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", seciliMesajId);
                        gonderenEmail = command.ExecuteScalar()?.ToString();
                    }

                    // Email gönder
                    if (!string.IsNullOrEmpty(gonderenEmail))
                    {
                        SendEmail(gonderenEmail, txtCevap.Text);
                    }

                    MessageBox.Show("Cevap başarıyla gönderildi.");
                    LoadMesajlar();
                    txtCevap.Text = "";
                    txtCevap.Enabled = false;
                    btnCevapGonder.Enabled = false;
                    seciliMesajId = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cevap gönderilirken hata oluştu: " + ex.Message);
            }
        }

        private void SendEmail(string toEmail, string cevap)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("your-email@example.com"); // SMTP ayarlarınıza göre değiştirin
                    mail.To.Add(toEmail);
                    mail.Subject = "Öneri/Şikayet Mesajınıza Cevap";
                    mail.Body = $"Mesajınıza verilen cevap:\n\n{cevap}";
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // SMTP ayarlarınıza göre değiştirin
                    {
                        smtp.Credentials = new NetworkCredential("your-email@gmail.com", "your-password"); // SMTP ayarlarınıza göre değiştirin
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email gönderilirken hata oluştu: " + ex.Message);
            }
        }

        AdminPaneliClass admin = new AdminPaneliClass(); //Class Tanımını Yaptım admin olarak tanımladım

        private static string connectionString = @"server=(localdb)\Salih;database=oyun;integrated security=true;";

        private void Listele()
        {
            dataGridView1.DataSource = admin.Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek için bir satır seçin.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM oyun WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Kullanıcı silindi.");
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri silinirken hata oluştu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.KullaniciEkle(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, pbAvatar.Image); //admin clasındaki Kullanıcı ekle metodunu çağırdım parametreleri textboxlarla doldurdum
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin.KullaniciGuncelle(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, pbAvatar.Image);//admin clasındaki Kullanıcı Güncelle metodunu çağırdım parametreleri textboxlarla doldurdum
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Listele();
            dataGridView1.Columns["Avatar"].Visible = false;
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Tüm Dosyalar|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate the TextBoxes with the selected row data
                textBox1.Text = row.Cells[0].Value.ToString(); // Assuming Username is in column 0
                textBox2.Text = row.Cells[1].Value.ToString(); // Assuming pass is in column 1
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString(); // Assuming e_mail is in column 3
                textBox5.Text = row.Cells[4].Value.ToString(); // Assuming Numara is in column 4
            }
            byte[] avatarBytes = dataGridView1.CurrentRow.Cells["Avatar"].Value as byte[];
            if (avatarBytes != null)
            {
                using (MemoryStream ms = new MemoryStream(avatarBytes))
                {
                    pbAvatar.Image = Image.FromStream(ms); // Avatar'ı PictureBox'a yükle
                }
            }
            else
            {
                pbAvatar.Image = null; // Avatar yoksa boş göster
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ExportToCSV()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV Dosyası|*.csv";
                    saveFileDialog.Title = "Verileri Dışa Aktar";
                    saveFileDialog.FileName = "KullaniciListesi.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                        {
                            // Yeni başlıkları yaz
                            string header = "Username,FullName,pass,e_mail,Numara";
                            sw.WriteLine(header);

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string query = "SELECT Username, FullName, pass, e_mail, Numara FROM oyun";
                                using (SqlCommand command = new SqlCommand(query, connection))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string line = $"{reader["Username"]},{reader["FullName"]},{reader["pass"]},{reader["e_mail"]},{reader["Numara"]}";
                                        sw.WriteLine(line);
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Veriler başarıyla dışa aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dışa aktarma sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        private void btnYetkiVer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
                return;
            }

            string username = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE oyun SET IsAdmin = 1 WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"{username} adlı kullanıcıya admin yetkisi verildi.");
            Listele();
        }

        private void btnEngelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
                return;
            }

            string username = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE oyun SET IsBlocked = 1 WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"{username} adlı kullanıcı engellendi.");
            Listele();
        }
    }
}
