using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsClient.Helpers
{
    public static class PasswordHasher
    {

        public static int KeyLength = 15;

        //Encryption
        public static string Encrypt(string password)
        {
            Random random = new Random();
            var rString = "";
            for (var i = 0; i < KeyLength; i++)
            {
                rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }

            if (string.IsNullOrEmpty(password)) return "";

            password = rString + password;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        //Decryption
        public static string Decrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            var base64fEncodeBytes = Convert.FromBase64String(password);
            var result = Encoding.UTF8.GetString(base64fEncodeBytes);
            result = result.Substring(KeyLength, result.Length - KeyLength);
            return result;
        }

    }
}
