using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Chams : MonoBehaviour
    {
        public Chams(IntPtr e) : base(e) { }
        static Color espcolor;
        public virtual void Update()
        {
            if (PluginConfig.chams)
            {
                if (PhotonNetwork.InRoom)
                {
                    if (GorillaParent.instance == null)
                        return;
                    switch (PluginConfig.ESPColour)
                    {
                        case 0:
                            espcolor = new Color(0.6f, 0f, 0.8f, 0.4f);
                            break;
                        case 1:
                            espcolor = new Color(1f, 0f, 0f, 0.4f);
                            break;
                        case 2:
                            espcolor = new Color(1f, 1f, 0f, 0.4f);
                            break;
                        case 3:
                            espcolor = new Color(0f, 1f, 0f, 0.4f);
                            break;
                        case 4:
                            espcolor = new Color(0f, 0f, 1f, 0.4f);
                            break;
                        default:
                            espcolor = new Color(0.6f, 0f, 0.8f, 0.4f);
                            break;
                    }
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig && vrrig.photonView.Owner != PhotonNetwork.LocalPlayer && vrrig.photonView.Owner.UserId != PhotonNetwork.LocalPlayer.UserId && !vrrig.photonView.IsMine && vrrig.mainSkin.material.shader != Shader.Find("GUI/Text Shader"))
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                            if (WhatAmI.IsPlayerSomethingWithTag(vrrig))
                                vrrig.mainSkin.material.color = new Color(1f, 0, 0, 0.4f);
                            else
                            {
                                if (espcolor != null)
                                    vrrig.mainSkin.material.color = espcolor;
                            }
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Chams>());
                if (PhotonNetwork.InRoom)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.myVRRig && vrrig != null && vrrig.mainSkin.material.shader != Shader.Find("Standard"))
                        {
                            vrrig.mainSkin.material = vrrig.materialsToChangeTo[vrrig.setMatIndex];
                        }
                    }
                }
            }
        }
    }
}