using LiteDB;
using Nozdormu.Server.Entities;
using System.Net;
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

        public long Create(string name, string pattern, long accountId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                var user = new User()
                {
                    Name = name,
                    Pattern = pattern,
                    Account = accounts.FindById(accountId),
                };

                return users.Insert(user);
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
