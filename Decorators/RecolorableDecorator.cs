﻿using System;
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
            Color clr = new Color(0, 0, 0);

            if (!string.IsNullOrEmpty(hex))
            {
                try
                {
                    string str = hex.Substring(1, hex.Length - 1);
                    clr.r = Parse(str.Substring(0, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    clr.g = Parse(str.Substring(2, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    clr.b = Parse(str.Substring(4, 2),
                        NumberStyles.AllowHexSpecifier) / 255.0f;
                    if (str.Length == 8)
                        clr.a = Parse(str.Substring(6, 2),
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
