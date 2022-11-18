using LiteDB;
using System.Net;

namespace Nozdormu.Server.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
        [BsonRef("accounts")]
        public Account Account { get; set; }
    }
}
