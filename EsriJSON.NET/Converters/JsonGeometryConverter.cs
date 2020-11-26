using System;
using EsriJSON.NET.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EsriJSON.NET.Converters
{
    public class JsonGeometryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IJsonGeometry).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject geom = JObject.Load(reader);

            if (geom.GetValue("x") != null && geom.GetValue("y") != null)
                return geom.ToObject<JsonPointGeometry>();
            if (geom.GetValue("xmin") != null)
                return geom.ToObject<JsonEnvelopeGeometry>();
            if (geom.GetValue("paths") != null)
                return geom.ToObject<JsonPolylineGeometry>();
            if (geom.GetValue("rings") != null)
                return geom.ToObject<JsonPolygonGeometry>();

            throw new Exception("Not supported geometry!");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
