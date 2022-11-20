using Newtonsoft.Json;
using Nozdormu.Library.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadORCIDPersonModel
    {
        [JsonProperty("name")]
        public ReadORCIDNameModel Name { get; set; }
    }
}
