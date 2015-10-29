using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Custom_Scenery.Decorators.Type
{
    class DecoDecorator : IDecorator
    {
        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            go.AddComponent<global::Deco>();

            if (options.ContainsKey("heightDelta"))
                (new HeightDecorator((double)options["heightDelta"])).Decorate(go, options, assetBundle);
        }
    }
}
