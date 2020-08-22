using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using Newtonsoft.Json;
using EsriJSON.NET.Converters;
using EsriJSON.NET.Geometry;
using EsriJSON.NET.Symbols;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a Feature, which can be serialized to EsriJSON
    /// </summary>
    public class JsonFeature
    {
        /// <summary>
        /// List of feature attributes and ther values
        /// </summary>
        [JsonProperty("attributes", Required = Required.Always)]
        public Dictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// Feature's geometry
        /// </summary>
        [JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonGeometryConverter))]
        public IJsonGeometry Geometry { get; set; }

        /// <summary>
        /// Feature's symbol
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonSymbolConverter))]
        public JsonSymbol Symbol { get; set; }

        /// <summary>
        /// Stores the GIS Feature - if defined - only as readonly
        /// </summary>
        [JsonIgnore]
        private readonly IRow feature;

        /// <summary>
        /// Stores the cloned copy of GIS geometry for this JsonFeature
        /// </summary>
        [JsonIgnore]
        public IGeometry GISGeometry { get { return this.HasGISGeometry ? (this.feature as IFeature).ShapeCopy : null; } }

        /// <summary>
        /// Does this feature has a gis geometry
        /// </summary>
        [JsonIgnore]
        public bool HasGISGeometry
        {
            get
            {
                return this.feature != null && (this.feature as IFeature) != null;
            }
        }

        /// <summary>
        /// Creates a new Feature, ready to be serialized to EsriJSON
        /// </summary>
        [JsonConstructor]
        public JsonFeature()
        {
            this.Attributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Creates a new Feature from ESRI Feature (as readonly), ready to be serialized to EsriJSON
        /// </summary>
        /// <param name="esriFeature"></param>
        public JsonFeature(IFeature esriFeature) : this()
        {
            this.feature = esriFeature;

            for (int i = 0; i < this.feature.Fields.FieldCount; i++)
            {
                IField field = this.feature.Fields.Field[i];
                if (field.Type != esriFieldType.esriFieldTypeGeometry)
                    this.SetValue(field.Name, this.feature.Value[i]);
            }

            if (this.HasGISGeometry)
            {
                IJsonGeometry jsonGeometry = null;

                IGeometry geometry = this.GISGeometry;

                switch (geometry.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        jsonGeometry = new JsonPointGeometry(geometry as IPoint);
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        jsonGeometry = new JsonPolylineGeometry(geometry as IPolyline4);
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        jsonGeometry = new JsonPolygonGeometry(geometry as IPolygon4);
                        break;
                }

                this.Geometry = jsonGeometry;
            }
        }

        public JsonFeature(IRow esriRow) : this()
        {
            this.feature = esriRow;

            for (int i = 0; i < this.feature.Fields.FieldCount; i++)
            {
                IField field = this.feature.Fields.Field[i];
                this.SetValue(field.Name, this.feature.Value[i]);
            }
        }

        /// <summary>
        /// Get an attribute value
        /// </summary>
        /// <param name="attributeName">Attribute by name to return</param>
        /// <returns>Attribute value</returns>
        public object GetValue(string attributeName)
        {
            if (this.Attributes.ContainsKey(attributeName))
            {
                return this.Attributes[attributeName];
            }

            return null;
        }

        /// <summary>
        /// Get an attribute value in specified type
        /// </summary>
        /// <typeparam name="T">Type of the returned value</typeparam>
        /// <param name="attributeName">Attribute by name to return</param>
        /// <returns>Attribute value converted to specified type</returns>
        public T GetValue<T>(string attributeName)
        {
            object value = this.GetValue(attributeName);

            if (value != null)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Set a value
        /// </summary>
        /// <param name="field">Field to set</param>
        /// <param name="value">Value to set</param>
        public void SetValue(JsonField field, object value)
        {
            this.SetValue(field.Name, value);
        }

        /// <summary>
        /// Set a value
        /// </summary>
        /// <param name="attributeName">Attribute by name to set</param>
        /// <param name="value">Value to set</param>
        public void SetValue(string attributeName, object value)
        {
            if (this.Attributes.ContainsKey(attributeName) == false)
            {
                this.Attributes.Add(attributeName, value);
            }
            else
            {
                this.Attributes[attributeName] = value;
            }
        }

        public void DeleteAttributes(HashSet<string> fieldsToRemove)
        {
            foreach (string field in fieldsToRemove)
            {
                if (this.Attributes.ContainsKey(field))
                {
                    this.Attributes.Remove(field);
                }
            }
        }

        /// <summary>
        /// Clones this JsonFeature
        /// </summary>
        /// <returns></returns>
        public JsonFeature Clone()
        {
            JsonFeature feature = new JsonFeature();

            if (this.feature != null)
            {
                feature = new JsonFeature(this.feature);
            }
            else
            {
                foreach (KeyValuePair<string, object> kv in this.Attributes)
                {
                    feature.SetValue(kv.Key, kv.Value);
                }

                feature.Geometry = this.Geometry?.Clone();
            }

            feature.Symbol = this.Symbol;

            return feature;
        }
    }
}
