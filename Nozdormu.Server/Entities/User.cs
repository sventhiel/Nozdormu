using LiteDB;

namespace Nozdormu.Server.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Pattern { get; set; }

        //[BsonRef("accounts")]
        //public Account Account { get; set; }
    }
}