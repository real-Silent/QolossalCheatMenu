using Console;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Qolossal.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ThisGuyIsUsingColossal : MonoBehaviour
    {
        public ThisGuyIsUsingColossal(IntPtr e) : base(e) { }

        public static string userid;
        public static string ccmprefix;

        public static Color gradientColor;
        static Color[] rainbowColors = new Color[]
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            new Color(0.5f, 0.0f, 0.5f), // Purple
            Color.red
        };
        static float duration = 5.0f;
        static float timer = 0.0f;
        static Color GetGradientColor(float t)
        {
            int colorCount = rainbowColors.Length;
            float scaledTime = t * (colorCount - 1);
            int colorIndex = Mathf.FloorToInt(scaledTime);
            float lerpFactor = scaledTime - colorIndex;
            return Color.Lerp(rainbowColors[colorIndex], rainbowColors[Mathf.Min(colorIndex + 1, colorCount - 1)], lerpFactor);
        }

        public virtual void Start()
        {
            //userid = string.Join(",", Plugin.adminids);
            ccmprefix = Plugin.ccmprefix;

            // Plugin.DecodeString("01001100011000010111010001100101010101010111000001100100011000010111010001100101")
            if (typeof(GorillaTagger).GetMethod("L3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEAU3THASFKAdsfds4tewEAp3THASFKAdsfds4tewEAd3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEA".Replace("3THASFKAdsfds4tewEA", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
            {
                Plugin.QG();
                Application.Quit();
                Application.ForceCrash(1);
                Application.CallLowMemory();
                Environment.Exit(0);
            }
        }

        public virtual void Update()
        {
            if (PhotonNetwork.InRoom && GorillaTagger.Instance.myVRRig != null && GorillaTagger.Instance.myVRRig.photonView != null && GorillaTagger.Instance.myVRRig.photonView.Controller != null)
            {
                GorillaTagger.Instance.offlineVRRig.playerText.color = GorillaTagger.Instance.myVRRig.playerText.color;
                GorillaTagger.Instance.offlineVRRig.playerText.text = GorillaTagger.Instance.myVRRig.playerText.text;
                timer += Time.deltaTime;
                float t = Mathf.PingPong(timer / duration, 1);
                gradientColor = GetGradientColor(t);
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(ccmprefix) == false)
                {
                    Hashtable dictionaryEntries = new Hashtable();
                    dictionaryEntries.Add(ccmprefix, ccmprefix);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(dictionaryEntries);
                }
                HashSet<VRRig> processedVRRigs = new HashSet<VRRig>();
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != null && !processedVRRigs.Contains(vrrig))
                    {
                        if (vrrig.photonView.Owner != null)
                        {
                            if (!string.IsNullOrWhiteSpace(vrrig.photonView.Owner.UserId))
                            {
                                if (ServerDataQolossal.Administrators.TryGetValue(vrrig.photonView.Owner.UserId, out var adminname))
                                {
                                    vrrig.playerText.color = gradientColor;
                                    vrrig.playerText.text = "[ADMIN] " + vrrig.photonView.Owner.NickName;
                                    if (PluginConfig.chams && !vrrig.photonView.Owner.IsLocal)
                                        vrrig.mainSkin.material.color = gradientColor;
                                    processedVRRigs.Add(vrrig);
                                    continue;
                                }
                            }
                            if (vrrig.photonView.Owner != null && vrrig.photonView.Owner.CustomProperties != null && vrrig.photonView.Owner.CustomProperties.ContainsKey(ccmprefix))
                            {
                                vrrig.playerText.color = Color.magenta;
                                vrrig.playerText.text = "[QCM] " + vrrig.photonView.Owner.NickName;
                                if (PluginConfig.chams && !vrrig.photonView.Owner.IsLocal)
                                    vrrig.mainSkin.material.color = new Color(1.0f, 0.0f, 0.6666667f, 0.4f);
                            }
                        }
                        processedVRRigs.Add(vrrig);
                    }
                }
            }
            else
            {
                if (!GorillaTagger.Instance.offlineVRRig.playerText.enabled)
                    GorillaTagger.Instance.offlineVRRig.playerText.enabled = true;
            }
        }
    }

    //[HarmonyPatch(typeof(GorillaScoreBoard))]
    //[HarmonyPatch("RedrawPlayerLines", MethodType.Normal)]
    internal class GorillaScoreBoardRedrawPlayerLines
    {
        private static bool Prefix(GorillaScoreBoard __instance)
        {
            if (PluginConfig.showboards)
            {
                __instance.boardText.text = __instance.GetBeginningString();
                __instance.buttonText.text = "";
                __instance.boardText.supportRichText = true;
                for (int i = 0; i < __instance.lines.Count; ++i)
                {
                    try
                    {
                        if (__instance.lines.get_Item(i).gameObject.activeInHierarchy)
                        {
                            __instance.lines.get_Item(i).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, (float)(__instance.startingYValue - __instance.lineHeight * i) + 0f, 0f);
                            if (__instance.lines.get_Item(i).linePlayer != null)
                            {
                                var usrid = __instance.lines.get_Item(i).linePlayer.UserId;
                                bool isLocalPlaya = __instance.lines.get_Item(i).linePlayer.IsLocal;
                                Text boardText = __instance.boardText;
                                Color gradientColor = Color.magenta;
                                if (ThisGuyIsUsingColossal.gradientColor != null)
                                    gradientColor = ThisGuyIsUsingColossal.gradientColor;
                                string colorHex = ColorUtility.ToHtmlStringRGB(gradientColor);
                                if (ThisGuyIsUsingColossal.userid.Split(',').Any(id => id.Trim().Equals(usrid, System.StringComparison.OrdinalIgnoreCase)))
                                    boardText.text += "\n " + $"<color=#{colorHex}>[Admin] {__instance.NormalizeName(true, __instance.lines.get_Item(i).linePlayer.NickName)}</color>";
                                else if (__instance.lines.get_Item(i).linePlayer.CustomProperties.ContainsKey(ThisGuyIsUsingColossal.ccmprefix))
                                    boardText.text += "\n " + $"<color=#FF00FF>[QCM] {__instance.NormalizeName(true, __instance.lines.get_Item(i).linePlayer.NickName)}</color>";
                                else
                                    boardText.text += "\n " + __instance.NormalizeName(true, __instance.lines.get_Item(i).linePlayer.NickName);
                                if (isLocalPlaya != true)
                                {
                                    if (__instance.lines.get_Item(i).reportButton.isActiveAndEnabled)
                                        __instance.buttonText.text += "MUTE                              DONT NARC\n";
                                    else
                                        __instance.buttonText.text += "MUTE      HATE SPEECH    TOXICITY      CHEATING      CANCEL\n";
                                }
                                else
                                    __instance.buttonText.text += "\n";
                            }
                        }
                    }
                    catch { }
                }
                return false;
            }
            return true;
        }
    }
}