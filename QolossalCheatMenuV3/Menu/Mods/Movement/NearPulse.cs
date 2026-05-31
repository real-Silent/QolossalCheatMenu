using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    class NearPulse : MonoBehaviour
    {
        public NearPulse(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.NearPulse == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<NearPulse>());
                return;
            }

            if (PluginConfig.NearPulse != 0 && PhotonNetwork.InRoom)
            {
                if (GorillaTagger.Instance == null)
                    return;
                bool infected = WhatAmI.IsPlayerSomethingWithTag(GorillaTagger.Instance.myVRRig);
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        float distance = Vector3.Distance(GorillaTagger.Instance.transform.position, vrrig.transform.position);
                        if (!infected)
                        {
                            if (WhatAmI.IsPlayerSomethingWithTag(vrrig) && distance <= PluginConfig.NearPulseDistance)
                                GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddExplosionForce(-PluginConfig.NearPulse * 4, vrrig.transform.position, PluginConfig.NearPulseDistance);
                        }
                        else
                        {
                            if (distance <= PluginConfig.NearPulseDistance)
                                GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddExplosionForce(PluginConfig.NearPulse * 4, vrrig.transform.position, PluginConfig.NearPulseDistance);
                        }
                    }
                }
                //can someone fix this -Starry
                // Fixed it pookie <333 -Colossus
                // Made it better -Nova
            }
        }
    }
}