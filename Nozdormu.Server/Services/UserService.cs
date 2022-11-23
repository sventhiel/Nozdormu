using LiteDB;
using Nozdormu.Server.Entities;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Nozdormu.Server.Services
{
    public class UserService
    {
        private readonly ConnectionString _connectionString;

        public UserService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string username, string password, string pattern, long accountId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                // salt
                var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6));

                var user = new User()
                {
                    Username = username,
                    Salt = salt,
                    Password = computeSHA512Hash(password, salt),
                    Pattern = pattern
                    //Account = accounts.FindById(accountId)
                };

                return users.Insert(user);
            }
        }

        public bool Verify(string username, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");

                var user = users.Find(u => u.Username == username).Single();

                return (user.Password == computeSHA512Hash(password, (user.Salt)));
            }
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.Delete(id);
            }
        }

        public User FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.FindById(id);
            }
        }

        public List<User> Find()
        {
            List<User> users = null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                users = col.Query().ToList();
            }

            return users;
        }

        private static string computeSHA512Hash(string password, string salt)
        {
            try
            {
                using (var sha512 = new SHA512Managed())
                {
                    byte[] saltedPassword = (Encoding.UTF8.GetBytes(salt).Concat(Encoding.UTF8.GetBytes(password))).ToArray();
                    return Convert.ToBase64String(sha512.ComputeHash(saltedPassword));
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        private static string generate(int size = 64)
        {
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                var result = new StringBuilder(size);
                foreach (var b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
                return result.ToString();
            }
        }
    }
}