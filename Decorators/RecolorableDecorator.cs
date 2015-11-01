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
                CustomColors cc = go.AddComponent<CustomColors>();

                List<Color> colors = new List<Color>();

                if (options.ContainsKey("recolorableOptions"))
                {
                    Dictionary<string, object> clrs = (Dictionary <string, object>)options["recolorableOptions"];

                    colors.AddRange(clrs.Values.Select(color => FromHex((string) color)));
                }

                cc.customColors = colors.ToArray();
                
                foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
                {
                    if (material.name == "CustomColorsDiffuse")
                    {
                        go.GetComponentInChildren<Renderer>().sharedMaterial = material;

                        break;
                    }
                }
            }
        }

        private Color FromHex(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length != 6) throw new Exception("Color not valid");

            return new Color(
                int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber));
        }
    }
}
