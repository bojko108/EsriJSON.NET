using ESRI.ArcGIS.Geodatabase;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EsriJSON.NET
{
    /// <summary>
    /// Represents a field, which can be serialized in EsriJSON, for more information go to <see cref="https://developers.arcgis.com/documentation/common-data-types/field.htm"/>
    /// </summary>
    public class JsonField
    {
        /// <summary>
        /// A string defining the field name.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        /// <summary>
        /// A string defining the field alias.
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }
        /// <summary>
        /// A string defining the field type <see cref="esriFieldType"/>
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public esriFieldType FieldType { get; set; }
        /// <summary>
        /// A number defining how many characters are allowed in a string field.
        /// </summary>
        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public int Length { get; set; }

        /// <summary>
        /// Creates a new field
        /// </summary>
        [JsonConstructor]
        public JsonField() { }

        /// <summary>
        /// Creates a new field, ready for serialization to EsriJSON.
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="alias">Field alias</param>
        /// <param name="fieldType">Field type</param>
        public JsonField(string name, string alias = null, esriFieldType fieldType = esriFieldType.esriFieldTypeString, int length = 0)
        {
            this.Name = name;
            this.Alias = alias ?? this.Name;
            this.FieldType = fieldType;
            this.Length = length;
        }
    }
}
