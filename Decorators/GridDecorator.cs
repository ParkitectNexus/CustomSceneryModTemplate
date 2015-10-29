using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Custom_Scenery.Decorators
{
    class GridDecorator : IDecorator
    {
        private bool _grid;

        public GridDecorator(bool grid)
        {
            _grid = grid;
        }
        
        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            if (go.GetComponent<Deco>() != null && _grid)
            {
                go.GetComponent<Deco>().buildOnGrid = _grid;
            }
        }
    }
}
