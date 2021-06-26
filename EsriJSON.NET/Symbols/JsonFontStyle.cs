using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Font Style, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonFontStyle
    {
        /// <summary>
        /// Font Family
        /// </summary>
        [JsonProperty("family")]
        public string Family { get; set; }

        /// <summary>
        /// Font Size
        /// </summary>
        [JsonProperty("size")]
        public double Size { get; set; }

        /// <summary>
        /// Font Style
        /// </summary>
        [JsonProperty("style")]
        public EsriFontStyle Style { get; set; }

        /// <summary>
        /// Font Weight
        /// </summary>
        [JsonProperty("weight")]
        public EsriFontWeight Weight { get; set; }

        public JsonFontStyle()
        {
            this.Family = "arial";
            this.Size = 8;
            this.Style = EsriFontStyle.normal;
            this.Weight = EsriFontWeight.normal;
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
