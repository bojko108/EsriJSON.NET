using System.Linq;
using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;
using Newtonsoft.Json;
using EsriJSON.NET.Helpers;

namespace EsriJSON.NET.Geometry
{
    /// <summary>
    /// Represents a Polygon geometry, which can be serialized to EsriJSON (<see cref="https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm#POLYGON"/>)
    /// </summary>
    public class JsonPolygonGeometry : IJsonGeometry
    {
        /// <summary>
        /// Spatial reference for this geometry
        /// </summary>
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSpatialReference SpatialReference { get; set; }

        /// <summary>
        /// List of rings composing the polygon geometry
        /// </summary>
        [JsonProperty("rings", Required = Required.Always)]
        public List<List<double[]>> Rings { get; set; }

        /// <summary>
        /// Source polygon geometry, if created from ESRI Feature
        /// </summary>
        [JsonIgnore]
        private IPolygon4 sourcePolygon;

        /// <summary>
        /// Creates a new JsonPolygonGeometry, ready to be serialized in EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonPolygonGeometry()
        {
            this.SpatialReference = new JsonSpatialReference(-1);
        }

        /// <summary>
        /// Creates a new JsonPolygonGeometry from the specified Polygon geometry, ready to be serialized in EsriJSON
        /// <param name="polyline"></param>
        public JsonPolygonGeometry(IPolygon4 polygon)
        {
            this.SetPolygon(polygon);
            this.SpatialReference = new JsonSpatialReference(polygon.SpatialReference.FactoryCode);
        }

        /// <summary>
        /// Sets the Polygon geometry associated with this JsonGeometry. If the input geometry contains 
        /// a sicrular segments they will be densified.
        /// </summary>
        /// <param name="polygon"></param>
        public void SetPolygon(IPolygon4 polygon)
        {
            this.sourcePolygon = polygon;

            this.Rings = new List<List<double[]>>
            {
                new List<double[]>()
            };

            // http://resources.esri.com/help/9.3/ArcGISServer/apis/ArcObjects/esriGeometry/IPath.htm

            IGeometryCollection rings = this.sourcePolygon as IGeometryCollection;
            for (int i = 0; i < rings.GeometryCount; i++)
            {
                IRing ring = rings.Geometry[i] as IRing;
                IPolygon editedPolygon = ring.ToPolygon();
                // densify the line segment: if circular segment it will be 
                // represented as series of line segments
                editedPolygon.Densify(0, 0);

                IPointCollection points = editedPolygon as IPointCollection;
                for (int k = 0; k < points.PointCount; k++)
                {
                    this.AddVertex(i, points.Point[k]);
                }
            }
        }

        /// <summary>
        /// Clones this geometry
        /// </summary>
        /// <returns></returns>
        public IJsonGeometry Clone()
        {
            if (this.sourcePolygon != null)
            {
                return new JsonPolygonGeometry(this.sourcePolygon);
            }
            else
            {
                return new JsonPolygonGeometry()
                {
                    Rings = this.Rings.Select(r => r.Select(c => c).ToList()).ToList(),
                    SpatialReference = this.SpatialReference
                };
            }
        }

        /// <summary>
        /// Adds a vertex to the geometry. Index of the ring is needed as there can be multiple ring 
        /// in one geometry
        /// </summary>
        /// <param name="ring">ring index</param>
        /// <param name="point">vertex coordinates</param>
        private void AddVertex(int ring, IPoint point)
        {
            double[] coords = new double[2] { point.X, point.Y };
            if (this.Rings.Count > ring)
            {
                this.Rings[ring].Add(coords);
            }
            else
            {
                this.Rings.Add(new List<double[]>() { coords });
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
