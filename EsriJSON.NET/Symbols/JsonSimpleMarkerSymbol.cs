using System.Collections.Generic;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Simple Marker Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonSimpleMarkerSymbol : JsonSymbol
    {
        /// <summary>
        /// Style of the marker symbol
        /// </summary>
        [JsonProperty("style", Required = Required.Always)]
        public EsriMarkerSymbolType Style { get; set; }

        /// <summary>
        /// Color for the marker symbol
        /// </summary>
        [JsonProperty("color", Required = Required.Always)]
        public List<int> Color { get; set; }

        /// <summary>
        /// Size of the symbol
        /// </summary>
        [JsonProperty("size", Required = Required.Always)]
        public int Size { get; set; }

        /// <summary>
        /// Rotation angle for the symbol
        /// </summary>
        [JsonProperty("angle")]
        public int Angle { get; set; }

        /// <summary>
        /// X Offset in pixels
        /// </summary>
        [JsonProperty("xoffset")]
        public int XOffset { get; set; }

        /// <summary>
        /// Y Offset in pixels
        /// </summary>
        [JsonProperty("yoffset")]
        public int YOffset { get; set; }

        /// <summary>
        /// Outline style
        /// </summary>
        [JsonProperty("outline")]
        public JsonSimpleLineSymbol Outline { get; set; }

        /// <summary>
        /// Creates a new Simple Marker Symbol, ready for serialization to EsriJSON.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="size"></param>
        /// <param name="color">default is 0,0,0,255 (black)</param>
        public JsonSimpleMarkerSymbol(EsriMarkerSymbolType style = EsriMarkerSymbolType.esriSMSCircle, int size = 8, List<int> color = null) : base(EsriSymbolType.esriSMS)
        {
            this.Style = style;
            this.Color = color ?? new List<int>() { 0, 0, 0, 255 };
            this.Size = size;
            this.Angle = 0;
            this.XOffset = 0;
            this.YOffset = 0;
            this.Outline = null;
        }

        /// <summary>
        /// Clones this Simple Marker Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonSimpleMarkerSymbol()
            {
                Style = this.Style,
                Color = new List<int>(this.Color),
                Size = this.Size,
                Angle = this.Angle,
                XOffset = this.XOffset,
                YOffset = this.YOffset,
                Outline = this.Outline != null ? (JsonSimpleLineSymbol)this.Outline.Clone() : null
            };
        }
    }

}
