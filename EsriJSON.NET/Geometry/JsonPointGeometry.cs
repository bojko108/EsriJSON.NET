using Newtonsoft.Json;
using ESRI.ArcGIS.Geometry;

namespace EsriJSON.NET.Geometry
{
    /// <summary>
    /// Represents a Point geometry, which can be serialized to EsriJSON (<see cref="https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm#POINT"/>)
    /// </summary>
    public class JsonPointGeometry : IJsonGeometry
    {
        /// <summary>
        /// Spatial reference for this geometry
        /// </summary>
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSpatialReference SpatialReference { get; set; }

        /// <summary>
        /// X (Easting) coordinate value
        /// </summary>
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        /// <summary>
        /// Y (Northing) coordinate value
        /// </summary>
        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonConstructor]
        public JsonPointGeometry() { }

        /// <summary>
        /// Creates a new Point geometry with specified coordinates, ready to be serialized to EsriJSON
        /// </summary>
        /// <param name="x">X (Easting) coordinate value</param>
        /// <param name="y">Y (Northing) coordinate value</param>
        public JsonPointGeometry(double x, double y) : this(x, y, -1) { }

        /// <summary>
        /// Creates a new Point geometry with specified coordinates and WKID, ready to be serialzied to EsriJSON
        /// </summary>
        /// <param name="x">X (Easting) coordinate value</param>
        /// <param name="y">Y (Northing) coordinate value</param>
        /// <param name="wkid">WKID of geometry's Spatial Reference</param>
        public JsonPointGeometry(double x, double y, int wkid = -1) : this(x, y, new JsonSpatialReference(wkid)) { }

        /// <summary>
        /// Creates a new Point geometry with specified coordinates and Spatial Reference, ready to be serialzied to EsriJSON
        /// </summary>
        /// <param name="x">X (Easting) coordinate value</param>
        /// <param name="y">Y (Northing) coordinate value</param>
        /// <param name="spatialReference">Spatial Reference of the geometry</param>
        public JsonPointGeometry(double x, double y, JsonSpatialReference spatialReference)
        {
            this.X = x;
            this.Y = y;
            this.SpatialReference = spatialReference;
        }

        /// <summary>
        /// Creates a new Point geometry from GIS Point geometry, ready to be serialized to EsriJSON
        /// </summary>
        /// <param name="point"></param>
        public JsonPointGeometry(IPoint point)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.SpatialReference = new JsonSpatialReference(point.SpatialReference != null ? point.SpatialReference.FactoryCode : -1);
        }

        /// <summary>
        /// Clones this geometry
        /// </summary>
        /// <returns></returns>
        public IJsonGeometry Clone()
        {
            return new JsonPointGeometry(this.X, this.Y, this.SpatialReference.WKID);
        }

        /// <summary>
        /// Creates a new IPoint from this JsonPointGeometry
        /// </summary>
        /// <returns></returns>
        public IPoint ToPoint()
        {
            IPoint point = new PointClass();
            point.PutCoords(this.X, this.Y);
            return point;
        }
    }
}
