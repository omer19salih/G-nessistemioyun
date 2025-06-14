using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Günessistemioyun
{
    public partial class dileksikayet : Form
    {
        private string connectionString = @"Data Source=(localdb)\Salih;Initial Catalog=oyun;Integrated Security=True";

        public dileksikayet()
        {
            InitializeComponent();
            this.Load += new EventHandler(dileksikayet_Load);
            this.buttonGonder.Click += new EventHandler(buttonGonder_Click);
        }

        private void dileksikayet_Load(object sender, EventArgs e)
        {
            // oyuncubilgi static üyelere erişilecek
            textBoxAd.Text = oyuncubilgi.kullaniciisimG;
           
        }

        private void buttonGonder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMesaj.Text))
            {
                MessageBox.Show("Lütfen mesajınızı yazın.");
                return;
            }

            try
            {
                // Mesajı veritabanına kaydet (Email sütunu olmadan)
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // GonderenEmail sütununu INSERT sorgusundan kaldır
                    string query = "INSERT INTO Mesajlar (GonderenKullanici, Mesaj, Tarih) VALUES (@gonderenKullanici, @mesaj, @tarih)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Oyuncu bilgisine static erişim
                        command.Parameters.AddWithValue("@gonderenKullanici", oyuncubilgi.kullaniciisimG);
                        command.Parameters.AddWithValue("@mesaj", textBoxMesaj.Text);
                        command.Parameters.AddWithValue("@tarih", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Mesajınız başarıyla gönderildi.");
                textBoxMesaj.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesaj gönderilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
