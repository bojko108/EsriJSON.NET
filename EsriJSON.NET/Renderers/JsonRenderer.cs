using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsriJSON.NET.Renderers
{
    public abstract class JsonRenderer
    {
        /// <summary>
        /// Symbol type
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public EsriRendererType RendererType { get; private set; }

        [JsonConstructor]
        public JsonRenderer() { }

        /// <summary>
        /// Creates a new Symbol
        /// </summary>
        /// <param name="symbolType"></param>
        public JsonRenderer(EsriRendererType rendererType)
        {
            this.RendererType = rendererType;
        }

        /// <summary>
        /// Clones this Renderer
        /// </summary>
        /// <returns></returns>
        public abstract JsonRenderer Clone();
    }
}
