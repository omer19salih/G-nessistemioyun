using System;
using System.Text;

namespace Günessistemioyun
{
    // Bu sınıf yalnızca bir yerde olacak, başka bir yerde tanımlanmadığından emin olun
    public static class Cryptology
    {
        // Şifreleme işlemi
        public static string Encryption(string text, int key)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char item in text)
            {
                encryptedText.Append(Convert.ToChar(item + key));
            }
            return encryptedText.ToString();
        }

       
  
    }
}