using System;
using EsriJSON.NET.Converters;
using EsriJSON.NET.Symbols;
using Newtonsoft.Json;

namespace EsriJSON.NET.Renderers
{
    /// <summary>
    /// Represents a Simple Renderer, ready to be serialized in EsriJSON. Simple Renderer is a renderer that uses one symbol only
    /// </summary>
    public class JsonSimpleRenderer : JsonRenderer
    {
        [JsonProperty("symbol", Required = Required.Always)]
        [JsonConverter(typeof(JsonSymbolConverter))]
        public JsonSymbol Symbol { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Creates a new Simple Renderer, ready to be serialized to EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonSimpleRenderer() : base(EsriRendererType.simple)
        {

        }

        /// <summary>
        /// Creates a new Simple Renderer, ready for serialization to EsriJSON
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        public JsonSimpleRenderer(JsonSymbol symbol, string label = null, string description = null) : base(EsriRendererType.simple)
        {
            this.Symbol = symbol;
            this.Label = label;
            this.Description = description;
        }

        /// <summary>
        /// Clones this Simple Renderer
        /// </summary>
        /// <returns></returns>
        public override JsonRenderer Clone()
        {
            return new JsonSimpleRenderer(this.Symbol?.Clone(), this.Label, this.Description);
        }
    }
}
