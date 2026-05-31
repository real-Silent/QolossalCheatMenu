using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class TagAll : MonoBehaviour
    {
        public TagAll(IntPtr e) : base(e) { }
        static LineRenderer radiusLine;
        static Material lineMaterial = new Material(Shader.Find("GUI/Text Shader"));
        public virtual void Update()
        {
            if (PluginConfig.tagall)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;

                if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                    return;

                switch (PluginConfig.BeamColour)
                {
                    case 0:
                        lineMaterial.color = new Color(0.6f, 0f, 0.8f, 0.5f);
                        break;
                    case 1:
                        lineMaterial.color = new Color(1f, 0f, 0f, 0.5f);
                        break;
                    case 2:
                        lineMaterial.color = new Color(1f, 1f, 0f, 0.5f);
                        break;
                    case 3:
                        lineMaterial.color = new Color(0f, 1f, 0f, 0.5f);
                        break;
                    case 4:
                        lineMaterial.color = new Color(0f, 0f, 1f, 0.5f);
                        break;
                }

                if (PhotonNetwork.InRoom)
                {
                    if (GorillaGameManager.instance.GetComponent<GorillaTagManager>().currentInfected.Count < 10)
                    {
                        if (PhotonNetwork.LocalPlayer.IsMasterClient)
                        {
                            foreach (Photon.Realtime.Player v in PhotonNetwork.PlayerList)
                                GorillaGameManager.instance.GetComponent<GorillaTagManager>().AddInfectedPlayer(v);
                            PluginConfig.tagall = false;
                            return;
                        }
                        if (GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected") || GorillaTagger.Instance.myVRRig.mainSkin.material.name.ToLower().Contains("it"))
                        {
                            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                            {
                                if (!vrrig.mainSkin.material.name.Contains("fected") || !vrrig.mainSkin.material.name.ToLower().Contains("it"))
                                {
                                    if (GorillaTagger.Instance.myVRRig.enabled)
                                        GorillaTagger.Instance.myVRRig.enabled = false;

                                    GorillaTagger.Instance.myVRRig.transform.position = vrrig.transform.position + new Vector3(0f, -2, 0f);
                                    GorillaTagger.Instance.rightHandTransform.position = vrrig.headMesh.transform.position;
                                    WhatAmI.infectionmanager().AddInfectedPlayer(vrrig.photonView.Owner);

                                    if (radiusLine == null)
                                    {
                                        GameObject lineObject = new GameObject("RadiusLine");
                                        lineObject.transform.parent = vrrig.transform;
                                        radiusLine = lineObject.AddComponent<LineRenderer>();
                                        radiusLine.positionCount = 2;
                                        radiusLine.startWidth = 0.05f;
                                        radiusLine.endWidth = 0.05f;
                                        radiusLine.material = lineMaterial;
                                        radiusLine.startColor = lineMaterial.color;
                                        radiusLine.endColor = lineMaterial.color;
                                    }
                                    radiusLine.SetPosition(0, vrrig.transform.position);
                                    radiusLine.SetPosition(1, GorillaTagger.Instance.mainCamera.transform.position);
                                    if (radiusLine.GetPosition(0) == null)
                                    {
                                        if (radiusLine != null)
                                        {
                                            Destroy(radiusLine);
                                            radiusLine = null;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                            {
                                if (vrrig.mainSkin.material.name.Contains("fected") || vrrig.mainSkin.material.name.ToLower().Contains("it"))
                                {
                                    if (GorillaTagger.Instance.myVRRig.enabled)
                                        GorillaTagger.Instance.myVRRig.enabled = false;
                                    GorillaTagger.Instance.myVRRig.transform.position = vrrig.rightHandTransform.position;
                                }
                            }
                        }
                    }
                    else
                    {
                        PluginConfig.tagall = false;

                        if (PhotonNetwork.InRoom)
                        {
                            if (!GorillaTagger.Instance.myVRRig.enabled)
                                GorillaTagger.Instance.myVRRig.enabled = true;
                        }
                        if (radiusLine != null)
                        {
                            Destroy(radiusLine.gameObject);
                            radiusLine = null;
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<TagAll>());
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;
                if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                    return;
                if (PhotonNetwork.InRoom)
                {
                    if (!GorillaTagger.Instance.myVRRig.enabled && !PluginConfig.SpinBot && !PluginConfig.fakelag && !PluginConfig.desync && !PluginConfig.taggun && !PluginConfig.ghostmonkey && !PluginConfig.invismonkey)
                        GorillaTagger.Instance.myVRRig.enabled = true;
                }
                if (radiusLine != null)
                {
                    Destroy(radiusLine.gameObject);
                    radiusLine = null;
                }
            }
        }
    }
}