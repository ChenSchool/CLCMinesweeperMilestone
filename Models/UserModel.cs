using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CLCMinesweeperMilestone.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }


        internal void SetPassword(string password)
        {
            Salt = new byte[16];
            Random random = new Random();
            for (int i = 0; i < Salt.Length; i++)
            {
                Salt[i] = (byte)random.Next(0, 16);
            }

            PasswordHash = ComputeHash(password, Salt);
        }

        private string ComputeHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashBytes);
            }
        }

        internal bool VerifyPassword(string password)
        {
            if (Salt == null || PasswordHash == null)
                return false;

            string computeHash = ComputeHash(password, Salt);
            return computeHash == PasswordHash;

        }

    }
}
