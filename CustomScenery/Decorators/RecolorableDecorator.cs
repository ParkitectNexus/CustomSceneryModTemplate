using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                CustomColors cc = go.AddComponent<CustomColors>();

                List<Color> colors = new List<Color>();

                if (options.ContainsKey("recolorableOptions"))
                {
                    Dictionary<string, object> clrs = (Dictionary<string, object>)options["recolorableOptions"];

                    foreach (KeyValuePair<string, object> clr in clrs)
                    {
                        colors.Add(FromHex((string)clr.Value));
                    }
                }

                cc.customColors = colors.ToArray();

                foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
                {
                    if (material.name == "CustomColorsDiffuse")
                    {
                        go.GetComponentInChildren<Renderer>().sharedMaterial = material;

                        // Go through all child objects and recolor		
                        Renderer[] renderCollection;
                        renderCollection = go.GetComponentsInChildren<Renderer>();

                        foreach (Renderer render in renderCollection)
                        {
                            render.sharedMaterial = material;
                        }

                        break;
                    }
                }
            }
        }

        private Color FromHex(string hex)
        {
            Color clr = new Color(0, 0, 0);

            if (!string.IsNullOrEmpty(hex))
            {
                try
                {
                    string str = hex.Substring(1, hex.Length - 1);
                    clr.r = Int32.Parse(str.Substring(0, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    clr.g = Int32.Parse(str.Substring(2, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    clr.b = Int32.Parse(str.Substring(4, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    if (str.Length == 8)
                        clr.a = Int32.Parse(str.Substring(6, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    else clr.a = 1.0f;
                }
                catch (Exception e)
                {
                    Debug.Log("Could not convert " + hex + " to Color. " + e);
                    return new Color(0, 0, 0, 0);
                }
            }

            return clr;
        }
    }
}
