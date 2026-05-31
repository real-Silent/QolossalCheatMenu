using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods {
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class PcCheckBypass : MonoBehaviour 
    {
        public PcCheckBypass(IntPtr e) : base(e) { }
        public virtual void Update() 
        {
            if (PluginConfig.pccheckbypass)
            {
                if (GameObject.Find("Mountain/Geometry/goodigloo") != null)
                {
                    if (GameObject.Find("Mountain/Geometry/goodigloo").activeSelf)
                        GameObject.Find("Mountain/Geometry/goodigloo").SetActive(false);
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<PcCheckBypass>());
                if (GameObject.Find("Mountain/Geometry/goodigloo") != null)
                {
                    if (!GameObject.Find("Mountain/Geometry/goodigloo").activeSelf)
                        GameObject.Find("Mountain/Geometry/goodigloo").SetActive(true);
                }
            }
        }
    }
}