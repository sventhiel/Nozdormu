using Newtonsoft.Json;
using Nozdormu.Library.Converters;

namespace Nozdormu.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ORCIDPerson
    {
        [JsonProperty("name")]
        public ORCIDName Name { get; set; }
    }
}