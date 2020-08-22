using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EsriJSON.NET.Renderers
{
    /// <summary>
    /// Supported Renderer types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriRendererType
    {
        simple, uniqueValue
    }

    /// <summary>
    /// Supported Renderer types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EsriLabelPlacement
    {
        esriServerPointLabelPlacementAboveCenter,
        esriServerPointLabelPlacementAboveLeft,
        esriServerPointLabelPlacementAboveRight,
        esriServerPointLabelPlacementBelowCenter,
        esriServerPointLabelPlacementBelowLeft,
        esriServerPointLabelPlacementBelowRight,
        esriServerPointLabelPlacementCenterCenter,
        esriServerPointLabelPlacementCenterLeft,
        esriServerPointLabelPlacementCenterRight,
        esriServerLinePlacementAboveAfter,
        esriServerLinePlacementAboveAlong,
        esriServerLinePlacementAboveBefore,
        esriServerLinePlacementAboveStart,
        esriServerLinePlacementAboveEnd,
        esriServerLinePlacementBelowAfter,
        esriServerLinePlacementBelowAlong,
        esriServerLinePlacementBelowBefore,
        esriServerLinePlacementBelowStart,
        esriServerLinePlacementBelowEnd,
        esriServerLinePlacementCenterAfter,
        esriServerLinePlacementCenterAlong,
        esriServerLinePlacementCenterBefore,
        esriServerLinePlacementCenterStart,
        esriServerLinePlacementCenterEnd,
        esriServerPolygonPlacementAlwaysHorizontal
    }
}
