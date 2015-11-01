using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Custom_Scenery.Decorators
{
    class RecolorableDecorator : IDecorator
    {
        private bool _recolorable;

        public RecolorableDecorator(bool recolorable)
        {
            _recolorable = recolorable;
        }

        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            if (go.GetComponent<BuildableObject>() != null && _recolorable)
            {
                go.AddComponent<CustomColors>();
            }
        }
    }
}
