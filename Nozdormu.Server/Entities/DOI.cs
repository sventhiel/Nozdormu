using LiteDB;

namespace Nozdormu.Server.Entities
{
    public class DOI
    {
        public long Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DOIState State { get; set; }
        [BsonRef("users")]
        public User User { get; set; }
    }

    public enum DOIState
    {
        Draft = 0,
        Registered = 1,
        Findable = 2
    }
}