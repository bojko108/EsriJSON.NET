using System.Collections.Generic;
using Newtonsoft.Json;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a container for layers, which can be serialized to EsriJSON
    /// </summary>
    public class JsonLayerContainer
    {
        // TODO: add extent property!

        /// <summary>
        /// Layers container
        /// </summary>
        [JsonProperty("layers")]
        public Dictionary<string, JsonLayer> Layers { get; private set; }

        /// <summary>
        /// Creates a new Layer Container, ready to be serialized to EsriJSON
        /// </summary>
        public JsonLayerContainer()
        {
            this.Layers = new Dictionary<string, JsonLayer>();
        }


        /// <summary>
        /// Adds a Layer to the container. ID will be set to FeatureSet.DisplayFieldName value!
        /// </summary>
        /// <param name="layer">Layer to be added</param>
        public void AddLayer(JsonLayer layer)
        {
            this.AddLayer(layer.LayerDefinition.Name, layer);
        }

        /// <summary>
        /// Adds a Layer to the container
        /// </summary>
        /// <param name="id">ID of the Layer.</param>
        /// <param name="layer">Layer to be added</param>
        public void AddLayer(string id, JsonLayer layer)
        {
            this.Layers.Add(id, layer);
        }
    }
}
