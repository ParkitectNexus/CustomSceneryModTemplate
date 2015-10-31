using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Custom_Scenery.Decorators
{
    class HeightDecorator : IDecorator
    {
        private float _height;

        public HeightDecorator(double height)
        {
            _height = (float)height;
        }

        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            go.GetComponent<Deco>().heightChangeDelta = _height;
        }
    }
}
