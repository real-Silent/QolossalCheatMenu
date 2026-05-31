using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class FullBright : MonoBehaviour
    {
        public FullBright(IntPtr e) : base(e) { }
        static bool fuckmylife = false;
        public virtual void Update()
        {
            if (PluginConfig.fullbright)
            {
                if (LightmapSettings.lightmaps != null)
                    LightmapSettings.lightmaps = null;
                RenderSettings.fog = false;
                RenderSettings.ambientLight = Color.white;
                if (fuckmylife)
                    fuckmylife = false;
            }
            else
            {
                if(!fuckmylife)
                {
                    RenderSettings.fog = true;
                    fuckmylife = true;
                }
                GameObject.Destroy(this.GetComponent<FullBright>());
            }
        }
    }
}