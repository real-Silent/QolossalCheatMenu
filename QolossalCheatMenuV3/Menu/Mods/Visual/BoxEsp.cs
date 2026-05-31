using Qolossal.Menu;
using Photon.Pun;
using UnityEngine;
using System;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class BoxEsp : MonoBehaviour
    {
        public BoxEsp(IntPtr e) : base(e) { }
        public static float objectScale;
        static Color espcolor;

        public virtual void Update()
        {
            if (PluginConfig.boxesp)
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

                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (rig != null && rig != GorillaTagger.Instance.myVRRig && rig.photonView.Owner != PhotonNetwork.LocalPlayer && rig.photonView.Owner.UserId != PhotonNetwork.LocalPlayer.UserId && !rig.photonView.IsMine)
                        {
                            Camera mainCamera = Camera.main;
                            Matrix4x4 projectionMatrix = mainCamera.projectionMatrix;
                            Vector3 objectWorldPosition = rig.headConstraint.transform.position;
                            float objectDistanceFromCamera = Vector3.Distance(objectWorldPosition, mainCamera.transform.position);
                            Matrix4x4 worldToCameraMatrix = mainCamera.worldToCameraMatrix;
                            Vector3 objectViewportPosition = mainCamera.WorldToViewportPoint(objectWorldPosition);
                            Vector4 objectClipPosition = projectionMatrix * worldToCameraMatrix * new Vector4(objectWorldPosition.x, objectWorldPosition.y, objectWorldPosition.z, 1);
                            objectClipPosition /= objectClipPosition.w;
                            objectScale = (objectDistanceFromCamera / objectClipPosition.w);
                            float minScale = 2f;
                            float maxScale = 8.5f;
                            objectScale = Mathf.Clamp(objectScale, minScale, maxScale);
                            GameObject go = new GameObject("box");
                            go.transform.position = rig.headConstraint.transform.position;
                            GameObject face = GameObject.CreatePrimitive(PrimitiveType.Plane);
                            GameObject.Destroy(face.GetComponent<Collider>());
                            face.transform.SetParent(go.transform);
                            face.transform.localPosition = new Vector3(0f, 0f, 0f);
                            face.transform.localRotation = Quaternion.Euler(90, 0, 0);
                            face.transform.localScale = new Vector3(objectScale / 40, objectScale / 40, objectScale / 40);
                            face.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            Color Espcolor;
                            if (WhatAmI.IsPlayerSomethingWithTag(rig))
                                Espcolor = new Color(1f, 0f, 0f, 0.4f);
                            else
                                Espcolor = espcolor;
                            face.GetComponent<Renderer>().material.color = Espcolor;
                            Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
                            go.transform.rotation = rotation;

                            GameObject.Destroy(go, Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<BoxEsp>());
            }
        }
    }
}