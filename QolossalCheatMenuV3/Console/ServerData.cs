using GorillaNetworking;
using Il2CppSystem.Net;
using MelonLoader;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using Qolossal;
using Qolossal.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Console
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ServerDataQolossal : MonoBehaviour
    {
        public ServerDataQolossal(IntPtr ptr) : base(ptr) { }

        public const string ServerEndpoint = "https://consolecopys.vercel.app"; // DO NOT EVER REMOVE OR CHANGE
        public static readonly string ServerDataEndpoint = $"{ServerEndpoint}/serverdata"; // DO NOT EVER REMOVE OR CHANGE

        public const string AssetsURL = "https://raw.githubusercontent.com/novaissilly/ConsoleCopies/master/ConsoleCopys/ServerData"; // DO NOT EVER REMOVE OR CHANGE

        public static ServerDataQolossal instance;

        private static float DataLoadTime = -1f;

        private static int LoadAttempts;

        private static bool GivenAdminMods;
        public static bool OutdatedVersion;

        private ExitGames.Client.Photon.Hashtable consoleHash; // KEEP THIS FOR OTHER INSTANCES OF CONSOLE AS WELL // DO NOT EVER REMOVE

        public virtual void Awake()
        {
            instance = this;
            DataLoadTime = Time.time + 5f;

            consoleHash = new ExitGames.Client.Photon.Hashtable(); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
            consoleHash.Add("console", "console"); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
            PhotonNetwork.LocalPlayer.SetCustomProperties(consoleHash); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
        }

        private readonly Dictionary<VRRig, TextMeshPro> activeTags = new Dictionary<VRRig, TextMeshPro>();

        public virtual void Update()
        {
            if (DataLoadTime > 0f && Time.time > DataLoadTime && GorillaComputer.instance.isConnectedToMaster)
            {
                DataLoadTime = Time.time + 5f;

                LoadAttempts++;
                if (LoadAttempts >= 3)
                {
                    Log("Server data could not be loaded");
                    DataLoadTime = -1f;
                    return;
                }

                Log("Attempting to load web data");
                MelonCoroutines.Start(LoadServerData());
            }

            if (!PhotonNetwork.InRoom || !PluginConfig.consoleusersnametags)
            {
                ClearAllTags();
                return;
            }

            Camera cam = Camera.main;
            if (cam == null)
                return;

            HashSet<VRRig> seenRigs = new HashSet<VRRig>();

            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig == null || !VRRigExtensions.GetVRRigWithoutMe(rig))
                    continue;

                if (rig.headMesh == null || rig.photonView == null || rig.photonView.Owner == null)
                    continue;

                seenRigs.Add(rig);

                var props = rig.photonView.Owner.CustomProperties;
                if (props == null)
                {
                    HideTag(rig);
                    continue;
                }

                string fullText = "";
                Color lastColor = Color.white;

                foreach (var prop in prefixMappingConsole)
                {
                    if (props.ContainsKey(prop.Key))
                    {
                        fullText += "| " + prop.Value.displayPrefix + " | ";
                        lastColor = StringToColor(prop.Value.color);
                    }
                }

                if (string.IsNullOrEmpty(fullText))
                {
                    HideTag(rig);
                    continue;
                }

                TextMeshPro nametag = GetOrCreateTag(rig);
                Transform head = rig.headMesh.transform;

                nametag.transform.position = head.position + new Vector3(0f, 0.9f, 0f);
                nametag.transform.LookAt(cam.transform);
                nametag.transform.Rotate(0f, 180f, 0f);

                nametag.color = lastColor;
                nametag.text = fullText;
                nametag.gameObject.SetActive(true);
            }

            List<VRRig> toRemove = new List<VRRig>();
            foreach (var pair in activeTags)
            {
                if (!seenRigs.Contains(pair.Key))
                {
                    if (pair.Value != null)
                        GameObject.Destroy(pair.Value.gameObject);

                    toRemove.Add(pair.Key);
                }
            }

            foreach (VRRig rig in toRemove)
                activeTags.Remove(rig);
        }

        private TextMeshPro GetOrCreateTag(VRRig rig)
        {
            if (activeTags.TryGetValue(rig, out var existing) && existing != null)
                return existing;

            GameObject tagObj = new GameObject("AdminNameTag");
            TextMeshPro tmp = tagObj.AddComponent<TextMeshPro>();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 0.7f;
            tmp.richText = true;
            tmp.fontStyle = FontStyles.Italic;
            tmp.fontSizeMin = 0;

            activeTags[rig] = tmp;
            return tmp;
        }

        private void HideTag(VRRig rig)
        {
            if (activeTags.TryGetValue(rig, out var tmp) && tmp != null)
                tmp.gameObject.SetActive(false);
        }

        private void ClearAllTags()
        {
            foreach (var pair in activeTags)
            {
                if (pair.Value != null)
                    pair.Value.gameObject.SetActive(false);
            }
        }

        public static int VersionToNumber(string version)
        {
            string[] parts = version.Split('.');
            if (parts.Length != 3)
                return -1; // Version must be in 'major.minor.patch' format

            return int.Parse(parts[0]) * 100 + int.Parse(parts[1]) * 10 + int.Parse(parts[2]);
        }

        public static readonly Dictionary<string, string> Administrators = new Dictionary<string, string>();
        public static readonly List<string> SuperAdministrators = new List<string>();
        public static bool isadmin = false;
        public IEnumerator LoadServerData()
        {
            yield return new WaitForSeconds(0.5f);

            WebClient request = new WebClient();

            string json = request.DownloadString(ServerDataEndpoint);
            DataLoadTime = -1f;

            JObject data = JObject.Parse(json);

            string minConsoleVersion = (string)data["min-console-version"];
            if (VersionToNumber(ConsoleQolossal.ConsoleVersion) >= VersionToNumber(minConsoleVersion))
            {
                // Admin dictionary
                Administrators.Clear();

                JArray admins = (JArray)data["admins"];
                foreach (var admin in admins)
                {
                    string name = admin["name"].ToString();
                    string userId = admin["user-id"].ToString();
                    Administrators[userId] = name;
                }

                SuperAdministrators.Clear();

                JArray superAdmins = (JArray)data["super-admins"];
                foreach (var superAdmin in superAdmins)
                    SuperAdministrators.Add(superAdmin.ToString());

                // Give admin panel if on list
                if (PhotonNetwork.LocalPlayer.UserId != null)
                {
                    bool isActuallyAdmin = Administrators.TryGetValue(PhotonNetwork.LocalPlayer.UserId, out var administrator);
                    if (!GivenAdminMods && isActuallyAdmin)
                    {
                        GivenAdminMods = true;
                        SetUpDevMenu();
                        isadmin = isActuallyAdmin;
                    }

                    if (isadmin && !isActuallyAdmin)
                    {
                        isadmin = isActuallyAdmin;
                        GivenAdminMods = isActuallyAdmin;
                    }
                }
                else
                {
                    isadmin = false;
                    GivenAdminMods = false;
                }
            }
            else
            {
                ConsoleQolossal.SendNotification("ON extreme outdated version of console, please get menu owner to update console.");
                Log("On extreme outdated version of Console, not loading administrators");
            }
        }

        private void SetUpDevMenu()
        {
            Menu.MainMenu = Menu.MainMenu.Concat(new[] { new MenuOption { DisplayName = "<color=magenta>Console</color>", _type = Plugin.submenuthingy, AssociatedString = "Console" } }).ToArray();

            Menu.Dev = new MenuOption[5];
            Menu.Dev[0] = new MenuOption { DisplayName = "Console Guns", _type = Plugin.submenuthingy, AssociatedString = "Console Guns" };
            Menu.Dev[1] = new MenuOption { DisplayName = "Console All", _type = Plugin.submenuthingy, AssociatedString = "Console All" };
            Menu.Dev[2] = new MenuOption { DisplayName = "Comfirm Using", _type = Plugin.buttonthingy, AssociatedString = "Comfirm Using" };
            Menu.Dev[3] = new MenuOption { DisplayName = "Console Users NameTags", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleusersnametags };
            Menu.Dev[4] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Menu.ConsoleGuns = new MenuOption[14];
            Menu.ConsoleGuns[0] = new MenuOption { DisplayName = "Console Quit Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolequitgun };
            Menu.ConsoleGuns[1] = new MenuOption { DisplayName = "Console Bring Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolebringgun };
            Menu.ConsoleGuns[2] = new MenuOption { DisplayName = "Console Kick Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolekickgun };
            Menu.ConsoleGuns[3] = new MenuOption { DisplayName = "Console Change Name Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolechangenamegun };
            Menu.ConsoleGuns[4] = new MenuOption { DisplayName = "Console Restart Mic Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolerestartmicgun };
            Menu.ConsoleGuns[5] = new MenuOption { DisplayName = "Console Ghost Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleghostgun };
            Menu.ConsoleGuns[6] = new MenuOption { DisplayName = "Console Unghost Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleunghostgun };
            Menu.ConsoleGuns[7] = new MenuOption { DisplayName = "Console Mute Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consolemutegun };
            Menu.ConsoleGuns[8] = new MenuOption { DisplayName = "Console Unmute Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleunmutegun };
            Menu.ConsoleGuns[9] = new MenuOption { DisplayName = "Console Disable Movement Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoledisablemovementgun };
            Menu.ConsoleGuns[10] = new MenuOption { DisplayName = "Console Enable Movement Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleenablemovementgun };
            Menu.ConsoleGuns[11] = new MenuOption { DisplayName = "Console Target Player Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoletargetplayergun };
            Menu.ConsoleGuns[12] = new MenuOption { DisplayName = "Console Fling Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.consoleflinggun };
            Menu.ConsoleGuns[13] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Menu.ConsoleAll = new MenuOption[14];
            Menu.ConsoleAll[0] = new MenuOption { DisplayName = "Console Quit All", _type = Plugin.buttonthingy, AssociatedString = "Console Quit All" };
            Menu.ConsoleAll[1] = new MenuOption { DisplayName = "Console Bring All", _type = Plugin.buttonthingy, AssociatedString = "Console Bring All" };
            Menu.ConsoleAll[2] = new MenuOption { DisplayName = "Console Kick All", _type = Plugin.buttonthingy, AssociatedString = "Console Kick All" };
            Menu.ConsoleAll[3] = new MenuOption { DisplayName = "Console Change Name All", _type = Plugin.buttonthingy, AssociatedString = "Console Change Name All" };
            Menu.ConsoleAll[4] = new MenuOption { DisplayName = "Console Restart Mic All", _type = Plugin.buttonthingy, AssociatedString = "Console Restart Mic All" };
            Menu.ConsoleAll[5] = new MenuOption { DisplayName = "Console Ghost All", _type = Plugin.buttonthingy, AssociatedString = "Console Ghost All" };
            Menu.ConsoleAll[6] = new MenuOption { DisplayName = "Console Unghost All", _type = Plugin.buttonthingy, AssociatedString = "Console Unghost All" };
            Menu.ConsoleAll[7] = new MenuOption { DisplayName = "Console Mute All", _type = Plugin.buttonthingy, AssociatedString = "Console Mute All" };
            Menu.ConsoleAll[8] = new MenuOption { DisplayName = "Console Unmute All", _type = Plugin.buttonthingy, AssociatedString = "Console Unmute All" };
            Menu.ConsoleAll[9] = new MenuOption { DisplayName = "Console Disable Movement All", _type = Plugin.buttonthingy, AssociatedString = "Console Disable Movement All" };
            Menu.ConsoleAll[10] = new MenuOption { DisplayName = "Console Enable Movement All", _type = Plugin.buttonthingy, AssociatedString = "Console Enable Movement All" };
            Menu.ConsoleAll[11] = new MenuOption { DisplayName = "Console Target All", _type = Plugin.buttonthingy, AssociatedString = "Console Target All" };
            Menu.ConsoleAll[12] = new MenuOption { DisplayName = "Console Fling All", _type = Plugin.buttonthingy, AssociatedString = "Console Fling All" };
            Menu.ConsoleAll[13] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };
        }

        public string ColorToString(Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        }
        public Color StringToColor(string color)
        {
            if (ColorUtility.TryParseHtmlString(color, out Color result))
            {
                return result;
            }
            return Color.white;
        }

        public Dictionary<string, (string displayPrefix, string color)> prefixMappingConsole = new Dictionary<string, (string displayPrefix, string color)>()
                {
                    { "console", ("CONSOLE", "grey") },
                    { "toomanyplayers", ("TOOMANYPLAYERS", "red") },
                    { "stupid", ("STUPID", "#ffa200") },
                    { "qolossal", ("QCM", "magenta") },
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

        public void Log(string msg)
        {
            MelonLogger.Msg($"[CONSOLE::LOG] {msg}");
        }
    }
}