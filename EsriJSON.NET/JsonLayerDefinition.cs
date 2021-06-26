using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents the attribute schema and drawing information for a layer, ready to be serialzied in EsriJSON
    /// </summary>
    public class JsonLayerDefinition
    {
        /// <summary>
        /// A string containing a unique name for the layer that can be displayed in a legend.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// ObjectId field name
        /// </summary>
        [JsonProperty("objectIdField")]
        public string ObjectIdFieldName { get; set; }

        /// <summary>
        /// An optional SQL-based definition expression string that narrows the data to be displayed in the layer. Used with feature services and single layers from ArcGIS Server map services.
        /// </summary>
        [JsonProperty("definitionExpression", NullValueHandling = NullValueHandling.Ignore)]
        public string DefinitionExpression { get; set; }

        /// <summary>
        /// A string containing the name of the field that best summarizes the layer. Values from this field are used by default as the titles for pop-up windows.
        /// </summary>
        [JsonProperty("displayField")]
        public string DisplayField { get; set; }

        /// <summary>
        /// A drawingInfo object containing drawing, labeling, and transparency information for the layer.
        /// </summary>
        [JsonProperty("drawingInfo")]
        public JsonDrawingInfo DrawingInfo { get; set; }

        /// <summary>
        /// An array of field objects containing information about the attribute fields for the feature collection or layer.
        /// </summary>
        [JsonProperty("fields")]
        public List<JsonField> Fields { get; set; }

        /// <summary>
        /// Geometry type for features in this Feature Class
        /// </summary>
        [JsonProperty("geometryType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public esriGeometryType GeometryType { get; set; }

        [JsonConstructor]
        public JsonLayerDefinition()
        {
            this.Fields = new List<JsonField>();
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
