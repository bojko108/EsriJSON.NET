using Newtonsoft.Json;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Base class used to define various symbols used in ArcGIS REST. Go to https://developers.arcgis.com/documentation/common-data-types/symbol-objects.htm for more information.
    /// </summary>
    public abstract class JsonSymbol
    {
        /// <summary>
        /// Symbol type
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public EsriSymbolType SymbolType { get; private set; }

        [JsonConstructor]
        public JsonSymbol() { }

        /// <summary>
        /// Creates a new Symbol
        /// </summary>
        /// <param name="symbolType"></param>
        public JsonSymbol(EsriSymbolType symbolType)
        {
            this.SymbolType = symbolType;
        }

        /// <summary>
        /// Clones this Symbol
        /// </summary>
        /// <returns></returns>
        public abstract JsonSymbol Clone();
    }
}
