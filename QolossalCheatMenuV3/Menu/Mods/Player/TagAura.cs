using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class TagAura : MonoBehaviour
    {
        public TagAura(IntPtr e) : base(e) { }
        static LineRenderer radiusLine;
        static Material lineMaterial;
        static float ammount;

        static readonly Color[] BeamColors = {
            new Color(0.6f, 0f, 0.8f, 0.5f), // Purple
            new Color(1f, 0f, 0f, 0.5f), // Red
            new Color(1f, 1f, 0f, 0.5f), // Yellow
            new Color(0f, 1f, 0f, 0.5f), // Green
            new Color(0f, 0f, 1f, 0.5f)  // Blue
        };

        public virtual void Update()
        {
            if (PluginConfig.tagaura == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<TagAura>());
                return;
            }
            if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                return;
            if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                return;
            if (lineMaterial == null)
                lineMaterial = new Material(Shader.Find("GUI/Text Shader"));
            ammount = GetAmmountFromConfig();
            lineMaterial.color = BeamColors[Mathf.Min(PluginConfig.BeamColour, BeamColors.Length - 1)];
            if (ammount > 0 && PhotonNetwork.InRoom && GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
            {
                HandleTagAura();
            }
            else
            {
                if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                    return;
                Cleanup();
            }
        }

        static float GetAmmountFromConfig()
        {
            switch (PluginConfig.tagaura)
            {
                case 1: return 4.5f;
                case 2: return 4f;
                case 3: return 3.5f;
                case 4: return 3f;
                case 5: return 2.5f;
                case 6: return 2f;
                case 7: return 1f;
                default: return 0f;
            }
        }

        static void HandleTagAura()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected")) continue;
                float distanceSquared = (GorillaTagger.Instance.myVRRig.transform.position - vrrig.transform.position).sqrMagnitude;
                float thresholdSquared = Mathf.Pow(GorillaGameManager.instance.GetComponent<GorillaTagManager>().tagDistanceThreshold / ammount, 2);
                if (distanceSquared <= thresholdSquared && !vrrig.isMyPlayer)
                {
                    CreateOrUpdateLine(vrrig);
                    WhatAmI.infectionmanager().AddInfectedPlayer(vrrig.photonView.Owner);
                }
            }
        }

        static void CreateOrUpdateLine(VRRig vrrig)
        {
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
            radiusLine.SetPosition(1, GorillaTagger.Instance.transform.position);
        }

        static void Cleanup()
        {
            if (radiusLine != null)
            {
                Destroy(radiusLine.gameObject);
                radiusLine = null;
            }
        }
    }
}