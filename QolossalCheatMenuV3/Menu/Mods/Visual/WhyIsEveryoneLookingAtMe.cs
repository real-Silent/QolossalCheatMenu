using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class WhyIsEveryoneLookingAtMe : MonoBehaviour
    {
        public WhyIsEveryoneLookingAtMe(IntPtr e) : base(e) { } 
        public virtual void Update()
        {
            if (PluginConfig.whyiseveryonelookingatme)
            {
                if (PhotonNetwork.InRoom)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig)
                            vrrig.headConstraint.LookAt(GorillaTagger.Instance.headCollider.transform);
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<WhyIsEveryoneLookingAtMe>());
            }
        }
    }
}