using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteDate
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateType")]
        public DataCiteDateType DateType { get; set; }

        [JsonConstructor]
        protected DataCiteDate()
        { }

        public DataCiteDate(string date, DataCiteDateType type)
        {
            Date = date;
            DateType = type;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDateType
    {
        [EnumMember(Value = "Issued")]
        Issued = 1,

        [EnumMember(Value = "Created")]
        Created = 2,

        [EnumMember(Value = "Updated")]
        Updated = 3
    }
}
