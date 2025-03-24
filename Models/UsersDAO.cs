using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RegisterandLoginApp.Models
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=192.168.0.10,1433;Initial Catalog=Test;User ID=SA;Password=ZenawiHaile32;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public User GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(@"Data Source=192.168.0.10,1433;Initial Catalog=Test;User ID=SA;Password=ZenawiHaile32;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Username = @Username";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = (string)reader["Username"],
                        PasswordHash = (string)reader["PasswordHash"],
                        Salt = (byte[])reader["Salt"],
                        Groups = reader["Groups"] as string
                    };
                }
            }

            return null;  // Return null if user not found
        }

        public bool CheckPassword(string enteredPassword, string storedHash, byte[] storedSalt)
        {
            var enteredHash = GenerateHash(enteredPassword, storedSalt);
            return storedHash == enteredHash;
        }

       
        public string GenerateHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + Convert.ToBase64String(salt);
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}

