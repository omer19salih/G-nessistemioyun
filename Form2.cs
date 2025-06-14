using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;


namespace Günessistemioyun
{
    public partial class Form2 : Form
    {  
        SqlConnection connection=Form1.connection;
        private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Kullanıcı_İsmi")

            { txtUsername.Text = ""; }


        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")

            { txtUsername.Text = "Kullanıcı_İsmi"; }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Şifre")

            {
                txtNewPassword.Text = "";

                txtNewPassword.PasswordChar = '*';

            }
        }
        char? none = null;
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "")

            {
                txtNewPassword.Text = "Şifre";

                txtNewPassword.PasswordChar = Convert.ToChar(none);

            }

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "Şifre Tekrar")

            {
                txtConfirmPassword.Text = "";

                txtConfirmPassword.PasswordChar = '*';


            }
        }
        
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "")

            {
                txtConfirmPassword.Text = "Şifre Tekrar";

                txtConfirmPassword.PasswordChar = Convert.ToChar(none);

            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "E -mail")

            { txtEmail.Text = ""; }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")

            { txtEmail.Text = "E -mail"; }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (txtPhone.Text == "Numara")

            { txtPhone.Text = ""; }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text == "")

            { txtPhone.Text = "Numara"; }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Ad_Soyad")

            { txtFullName.Text = ""; }

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")

            { txtFullName.Text = "Ad_Soyad"; }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text == "Doğrulama_kodunu_gir ")

            { txtVerificationCode.Text = ""; }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text == "")

            { txtVerificationCode.Text = "Doğrulama_kodunu_gir "; }
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            // ToolTip'leri ayarla
            toolTip1.SetToolTip(txtUsername, "Kullanıcı adınızı girin.");
            toolTip1.SetToolTip(txtFullName, "Adınızı ve soyadınızı girin.");
            toolTip1.SetToolTip(txtEmail, "E-posta adresinizi girin.");
            toolTip1.SetToolTip(txtPhone, "Telefon numaranızı girin.");
            toolTip1.SetToolTip(pbAvatar, "Avatarınızı seçmek için tıklayın.");
 
            toolTip1.SetToolTip(txtNewPassword, "Yeni şifrenizi girin.");
            toolTip1.SetToolTip(txtConfirmPassword, "Yeni şifrenizi tekrar girin.");
          
            toolTip1.SetToolTip(button4, "avatar sec");
            toolTip1.SetToolTip(chkShowPassword, "Şifreyi göster/gizle.");
            toolTip1.SetToolTip(button5, "Giriş sayfasına dön.");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            // Kullanıcıyı başarıyla kaydettikten ve doğrulama mailini gönderdikten sonra:
            SendVerificationEmaill(email, fullName);
            SendWelcomeEmail(email, fullName); // <-- Bu satırı ekleyin




            byte[] avatarBytes = null;

            if (pbAvatar.Image != null)
            {
                avatarBytes = ImageToByteArray(pbAvatar.Image);
            }


            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            string connectionString = "server=(localdb)\\Salih;database=oyun;integrated security=true;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                  
                    string checkQuery = "SELECT COUNT(*) FROM dbo.oyun WHERE Username = @Username";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Username", username);
                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        MessageBox.Show("Bu kullanıcı zaten kayıtlı. Lütfen yeni bir kullanıcı adı girin.");
                        return; 
                    }

                  
                    string checkEmailQuery = "SELECT COUNT(*) FROM dbo.oyun WHERE e_mail = @e_mail";
                    SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, conn);
                    checkEmailCmd.Parameters.AddWithValue("@e_mail", email);
                    int emailExists = Convert.ToInt32(checkEmailCmd.ExecuteScalar());

                    if (emailExists > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı. Lütfen başka bir e-posta adresi kullanın.");
                        return;
                    }


                    string query = "INSERT INTO dbo.oyun (Username, FullName, e_mail, Numara, IsVerified) VALUES (@Username, @FullName, @e_mail, @Numara, 0)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Parametreleri ekliyoruz
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@e_mail", email);
                    cmd.Parameters.AddWithValue("@Numara", phone);
                    cmd.Parameters.AddWithValue("@Avatar", avatarBytes ?? (object)DBNull.Value);

                    if (pbAvatar.Image == null)
                    {
                        MessageBox.Show("Lütfen bir fotoğraf seçin.");
                        return;
                    }

                    cmd.ExecuteNonQuery(); // Kullanıcıyı veritabanına ekle

                    MessageBox.Show("Kullanıcı başarıyla kaydedildi. Doğrulama e-postası gönderildi.");

                    // Doğrulama e-postası gönderme
                    SendVerificationEmaill(email, fullName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }//BtnRegister


        private void btnSelectAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
               // Filter = "Image Files|.jpg;.jpeg;.png;.bmp;*.gif"
                Filter = "Tüm Dosyalar|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
            }
        }


        public void SendVerificationEmaill(string email, string fullName)
        {
            string[] nameParts = fullName.Split(' ');
            string verificationCode = nameParts[0].Substring(0, 3) + nameParts[nameParts.Length - 1].Substring(nameParts[nameParts.Length - 1].Length - 2) + "2025";

            MailMessage mail = new MailMessage("example@gmail.com", email);
            mail.Subject = "Hesap Doğrulama Kodu";
            mail.Body = $"Merhaba {nameParts[0]},\n\nDoğrulama kodunuz: {verificationCode}\n\nKodunuzu kullanarak şifrenizi belirleyebilirsiniz.";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("yigitkuru064@gmail.com", "vqsl pdkd omnx kteu");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                MessageBox.Show("Doğrulama e-postası gönderildi.");
                dogrulamaPanel.Visible = true; 
                ShowPanelInCenter(dogrulamaPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderilemedi: " + ex.Message);
                Console.WriteLine("Hata Detayı: " + ex.ToString());
            }
        }

        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            string enteredCode = txtVerificationCode.Text; 
            string username = txtUsername.Text;

            string[] nameParts = txtFullName.Text.Split(' ');
            string expectedCode = nameParts[0].Substring(0, 3) + nameParts[nameParts.Length - 1].Substring(nameParts[nameParts.Length - 1].Length - 2) + "2025";

            if (enteredCode == expectedCode)
            {
                MessageBox.Show("Kod doğru! Şifrenizi belirleyebilirsiniz.");
                ShowPasswordSetPage();
                ShowPanelInCenter(sifreayarlaPanel);

            }
            else
            {
                MessageBox.Show("Doğrulama kodu yanlış. Lütfen tekrar deneyin.");
            }
        }

        private void ShowPasswordSetPage()
        {
            sifreayarlaPanel.Visible = true;
            dogrulamaPanel.Visible = false;
        }


        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            string password = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text; 

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Lütfen şifre giriniz.");
                return;
            }

           
            if (password != confirmPassword)
            {
                MessageBox.Show("Şifreler eşleşmiyor. Lütfen KOntrol Edin.");
                return;
            }
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Şifre en az bir büyük harf içermelidir.");
                return;
            }
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]"))
            {
                MessageBox.Show("Şifre en az bir özel karakter içermelidir.");
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Şifre en az 8 karakter olmalıdır.");
                return;
            }

            
            string username = txtUsername.Text;
            string connectionString = "server=(localdb)\\Salih;database=oyun;integrated security=true;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE dbo.oyun SET pass = @pass, IsVerified = 1 WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@pass", password);  

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Şifreniz başarıyla belirlendi. Giriş sayfasındasn giriş yapabilirsiniz.");

                   
                    ShowLoginPage();  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        public void SendWelcomeEmail(string email, string fullName)
        {
            MailMessage mail = new MailMessage("example@gmail.com", email);
            mail.Subject = "Kayıt Başarılı!";
            mail.Body = $"Merhaba {fullName},\n\nSisteme başarıyla kayıt oldunuz. Güneş Sistemi Oyunu'na hoş geldiniz!";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("yigitkuru064@gmail.com", "vqsl pdkd omnx kteu");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Hoş geldiniz e-postası gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hoş geldiniz e-postası gönderilemedi: " + ex.Message);
            }
        }
        private void ShowLoginPage()
        {

            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void ShowPanelInCenter(Panel panel)
        {
            
            panel.Left = (this.ClientSize.Width - panel.Width) / 2;
            panel.Top = (this.ClientSize.Height - panel.Height) / 2;
        }

        public void GoMenu()
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GoMenu();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                // Şifreyi göster
                txtNewPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
                chkShowPassword.Text = "Gizle";
            }
            else
            {
                // Şifreyi gizle
                txtNewPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
                chkShowPassword.Text = "Göster";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sifreayarlaPanel_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
