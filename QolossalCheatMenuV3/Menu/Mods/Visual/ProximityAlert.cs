using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ProximityAlert : MonoBehaviour
    {
        public ProximityAlert(IntPtr e) : base(e) { }

        public static GameObject AlertHub;
        public static Text AlertHubText;

        public virtual void Update()
        {
            if (GorillaTagManager.instance == null || GorillaGameManager.instance == null)
                return;
            if (GorillaParent.instance == null)
                return;
            if (PluginConfig.ProximityAlert)
            {
                if (AlertHub == null && AlertHubText == null)
                    (AlertHub, AlertHubText) = GUICreator.CreateTextGUI("", "AlertHub", TextAnchor.LowerCenter, new Vector3(0, 0, 2));
                if (PhotonNetwork.InRoom && WhatAmI.infectionmanager() != null && WhatAmI.infectionmanager().currentInfectedArray.Length > 0)
                {
                    float closestDistance = float.MaxValue;
                    string distanceText = "";
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig == null || vrrig == GorillaTagger.Instance.myVRRig ||  vrrig.photonView == null || vrrig.photonView.Owner == null || vrrig.photonView.IsMine)
                            continue;
                        if (!WhatAmI.IsPlayerSomethingWithTag(vrrig))
                            continue;
                        float distance = Vector3.Distance(Camera.main.transform.position, vrrig.transform.position);
                        if (distance < closestDistance)
                            closestDistance = distance;
                    }
                    if (closestDistance == float.MaxValue)
                    {
                        AlertHubText.color = Color.green;
                        AlertHubText.text = $"[-{int.MaxValue}]\nGood";
                        return;
                    }
                    if (closestDistance < 8f)
                    {
                        distanceText = "Very Close!";
                        AlertHubText.color = Color.red;
                    }
                    else if (closestDistance < 16f)
                    {
                        distanceText = "Close";
                        AlertHubText.color = Color.yellow;
                    }
                    else if (closestDistance < 20f)
                    {
                        distanceText = "Nearby";
                        AlertHubText.color = Color.cyan;
                    }
                    else
                    {
                        distanceText = "Good";
                        AlertHubText.color = Color.green;
                    }
                    AlertHubText.text = $"[{Mathf.RoundToInt(closestDistance)}M]\n{distanceText}";
                }
                else if (AlertHubText != null && AlertHubText.text != "")
                {
                    AlertHubText.text = "";
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<ProximityAlert>());
                if (AlertHub != null)
                    Destroy(AlertHub);
                if (AlertHubText != null)
                    Destroy(AlertHubText);
            }
        }
    }
}