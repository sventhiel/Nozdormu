using Newtonsoft.Json;
using Nozdormu.Library.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadORCIDNameModel
    {
        [JsonProperty("given-names.value")]
        public string Firstname { get; set; }

        [JsonProperty("family-name.value")]
        public string Lastname { get; set; }
    }
}
