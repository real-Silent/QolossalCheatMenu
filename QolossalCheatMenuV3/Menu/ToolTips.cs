using Il2CppSystem.Text.RegularExpressions;
using Photon.Pun;
using Qolossal.Notifacation;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Qolossal.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    internal class ToolTips : MonoBehaviour
    {
        public ToolTips(IntPtr e) : base(e) { }

        public static string[] MainMenutips = new string[]
        {
            $"<color={Menu.MenuColour}>Submenu</color>\nMovement mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nVisual mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nPlayer mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nComputer mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nExploits mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nSafety mods",
            $"<color={Menu.MenuColour}>Submenu</color>\nMusic player",
            $"<color={Menu.MenuColour}>Submenu</color>\nMenu settings",
            $"<color={Menu.MenuColour}>Submenu</color>\nMenu settings",
            $"<color={Menu.MenuColour}>Submenu</color>\nInformation",
            $"<color={Menu.MenuColour}>Passive</color>\nToggles noti",
            $"<color={Menu.MenuColour}>Passive</color>\nToggles overlay",
            $"<color={Menu.MenuColour}>Passive</color>\nToggles tooltips",
        };

        public static string[] Movementtips = new string[]
        {
            $"<color={Menu.MenuColour}>Custom</color>\nFly Like IronMan",
            $"<color={Menu.MenuColour}>L Secondary & Custom</color>\nFly in your right hands direction",
            $"<color={Menu.MenuColour}>Custom</color>\nPoint palms towards walls to stick",
            $"<color={Menu.MenuColour}>Submenu</color>\nDisplays Speed Options",
            $"<color={Menu.MenuColour}>Custom</color>\nJump on air",
            $"<color={Menu.MenuColour}>Passive</color>\nFlip upside down",
            $"<color={Menu.MenuColour}>Custom</color>\nSwim in air",
            $"<color={Menu.MenuColour}>R Joystick > L Trigger & R Trigger</color>\nScale the world",
            $"<color={Menu.MenuColour}>Passive</color>\nConstantly spins your ss rig",
            $"<color={Menu.MenuColour}>R & L Joystick</color>\nFly with joystick",
        };
        public static string[] Movement2tips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nSpeed up time",
            $"<color={Menu.MenuColour}>Custom</color>\nScale gravity",
            $"<color={Menu.MenuColour}>L Or R Grip</color>\nClimb Gorillas",
            $"<color={Menu.MenuColour}>Passive</color>\nFly away from tagged players",
            $"<color={Menu.MenuColour}>Passive</color>\nFly away from tagged players",
            $"<color={Menu.MenuColour}>R Joystick > L Trigger & R Trigger</color>\nScale yourself",
            $"<color={Menu.MenuColour}>Custom</color>\nPhase through walls",
            $"<color={Menu.MenuColour}>Passive</color>\nMakes you unable to move",
            $"<color={Menu.MenuColour}>Passive</color>\nTeleports to random player",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges your movement like how different hz would",
            $"<color={Menu.MenuColour}>Custom</color>\nThrow yourself by swinging your arms",
            $"<color={Menu.MenuColour}>Submenu</color>\nStrafe Options",
            $"<color={Menu.MenuColour}>Custom</color>\nLets you pull better",
        };
        public static string[] Speedtips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nAdds a speed boost",
            $"<color={Menu.MenuColour}>Custom</color>\nAdds a speed boost",
            $"<color={Menu.MenuColour}>Custom</color>\nAdds a speed boost when near infected",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges the near speed distance",
        };
        public static string[] Strafetips = new string[]
        {
            $"<color={Menu.MenuColour}>Custom</color>\nDifferent strafe modes",
            $"<color={Menu.MenuColour}>Setting</color>\nStrafe speed amount",
            $"<color={Menu.MenuColour}>Setting</color>\nStrafe jump amount",
        };

        public static string[] Visualtips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nHighlight monkies through walls",
            $"<color={Menu.MenuColour}>Passive</color>\nA filled box you can see through walls",
            $"<color={Menu.MenuColour}>Passive</color>\nA box you can see through walls",
            $"<color={Menu.MenuColour}>Passive</color>\nShows the skeleton of monkies through walls",
            $"<color={Menu.MenuColour}>Submenu</color>\nShow tracer settings",
            $"<color={Menu.MenuColour}>Submenu</color>\nShow nametag settings",
            $"<color={Menu.MenuColour}>Passive</color>\nShows how far away the nearest infected is",
            $"<color={Menu.MenuColour}>Passive</color>\nMakes everything max brightness",
            $"<color={Menu.MenuColour}>Passive</color>\nChange the sky colour",
            $"<color={Menu.MenuColour}>Passive</color>\nMake everyone look at you",
        };
        public static string[] Visual2tips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nRemoves all leaves in forest",
            $"<color={Menu.MenuColour}>Passive</color>\nShows the custom boards",
        };
        public static string[] Tracers = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nPosition of tracers",
            $"<color={Menu.MenuColour}>Passive</color>\nSize of tracers",
        };
        public static string[] Nametags = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nTurn nametags on and off",
            $"<color={Menu.MenuColour}>Passive</color>\nShow account creation date of other players",
            $"<color={Menu.MenuColour}>Passive</color>\nShow other players colour code",
            $"<color={Menu.MenuColour}>Passive</color>\nShow distance to other players",
            $"<color={Menu.MenuColour}>Passive</color>\nShow nametags through walls",
            $"<color={Menu.MenuColour}>Setting</color>\nThe height the nametag should be",
            $"<color={Menu.MenuColour}>Setting</color>\nThe size the nametag should be",
            $"<color={Menu.MenuColour}>Setting</color>\nThe colour the nametag should be",
        };

        public static string[] Playertips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nRemoves hand animations",
            $"<color={Menu.MenuColour}>Custom</color>\nTag with a gun",
            $"<color={Menu.MenuColour}>L Trigger & R Trigger</color>\nPoints and looks at monkies",
            $"<color={Menu.MenuColour}>Custom</color>\nFreezes ss rig",
            $"<color={Menu.MenuColour}>Custom</color>\nGo invis",
            $"<color={Menu.MenuColour}>Passive</color>\nAutomatically tags nearest monkey",
            $"<color={Menu.MenuColour}>Passive</color>\nTags all monkies",
            $"<color={Menu.MenuColour}>Passive</color>\nDesyncs hitbox and visual position",
            $"<color={Menu.MenuColour}>Passive</color>\nIncreases how far you can tag from",
            $"<color={Menu.MenuColour}>Passive</color>\nFakes lag",
            $"<color={Menu.MenuColour}>Passive</color>\nMakes your colour rainbow for everyone",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges your name to names from a file",
        };

        public static string[] Player2tips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nDesyncs your head and body rotations",
            $"<color={Menu.MenuColour}>Passive</color>\nMakes you impossible to tag",
            $"<color={Menu.MenuColour}>Passive</color>\nMakes your rig teleport around other players",
        };

        public static string[] Exploittips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nBreaks nametag mods",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges everyones name",
            $"<color={Menu.MenuColour}>Passive</color>\nLags everyones game (May take a while)",
            $"<color={Menu.MenuColour}>Submenu</color>\nFree Cosmetics",
            $"<color={Menu.MenuColour}>Passive</color>\nLags everyones game",
            $"<color={Menu.MenuColour}>Passive</color>\nClears prefabs",
            $"<color={Menu.MenuColour}>Passive</color>\nSets you as master client",
            $"<color={Menu.MenuColour}>Passive</color>\nCrashes everyones game instantly",
        };

        public static string[] Exploit2tips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nPrevents you from getting banned",
            $"<color={Menu.MenuColour}>Custom</color>\nA gun that launches a projectile",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges the projectile type",
            $"<color={Menu.MenuColour}>Passive</color>\nAttempts to ban everyone in the current lobby",
            $"<color={Menu.MenuColour}>Passive</color>\nAttempts to kick everyone in the current lobby",
            $"<color={Menu.MenuColour}>Passive</color>\nAttempts to clone yourself",
            $"<color={Menu.MenuColour}>Passive</color>\nSpazes the infection gamemode",
            $"<color={Menu.MenuColour}>Custom</color>\nBans who you shoot",
            $"<color={Menu.MenuColour}>Custom</color>\nCrashes who you shoot",
        };

        public static string[] Exploit3tips = new string[]
        {
            $"<color={Menu.MenuColour}>Custom</color>\nAttempts to kick someome you shoot",
            $"<color={Menu.MenuColour}>Custom</color>\nAttempts to lag someome you shoot",
            $"<color={Menu.MenuColour}>Custom</color>\nLets you spam your rig",
            $"<color={Menu.MenuColour}>Custom</color>\nChanges the name of the person you shoot",
            $"<color={Menu.MenuColour}>Custom</color>\nMaterial Spams everyone in the lobby",
            $"<color={Menu.MenuColour}>Custom</color>\nMaterial Spams the person you shoot",
            $"<color={Menu.MenuColour}>Custom</color>\nLets you become the person you shoot",
            $"<color={Menu.MenuColour}>Passive</color>\nLets you become a network player",
        };

        public static string[] Computertips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nDisconnects from room",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins code GTC",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins code TTT",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins code YTTV",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins code 1",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins a public",
            $"<color={Menu.MenuColour}>Passive</color>\nJoins a QCMV3 only code",
            $"<color={Menu.MenuColour}>Passive</color>\nLets you turn",
        };

        public static string[] Safetytips = new string[]
        {
            $"<color={Menu.MenuColour}>All Face Buttons</color>\nDisables Everything",
            $"<color={Menu.MenuColour}>Passive</color>\nAntiReport",
            $"<color={Menu.MenuColour}>Passive</color>\nRandomly changes name",
            $"<color={Menu.MenuColour}>Passive</color>\nDisables Igloo to pass a PC check",
            $"<color={Menu.MenuColour}>Passive</color>\nFakes having your quest menu open",
            $"<color={Menu.MenuColour}>Passive</color>\nPrevents you from geteting crashed",
            $"<color={Menu.MenuColour}>Passive</color>\nChanges what the anti crashes destroy",
        };

        public static string[] Settingstips = new string[]
        {
            $"<color={Menu.MenuColour}>Submenu</color>\nMenu Colour options",
            $"<color={Menu.MenuColour}>Passive</color>\nMenu position",
            $"<color={Menu.MenuColour}>Passive</color>\nConfig to load",
            $"<color={Menu.MenuColour}>Passive</color>\nLoad selected config",
            $"<color={Menu.MenuColour}>Passive</color>\nSave menu settings",
            $"<color={Menu.MenuColour}>Passive</color>\nSaves player info to a file",
            $"<color={Menu.MenuColour}>Passive</color>\nInverts the menu controls",
            //$"<color={Menu.MenuColour}>Passive</color>\nMakes the menu use a click ui",
            $"<color={Menu.MenuColour}>Passive</color>\nLets you change the menus font",
        };
        public static string[] SettingsColourtips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nGUI colour",
            $"<color={Menu.MenuColour}>Passive</color>\nExtra rig colour",
            $"<color={Menu.MenuColour}>Passive</color>\nTagging beam colour",
            $"<color={Menu.MenuColour}>Passive</color>\nESP colour",
            $"<color={Menu.MenuColour}>Passive</color>\nExtra rig opacity",
        };

        public static string[] Musictips = new string[]
        {
            $"<color={Menu.MenuColour}>Passive</color>\nSelected music",
            $"<color={Menu.MenuColour}>Passive</color>\nPlays the selected music",
            $"<color={Menu.MenuColour}>Passive</color>\nStops selected music",
            $"<color={Menu.MenuColour}>Passive</color>\nLoops selected music",
            $"<color={Menu.MenuColour}>Passive</color>\nLets everyone else hear it",
            $"<color={Menu.MenuColour}>Passive</color>\nVolume of the music",
        };

        public static string[] Info
        {
            get
            {
                return new string[]
                {
                    $"<color={Menu.MenuColour}>PlayerList</color>\n{playerinfo}",
                    $"<color={Menu.MenuColour}>Battery</color>\n{battery}",
                    $"<color={Menu.MenuColour}>QCMV3 Users</color>\n{Plugin.usercount}",
                };
            }
        }

        public static GameObject HUDObj;
        public static GameObject HUDObj2;
        static GameObject MainCamera;
        public static Text Testtext;
        private static TextAnchor textAnchor = TextAnchor.UpperRight;
        static Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        static Text NotifiText;
        private static GameObject TestText;

        public static string playerinfo;
        public static int battery;

        public virtual void Update()
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

            if (HUDObj2 != null)
            {
                HUDObj2.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                HUDObj2.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
            }
        }
        private static string[] GetTooltipArray(string category)
        {
            switch (category)
            {
                case "Main":
                    return MainMenutips;
                case "Back":
                    return MainMenutips;
                case "Movement":
                    return Movementtips;
                case "Movement2":
                    return Movement2tips;
                case "Speed Options":
                    return Speedtips;
                case "Strafe Options":
                    return Strafetips;
                case "Visual":
                    return Visualtips;
                case "Visual2":
                    return Visual2tips;
                case "Tracers":
                    return Tracers;
                case "NameTags":
                    return Nametags;
                case "Player":
                    return Playertips;
                case "Player2":
                    return Player2tips;
                case "Exploits":
                    return Exploittips;
                case "Exploits2":
                    return Exploit2tips;
                case "Exploits3":
                    return Exploit3tips;
                case "Computer":
                    return Computertips;
                case "Safety":
                    return Safetytips;
                case "Settings":
                    return Settingstips;
                case "ColourSettings":
                    return SettingsColourtips;
                case "MusicPlayer":
                    return Musictips;
                case "Info":
                    return Info;
                default:
                    return null;
            }
        }
        private static float nextPlayerInfoUpdate = 0f;
        public static void HandToolTips(string category, int selectedIndex)
        {
            if (Menu.GUIToggled && PluginConfig.tooltips)
            {
                if (Menu.agreement)
                {
                    if (Plugin.update)
                    {
                        Testtext.text = "<color=red>UPDATE NEEDED</color>";
                        return;
                    }
                    MainCamera = GameObject.Find("Main Camera") ?? Camera.main.gameObject;
                    if (HUDObj == null)
                    {
                        HUDObj = new GameObject();
                        HUDObj2 = new GameObject();
                        HUDObj2.name = "CLIENT_HUB_TOOLTIP";
                        HUDObj.name = "CLIENT_HUB_TOOLTIP";
                        HUDObj.AddComponent<Canvas>();
                        HUDObj.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10f;
                        HUDObj.AddComponent<GraphicRaycaster>();
                        HUDObj.GetComponent<Canvas>().enabled = true;
                        HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                        HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
                        HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
                        HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                        HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
                        HUDObj.transform.parent = HUDObj2.transform;
                        HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0.3f, 0.2f, 2.2f);
                        var Temp = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
                        Temp.y = -270f;
                        HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
                        HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(Temp);
                    }
                    string[] tooltipArray = GetTooltipArray(category);
                    if (tooltipArray != null && selectedIndex >= 0 && selectedIndex < tooltipArray.Length)
                    {
                        string tooltipText = tooltipArray[selectedIndex];
                        if (!string.IsNullOrWhiteSpace(tooltipText))
                        {
                            if (TestText == null)
                            {
                                TestText = new GameObject();
                                TestText.transform.parent = HUDObj.transform;
                                Testtext = TestText.AddComponent<Text>();
                                Testtext.fontSize = 10;
                                Testtext.font = Plugin.gtagfont;
                                Testtext.rectTransform.sizeDelta = new Vector2(260, 300);
                                Testtext.rectTransform.localScale = new Vector3(0.004f, 0.004f, 0.1f);
                                Testtext.rectTransform.localPosition = new Vector3(2.2f, -0.1f, -0.2f);
                                Testtext.rectTransform.localRotation = Quaternion.Euler(0, 90, 0);
                                Testtext.material = AlertText;
                                NotifiText = Testtext;
                                Testtext.alignment = TextAnchor.MiddleCenter;
                            }
                            Testtext.text = tooltipText;
                        }
                        else
                        {
                            if (TestText != null)
                                Testtext.text = "";
                            else
                                CustomConsole.LogToConsole("[QOLOSSAL] ToolTip is null");
                        }
                    }
                    else
                    {
                        if (TestText != null)
                            Testtext.text = "";
                    }
                    battery = Mathf.RoundToInt(SystemInfo.batteryLevel * 100f);
                    if (PhotonNetwork.InRoom && category.ToLower().Contains("info"))
                    {
                        if (Time.time >= nextPlayerInfoUpdate)
                        {
                            nextPlayerInfoUpdate = Time.time + 1.5f;
                            UpdatePlayerInfo();
                        }
                    }
                    else
                    {
                        if (playerinfo != "Not In Room")
                            playerinfo = "Not In Room";
                    }
                }
            }
            else
            {
                if (TestText != null)
                    Testtext.text = "";
                else
                    CustomConsole.LogToConsole("[QOLOSSAL] ToolTip is null");
            }
        }

        private static void UpdatePlayerInfo()
        {
            List<string> playerInfoList = new List<string>();
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null || vrrig.playerName == null) continue;
                string playerName = vrrig.photonView.Owner.NickName;
                playerName = playerName.ToUpper().Normalize();
                playerName = new Regex("<\\s*color=.*?>", RegexOptions.IgnoreCase).Replace(playerName, "");
                playerName = new Regex("</\\s*color>", RegexOptions.IgnoreCase).Replace(playerName, "");
                playerName = new Regex("<\\s*size=.*?>", RegexOptions.IgnoreCase).Replace(playerName, "");
                playerName = new Regex("</\\s*size>", RegexOptions.IgnoreCase).Replace(playerName, "");
                if (playerName.Length > 14) playerName = playerName.Substring(0, 14);
                bool isInfected = vrrig.mainSkin.material.name.ToLower().Contains("fected") || vrrig.mainSkin.material.name.ToLower().Contains("it");
                string nameColor = isInfected ? "red" : "white";
                string prefix = "";
                Dictionary<string, (string displayPrefix, string color)> prefixMapping = new Dictionary<string, (string displayPrefix, string color)>()
                {
                    { "console", ("CONSOLE", "grey") },
                    { "toomanyplayers", ("TOOMANYPLAYERS", "red") },
                    { "stupid", ("STUPID", "#ffa200") },
                    { ThisGuyIsUsingColossal.ccmprefix, ("QCM", "magenta") },
                    { "colossal", ("CCM", "magenta") },
                    { "zyph", ("ZYPH", "#6600CC") },
                    { "solarnovapleasestopdoingdumbshityoudotsallthetimrimgettingpissed", ("SOLAR - OLD", "grey") },
                    { "solaaaaaaaaaaaa", ("SOLAR", "grey") },
                    { "props changed by solar user", ("SOLAR GAVE PROPS", "grey") },
                    { "jupiterxusersosigma", ("JUPITERX - old", "yellow") },
                    { "jupiterx2026revive", ("JUPITERX", "cyan") },
                    { "sleepyissillyidontknowwhattotypesoyeauhmnovaiscutecolonthreeyeaiguesssorrawrdiscord.gg/35WzS7w66t", ("SLEEP.EZ", "#ED7014") },
                    { "bunny", ("BUNNY.LOL", "#ED7014") },
                    { "titled", ("TITLED", "#333333") },
                    { "untitled", ("UNTITLED", "blue") },
                    { "genesis", ("GENESIS", "grey") },
                    { "pneumonoultramicroscopicsilicovolcanoconiosisz0real", ("KILLER", "#8B0000") },
                    { "272issogoodilove272menu", ("272", "red") },
                    { "terrormenussohot", ("Terror", "red") }
                };

                if (vrrig.photonView.Owner?.CustomProperties != null)
                {
                    foreach (var mapping in prefixMapping)
                    {
                        if (vrrig.photonView.Owner.CustomProperties.ContainsKey(mapping.Key))
                        {
                            prefix += $"<color={mapping.Value.color}>[{mapping.Value.displayPrefix}]</color> ";
                        }
                    }
                }
                playerInfoList.Add($"{prefix}<color={nameColor}>{playerName}</color>");
            }
            playerinfo = string.Join("\n", playerInfoList);
        }
    }
}