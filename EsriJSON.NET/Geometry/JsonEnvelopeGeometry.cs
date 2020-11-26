using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsriJSON.NET.Geometry
{
    /// <summary>
    /// Represents an Envelope geometry, which can be serialized to EsriJSON (<see cref="https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm#ENVELOPE"/>)
    /// </summary>
    public class JsonEnvelopeGeometry : IJsonGeometry
    {
        /// <summary>
        /// Spatial reference for this geometry
        /// </summary>
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSpatialReference SpatialReference { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("xmin")]
        public double XMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ymin")]
        public double YMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("xmax")]
        public double XMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ymax")]
        public double YMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("zmin", NullValueHandling = NullValueHandling.Ignore)]
        public double ZMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("zmax", NullValueHandling = NullValueHandling.Ignore)]
        public double ZMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("mmin", NullValueHandling = NullValueHandling.Ignore)]
        public double MMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("mmax", NullValueHandling = NullValueHandling.Ignore)]
        public double MMax { get; set; }

        /// <summary>
        /// Creates a new empty JsonEnvelopeGeometry, ready to be serialized in EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonEnvelopeGeometry()
        {
            this.SpatialReference = new JsonSpatialReference(-1);
        }

        /// <summary>
        /// Creates a new JsonEnvelopeGeometry ready to be serialized in EsriJSON
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        public JsonEnvelopeGeometry(double xmin, double ymin, double xmax, double ymax)
            : this(xmin, xmax, ymin, ymax, 0, 0, 0, 0)
        {

        }

        /// <summary>
        /// Creates a new JsonEnvelopeGeometry ready to be serialized in EsriJSON
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="zmin"></param>
        /// <param name="zmax"></param>
        public JsonEnvelopeGeometry(double xmin, double ymin, double xmax, double ymax, double zmin, double zmax)
            : this(xmin, xmax, ymin, ymax, zmin, zmax, 0, 0)
        {

        }

        /// <summary>
        /// Creates a new JsonEnvelopeGeometry ready to be serialized in EsriJSON
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="zmin"></param>
        /// <param name="zmax"></param>
        /// <param name="mmin"></param>
        /// <param name="mmax"></param>
        public JsonEnvelopeGeometry(double xmin, double ymin, double xmax, double ymax, double zmin, double zmax, double mmin, double mmax)
            : this()
        {
            this.XMin = xmin;
            this.YMin = ymin;
            this.XMax = xmax;
            this.YMax = ymax;
            this.ZMin = zmin;
            this.ZMax = zmax;
            this.MMin = mmin;
            this.MMax = mmax;
        }

        /// <summary>
        /// Clones this geometry
        /// </summary>
        /// <returns></returns>
        public IJsonGeometry Clone()
        {
            return new JsonEnvelopeGeometry(this.XMin, this.XMin, this.XMax, this.YMax, this.ZMin, this.ZMax, this.MMin, this.MMax)
            {
                SpatialReference = this.SpatialReference.Clone()
            };
        }
    }
}
