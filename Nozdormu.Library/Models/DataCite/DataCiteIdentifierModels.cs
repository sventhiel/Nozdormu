﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class ReadDataCiteIdentifierModel
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }

        [JsonConstructor]
        protected ReadDataCiteIdentifierModel()
        { }

        public ReadDataCiteIdentifierModel(string identifier, DataCiteIdentifierType identifierType)
        {
            Identifier = identifier;
            IdentifierType = identifierType;
        }
    }

    public enum DataCiteIdentifierType
    {
        [EnumMember(Value = "DOI")]
        DOI = 1
    }
}
