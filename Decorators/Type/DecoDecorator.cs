using System.Collections.Generic;
using UnityEngine;

namespace Custom_Scenery.Decorators.Type
{
    class DecoDecorator : IDecorator
    {
        public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
        {
            go.AddComponent<Deco>();

            if (options.ContainsKey("heightDelta"))
                (new HeightDecorator((double)options["heightDelta"])).Decorate(go, options, assetBundle);
        }
    }
}
