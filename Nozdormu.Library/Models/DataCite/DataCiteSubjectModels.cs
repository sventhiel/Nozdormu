using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteSubject
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonConstructor]
        public DataCiteSubject()
        { }

        public DataCiteSubject(string subject)
        {
            Subject = subject;
        }
    }
}
