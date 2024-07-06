using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraArtifex.Core.Interfaces;
using TerraArtifex.Generating.Models;

namespace TerraArtifex.Generating
{
    public class MapBuilder
    {
        private IMap _map;

        public MapBuilder() 
        {
            _map = new Map();
        }

        public ILayerBuilderResult BuildLayer(ILayerBuilder layerBuilder)
        {
            return layerBuilder.Execute(_map);
        }
    }
}
