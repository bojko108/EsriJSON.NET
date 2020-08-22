using System;
using EsriJSON.NET.Symbols;
using Newtonsoft.Json;

namespace EsriJSON.NET.Renderers
{
    /// <summary>
    /// Represents a Labeling Info, ready to be serialized in EsriJSON
    /// </summary>
    public class JsonLabelingInfo
    {
        [JsonProperty("labelPlacement")]
        public EsriLabelPlacement LabelPlacement { get; set; }

        [JsonProperty("labelExpression")]
        public string LabelExpression { get; set; }

        [JsonProperty("useCodedValues")]
        public bool UseCodedValues { get; set; }

        [JsonProperty("symbol")]
        public JsonTextSymbol Symbol { get; set; }

        [JsonProperty("minScale")]
        public double MinScale { get; set; }

        [JsonProperty("maxScale")]
        public double MaxScale { get; set; }

        [JsonProperty("where")]
        public string WhereClause { get; set; }
    }
}
