using System.Collections.Generic;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Simple Line Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonSimpleLineSymbol : JsonSymbol
    {
        /// <summary>
        /// Color for the line - represented as RED, GREEN, BLUE and ALPHA
        /// </summary>
        [JsonProperty("color", Required = Required.Always)]
        public List<int> Color { get; set; }

        /// <summary>
        /// Width for the line
        /// </summary>
        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Style for the line symbol
        /// </summary>
        [JsonProperty("style", Required = Required.Always)]
        public EsriLineSymbolType Style { get; set; }

        /// <summary>
        /// Creates a new Simple Line Symbol
        /// </summary>
        /// <param name="style"></param>
        /// <param name="width"></param>
        /// <param name="color">default is 0,0,0,255 (black)</param>
        public JsonSimpleLineSymbol(EsriLineSymbolType style = EsriLineSymbolType.esriSLSSolid, int width = 1, List<int> color = null) : base(EsriSymbolType.esriSLS)
        {
            this.Style = style;
            this.Color = color ?? new List<int>() { 0, 0, 0, 255 };
            this.Width = width;
        }

        /// <summary>
        /// Clones this Simple Line Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonSimpleLineSymbol
            {
                Color = new List<int>(this.Color),
                Width = this.Width,
                Style = this.Style
            };
        }
    }
}
