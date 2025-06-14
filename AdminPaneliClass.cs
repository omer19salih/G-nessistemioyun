using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Günessistemioyun
{
    internal class AdminPaneliClass
    {
        private readonly string connectionString = @"server=(localdb)\Salih;database=oyun;integrated security=true;";

        // Kullanıcıları listeleme
        public DataTable Listele()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM oyun";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Veri listelenirken hata oluştu: " + ex.Message);
            }
        }

        // Kullanıcı ekleme
        public void KullaniciEkle(string username, string pass, string fullName, string email, string numara, Image avatar)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO oyun (Username, pass, FullName, e_mail, Numara, Avatar) VALUES (@Username, @pass, @FullName, @e_mail, @Numara, @Avatar)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@FullName", fullName);
                        command.Parameters.AddWithValue("@e_mail", email);
                        command.Parameters.AddWithValue("@Numara", numara);

                        if (avatar != null)
                        {
                            command.Parameters.AddWithValue("@Avatar", ImageToByteArray(avatar));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Avatar", DBNull.Value);
                        }
                        
                        command.ExecuteNonQuery();
                        Listele();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenirken hata oluştu: " + ex.Message);
            }
        }

        // Kullanıcı güncelleme
        public void KullaniciGuncelle(string username, string pass, string fullName, string email, string numara, Image avatar)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE oyun SET pass = @pass, FullName = @FullName, e_mail = @e_mail, Numara = @Numara, Avatar = @Avatar WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@FullName", fullName);
                        command.Parameters.AddWithValue("@e_mail", email);
                        command.Parameters.AddWithValue("@Numara", numara);

                        if (avatar != null)
                        {
                            command.Parameters.AddWithValue("@Avatar", ImageToByteArray(avatar));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Avatar", DBNull.Value);
                        }
                       ;
                        command.ExecuteNonQuery();
                        
                    }
                }
                Listele();
                MessageBox.Show(username + "güncellendi");
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenirken hata oluştu: " + ex.Message);
            }
        }

        // Kullanıcı silme
        public void KullaniciSil(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM oyun WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        Listele();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı silinirken hata oluştu: " + ex.Message);
            }
        }

        // Görseli byte dizisine çevirme
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

    }
}
