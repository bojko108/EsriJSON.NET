using System.Linq;
using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;
using Newtonsoft.Json;
using EsriJSON.NET.Helpers;

namespace EsriJSON.NET.Geometry
{
    /// <summary>
    /// Represents a Polyline geometry, which can be serialized to EsriJSON
    /// </summary>
    public class JsonPolylineGeometry : IJsonGeometry
    {
        /// <summary>
        /// Spatial reference for this geometry
        /// </summary>
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSpatialReference SpatialReference { get; set; }

        /// <summary>
        /// List of paths composing the polyline geometry
        /// </summary>
        [JsonProperty("paths", Required = Required.Always)]
        public List<List<double[]>> Paths { get; set; }

        /// <summary>
        /// Source polyline geometry, if created from ESRI Feature
        /// </summary>
        [JsonIgnore]
        private IPolyline4 sourcePolyline;

        /// <summary>
        /// Creates a new JsonPolylienGeometry, ready to be serialized in EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonPolylineGeometry()
        {
            this.SpatialReference = new JsonSpatialReference(-1);
        }

        /// <summary>
        /// Creates a new JsonPolylineGeometry from the specified Polyline geometry, ready to be serialized in EsriJSON
        /// <param name="polyline"></param>
        public JsonPolylineGeometry(IPolyline4 polyline)
        {
            this.SetPolyline(polyline);
            this.SpatialReference = new JsonSpatialReference(polyline.SpatialReference.FactoryCode);
        }

        /// <summary>
        /// Sets the Polyline geometry associated with this JsonGeometry. If the input geometry contains 
        /// a sicrular segments they will be densified.
        /// </summary>
        /// <param name="polyline"></param>
        public void SetPolyline(IPolyline4 polyline)
        {
            this.sourcePolyline = polyline;

            this.Paths = new List<List<double[]>>
            {
                new List<double[]>()
            };

            // http://resources.esri.com/help/9.3/ArcGISServer/apis/ArcObjects/esriGeometry/IPath.htm

            IGeometryCollection paths = this.sourcePolyline as IGeometryCollection;
            for (int i = 0; i < paths.GeometryCount; i++)
            {
                IPath path = paths.Geometry[i] as IPath;
                IPolyline editedPolyline = path.ToPolyline();
                // densify the line segment: if circular segment it will be 
                // represented as series of line segments
                editedPolyline.Densify(0, 0);

                IPointCollection points = editedPolyline as IPointCollection;
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
            if (this.sourcePolyline != null)
            {
                return new JsonPolylineGeometry(this.sourcePolyline);
            }
            else
            {
                return new JsonPolylineGeometry()
                {
                    Paths = this.Paths.Select(p => p.Select(c => c).ToList()).ToList(),
                    SpatialReference = this.SpatialReference
                };
            }
        }

        /// <summary>
        /// Adds a vertex to the geometry. Index of the path is needed as there can be multiple paths
        /// in one geometry
        /// </summary>
        /// <param name="path">path index</param>
        /// <param name="point">vertex coordinates</param>
        private void AddVertex(int path, IPoint point)
        {
            double[] coords = new double[2] { point.X, point.Y };

            if (this.Paths.Count > path)
            {
                this.Paths[path].Add(coords);
            }
            else
            {
                this.Paths.Add(new List<double[]>() { coords });
            }
        }
    }
}
