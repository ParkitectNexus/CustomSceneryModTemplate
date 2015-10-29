using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Custom_Scenery.Decorators
{
    class NameDecorator : IDecorator
    {
        private string _name;

        public NameDecorator(string name)
        {
            _name = name;
        }

        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            go.GetComponent<BuildableObject>().setDisplayName(_name);
            go.name = _name;
        }
    }
}
