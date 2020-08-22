using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EsriJSON.NET.Symbols
{
    /// <summary>
    /// Supported Symbols
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriSymbolType
    {
        esriSMS, esriSLS, esriSFS, esriPMS, esriPFS, esriTS
    }

    /// <summary>
    /// Supported Line Symbols
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriLineSymbolType
    {
        esriSLSDash, esriSLSDashDot, esriSLSDashDotDot, esriSLSDot, esriSLSNull, esriSLSSolid
    }

    /// <summary>
    /// Supported Fill Symbols
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriFillSymbolType
    {
        esriSFSBackwardDiagonal, esriSFSCross, esriSFSDiagonalCross, esriSFSForwardDiagonal, esriSFSHorizontal, esriSFSNull, esriSFSSolid, esriSFSVertical
    }

    /// <summary>
    /// Supported Marker Symbols
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriMarkerSymbolType
    {
        esriSMSCircle, esriSMSCross, esriSMSDiamond, esriSMSSquare, esriSMSX, esriSMSTriangle
    }

    /// <summary>
    /// Supported Font styles
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriFontStyle
    {
        italic, normal, oblique
    }

    /// <summary>
    /// Supported Font weights
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriFontWeight
    {
        bold, bolder, lighter, normal
    }

    /// <summary>
    /// Supported Font decorations
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriFontDecoration
    {
        underline, none
    }

    /// <summary>
    /// Supported Text vertical alignment values
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriTextVerticalAlignment
    {
        baseline, top, middle, bottom
    }

    /// <summary>
    /// Supported Text horizontal alignment values
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriTextHorizontalAlignment
    {
        left, right, center, justify
    }
}
