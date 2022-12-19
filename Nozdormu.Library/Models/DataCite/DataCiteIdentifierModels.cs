using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }

        [JsonConstructor]
        public DataCiteIdentifier()
        { }

        public DataCiteIdentifier(string identifier, DataCiteIdentifierType identifierType)
        {
            Identifier = identifier;
            IdentifierType = identifierType;
        }
    }

    public enum DataCiteIdentifierType
    {
        DOI = 1
    }
}
