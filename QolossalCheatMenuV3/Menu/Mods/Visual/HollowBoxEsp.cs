using Photon.Pun;
using Qolossal.Menu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class HollowBoxEsp : MonoBehaviour
    {
        public HollowBoxEsp(IntPtr e) : base(e) { }
        private static Color espColor;
        static Dictionary<string, TextMesh> espBoxes = new Dictionary<string, TextMesh>();
        private Color ESPCOLOR;

        public virtual void Update()
        {
            if (PluginConfig.hollowboxesp)
            {
                if (GorillaParent.instance == null)
                    return;

                switch (PluginConfig.ESPColour)
                {
                    case 0: espColor = new Color(0.6f, 0f, 0.8f, 0.4f); break;
                    case 1: espColor = new Color(1f, 0f, 0f, 0.4f); break;
                    case 2: espColor = new Color(1f, 1f, 0f, 0.4f); break;
                    case 3: espColor = new Color(0f, 1f, 0f, 0.4f); break;
                    case 4: espColor = new Color(0f, 0f, 1f, 0.4f); break;
                    default: espColor = new Color(0.6f, 0f, 0.8f, 0.4f); break;
                }

                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig == null || rig.photonView == null || rig.photonView.Owner == null)
                        continue;

                    if (rig.photonView.Owner.UserId == PhotonNetwork.LocalPlayer.UserId)
                        continue;

                    string id = rig.photonView.Owner.UserId;
                    if (string.IsNullOrEmpty(id))
                        continue;

                    if (WhatAmI.IsPlayerSomethingWithTag(rig))
                        ESPCOLOR = new Color(1f, 0f, 0f, 0.4f);
                    else
                        ESPCOLOR = espColor;

                    CreateOrUpdateBox(id, rig.headMesh.gameObject, ESPCOLOR);
                }

                List<string> toRemove = new List<string>();
                foreach (var kvp in espBoxes)
                {
                    if (kvp.Value == null)
                        toRemove.Add(kvp.Key);
                }
                foreach (string key in toRemove)
                {
                    espBoxes.Remove(key);
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<HollowBoxEsp>());
                foreach (var kvp in espBoxes)
                {
                    if (kvp.Value != null)
                        GameObject.Destroy(kvp.Value.gameObject);
                }
                espBoxes.Clear();
                return;
            }
        }

        static void CreateOrUpdateBox(string userId, GameObject headMesh, Color color)
        {
            if (!espBoxes.TryGetValue(userId, out TextMesh box) || box == null)
            {
                GameObject textObject = new GameObject($"HollowBoxESP_{userId}");
                box = textObject.AddComponent<TextMesh>();
                box.alignment = TextAlignment.Center;
                box.anchor = TextAnchor.MiddleCenter;
                box.text = "□";
                box.fontSize = 300;
                box.characterSize = 0.05f;
                box.color = color;

                textObject.transform.SetParent(headMesh.transform, false);
                textObject.transform.localPosition = Vector3.zero;

                espBoxes[userId] = box;
            }

            if (Camera.main != null)
            {
                box.transform.LookAt(
                    box.transform.position + Camera.main.transform.rotation * Vector3.forward,
                    Camera.main.transform.rotation * Vector3.up);
            }

            box.color = color;
        }
    }
}