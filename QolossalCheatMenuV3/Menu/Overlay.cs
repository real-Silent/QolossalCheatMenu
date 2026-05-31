using GorillaNetworking;
using Photon.Pun;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Qolossal.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Overlay : MonoBehaviour
    {
        public Overlay(IntPtr e) : base(e) { }

        static float deltaTime;

        public static GameObject OverlayHub;
        public static Text OverlayHubText;

        public static GameObject OverlayHubRoom;
        public static Text OverlayHubTextRoom;

        public bool ExtraDebugUselessStuff = false; // just for like dev stuff to see if some mods work or not

        public static void SpawnOverlay()
        {
            if (typeof(GorillaTagger).GetMethod("L3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEAU3THASFKAdsfds4tewEAp3THASFKAdsfds4tewEAd3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEA".Replace("3THASFKAdsfds4tewEA", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
            {
                Plugin.QG();
                Application.Quit();
                Application.ForceCrash(1);
                Application.CallLowMemory();
                Environment.Exit(0);
            }

            // Security stuff dont touch it
            Plugin.CheckIntegrity2(Plugin.DecodeString(Plugin.anti2));

            (OverlayHub, OverlayHubText) = GUICreator.CreateTextGUI("", "OverlayHub", TextAnchor.LowerLeft, new Vector3(0, 0f, 3.6f));
            (OverlayHubRoom, OverlayHubTextRoom) = GUICreator.CreateTextGUI("", "OverlayHubRoom", TextAnchor.LowerRight, new Vector3(0, 0f, 3.6f));
        }

        public virtual void Update()
        {
            if (PluginConfig.overlay && Menu.agreement)
            {
                if (Plugin.update)
                {
                    OverlayHubText.text = "<color=red>UPDATE NEEDED</color>";
                    OverlayHubTextRoom.text = "<color=red>UPDATE NEEDED</color>";
                    return;
                }
                if (Plugin.locked || Plugin.serverLocked)
                {
                    OverlayHubText.text = "<color=red>QOLOSSAL HAS BEEN LOCKED</color>";
                    OverlayHubTextRoom.text = "<color=red>KILLSWITCHED</color>";
                    return;
                }
                if (!Plugin.hasvalidkey)
                {
                    OverlayHubText.text = "<color=yellow>DONT TRY TO CRACK QOLOSSAL\nYOUR IP AND HWID\nHAS BEEN COLLECTED\nDUE TO SAFETY CONCERNS</color>";
                    OverlayHubTextRoom.text = "<color=yellow>DONT TRY TO CRACK QOLOSSAL\nYOUR IP AND HWID\nHAS BEEN COLLECTED\nDUE TO SAFETY CONCERNS</color>";
                    return;
                }

                deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;

                if (PhotonNetwork.InRoom)
                    OverlayHubTextRoom.text = $"<color={Menu.MenuColour}>RoomName: </color>{PhotonNetwork.CurrentRoom.Name}\n<color={Menu.MenuColour}>Players: </color>{PhotonNetwork.CurrentRoom.PlayerCount}";
                else
                {
                    if (OverlayHubTextRoom.text != null)
                        OverlayHubTextRoom.text = "";
                }
                if (!ExtraDebugUselessStuff)
                    OverlayHubText.text = $"<color={Menu.MenuColour}>Ping: </color>{PhotonNetwork.GetPing()}\n<color={Menu.MenuColour}>FPS: </color>{fps.ToString("F2")}\n<color={Menu.MenuColour}>Play Time: </color>{Plugin.playtimestring}";
                else
                    OverlayHubText.text = $"<color={Menu.MenuColour}>Ping: </color>{PhotonNetwork.GetPing()}\n<color={Menu.MenuColour}>FPS: </color>{fps.ToString("F2")}\n<color={Menu.MenuColour}>Play Time: </color>{Plugin.playtimestring}\n<color={Menu.MenuColour}>Max Speed: </color>{GorillaLocomotion.Player.Instance.maxJumpSpeed}\n<color={Menu.MenuColour}>Current Speed: </color>{GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude}\n<color={Menu.MenuColour}>Master: </color>{PhotonNetwork.IsMasterClient}\n<color={Menu.MenuColour}>Mode: </color>{GorillaComputer.instance.currentGameMode}";
                if (SystemInfo.batteryLevel <= 0.15f)
                {
                    OverlayHubText.text = $"<color={Menu.MenuColour}>Ping: </color>{PhotonNetwork.GetPing()}\n<color={Menu.MenuColour}>FPS: </color>{fps.ToString("F2")}\n<color={Menu.MenuColour}>Play Time: </color>{Plugin.playtimestring}\n<color={Menu.MenuColour}>Battery: </color>{SystemInfo.batteryLevel}";
                }
            }
            else
            {
                if (OverlayHubText.text != null)
                    OverlayHubText.text = "";
                if (OverlayHubTextRoom.text != null)
                    OverlayHubTextRoom.text = "";
            }
        }
    }
}