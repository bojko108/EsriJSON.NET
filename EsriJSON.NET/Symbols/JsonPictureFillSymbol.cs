using System;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Picture Fill Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonPictureFillSymbol : JsonSymbol
    {
        /// <summary>
        /// Relative URL for static layers and full URL for dynamic layers. Access relative URL using http://<mapservice-url>/<layerId1>/images/<imageUrl11>
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Base64 Encoded Image Data
        /// </summary>
        [JsonProperty("imageData")]
        public string ImageData { get; set; }

        /// <summary>
        /// Image Content-Type
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("ourline", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSimpleLineSymbol Outline { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("angle")]
        public int Angle { get; set; }

        [JsonProperty("xoffset")]
        public double XOffset { get; set; }

        [JsonProperty("yoffset")]
        public double YOffset { get; set; }

        [JsonProperty("xscale")]
        public double XScale { get; set; }

        [JsonProperty("yscale")]
        public double YScale { get; set; }

        /// <summary>
        /// Creates a new Picture Fill Symbol
        /// </summary>
        public JsonPictureFillSymbol() : base(EsriSymbolType.esriPFS)
        {

        }

        /// <summary>
        /// Clones this Picture Fill Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonPictureFillSymbol
            {
                Angle = this.Angle,
                ContentType = this.ContentType,
                Height = this.Height,
                ImageData = this.ImageData,
                Outline = this.Outline,
                Url = this.Url,
                Width = this.Width,
                XOffset = this.XOffset,
                XScale = this.XScale,
                YOffset = this.YOffset,
                YScale = this.YScale
            };
        }
    }
}
