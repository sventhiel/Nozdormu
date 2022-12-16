using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class ReadDataCiteSubjectModel
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonConstructor]
        protected ReadDataCiteSubjectModel()
        { }

        public ReadDataCiteSubjectModel(string subject)
        {
            Subject = subject;
        }
    }
}
