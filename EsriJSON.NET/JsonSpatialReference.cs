using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;
using Newtonsoft.Json;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a Spatial Reference, which can be serialized to EsriJSON
    /// </summary>
    public class JsonSpatialReference
    {
        /// <summary>
        /// WKID of the Spatial Reference according to https://epsg.io
        /// </summary>
        [JsonProperty("wkid", NullValueHandling = NullValueHandling.Ignore)]
        public int WKID { get; set; }

        [JsonConstructor]
        public JsonSpatialReference() { }

        /// <summary>
        /// Creates a new Spatial Reference, ready for serialization to EsriJSON
        /// </summary>
        /// <param name="wkid"></param>
        public JsonSpatialReference(int wkid)
        {
            this.WKID = wkid;
        }

        /// <summary>
        /// Creates a new Spatial Reference from ESRI Spatial reference, ready for serialization to EsriJSON
        /// </summary>
        /// <param name="spatialReference"></param>
        public JsonSpatialReference(ISpatialReference spatialReference)
        {
            this.WKID = spatialReference.FactoryCode;
        }

        /// <summary>
        /// Creates a new ESRI SpatialReference
        /// </summary>
        /// <returns></returns>
        public ISpatialReference ToSpatialReference()
        {
            ISpatialReferenceFactory2 factory = new SpatialReferenceEnvironmentClass();
            return factory.CreateProjectedCoordinateSystem(this.WKID);
        }

        /// <summary>
        /// Clones this spatial reference
        /// </summary>
        /// <returns></returns>
        public JsonSpatialReference Clone()
        {
            return new JsonSpatialReference(this.WKID);
        }
    }
}
