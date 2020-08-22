using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsriJSON.NET
{
    /// <summary>
    /// Layer definition, for more information go to <see cref="https://developers.arcgis.com/documentation/common-data-types/layer.htm"/>
    /// </summary>
    public class JsonLayer
    {
        /// <summary>
        /// L<see cref="JsonLayerDefinition"/> object defining the attribute schema and drawing information for the layer.
        /// </summary>
        [JsonProperty("layerDefinition")]
        public JsonLayerDefinition LayerDefinition { get; set; }
        /// <summary>
        /// A <see cref="JsonFeatureSet"/> object containing the geometry and attributes of the features in the layer. Used with feature collections only.
        /// </summary>
        [JsonProperty("featureSet")]
        public JsonFeatureSet FeatureSet { get; set; }

        /// <summary>
        /// Creates a new layer
        /// </summary>
        [JsonConstructor]
        public JsonLayer() { }

        /// <summary>
        /// Creates a new layer from a <see cref="JsonFeatureSet"/> object
        /// </summary>
        /// <param name="featureSet"></param>
        public JsonLayer(JsonFeatureSet featureSet)
        {
            this.LayerDefinition = new JsonLayerDefinition
            {
                ObjectIdFieldName = featureSet.ObjectIdFieldName,
                DisplayField = featureSet.DisplayFieldName,
                Fields = featureSet.Fields,
                GeometryType = featureSet.GeometryType,
                Name = featureSet.DisplayFieldName
            };

            this.FeatureSet = featureSet;
        }
    }
}
