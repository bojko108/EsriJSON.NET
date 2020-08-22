using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsriJSON.NET.Renderers;
using Newtonsoft.Json;

namespace EsriJSON.NET
{
    public class JsonDrawingInfo
    {
        [JsonProperty("renderer")]
        public JsonRenderer Renderer { get; set; }

        //[JsonProperty("transparency")]
        //public double Transparency { get; set; }

        [JsonProperty("labelingInfo")]
        public List<JsonLabelingInfo> LabelingInfo { get; set; }

        [JsonConstructor]
        public JsonDrawingInfo()
        {
            this.LabelingInfo = new List<JsonLabelingInfo>();
        }
    }
}
