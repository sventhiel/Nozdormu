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

        public long Create(string host, string prefix, string username, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                var credential = new Account()
                {
                    Host = host,
                    Prefix = prefix,
                    Username = username,
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

        public Account FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");
                return col.FindById(id);
            }
        }

        public bool DeleteById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");
                return col.Delete(id);
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