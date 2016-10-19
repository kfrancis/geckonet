using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Geckonet.Core.Models
{
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoDataset
    {
        [DataMember(Name = "fields", IsRequired = false), XmlElement("fields"), JsonProperty("fields")]
        public Dictionary<string, IDatasetField> Fields { get; set; }
        public bool ShouldSerializeFields() { return (Fields != null) && (Fields.Count > 0); }

        [DataMember(Name = "data", IsRequired = false), XmlElement("data"), JsonProperty("data")]
        public List<Dictionary<string, object>> Data { get; set; }
        public bool ShouldSerializeData() { return (Data != null) && (Data.Count > 0); }

        [DataMember(Name = "unique_by", IsRequired = false), XmlElement("unique_by"), JsonProperty("unique_by")]
        public List<string> UniqueBy { get; set; }
        public bool ShouldSerializeUniqueBy() { return (UniqueBy != null) && (UniqueBy.Count > 0); }
    }

    public class GeckoDatasetResult : GeckoDataset
    {
        [DataMember(Name = "id", IsRequired = false), XmlElement("id"), JsonProperty("id")]
        public string Id { get; set; }

        [DataMember(Name = "created_at", IsRequired = false), XmlElement("created_at"), JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", IsRequired = false), XmlElement("updated_at"), JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class IDatasetField
    {
        string Name { get; set; }
        DatasetFieldType Type { get; set; }
        string CurrencyCode { get; set; }
    }
    public class DatasetField : IDatasetField
    {
        public DatasetField (DatasetFieldType type, string name = null, string currencyCode = null)
        {
            this.Type = type;
            this.Name = name;
            this.CurrencyCode = currencyCode;
        }

        [DataMember(Name = "name", IsRequired = false), XmlElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        public bool ShouldSerializeName() { return !string.IsNullOrEmpty(Name); }

        [DataMember(Name = "type", IsRequired = true), XmlElement("type"), JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public DatasetFieldType Type { get; set; }

        [DataMember(Name = "currency_code", IsRequired = false), XmlElement("currency_code"), JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        public bool ShouldSerializeCurrencyCode() { return !string.IsNullOrEmpty(CurrencyCode); }
    }

    public enum DatasetFieldType
    {
        date,
        datetime,
        number,
        percentage,
        @string,
        money
    }
}
