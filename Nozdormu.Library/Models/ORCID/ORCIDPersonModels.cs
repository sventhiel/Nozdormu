using Newtonsoft.Json;
using Nozdormu.Library.Converters;

namespace Nozdormu.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadORCIDPersonModel
    {
        [JsonProperty("name")]
        public ReadORCIDNameModel Name { get; set; }
    }
}