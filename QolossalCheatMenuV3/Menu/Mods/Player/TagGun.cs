using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class TagGun : MonoBehaviour
    {
        public TagGun(IntPtr e) : base(e) { }
        static GameObject pointer;
        static LineRenderer radiusLine;
        static Material lineMaterial = new Material(Shader.Find("GUI/Text Shader"));
        static Vector3 originalPosition;

        static Color beamColour;

        public virtual void Update()
        {
            if (PluginConfig.taggun && PhotonNetwork.InRoom)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;

                if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                    return;

                switch (PluginConfig.BeamColour)
                {
                    case 0: beamColour = new Color(0.6f, 0f, 0.8f, 0.5f); break; // Purple
                    case 1: beamColour = new Color(1f, 0f, 0f, 0.5f); break;    // Red
                    case 2: beamColour = new Color(1f, 1f, 0f, 0.5f); break;    // Yellow
                    case 3: beamColour = new Color(0f, 1f, 0f, 0.5f); break;    // Green
                    case 4: beamColour = new Color(0f, 0f, 1f, 0.5f); break;    // Blue
                }

                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    pointer.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));
                    pointer.GetComponent<Renderer>().material.color = beamColour;
                }

                RaycastHit raycastHit;
                LayerMask combinedLayerMask = GorillaLocomotion.Player.Instance.locomotionEnabledLayers | 16384;
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position - GorillaTagger.Instance.rightHandTransform.up,
                                -GorillaTagger.Instance.rightHandTransform.up, out raycastHit, float.PositiveInfinity, combinedLayerMask);
                pointer.transform.position = raycastHit.point;
                originalPosition = GorillaTagger.Instance.myVRRig.transform.position;

                string bind = CustomBinding.GetBinds("taggun");
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }
                if (ControlsV2.GetControl(bind))
                {
                    if (radiusLine == null)
                    {
                        lineMaterial.color = beamColour;

                        radiusLine = new GameObject("RadiusLine") { transform = { parent = pointer.transform } }.AddComponent<LineRenderer>();
                        radiusLine.positionCount = 2;
                        radiusLine.startWidth = 0.05f;
                        radiusLine.endWidth = 0.05f;
                        radiusLine.material = lineMaterial;
                        radiusLine.startColor = lineMaterial.color;
                        radiusLine.endColor = lineMaterial.color;
                    }
                    radiusLine.SetPosition(0, raycastHit.point);
                    radiusLine.SetPosition(1, GorillaTagger.Instance.rightHandTransform.position);

                    VRRig targetRig = null;
                    float tagRadius = GorillaGameManager.instance.GetComponent<GorillaTagManager>().tagDistanceThreshold;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.isMyPlayer) continue;
                        float distanceToRig = Vector3.Distance(raycastHit.point, vrrig.transform.position);
                        if (distanceToRig < 1f)
                        {
                            targetRig = vrrig;
                            break;
                        }
                    }

                    if (targetRig != null)
                    {
                        float distanceToTarget = Vector3.Distance(originalPosition, targetRig.transform.position);

                        if (distanceToTarget <= tagRadius)
                        {
                            GorillaTagger.Instance.rightHandTransform.position = targetRig.headMesh.transform.position;
                            GorillaTagger.Instance.rightHandTransform.position = targetRig.headMesh.transform.position;
                        }
                        else
                        {
                            Vector3 directionToTarget = (targetRig.transform.position - originalPosition).normalized;
                            Vector3 newPosition = targetRig.transform.position - (directionToTarget * (tagRadius * 0.9f));
                            if (GorillaTagger.Instance.myVRRig.enabled)
                                GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = newPosition;
                            GorillaTagger.Instance.rightHandTransform.position = targetRig.headMesh.transform.position;
                            GorillaTagger.Instance.rightHandTransform.position = targetRig.headMesh.transform.position;
                            WhatAmI.infectionmanager().AddInfectedPlayer(targetRig.photonView.Owner);
                        }
                    }

                    return;
                }

                if (!GorillaTagger.Instance.myVRRig.enabled && !PluginConfig.SpinBot && !PluginConfig.fakelag && !PluginConfig.desync && !PluginConfig.tagall && !PluginConfig.ghostmonkey && !PluginConfig.invismonkey)
                    GorillaTagger.Instance.myVRRig.enabled = true;
                if (GorillaTagger.Instance.myVRRig.transform.position != originalPosition)
                    GorillaTagger.Instance.myVRRig.transform.position = originalPosition;

                if (radiusLine != null)
                {
                    UnityEngine.Object.Destroy(radiusLine);
                    radiusLine = null;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<TagGun>());
                if (pointer != null)
                {
                    UnityEngine.Object.Destroy(pointer);
                }
            }
        }
    }
}