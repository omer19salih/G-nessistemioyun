using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;



namespace Günessistemioyun
{
    public partial class Form5 : Form
    {
        string connectionString = @"server=(localdb)\Salih;database=oyun;integrated security=true;";
        string generatedCode = ""; // Rastgele oluşturulan doğrulama kodu


        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;

            // Veritabanında e-posta adresinin var olup olmadığını kontrol et
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM oyun WHERE e_mail = @e_mail";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@e_mail", email);

                        int userCount = (int)cmd.ExecuteScalar();
                        if (userCount == 0)
                        {
                            MessageBox.Show("Bu e-posta adresiyle kayıtlı bir kullanıcı bulunmamaktadır.");
                            return;
                        }
                    }

                    // 6 haneli rastgele bir doğrulama kodu oluştur
                    Random rand = new Random();
                    generatedCode = rand.Next(100000, 1000000).ToString();

                    // E-posta içeriğini oluştur
                    string subject = "Şifre Sıfırlama Kodu";
                    string body = $"Şifrenizi sıfırlamak için doğrulama kodunuz: {generatedCode}";

                    // Gmail SMTP sunucusu ile e-posta gönderimi
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("yigitkuru064@gmail.com", "vqsl pdkd omnx kteu"),
                        EnableSsl = true
                    };

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("yigitkuru064@gmail.com"),
                        Subject = subject,
                        Body = body
                    };
                    mailMessage.To.Add(email);

                    // E-posta gönder
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Doğrulama kodu e-posta adresinize gönderildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("E-posta gönderilemedi: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string enteredCode = textBox4.Text;
            

            // Kullanıcıdan girilen doğrulama kodu ile oluşturduğumuz kodu karşılaştır
            if (enteredCode != generatedCode)
            {
                MessageBox.Show("Doğrulama kodu hatalı.");
                return;
            }
            else
            {
                MessageBox.Show("Kod Doğrulandı");
                ShowLabels();
            }

            
        }

        private void label9_Click(object sender, EventArgs e)
        {



        }

        public void ShowLabels()
        {
            textBox5.Visible = true;
            label9.Visible = true;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            string newPassword = textBox5.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE oyun SET pass = @pass WHERE e_mail = @e_mail";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pass", newPassword);
                        cmd.Parameters.AddWithValue("@e_mail", email);

                        int rowsAffected = cmd.ExecuteNonQuery(); // ← Tanım burada

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Şifreniz başarıyla güncellenmiştir.");

                            // E-posta gönderimi
                            string subject = "Şifre Başarıyla Güncellendi";
                            string body = "Merhaba,\n\nŞifreniz başarıyla değiştirilmiştir. Eğer bu işlemi siz yapmadıysanız, lütfen hemen bizimle iletişime geçin.";

                            try
                            {
                                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                                {
                                    Port = 587,
                                    Credentials = new NetworkCredential("yigitkuru064@gmail.com", "vqsl pdkd omnx kteu"),
                                    EnableSsl = true
                                };

                                MailMessage mailMessage = new MailMessage
                                {
                                    From = new MailAddress("yigitkuru064@gmail.com"),
                                    Subject = subject,
                                    Body = body
                                };
                                mailMessage.To.Add(email);

                                smtpClient.Send(mailMessage);
                                MessageBox.Show("Şifre değişikliği bilgisi e-posta adresinize gönderildi.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Bilgilendirme e-postası gönderilemedi: " + ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Şifre güncellenemedi.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
