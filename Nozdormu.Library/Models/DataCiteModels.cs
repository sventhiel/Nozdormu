using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Nozdormu.Library.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Nozdormu.Library.Models.DataCite;
using Nozdormu.Library.Attributes;

namespace Nozdormu.Library.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class CreateDataCiteModel
    {
        #region data

        [JsonProperty("data.id")]
        public string Id { get; set; }

        [JsonRequired]
        [JsonProperty("data.type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteType Type { get; set; }

        #region data.attributes

        [JsonRequired]
        [JsonProperty("data.attributes.doi")]
        public string Doi { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.prefix")]
        public string Prefix { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.suffix")]
        public string Suffix { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.event", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("data.attributes.identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [NotEmpty]
        [JsonRequired]
        [JsonProperty("data.attributes.creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.publisher")]
        public string Publisher { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("data.attributes.subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("data.attributes.contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("data.attributes.dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("data.attributes.language")]
        public string Language { get; set; }

        #region data.attributes.types

        [JsonRequired]
        [JsonProperty("data.attributes.types.resourceTypeGeneral")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteResourceType ResourceTypeGeneral { get; set; }

        [JsonRequired]
        [JsonProperty("data.attributes.types.resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("data.attributes.types.schemaOrg")]
        public string SchemaOrg { get; set; }

        [JsonProperty("data.attributes.types.bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("data.attributes.types.citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("data.attributes.types.ris")]
        public string Ris { get; set; }

        #endregion data.attributes.types

        // Related Identifiers

        [JsonProperty("data.attributes.version")]
        public string Version { get; set; }

        [JsonProperty("data.attributes.url")]
        public string URL { get; set; }

        [JsonProperty("data.attributes.descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        #endregion data.attributes

        #endregion data

        public CreateDataCiteModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
        }

        public static CreateDataCiteModel Deserialize(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };

            return JsonConvert.DeserializeObject<CreateDataCiteModel>(json, jsonSettings);
        }
    }

    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadDataCiteModel
    {
        #region data

        [JsonProperty("data.id")]
        public string Id { get; set; }

        [JsonProperty("data.type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteType Type { get; set; }

        #region data.attributes

        [JsonProperty("data.attributes.doi")]
        public string Doi { get; set; }

        [JsonProperty("data.attributes.prefix")]
        public string Prefix { get; set; }

        [JsonProperty("data.attributes.suffix")]
        public string Suffix { get; set; }

        [JsonProperty("data.attributes.state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteStateType State { get; set; }

        [JsonProperty("data.attributes.identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("data.attributes.creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("data.attributes.titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("data.attributes.publisher")]
        public string Publisher { get; set; }

        [JsonProperty("data.attributes.publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("data.attributes.subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("data.attributes.contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("data.attributes.dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("data.attributes.language")]
        public string Language { get; set; }

        #region data.attributes.types

        [JsonProperty("data.attributes.types.resourceTypeGeneral")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataCiteResourceType ResourceTypeGeneral { get; set; }

        [JsonProperty("data.attributes.types.resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("data.attributes.types.schemaOrg")]
        public string SchemaOrg { get; set; }

        [JsonProperty("data.attributes.types.bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("data.attributes.types.citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("data.attributes.types.ris")]
        public string Ris { get; set; }

        #endregion data.attributes.types

        // Related Identifiers

        [JsonProperty("data.attributes.version")]
        public string Version { get; set; }

        [JsonProperty("data.attributes.url")]
        public string URL { get; set; }

        [JsonProperty("data.attributes.descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        #endregion data.attributes

        #endregion data

        public ReadDataCiteModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
        }

        public static ReadDataCiteModel Deserialize(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
                //Converters = new[] { new JsonPathConverter() }
            };

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(json, jsonSettings);
        }
    }

    public enum DataCiteEventType
    {
        Publish = 1,
        Register = 2,
        Hide = 3
    }

    public enum DataCiteStateType
    {
        Findable = 1,
        Registered = 2,
        Draft = 3
    }

    public enum DataCiteResourceType
    {
        Audiovisual = 1,
        Book = 2,
        BookChapter = 3,
        Collection = 4,
        ComputationalNotebook = 5,
        ConferencePaper = 6,
        ConferenceProceeding = 7,
        DataPaper = 8,
        Dataset = 9
    }

    public enum DataCiteType
    {
        DOIs = 1
    }
}