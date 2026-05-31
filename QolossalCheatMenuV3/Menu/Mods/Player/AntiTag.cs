using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class AntiTag : MonoBehaviour
    {
        public AntiTag(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.antitag)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null || GorillaTagManager.instance == null || GorillaGameManager.instance == null || GorillaParent.instance == null)
                    return;
                if (PhotonNetwork.InRoom)
                {
                    if (!WhatAmI.IsInfected(PhotonNetwork.LocalPlayer))
                    {
                        bool shouldDisable = false;
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != null)
                            {
                                if (WhatAmI.infectionmanager() == null || PhotonNetwork.LocalPlayer == null)
                                    return;
                                if (!WhatAmI.IsInfected(vrrig.photonView.Owner) || vrrig.isMyPlayer)
                                    continue;
                                float distance = Vector3.Distance(GorillaTagger.Instance.myVRRig.transform.position, vrrig.transform.position);
                                if (distance <= WhatAmI.infectionmanager().tagDistanceThreshold * 1.6f)
                                {
                                    shouldDisable = true;
                                }
                            }
                        }
                        if (shouldDisable)
                        {
                            if (GorillaTagger.Instance.myVRRig.enabled)
                                GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = new Vector3(0, -6969, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.myVRRig.enabled)
                                GorillaTagger.Instance.myVRRig.enabled = true;
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<AntiTag>());
                if (!PhotonNetwork.InRoom) return;
                if (!GorillaTagger.Instance.myVRRig.enabled && !PluginConfig.SpinBot && !PluginConfig.fakelag && !PluginConfig.desync && !PluginConfig.tagall && !PluginConfig.taggun && !PluginConfig.ghostmonkey && !PluginConfig.invismonkey)
                    GorillaTagger.Instance.myVRRig.enabled = true;
                return;
            }
        }
    }
}