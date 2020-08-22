using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Simple Fill Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonSimpleFillSymbol : JsonSymbol
    {
        /// <summary>
        /// Style for the polygon fill
        /// </summary>
        [JsonProperty("style", Required = Required.Always)]
        public EsriFillSymbolType Style { get; set; }

        /// <summary>
        /// Outline symbol
        /// </summary>
        [JsonProperty("outline")]
        public JsonSimpleLineSymbol Outline { get; set; }

        /// <summary>
        /// Creates a new Simple Fill Symbol
        /// </summary>
        public JsonSimpleFillSymbol() : base(EsriSymbolType.esriSFS)
        {
            this.Style = EsriFillSymbolType.esriSFSNull;
            this.Outline = null;
        }

        /// <summary>
        /// Creates a new Simple Fill Symbol
        /// </summary>
        public JsonSimpleFillSymbol(JsonSimpleLineSymbol outline, EsriFillSymbolType fillStyle = EsriFillSymbolType.esriSFSNull) : base(EsriSymbolType.esriSFS)
        {
            this.Style = fillStyle;
            this.Outline = outline;
        }

        /// <summary>
        /// Clones this Simple Fill Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonSimpleFillSymbol
            {
                Outline = this.Outline != null ? (JsonSimpleLineSymbol)this.Outline.Clone() : null,
                Style = this.Style
            };
        }
    }
}
