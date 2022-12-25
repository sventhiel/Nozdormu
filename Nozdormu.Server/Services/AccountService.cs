using LiteDB;
using Nozdormu.Server.Entities;

namespace Nozdormu.Server.Services
{
    public class AccountService
    {
        private readonly ConnectionString _connectionString;

        public AccountService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string host, string prefix, string name, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                var credential = new Account()
                {
                    Host = host,
                    Prefix = prefix,
                    Name = name,
                    Password = password
                };

                return col.Insert(credential);
            }
        }

        public List<Account> Find()
        {
            List<Account> accounts = null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");
                accounts = col.Query().ToList();
            }

            return accounts;
        }

        public Account FindById(int id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");
                return col.FindById(id);
            }
        }

        public bool DeleteById(int id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var accounts = db.GetCollection<Account>("accounts");
                var users = db.GetCollection<User>("users");

                // cascade delete
                foreach (var user in users.Find(u => u.Account.Id == id))
                {
                    user.Account = null;
                    users.Update(user);
                }

                //
                // prevent deletion
                if (!users.Exists(u => u.Account.Id == id))
                {
                    return accounts.Delete(id);
                }

                return false;
            }
        }

        public bool Update(Account account)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                return col.Update(account);
            }
        }
    }
}