namespace EsriJSON.NET.Geometry
{
    /// <summary>
    /// Interface for geometry objects, ready to be serialized to EsriJSON (<see cref="https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm"/>)
    /// </summary>
    public interface IJsonGeometry
    {
        /// <summary>
        /// Spatial Reference for this geometry
        /// </summary>
        JsonSpatialReference SpatialReference { get; set; }

        /// <summary>
        /// Clones this geometry
        /// </summary>
        /// <returns></returns>
        IJsonGeometry Clone();
    }
}
