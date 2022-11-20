using Newtonsoft.Json;
using Nozdormu.Library.Converters;
using Nozdormu.Library.Models.ORCID;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadORCIDModel
    {
        [JsonProperty("person")]
        public ReadORCIDModel Person { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
