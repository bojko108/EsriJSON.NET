using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EsriJSON.NET.Helpers;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a Feature Set, which can be serialized to EsriJSON
    /// </summary>
    public class JsonFeatureSet
    {
        /// <summary>
        /// ObjectId field name
        /// </summary>
        [JsonProperty("objectIdField")]
        public string ObjectIdFieldName { get; set; }

        /// <summary>
        /// Display field name
        /// </summary>
        [JsonProperty("displayField")]
        public string DisplayFieldName { get; set; }

        /// <summary>
        /// List of feature class fields and their aliases
        /// </summary>
        [JsonProperty("fieldAliases", Required = Required.Always)]
        public Dictionary<string, string> FieldAliases { get; private set; }

        /// <summary>
        /// Geometry type for features in this Feature Class
        /// </summary>
        [JsonProperty("geometryType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public esriGeometryType GeometryType { get; set; }

        /// <summary>
        /// Spatial reference for all features in this Feature Class
        /// </summary>
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public JsonSpatialReference SpatialReference { get; set; }

        /// <summary>
        /// List of fields for this feature class. Use AddField() and RemoveField() methods to edit fields list
        /// </summary>
        [JsonProperty("fields")]
        public List<JsonField> Fields { get; private set; }

        /// <summary>
        /// List of eatures in this Feature Class. Use AddFeature() and RemoveFeature() methods to edit features list
        /// </summary>
        [JsonProperty("features")]
        public List<JsonFeature> Features { get; private set; }

        [JsonConstructor]
        public JsonFeatureSet()
        {
            this.FieldAliases = new Dictionary<string, string>();
            this.Fields = new List<JsonField>();
            this.Features = new List<JsonFeature>();
        }

        /// <summary>
        /// Creates a new Feature Set, ready to be serialized to EsriJSON
        /// </summary>
        /// <param name="geometryType"></param>
        /// <param name="displayFieldName"></param>
        public JsonFeatureSet(string objectIdField, esriGeometryType geometryType, string displayFieldName = null) : this()
        {
            this.ObjectIdFieldName = objectIdField;
            this.GeometryType = geometryType;
            this.DisplayFieldName = displayFieldName;
        }

        /// <summary>
        /// Creates a new Feature Set from an ESRI Feature Class, ready for serialization to EsriJSON
        /// </summary>
        /// <param name="esriFeatureClass">ESRI Feature Class</param>
        /// <param name="filter">If defined, all features, returned from the filter will be added to Features List</param>
        public JsonFeatureSet(IFeatureClass esriFeatureClass, IQueryFilter filter = null) : this(esriFeatureClass.OIDFieldName, esriFeatureClass.ShapeType, esriFeatureClass.AliasName)
        {
            for (int i = 0; i < esriFeatureClass.Fields.FieldCount; i++)
            {
                IField field = esriFeatureClass.Fields.Field[i];

                if (field.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    JsonField jsonField = new JsonField(field.Name, field.AliasName, field.Type, field.Length);
                    this.AddField(jsonField);
                }
            }

            List<IFeature> gisFeatures = esriFeatureClass.Search(filter, false).GetFeatures();

            this.Features = gisFeatures.Select(f => new JsonFeature(f)).ToList();
        }

        public JsonFeatureSet(IFeatureClass esriFeatureClass, int[] objectIDs) : this(esriFeatureClass.OIDFieldName, esriFeatureClass.ShapeType, esriFeatureClass.AliasName)
        {
            for (int i = 0; i < esriFeatureClass.Fields.FieldCount; i++)
            {
                IField field = esriFeatureClass.Fields.Field[i];

                if (field.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    JsonField jsonField = new JsonField(field.Name, field.AliasName, field.Type, field.Length);
                    this.AddField(jsonField);
                }
            }

            List<IFeature> gisFeatures = esriFeatureClass.GetFeatures(objectIDs, true).GetFeatures();

            this.Features = gisFeatures.Select(f => new JsonFeature(f)).ToList();
        }

        /// <summary>
        /// Creates a new Feature Set from an ESRI Feature Class, ready for serialization to EsriJSON
        /// </summary>
        /// <param name="esriFeatureClass">ESRI Table</param>
        /// <param name="filter">If null all features will be added to Features List!</param>
        public JsonFeatureSet(ITable esriTable, IQueryFilter filter = null) : this(esriTable.OIDFieldName, esriGeometryType.esriGeometryPoint, (esriTable as IObjectClass).AliasName)
        {
            for (int i = 0; i < esriTable.Fields.FieldCount; i++)
            {
                IField field = esriTable.Fields.Field[i];
                JsonField jsonField = new JsonField(field.Name, field.AliasName, field.Type, field.Length);
                this.AddField(jsonField);
            }

            this.Features = esriTable.GetJsonFeatures(filter);

            List<IRow> gisFeatures = esriTable.Search(filter, false).GetRows();

            this.Features = gisFeatures.Select(r => new JsonFeature(r)).ToList();
        }

        public JsonFeatureSet(ITable esriTable, int[] objectIDs) : this(esriTable.OIDFieldName, esriGeometryType.esriGeometryPoint, (esriTable as IObjectClass).AliasName)
        {
            for (int i = 0; i < esriTable.Fields.FieldCount; i++)
            {
                IField field = esriTable.Fields.Field[i];
                JsonField jsonField = new JsonField(field.Name, field.AliasName, field.Type, field.Length);
                this.AddField(jsonField);
            }

            List<IRow> gisFeatures = esriTable.GetRows(objectIDs, true).GetRows();

            this.Features = gisFeatures.Select(r => new JsonFeature(r)).ToList();
        }

        /// <summary>
        /// Adds a new Field to the Feature Set
        /// </summary>
        /// <param name="field">Field to add</param>
        public void AddField(JsonField field)
        {
            if (this.Fields.FirstOrDefault(f => f.Name == field.Name) != null)
            {
                throw new ArgumentException($"Field with name {field.Name} already defined for this feature class.");
            }
            else
            {
                this.Fields.Add(field);

                if (this.FieldAliases.ContainsKey(field.Name) == false)
                {
                    this.FieldAliases.Add(field.Name, field.Alias);
                }
                else
                {
                    this.FieldAliases[field.Name] = field.Alias;
                }
            }
        }

        /// <summary>
        /// Adds a list of fields to this Feature Set
        /// </summary>
        /// <param name="fields">List of fields to add</param>
        public void AddFields(JsonField[] fields)
        {
            foreach (JsonField field in fields)
            {
                this.AddField(field);
            }
        }

        /// <summary>
        /// Removes a Field from this Feature Set
        /// </summary>
        /// <param name="field">Field to remove</param>
        /// <returns></returns>
        public bool RemoveField(JsonField field)
        {
            bool result = this.Fields.Remove(field);

            if (result)
            {
                this.FieldAliases.Remove(field.Name);
            }

            return result;
        }

        public void DeleteFields(HashSet<string> fieldsToRemove)
        {
            foreach (string field in fieldsToRemove)
            {
                JsonField fieldToRemove = this.Fields.FirstOrDefault(f => f.Name == field);

                if (fieldToRemove != null)
                {
                    this.RemoveField(fieldToRemove);
                }
            }
        }

        /// <summary>
        /// Adds a new Feature to the Feature Set
        /// </summary>
        /// <param name="feature">Feature to add</param>
        public void AddFeature(JsonFeature feature)
        {
            if (this.Features.Contains(feature) == false)
            {
                if (feature.Geometry != null)
                {
                    feature.Geometry.SpatialReference = this.SpatialReference;
                }

                this.Features.Add(feature);
            }
        }

        /// <summary>
        /// Removes a Feature from the Feature Set
        /// </summary>
        /// <param name="feature">Feature to remove</param>
        /// <returns></returns>
        public bool RemoveFeature(JsonFeature feature)
        {
            return this.Features.Remove(feature);
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
