using LiteDB;
using Nozdormu.Server.Entities;

namespace Nozdormu.Server.Services
{
    public class PlaceholderService
    {
        private readonly ConnectionString _connectionString;

        public PlaceholderService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string expression, string regularExpression, long userId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");
                var users = db.GetCollection<User>("users");

                var placeholder = new Placeholder()
                {
                    Expression = expression,
                    RegularExpression = regularExpression,
                    User = users.FindById(userId)
                };

                return col.Insert(placeholder);
            }
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.Delete(id);
            }
        }

        public List<Placeholder> Find()
        {
            List<Placeholder> placeholders = null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                placeholders = col.Query().ToList();
            }

            return placeholders;
        }

        public Dictionary<string, string> FindByUserId(long userId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.Include(c => c.User).Find(c => c.User.Id == userId).Select(c => new KeyValuePair<string, string>(c.Expression, c.RegularExpression)).ToDictionary(item => item.Key, item => item.Value);
            }
        }
    }
}
