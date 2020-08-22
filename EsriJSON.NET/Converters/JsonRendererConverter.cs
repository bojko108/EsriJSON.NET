using System;
using EsriJSON.NET.Renderers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EsriJSON.NET.Converters
{
    public class JsonRendererConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(JsonRenderer));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject symbol = JObject.Load(reader);
            string value = symbol.GetValue("type").Value<string>();

            EsriRendererType rendererType = (EsriRendererType)Enum.Parse(typeof(EsriRendererType), value);

            switch (rendererType)
            {
                case EsriRendererType.simple:
                    return symbol.ToObject<JsonSimpleRenderer>();
                case EsriRendererType.uniqueValue:
                    return symbol.ToObject<JsonUniqueValueRenderer>();
                default:
                    throw new Exception($"Not supported renderer type: {value}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
