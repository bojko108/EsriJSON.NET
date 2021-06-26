using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Picture Marker Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonPictureMarkerSymbol : JsonSymbol
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

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("xoffset")]
        public double XOffset { get; set; }

        [JsonProperty("yoffset")]
        public double YOffset { get; set; }

        /// <summary>
        /// Creates a new Picture Marker Symbol
        /// </summary>
        public JsonPictureMarkerSymbol() : base(EsriSymbolType.esriPMS)
        {

        }

        /// <summary>
        /// Clones this Picture Marker Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonPictureMarkerSymbol
            {
                ContentType = this.ContentType,
                Height = this.Height,
                ImageData = this.ImageData,
                Url = this.Url,
                Width = this.Width,
                XOffset = this.XOffset,
                YOffset = this.YOffset
            };
        }

        /// <summary>
        /// Returns the JSON text representing this object
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
