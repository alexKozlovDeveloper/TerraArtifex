using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraArtifex.Core.Interfaces;

namespace TerraArtifex.Generating.Models
{
    public class Map : IMap
    {
        private Dictionary<Type, ILayer> _layers = [];

        public Map() 
        {
        
        }

        public void AddLayer(ILayer layer) 
        {
            var layerType = layer.GetType();

            if (_layers.ContainsKey(layerType))
            {
                throw new Exception($"Map already contains '{layerType}' layer");
            }

            _layers.Add(layerType, layer);
        }

        public T GetLayer<T>() where T : ILayer
        {
            var layerType = typeof(T);

            if (!_layers.ContainsKey(layerType)) 
            {
                throw new Exception($"Map does not contain '{layerType}' layer");
            }

            return (T) _layers[layerType];
        }
    }
}
