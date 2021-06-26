using System.Collections.Generic;
using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Represents a Text Symbol, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonTextSymbol : JsonSymbol
    {
        /// <summary>
        /// Color for the text - represented as RED, GREEN, BLUE and ALPHA
        /// </summary>
        [JsonProperty("color", Required = Required.Always)]
        public List<int> Color { get; set; }

        /// <summary>
        /// Rotation angle for the text
        /// </summary>
        [JsonProperty("angle")]
        public double Angle { get; set; }

        /// <summary>
        /// Text value
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Text Vertical Alignemnt
        /// </summary>
        [JsonProperty("verticalAlignment")]
        public EsriTextVerticalAlignment VerticalAlignment { get; set; }

        /// <summary>
        /// Text Horizontal Alignemnt
        /// </summary>
        [JsonProperty("horizontalAlignment")]
        public EsriTextHorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Text Font style
        /// </summary>
        [JsonProperty("font", NullValueHandling = NullValueHandling.Ignore)]
        public JsonFontStyle FontStyle { get; set; }

        /// <summary>
        /// Text x offset in pixels
        /// </summary>
        [JsonProperty("xoffset")]
        public double XOffset { get; set; }

        /// <summary>
        /// Text y offset in pixels
        /// </summary>
        [JsonProperty("yoffset")]
        public double YOffset { get; set; }

        /// <summary>
        /// Creates a new Text Symbol, ready for serialization to EsriJSON.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color">default is 0,0,0,255 (black)</param>
        public JsonTextSymbol(string text = null, List<int> color = null) : base(EsriSymbolType.esriTS)
        {
            this.Color = color ?? new List<int>() { 0, 0, 0, 255 };
            this.Angle = 0;
            this.Text = text;
            this.VerticalAlignment = EsriTextVerticalAlignment.baseline;
            this.HorizontalAlignment = EsriTextHorizontalAlignment.left;
        }

        /// <summary>
        /// Clones this Text Symbol
        /// </summary>
        /// <returns></returns>
        public override JsonSymbol Clone()
        {
            return new JsonTextSymbol(this.Text)
            {
                Angle = this.Angle,
                Color = new List<int>(this.Color)
            };
        }

        /// <summary>
        /// Gets ESRI Text horizontal alignment from AutoCAD alignment property
        /// </summary>
        /// <param name="textJustify">property value in AutoCAD</param>
        /// <returns></returns>
        public static EsriTextHorizontalAlignment GetHorizontalAlignment(string textJustify)
        {
            switch (textJustify)
            {
                case "Left":
                    return EsriTextHorizontalAlignment.left;
                case "Right":
                    return EsriTextHorizontalAlignment.right;
                case "Center":
                    return EsriTextHorizontalAlignment.center;
                case "Middle":
                    return EsriTextHorizontalAlignment.center;
                case "Align":
                    return EsriTextHorizontalAlignment.justify;
                case "Fit":
                default:
                    return EsriTextHorizontalAlignment.left;
            }
        }

        /// <summary>
        /// Gets ESRI Text vertical alignment from AutoCAD alignment property
        /// </summary>
        /// <param name="verticalAlignment">property value in AutoCAD</param>
        /// <returns></returns>
        public static EsriTextVerticalAlignment GetVerticalAlignment(string verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case "Top":
                    return EsriTextVerticalAlignment.top;
                case "Bottom":
                    return EsriTextVerticalAlignment.bottom;
                case "Middle":
                    return EsriTextVerticalAlignment.middle;
                case "Baseline":
                default:
                    return EsriTextVerticalAlignment.baseline;
            }
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
