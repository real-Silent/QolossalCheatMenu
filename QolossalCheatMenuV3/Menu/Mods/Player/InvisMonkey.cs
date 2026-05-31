using Qolossal.Menu;
using Photon.Pun;
using UnityEngine;
using System;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class InvisMonkey : MonoBehaviour
    {
        public InvisMonkey(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.invismonkey)
            {
                if (PhotonNetwork.InRoom)
                {
                    string bind = CustomBinding.GetBinds("invismonkey");
                    if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                    {
                        return;
                    }
                    if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                        return;
                    if (ControlsV2.GetControl(bind))
                    {
                        if (GorillaTagger.Instance.myVRRig.enabled)
                            GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.transform.position = new Vector3(GorillaTagger.Instance.headCollider.transform.position.x, -646.46466f, GorillaTagger.Instance.headCollider.transform.position.z);
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
                GameObject.Destroy(Plugin.holder.GetComponent<InvisMonkey>());
            }
        }
    }
}