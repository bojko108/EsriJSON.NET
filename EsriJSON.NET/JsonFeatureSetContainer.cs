using System.Collections.Generic;
using Newtonsoft.Json;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a container for feature sets, which can be serialized to EsriJSON
    /// </summary>
    public class JsonFeatureSetContainer
    {
        /// <summary>
        /// Feature classes container
        /// </summary>
        [JsonProperty("featureSets")]
        public Dictionary<string, JsonFeatureSet> FeatureSets { get; private set; }

        /// <summary>
        /// Creates a new Feature Set Container, ready to be serialized to EsriJSON
        /// </summary>
        public JsonFeatureSetContainer()
        {
            this.FeatureSets = new Dictionary<string, JsonFeatureSet>();
        }


        /// <summary>
        /// Adds a Feature Set to the container. ID will be set to FeatureSet.DisplayFieldName value!
        /// </summary>
        /// <param name="featureSet">Feature Set to be added</param>
        public void AddFeatureSet(JsonFeatureSet featureSet)
        {
            this.AddFeatureSet(featureSet.DisplayFieldName, featureSet);
        }

        /// <summary>
        /// Adds a Feature Set to the container
        /// </summary>
        /// <param name="id">ID of the FeatureSet.</param>
        /// <param name="featureSet">Feature Set to be added</param>
        public void AddFeatureSet(string id, JsonFeatureSet featureSet)
        {
            this.FeatureSets.Add(id, featureSet);
        }
    }
}
