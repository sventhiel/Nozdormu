using Newtonsoft.Json;
using NameParser;
using Nozdormu.Library.Converters;

namespace Nozdormu.Library.Models.Names
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadHumanNameModel
    {
        [JsonProperty("first")]
        public string Firstname { get; set; }

        [JsonProperty("last")]
        public string Lastname { get; set; }

        public static ReadHumanNameModel Convert(HumanName humanName)
        {
            return new ReadHumanNameModel()
            {
                Firstname = humanName.First,
                Lastname = humanName.Last
            };
        }
    }
}
