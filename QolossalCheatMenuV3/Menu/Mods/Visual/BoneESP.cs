using Photon.Pun;
using Qolossal.Menu;
using System;
using System.Linq;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class BoneESP : MonoBehaviour
    {
        public BoneESP(IntPtr e) : base(e) { }
        static Color espcolor;
        static int[] bones = new int[]
        {
            4,
            3,
            5,
            4,
            19,
            18,
            20,
            19,
            3,
            18,
            21,
            20,
            22,
            21,
            25,
            21,
            29,
            21,
            31,
            29,
            27,
            25,
            24,
            22,
            6,
            5,
            7,
            6,
            10,
            6,
            14,
            6,
            16,
            14,
            12,
            10,
            9,
            7
        };
        public virtual void Update()
        {
            if (PluginConfig.boneesp)
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
                        if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig && vrrig.photonView.Owner != PhotonNetwork.LocalPlayer && vrrig.photonView.Owner.UserId != PhotonNetwork.LocalPlayer.UserId && !vrrig.photonView.IsMine)
                        {
                            Color color;
                            if (WhatAmI.IsPlayerSomethingWithTag(vrrig))
                            {
                                color = new Color(1f, 0f, 0f, 0.4f);
                            }
                            else
                            {
                                color = espcolor;
                            }
                            LineRenderer lineRenderer = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                            lineRenderer.startWidth = 0.025f;
                            lineRenderer.endWidth = 0.025f;
                            lineRenderer.startColor = color;
                            lineRenderer.endColor = color;
                            lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                            lineRenderer.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                            lineRenderer.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                            GameObject.Destroy(lineRenderer, Time.deltaTime);
                            for (int i = 0; i < bones.Count<int>(); i += 2)
                            {
                                lineRenderer = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();
                                lineRenderer.startWidth = 0.025f;
                                lineRenderer.endWidth = 0.025f;
                                lineRenderer.startColor = color;
                                lineRenderer.endColor = color;
                                lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                                lineRenderer.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                                lineRenderer.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);
                                GameObject.Destroy(lineRenderer, Time.deltaTime);
                            }
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<BoneESP>());
            }
        }
    }
}