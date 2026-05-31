using Qolossal.Menu;
using Photon.Pun;
using UnityEngine;
using System;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GhostMonkey : MonoBehaviour
    {
        public GhostMonkey(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.ghostmonkey)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;
                if (PhotonNetwork.InRoom)
                {
                    string bind = CustomBinding.GetBinds("ghostmonkey");
                    if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                    {
                        return;
                    }
                    if (ControlsV2.GetControl(bind))
                    {
                        if (GorillaTagger.Instance.myVRRig.enabled)
                            GorillaTagger.Instance.myVRRig.enabled = false;
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.myVRRig.enabled)
                            GorillaTagger.Instance.myVRRig.enabled = true;
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<GhostMonkey>());
            }
        }
    }
}