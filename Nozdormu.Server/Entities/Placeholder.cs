using LiteDB;

namespace Nozdormu.Server.Entities
{
    public class Placeholder
    {
        public long Id { get; set; }
        public string Expression { get; set; }
        public string RegularExpression { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}