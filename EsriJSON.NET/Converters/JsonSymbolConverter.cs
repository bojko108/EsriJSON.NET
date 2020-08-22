using System;
using EsriJSON.NET.Symbols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EsriJSON.NET.Converters
{
    public class JsonSymbolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(JsonSymbol));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject symbol = JObject.Load(reader);
            string value = symbol.GetValue("type").Value<string>();

            EsriSymbolType symbolType = (EsriSymbolType)Enum.Parse(typeof(EsriSymbolType), value);

            switch (symbolType)
            {
                case EsriSymbolType.esriPFS:
                    return symbol.ToObject<JsonPictureFillSymbol>();
                case EsriSymbolType.esriPMS:
                    return symbol.ToObject<JsonPictureMarkerSymbol>();
                case EsriSymbolType.esriSFS:
                    return symbol.ToObject<JsonSimpleFillSymbol>();
                case EsriSymbolType.esriSLS:
                    return symbol.ToObject<JsonSimpleLineSymbol>();
                case EsriSymbolType.esriSMS:
                    return symbol.ToObject<JsonSimpleMarkerSymbol>();
                case EsriSymbolType.esriTS:
                    return symbol.ToObject<JsonTextSymbol>();
                default:
                    throw new Exception($"Not supported symbol type: {value}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
