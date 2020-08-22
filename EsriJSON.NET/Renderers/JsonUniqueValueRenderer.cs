using System;
using System.Collections.Generic;
using System.Linq;
using EsriJSON.NET.Converters;
using EsriJSON.NET.Symbols;
using Newtonsoft.Json;
namespace EsriJSON.NET.Renderers
{
    /// <summary>
    /// Represents an Unique Value Renderer, ready to be serialized in EsriJSON. Unique Value Renderer symbolizes groups of features which have matching field values
    /// </summary>
    public class JsonUniqueValueRenderer : JsonRenderer
    {
        [JsonProperty("field1", Required = Required.Always)]
        public string Field1 { get; set; }

        [JsonProperty("field2")]
        public string Field2 { get; set; }

        [JsonProperty("field3")]
        public string Field3 { get; set; }

        [JsonProperty("fieldDelimiter")]
        public string FieldDelimiter { get; set; }

        [JsonProperty("defaultLabel")]
        public string DefaultLabel { get; set; }

        [JsonProperty("defaultSymbol", Required = Required.Always)]
        [JsonConverter(typeof(JsonSymbolConverter))]
        public JsonSymbol DefaultSymbol { get; set; }

        [JsonProperty("uniqueValueInfos", Required = Required.Always)]
        public List<JsonUniqueValueInfo> UniqueValueInfos { get; private set; }

        /// <summary>
        /// Creates a new Unique Value Renderer, ready to be serialized to EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonUniqueValueRenderer() : base(EsriRendererType.uniqueValue)
        {
            this.UniqueValueInfos = new List<JsonUniqueValueInfo>();
        }

        public void AddUniqueValueInfo(JsonUniqueValueInfo valueInfo)
        {
            this.UniqueValueInfos.Add(valueInfo);
        }

        /// <summary>
        /// Clones this Unique Value Renderer
        /// </summary>
        /// <returns></returns>
        public override JsonRenderer Clone()
        {
            return new JsonUniqueValueRenderer
            {
                DefaultLabel = this.DefaultLabel,
                DefaultSymbol = this.DefaultSymbol?.Clone(),
                Field1 = this.Field1,
                Field2 = this.Field2,
                Field3 = this.Field3,
                FieldDelimiter = this.FieldDelimiter,
                UniqueValueInfos = this.UniqueValueInfos.Select(i => i.Clone()).ToList()
            };
        }
    }

    /// <summary>
    /// Represents an Unique Value Info object, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonUniqueValueInfo
    {
        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
        [JsonConverter(typeof(JsonSymbolConverter))]
        public JsonSymbol Symbol { get; set; }

        /// <summary>
        /// Creates a new Unique Value Info object
        /// </summary>
        [JsonConstructor]
        public JsonUniqueValueInfo() { }

        /// <summary>
        /// Clones this Unique Value Info object
        /// </summary>
        /// <returns></returns>
        public JsonUniqueValueInfo Clone()
        {
            return new JsonUniqueValueInfo()
            {
                Label = this.Label,
                Symbol = this.Symbol?.Clone(),
                Value = this.Value
            };
        }
    }
}
