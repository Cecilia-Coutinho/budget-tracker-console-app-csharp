using System.Text;
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace WizzyLiConsoleApp.Models
{
    internal class UserAuthenticator
    {
        private static string SaltGenerator(int sizeInBytes)
        {
            using RandomNumberGenerator randomNumGenerator = RandomNumberGenerator.Create(); //new instance of CryptoServiceProvider for generating random bytes
            byte[] salt = new byte[sizeInBytes];
            randomNumGenerator.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        private static string PasswordHashing(string password, string salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            using SHA256 sHA256 = SHA256.Create(); //new instance of SHA256 algorithm for hashing
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] combineBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Array.Copy(passwordBytes, 0, combineBytes, 0, passwordBytes.Length);
            Array.Copy(saltBytes, 0, combineBytes, passwordBytes.Length, saltBytes.Length);

            byte[] hashedBytes = sHA256.ComputeHash(combineBytes); //Hash the combined byte array

            return Convert.ToBase64String(hashedBytes);
        }

        public static string GetHashPassword(UserData user)
        {
            if (string.IsNullOrEmpty(user.user_password))
            {
                throw new ArgumentException("Invalid password.");
            }

            string salt = SaltGenerator(32);
            string hashedPassword = PasswordHashing(user.user_password, salt);
            return hashedPassword;
        }
    }
}
