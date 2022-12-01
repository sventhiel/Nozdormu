using LiteDB;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Utilities;

namespace Nozdormu.Server.Services
{
    public class DOIService
    {
        private readonly ConnectionString _connectionString;

        public DOIService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string prefix, string suffix, DOIState state, long userId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");
                var dois = db.GetCollection<DOI>("dois");

                var doi = new DOI()
                {
                    Prefix = prefix,
                    Suffix = suffix,
                    CreationDate = DateTime.Now,
                    State = state,
                    User = users.FindById(userId)
                };

                return dois.Insert(doi);
            }
        }

        public DOI FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<DOI>("dois");

                return col.FindById(id);
            }
        }

        public List<DOI> Find()
        {
            List<DOI> dois = null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<DOI>("dois");

                dois = col.Query().ToList();
            }

            return dois;
        }

        public bool DeleteById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<DOI>("dois");
                return col.Delete(id);
            }
        }
    }
}
