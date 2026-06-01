using GorillaNetworking;
using Il2CppSystem.Net;
using MelonLoader;
using Photon.Pun;
using PlayFab;
using Qolossal;
using Qolossal.Menu;
using Qolossal.Notifacation;
using Qolossal.Mods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Reflection;

[assembly: MelonInfo(typeof(Plugin), "QolossalCheatMenuV3", "1.0.0", "nova_is_cute_and_silly")]
[assembly: MelonGame()]
namespace Qolossal
{
    public class Plugin : MelonMod
    {
        public static string togglethingy;
        public static string sliderthingy;
        public static string submenuthingy;
        public static string backthingy;
        public static string buttonthingy;

        // Security stuff dont touch it
        // dont fuck with these strigns or menu wont work
        public static string anti1 = "01110100011010000110100101110011011010010111001101110010011001010110000101101100011000010110111001110100011010010111010001110010011101010111001101110100";
        public static string anti2 = "01100011011000010110111001110100011000110111001001100001011000110110101101110100011010000110010101101101011001010110111001110101011011000110111101101100";

        public static bool shouldbeallowed = false;
        public static string modspath = Path.Combine(Application.persistentDataPath, "Mods");
        public static void CheckIntegrity(string anti)
        {
            string decodedAnti = DecodeString(anti1);

            if (anti != decodedAnti)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (!Directory.Exists(modspath))
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (Directory.GetFiles(modspath, "*QolossalCheatMenuV3*.dll").Length == 0)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            shouldbeallowed = true;
        }

        public static void CheckIntegrity2(string anti)
        {
            string decodedAnti = DecodeString(anti2);

            if (anti != decodedAnti)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (!Directory.Exists(modspath))
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (Directory.GetFiles(modspath, "*QolossalCheatMenuV3*.dll").Length == 0)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            shouldbeallowed = true;
        }


        public static GameObject holder;
        public static Font gtagfont;
        public static float version = 8.4f;

        public static bool sussy = false;
        public static bool update = false;
        public static bool oculus = false;
        public static bool infected;

        public static float runtime = 0;
        public static float playtime = 0;
        public static string rutimestring;
        public static string playtimestring;

        // api stuff
        public static string SERVER_ENDPOINT = DecodeString("01101000011101000111010001110000011100110011101000101111001011110110000101110000011010010010110101101110011011110111011001100001001011010111010001110111011011110010111001110110011001010111001001100011011001010110110000101110011000010111000001110000");
        private static string KEY_DATA_URL = SERVER_ENDPOINT + DecodeString("0010111101101011011001010111100101110011"); //"https://pastebin.com/raw/yRXhBFj7";
        private static string SERVER_DATA_URL = SERVER_ENDPOINT + DecodeString("0010111101110011011001010111001001110110011001010111001001100100011000010111010001100001"); //"https://pastebin.com/raw/xzAK9pLp";
        public static string motd;
        public static string[] adminids;
        private static string adminname;
        public static string serverversion;
        public static string discord;
        public static string ccmprefix;
        public static bool locked = false;
        public static bool serverLocked = false;
        public static bool hasvalidkey = false;

        public static PhotonNetworkController networkController;

        public static int lastUserCount = -1;
        public static int usercount;
        public static IEnumerator UpdateRequestUsercount()
        {
            while (true)
            {
                UnityWebRequest www = UnityWebRequest.Get("https://api-nova-two.vercel.app/heartbeat");
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string text = www.downloadHandler.text.Trim();
                    if (int.TryParse(text, out int count))
                    {
                        usercount = count;
                        //MelonLogger.Msg($"[QOLOSSAL] Got User Count {usercount}");
                    }
                }
                yield return new WaitForSeconds(5f);
            }
        }

        [Obsolete]
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            CustomConsole.LogToConsole("[QOLOSSAL] Plugin Start Call");

            ClassInjector.RegisterTypeInIl2Cpp<Notifacations>();
            ClassInjector.RegisterTypeInIl2Cpp<Overlay>();
            ClassInjector.RegisterTypeInIl2Cpp<ToolTips>();
            ClassInjector.RegisterTypeInIl2Cpp<ThisGuyIsUsingColossal>();
            ClassInjector.RegisterTypeInIl2Cpp<Boards>();
            ClassInjector.RegisterTypeInIl2Cpp<CustomBinding>();
            ClassInjector.RegisterTypeInIl2Cpp<Configs>();
            ClassInjector.RegisterTypeInIl2Cpp<PlayerLog>();
            ClassInjector.RegisterTypeInIl2Cpp<LocalGorillaVelocityTracker>();
            ClassInjector.RegisterTypeInIl2Cpp<Macro>();
            ClassInjector.RegisterTypeInIl2Cpp<AntiDestroyPlayerObjects>();
            ClassInjector.RegisterTypeInIl2Cpp<AntiGEnemySpam>();

            ClassInjector.RegisterTypeInIl2Cpp<AssetBundleLoader>();
            ClassInjector.RegisterTypeInIl2Cpp<Outline>();
            ClassInjector.RegisterTypeInIl2Cpp<PointerLine>();
            ClassInjector.RegisterTypeInIl2Cpp<ButtonBehavior>();

            ClassInjector.RegisterTypeInIl2Cpp<Console.Mods.ConsoleGuns>();

            // The mods help me why am i doing this - nova
            // Bools
            ClassInjector.RegisterTypeInIl2Cpp<LongArm>();
            ClassInjector.RegisterTypeInIl2Cpp<WhyIsEveryoneLookingAtMe>();
            ClassInjector.RegisterTypeInIl2Cpp<WateryAir>();
            ClassInjector.RegisterTypeInIl2Cpp<Platforms>();
            ClassInjector.RegisterTypeInIl2Cpp<TFly>();
            ClassInjector.RegisterTypeInIl2Cpp<ForceTagFreeze>();
            ClassInjector.RegisterTypeInIl2Cpp<NoClip>();
            ClassInjector.RegisterTypeInIl2Cpp<UpsideDownMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<SpinBot>();
            ClassInjector.RegisterTypeInIl2Cpp<JoystickFly>();
            ClassInjector.RegisterTypeInIl2Cpp<BoneESP>();
            ClassInjector.RegisterTypeInIl2Cpp<BoxEsp>();
            ClassInjector.RegisterTypeInIl2Cpp<Chams>();
            ClassInjector.RegisterTypeInIl2Cpp<HollowBoxEsp>();
            ClassInjector.RegisterTypeInIl2Cpp<NameTags>();
            ClassInjector.RegisterTypeInIl2Cpp<ProximityAlert>();
            ClassInjector.RegisterTypeInIl2Cpp<Panic>();
            ClassInjector.RegisterTypeInIl2Cpp<Turning>();
            ClassInjector.RegisterTypeInIl2Cpp<CreeperMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<Desync>();
            ClassInjector.RegisterTypeInIl2Cpp<GhostMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<InvisMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<TagAll>();
            ClassInjector.RegisterTypeInIl2Cpp<TagGun>();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<Decapitation>();
            ClassInjector.RegisterTypeInIl2Cpp<FakeLag>();
            ClassInjector.RegisterTypeInIl2Cpp<NameChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<AntiTag>();
            ClassInjector.RegisterTypeInIl2Cpp<BreakNameTags>();
            ClassInjector.RegisterTypeInIl2Cpp<FakeQuestMenu>();
            ClassInjector.RegisterTypeInIl2Cpp<PcCheckBypass>();
            ClassInjector.RegisterTypeInIl2Cpp<MaterialSpam>();
            ClassInjector.RegisterTypeInIl2Cpp<LagAll>();
            ClassInjector.RegisterTypeInIl2Cpp<CrashAll>();
            ClassInjector.RegisterTypeInIl2Cpp<ChangeNameAll>();
            ClassInjector.RegisterTypeInIl2Cpp<RigSpam>();
            //ClassInjector.RegisterTypeInIl2Cpp<CubeSpam>();
            ClassInjector.RegisterTypeInIl2Cpp<SnowballGun>();
            ClassInjector.RegisterTypeInIl2Cpp<LagGun>();
            ClassInjector.RegisterTypeInIl2Cpp<MatSpamGun>();
            ClassInjector.RegisterTypeInIl2Cpp<CrashGun>();
           // ClassInjector.RegisterTypeInIl2Cpp<ChangeNameGun>();
            //ClassInjector.RegisterTypeInIl2Cpp<CubeGun>();
            ClassInjector.RegisterTypeInIl2Cpp<BanGun>();
            ClassInjector.RegisterTypeInIl2Cpp<SpazAllCosmeics>();
            ClassInjector.RegisterTypeInIl2Cpp<SpazAllCosmeicsTryOn>();
            ClassInjector.RegisterTypeInIl2Cpp<Throw>();
            //ClassInjector.RegisterTypeInIl2Cpp<BecomeNetworkPlayer>();
            ClassInjector.RegisterTypeInIl2Cpp<BecomePlayerGun>();
            ClassInjector.RegisterTypeInIl2Cpp<AudioCrash>();
            ClassInjector.RegisterTypeInIl2Cpp<KickGun>();
            ClassInjector.RegisterTypeInIl2Cpp<SpazInfection>();
            ClassInjector.RegisterTypeInIl2Cpp<NoLeaves>();
            ClassInjector.RegisterTypeInIl2Cpp<FullBright>();
            //ClassInjector.RegisterTypeInIl2Cpp<GiveCustomPropertiesGun>();
            //ClassInjector.RegisterTypeInIl2Cpp<CrashAllCustomProperties>();
            //ClassInjector.RegisterTypeInIl2Cpp<StickableTargetGun>();
            ClassInjector.RegisterTypeInIl2Cpp<Bees>();

            // Ints
            ClassInjector.RegisterTypeInIl2Cpp<ExcelFly>();
            ClassInjector.RegisterTypeInIl2Cpp<FloatyMonkey>();
            ClassInjector.RegisterTypeInIl2Cpp<NearPulse>();
            ClassInjector.RegisterTypeInIl2Cpp<SpeedMod>();
            ClassInjector.RegisterTypeInIl2Cpp<Timer>();
            ClassInjector.RegisterTypeInIl2Cpp<WallWalk>();
            ClassInjector.RegisterTypeInIl2Cpp<Strafe>();
            ClassInjector.RegisterTypeInIl2Cpp<PullMod>();
            ClassInjector.RegisterTypeInIl2Cpp<Tracers>();
            ClassInjector.RegisterTypeInIl2Cpp<AntiReport>();
            ClassInjector.RegisterTypeInIl2Cpp<HitBoxes>();
            ClassInjector.RegisterTypeInIl2Cpp<TagAura>();
            //ClassInjector.RegisterTypeInIl2Cpp<ModSpoofer>();
            ClassInjector.RegisterTypeInIl2Cpp<HzHands>();

            holder = new GameObject();
            holder.name = "Dfg8afb3AsiHDfg8afb3AsioDfg8afb3AsilDfg8afb3AsidDfg8afb3AsieDfg8afb3AsirDfg8afb3AsiQDfg8afb3AsiCDfg8afb3AsiMDfg8afb3AsiVDfg8afb3Asi3Dfg8afb3Asi".Replace("Dfg8afb3Asi", "");
            //holder.AddComponent<AssetBundleLoader>();
            holder.AddComponent<ThisGuyIsUsingColossal>();
            holder.AddComponent<Boards>();
            holder.AddComponent<CustomBinding>();
            holder.AddComponent<Configs>();
            holder.AddComponent<PlayerLog>();
            holder.AddComponent<Macro>();
            holder.AddComponent<AntiDestroyPlayerObjects>();
            holder.AddComponent<AntiGEnemySpam>();

            if (holder.name != "Dfg8afb3AsiHDfg8afb3AsioDfg8afb3AsilDfg8afb3AsidDfg8afb3AsieDfg8afb3AsirDfg8afb3AsiQDfg8afb3AsiCDfg8afb3AsiMDfg8afb3AsiVDfg8afb3Asi3Dfg8afb3Asi".Replace("Dfg8afb3Asi", ""))
            {
                update = true;
                locked = true;
                hasvalidkey = false;
                serverversion = "0.0";
                version = -0.1f;
                QG();
                return;
            }

            // Security stuff dont touch it
            if (string.IsNullOrEmpty(SERVER_DATA_URL) || string.IsNullOrEmpty(KEY_DATA_URL) || string.IsNullOrEmpty(SERVER_ENDPOINT) || SERVER_ENDPOINT != DecodeString("01101000011101000111010001110000011100110011101000101111001011110110000101110000011010010010110101101110011011110111011001100001001011010111010001110111011011110010111001110110011001010111001001100011011001010110110000101110011000010111000001110000") || KEY_DATA_URL != SERVER_ENDPOINT + DecodeString("0010111101101011011001010111100101110011") || SERVER_DATA_URL != SERVER_ENDPOINT + DecodeString("0010111101110011011001010111001001110110011001010111001001100100011000010111010001100001"))
            {
                update = true;
                locked = true;
                hasvalidkey = false;
                serverversion = "0.0";
                version = -0.1f;
                QG();
                return;
            }

            if (!IsInternetConnected())
            {
                locked = true;
            }

            Manager();

            togglethingy = "t786dfIGhjkfdISad2o786dfIGhjkfdISad2g786dfIGhjkfdISad2g786dfIGhjkfdISad2l786dfIGhjkfdISad2e786dfIGhjkfdISad2t786dfIGhjkfdISad2h786dfIGhjkfdISad2i786dfIGhjkfdISad2n786dfIGhjkfdISad2g786dfIGhjkfdISad2y786dfIGhjkfdISad2".Replace("786dfIGhjkfdISad2", "");
            submenuthingy = "786dfIGhjkfdISad2s786dfIGhjkfdISad2u786dfIGhjkfdISad2b786dfIGhjkfdISad2m786dfIGhjkfdISad2e786dfIGhjkfdISad2n786dfIGhjkfdISad2u786dfIGhjkfdISad2t786dfIGhjkfdISad2h786dfIGhjkfdISad2i786dfIGhjkfdISad2n786dfIGhjkfdISad2g786dfIGhjkfdISad2y786dfIGhjkfdISad2".Replace("786dfIGhjkfdISad2", "");
            buttonthingy = "786dfIGhjkfdISad2b786dfIGhjkfdISad2u786dfIGhjkfdISad2t786dfIGhjkfdISad2t786dfIGhjkfdISad2o786dfIGhjkfdISad2n786dfIGhjkfdISad2t786dfIGhjkfdISad2h786dfIGhjkfdISad2i786dfIGhjkfdISad2n786dfIGhjkfdISad2g786dfIGhjkfdISad2y786dfIGhjkfdISad2".Replace("786dfIGhjkfdISad2", "");
            backthingy = "786dfIGhjkfdISad2b786dfIGhjkfdISad2a786dfIGhjkfdISad2c786dfIGhjkfdISad2k786dfIGhjkfdISad2t786dfIGhjkfdISad2h786dfIGhjkfdISad2i786dfIGhjkfdISad2n786dfIGhjkfdISad2g786dfIGhjkfdISad2y786dfIGhjkfdISad2".Replace("786dfIGhjkfdISad2", "");
            sliderthingy = "786dfIGhjkfdISad2s786dfIGhjkfdISad2l786dfIGhjkfdISad2i786dfIGhjkfdISad2d786dfIGhjkfdISad2e786dfIGhjkfdISad2r786dfIGhjkfdISad2t786dfIGhjkfdISad2h786dfIGhjkfdISad2i786dfIGhjkfdISad2n786dfIGhjkfdISad2g786dfIGhjkfdISad2y786dfIGhjkfdISad2".Replace("786dfIGhjkfdISad2", "");

            foreach (PhotonNetworkController photonController in GameObject.FindObjectsOfType<PhotonNetworkController>())
            {
                networkController = photonController;
                MelonLogger.Msg("Set up networkController");
            }

            // The key system
            string keyPath = Path.Combine(Application.persistentDataPath, "key.txt");

            if (!File.Exists(keyPath))
            {
                File.WriteAllText(keyPath, "");
                locked = true;
                hasvalidkey = false;
                return;
            }

            WebClient wc = new WebClient();
            string secret = "adf64vsssadf64vssiadf64vssgadf64vssvadf64vsseadf64vsstadf64vss4adf64vss3adf64vsssadf64vsscadf64vssvadf64vsshadf64vsswadf64vsswadf64vsszadf64vss".Replace("adf64vss", "");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string token;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(timestamp));
                token = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
            wc.Headers.Add("User-Agent", "qn8slsu2qqn8slsu2oqn8slsu2lqn8slsu2oqn8slsu2sqn8slsu2sqn8slsu2aqn8slsu2lqn8slsu2aqn8slsu2pqn8slsu2iqn8slsu2sqn8slsu2eqn8slsu2cqn8slsu2uqn8slsu2rqn8slsu2eqn8slsu2".Replace("qn8slsu2", ""));
            wc.Headers.Add("X-Time", timestamp);
            wc.Headers.Add("X-Token", token);

            string[] validKeys = wc.DownloadString(KEY_DATA_URL).Replace("\r", "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrEmpty(k)).ToArray();
            string localKey = File.ReadAllText(Path.Combine(Application.persistentDataPath, "key.txt")).Trim();
            //hasvalidkey = localKey.StartsWith("QOLOSSAL") && localKey.Length == 29 && validKeys.Contains(localKey);
            if (string.IsNullOrEmpty(localKey))
            {
                locked = true;
                hasvalidkey = false;
                return;
            }
            bool isValid = localKey.StartsWith("QOLOSSAL") && localKey.Length == 29 && validKeys.Contains(localKey);
            if (!isValid)
            {
                locked = true;
                hasvalidkey = false;
                webhook($"Invalid key attempt | Key: {localKey} | IP: {GetStringFromURL("https://api.ipify.org")} | HWID: {SystemInfo.deviceUniqueIdentifier}", true);
                QG();
                return;
            }
            hasvalidkey = true;
            locked = false;
            webhook($"{localKey} <- valid key | IP: {GetStringFromURL("https://api.ipify.org")} | HWID: {SystemInfo.deviceUniqueIdentifier}", true);

            // end of key system

            if (GameObject.Find("COC Text").GetComponent<Text>() == null)
                gtagfont = GameObject.Find("motd").GetComponent<Text>().font;
            else
                gtagfont = GameObject.Find("COC Text").GetComponent<Text>().font;

            HarmonyLib.Harmony patcher = new HarmonyLib.Harmony("org.nova");
            patcher.PatchAll();

            MelonLoader.MelonCoroutines.Start(UpdateRequestUsercount());

            if (gtagfont != null) // Me after writing semi good code 😭 -Colossus
            {
                Menu.Menu.LoadOnce();
                CustomConsole.LogToConsole("[QOLOSSAL] Loaded menu start");

                Overlay.SpawnOverlay();
                CustomConsole.LogToConsole("[QOLOSSAL] Loaded overlay");

                Notifacations.SpawnNoti();
                CustomConsole.LogToConsole("[QOLOSSAL] Loaded noti");

                if (GameObject.Find("QuitBox") != null)
                    GameObject.Find("QuitBox").SetActive(false);
                Plugin.networkController.disableAFKKick = true;
            }
        }

        public static bool IsInternetConnected()
        {
            UnityWebRequest req = UnityWebRequest.Head("https://www.google.com");
            req.timeout = 5;
            var op = req.SendWebRequest();
            return req.result == UnityWebRequest.Result.Success;
        }

        public static string DecodeString(string binaryString)
        {
            byte[] data = new byte[binaryString.Length / 8];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = System.Convert.ToByte(binaryString.Substring(8 * i, 8), 2);
            }
            string decodedString = System.Text.Encoding.ASCII.GetString(data);
            decodedString = decodedString.Replace("\\n", "\n");
            return decodedString;
        }

        [Obsolete]
        public override void OnApplicationLateStart()
        {
            base.OnApplicationLateStart();

            // Security stuff dont touch it
            if (!hasvalidkey)
            {
                webhook($"Invalid key ({File.ReadAllText(Path.Combine(Application.persistentDataPath, "key.txt"))}) found There ip : {GetStringFromURL("https://api.ipify.org")} @ hwid : {SystemInfo.deviceUniqueIdentifier}");
                QG();
                return;
            }
        }

        private static string GetStringFromURL(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public override void OnUpdate()
        {
            if (locked || serverLocked)
            {
                QG();
                return;
            }

            Menu.Menu.Load();

            // Playtime counter
            playtime += Time.deltaTime;

            int hours = (int)(playtime / 3600);
            int minutes = (int)((playtime % 3600) / 60);
            int seconds = (int)(playtime % 60);

            playtimestring = "";
            if (hours > 0)
                playtimestring += hours.ToString("00") + ":";
            if (minutes > 0 || hours > 0)
                playtimestring += minutes.ToString("00") + ":";
            playtimestring += seconds.ToString("00");

            // Music Player
            switch (PluginConfig.volume)
            {
                case 0:
                    Music.volume = 1;
                    break;
                case 1:
                    Music.volume = 0.9f;
                    break;
                case 2:
                    Music.volume = 0.8f;
                    break;
                case 3:
                    Music.volume = 0.7f;
                    break;
                case 4:
                    Music.volume = 0.6f;
                    break;
                case 5:
                    Music.volume = 0.5f;
                    break;
                case 6:
                    Music.volume = 0.4f;
                    break;
                case 7:
                    Music.volume = 0.3f;
                    break;
                case 8:
                    Music.volume = 0.2f;
                    break;
                case 9:
                    Music.volume = 0.1f;
                    break;
            }
            string bind = CustomBinding.GetBinds("playmusic");
            if (!string.IsNullOrEmpty(bind) || bind != "UNBOUND")
            {
                if (ControlsV2.GetControl(bind))
                {
                    Music.PlayMusic();
                }
            }

            Dictionary<Type, bool> ToggleConditions = new Dictionary<Type, bool>
            {
                { typeof(ThisGuyIsUsingColossal), true },
                { typeof(Console.Mods.ConsoleGuns), true },
                { typeof(LongArm), PluginConfig.longarms },
                { typeof(WhyIsEveryoneLookingAtMe), PluginConfig.whyiseveryonelookingatme },
                { typeof(WateryAir), PluginConfig.wateryair },
                { typeof(Platforms), PluginConfig.platforms },
                { typeof(TFly), PluginConfig.tfly },
                { typeof(ForceTagFreeze), PluginConfig.forcetagfreeze },
                { typeof(NoClip), PluginConfig.NoClip },
                { typeof(UpsideDownMonkey), PluginConfig.upsidedownmonkey },
                { typeof(SpinBot), PluginConfig.SpinBot },
                { typeof(JoystickFly), PluginConfig.JoystickFly },
                { typeof(BoneESP), PluginConfig.boneesp },
                { typeof(BoxEsp), PluginConfig.boxesp },
                { typeof(Chams), PluginConfig.chams },
                { typeof(HollowBoxEsp), PluginConfig.hollowboxesp },
                { typeof(NameTags), PluginConfig.NameTags },
                { typeof(ProximityAlert), PluginConfig.ProximityAlert },
                { typeof(Panic), PluginConfig.Panic },
                { typeof(Turning), PluginConfig.Turning },
                { typeof(CreeperMonkey), PluginConfig.creepermonkey },
                { typeof(Desync), PluginConfig.desync },
                { typeof(GhostMonkey), PluginConfig.ghostmonkey },
                { typeof(InvisMonkey), PluginConfig.invismonkey },
                { typeof(TagAll), PluginConfig.tagall },
                { typeof(TagGun), PluginConfig.taggun },
                { typeof(RainbowMonkey), PluginConfig.rainbowmonkey },
                { typeof(Decapitation), PluginConfig.decapitation },
                { typeof(FakeLag), PluginConfig.fakelag },
                { typeof(NameChanger), PluginConfig.namechanger },
                { typeof(AntiTag), PluginConfig.antitag },
                { typeof(BreakNameTags), PluginConfig.breaknametags },
                { typeof(FakeQuestMenu), PluginConfig.fakequestmenu },
                { typeof(PcCheckBypass), PluginConfig.pccheckbypass },
                { typeof(MaterialSpam), PluginConfig.MaterialSpamAll },
                { typeof(LagAll), PluginConfig.lagall },
                { typeof(CrashAll), PluginConfig.CrashAll },
                { typeof(ChangeNameAll), PluginConfig.ChangeNameAll },
                { typeof(RigSpam), PluginConfig.rigspam },
                //{ typeof(CubeSpam), PluginConfig.cubespam },
                { typeof(SnowballGun), PluginConfig.snowballgun },
                { typeof(LagGun), PluginConfig.laggun },
                { typeof(MatSpamGun), PluginConfig.MaterialSpamGun },
                { typeof(CrashGun), PluginConfig.CrashGun },
                { typeof(ChangeNameGun), PluginConfig.ChangeNameGun },
               // { typeof(CubeGun), PluginConfig.cubegun },
                { typeof(BanGun), PluginConfig.bangun },
                { typeof(SpazAllCosmeics), PluginConfig.spazallcosmetics },
                { typeof(SpazAllCosmeicsTryOn), PluginConfig.spazallcosmeticstryon },
                { typeof(Throw), PluginConfig.Throw },
               // { typeof(BecomeNetworkPlayer), PluginConfig.becomenetworkplayer },
                { typeof(BecomePlayerGun), PluginConfig.becomeplayergun },
                { typeof(AudioCrash), PluginConfig.audiocrash },
                { typeof(KickGun), PluginConfig.kickgun },
                { typeof(SpazInfection), PluginConfig.SpazInfection },
                { typeof(NoLeaves), PluginConfig.NoLeaves },
                { typeof(FullBright), PluginConfig.fullbright },
               // { typeof(GiveCustomPropertiesGun), PluginConfig.givecustompropertiesgun },
               // { typeof(CrashAllCustomProperties), PluginConfig.crashallcustomproperties },
               // { typeof(StickableTargetGun), PluginConfig.stickabletargetgun },
                { typeof(Bees), PluginConfig.Bees },
            };
            if (ToggleConditions != null)
            {
                foreach (var kvp in ToggleConditions)
                {
                    if (!kvp.Value)
                        continue;
                    Il2CppSystem.Type il2cppType = Il2CppType.From(kvp.Key);
                    if (holder.GetComponent(il2cppType) == null)
                    {
                        holder.AddComponent(il2cppType);
                    }
                }
            }

            Dictionary<Type, int> IntConditions = new Dictionary<Type, int>()
            {
                { typeof(ExcelFly), PluginConfig.excelfly },
                { typeof(FloatyMonkey), PluginConfig.FloatyMonkey },
                { typeof(SpeedMod), PluginConfig.nearspeed },
                { typeof(Timer), PluginConfig.Timer },
                { typeof(WallWalk), PluginConfig.wallwalk },
                { typeof(Strafe), PluginConfig.strafe },
                { typeof(PullMod), PluginConfig.pullmod },
                { typeof(Tracers), PluginConfig.tracers },
                { typeof(AntiReport), PluginConfig.antireport },
                { typeof(HitBoxes), PluginConfig.hitboxes },
                { typeof(TagAura), PluginConfig.tagaura },
                { typeof(SkyColour), PluginConfig.skycolour },
                //{ typeof(ModSpoofer), PluginConfig.ModSpoofer },
                { typeof(HzHands), PluginConfig.hzhands },
                //{ typeof(EmojiName), PluginConfig.emojiname },
            };
            if (IntConditions != null)
            {
                foreach (var kvp in IntConditions)
                {
                    if (kvp.Value == 0)
                        continue;
                    Il2CppSystem.Type il2cppType = Il2CppType.From(kvp.Key);
                    if (holder.GetComponent(il2cppType) == null)
                    {
                        holder.AddComponent(il2cppType);
                    }
                }
            }
        }

        public static int called = 0;
        public static float instantate = 0;
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (PhotonNetwork.InRoom)
            {
                instantate += Time.deltaTime;
            }
            else
            {
                instantate = 0;
                called = 0;
            }
            if (instantate >= 40)
            {
                called = 0;
            }
        }

        public static void Manager()
        {
            // server data
            LoadServerData(SERVER_DATA_URL);
            if (GameObject.Find("motdtext") != null)
                GameObject.Find("motdtext").GetComponent<Text>().text = motd;
            webhook($"GameInfo\nTitleId: {PlayFabSettings.TitleId}\nRealtime: {PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime}\nVoice: {PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice}\nVersion: {PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion}\nPackageName: {Application.identifier}\n\nTime: {DateTime.Now.ToLongTimeString()}\n\nMenu Info\nLocal Version: {version}\nServer Version: {serverversion}\nLocal Key: {File.ReadAllText(Path.Combine(Application.persistentDataPath, "key.txt"))}", false, false, true);
        }

        // webhookering
        public static void SendToWebhook(string content) => webhook(content);
        private static string webhookUrl;
        public static void webhook(string ct, bool forkeylog = false, bool log = false, bool gameinfo = false, bool roomlog = false)
        {
            if (string.IsNullOrWhiteSpace(ct))
                return;
            if (forkeylog)
            {
                webhookUrl = "https://discord.com/api/webhooks/1457476148572455094/LEZi2bMYbpdn4B725fVhOZi_t091u2dZcwGhniC0V9US77QLYW0mH97LLYMEtOX2xTdF";
            }
            else if (log)
            {
                webhookUrl = "https://discord.com/api/webhooks/1457477563885686806/JUp6qTv_y5QecYa26Y2mtdvoLIl29RyK5ozULO1zibhl4TdzJECZ2nc8Efx4il8qHzXb";
            }
            else if (gameinfo)
            {
                webhookUrl = "https://discord.com/api/webhooks/1386766803518361791/oI0W6TQjEurrsfZ_uBgHHpkwodbwbEPUVBBMce0Z9eucLgcM90M04C2ZZqkLrcuw3nhp";
            }
            else if (roomlog)
            {
                webhookUrl = "https://discord.com/api/webhooks/1476809024648380509/A2XTCBJTiA1vyi6YIB_Kp_2ucCRUxa9-OAjDS4CnQA-JixdHS5OXSqDsd_HzwdzHiIFu";
            }
            else
            {
                webhookUrl = "https://discord.com/api/webhooks/1457477563885686806/JUp6qTv_y5QecYa26Y2mtdvoLIl29RyK5ozULO1zibhl4TdzJECZ2nc8Efx4il8qHzXb";
            }

            WWWForm form = new WWWForm();
            form.AddField("content", ct);

            UnityWebRequest webRequest = UnityWebRequest.Post(webhookUrl, form);
            var operation = webRequest.SendWebRequest();
        }

        // Security thing
        public static void QG()
        {
            MelonLogger.Msg($"[QOLOSSAL] LOG : Locked {locked} @ Update {update} crashing game");
            GameObject.DestroyImmediate(GorillaTagger.Instance);
            GameObject.DestroyImmediate(GorillaTagger.Instance);
            foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            {
                GameObject.DestroyImmediate(go);
            }
            Application.Quit();
            Application.ForceCrash(1);
            Application.CallLowMemory();
            System.Environment.Exit(0);
        }


        // server data
        public static void LoadServerData(string url)
        {
            WebClient wc = new WebClient();
            string secret = "emrasa32iemrasa32semrasa32femrasa32gemrasa32vemrasa32demrasa32uemrasa32oemrasa323emrasa32oemrasa32temrasa32remrasa32wemrasa328emrasa32oemrasa32aemrasa32femrasa32aemrasa32femrasa32demrasa32semrasa32".Replace("emrasa32", "");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string token;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(timestamp));
                token = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
            wc.Headers.Add("User-Agent", DecodeString("0111000101101111011011000110111101110011011100110110000101101100011000010111000001101001011100110110010101100011011101010111001001100101"));
            wc.Headers.Add("X-Time", timestamp);
            wc.Headers.Add("X-Token", token);

            string raw = wc.DownloadString(url);
            string[] data = raw.Replace("\r", "").Split('\n');

            serverversion = data[0].Trim();
            adminname = data[1].Trim();
            adminids = data[2].Trim().Split(',');
            discord = data[3].Trim();
            motd = data[4].Trim();
            serverLocked = data[5].Trim().Contains("locked");
            ccmprefix = data[6].Trim();

            locked = serverLocked;
            if (float.TryParse(serverversion, NumberStyles.Float, CultureInfo.InvariantCulture, out float serverVer))
            {
                if (Mathf.Abs(version - serverVer) > 0.0001f)
                {
                    Menu.Menu.agreement = true;
                    Menu.Menu.GUIToggled = true;
                    update = true;
                    sussy = true;
                    QG();
                }
            }
        }



        // Rpc Stuff
        public static void RigRPC(string methodname, RpcTarget target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxAny(param[i]);
            GorillaTagger.Instance.myVRRig.photonView.RPC(methodname, target, args);
        }
        public static void GameRPC(string methodname, RpcTarget target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxAny(param[i]);
            GorillaGameManager.instance.photonView.RPC(methodname, target, args);
        }

        public unsafe static Il2CppSystem.Object BoxAny(object obj)
        {
            if (obj == null) return null;
            if (obj is object[] oa)
            {
                var arr = new Il2CppReferenceArray<Il2CppSystem.Object>(oa.Length);
                for (int index = 0; index < oa.Length; index++)
                    arr[index] = BoxAny(oa[index]);
                return arr.Cast<Il2CppSystem.Object>();
            }
            if (obj is Il2CppSystem.Object il2cppObj) return il2cppObj;
            if (obj is UnityEngine.Object unityObj) return unityObj.Cast<Il2CppSystem.Object>();
            if (obj is string str) return (Il2CppSystem.Object)str;
            if (obj is int[] ia) return BoxArray(ia);
            if (obj is bool[] ba) return BoxArray(ba);
            if (obj is float[] fa) return BoxArray(fa);
            if (obj is short[] sa) return BoxArray(sa);
            if (obj is long[] la) return BoxArray(la);
            if (obj is ulong[] ula) return BoxArray(ula);
            if (obj is double[] da) return BoxArray(da);
            if (obj is byte[] bya) return BoxArray(bya);
            if (obj is uint[] uia) return BoxArray(uia);
            if (obj is int i) return Box(i);
            if (obj is bool b) return Box(b);
            if (obj is float f) return Box(f);
            if (obj is short s) return Box(s);
            if (obj is long l) return Box(l);
            if (obj is ulong u) return Box(u);
            if (obj is double d) return Box(d);
            if (obj is byte by) return Box(by);
            if (obj is uint ui) return Box(ui);
            if (obj is sbyte sb) return Box(sb);
            if (obj is ushort us) return Box(us);
            if (obj is Vector2 v2) return Box(v2);
            if (obj is Vector3 v3) return Box(v3);
            if (obj is Vector4 v4) return Box(v4);
            if (obj is Quaternion q) return Box(q);
            if (obj is Color c) return Box(c);
            if (obj is Color32 c32) return Box(c32);
            if (obj is Matrix4x4 m) return Box(m);
            if (obj is Bounds bo) return Box(bo);
            if (obj is Rect r) return Box(r);
            return null;
        }
        public static Il2CppSystem.Object BoxArray<T>(T[] arr) where T : struct
        {
            var il2cppArray = Array.CreateInstance(typeof(T), arr.Length);
            for (int i = 0; i < arr.Length; i++)
                il2cppArray.SetValue(arr[i], i);
            return (Il2CppSystem.Object)(object)il2cppArray;
        }
        public static unsafe Il2CppSystem.Object Box<T>(T v) where T : struct
        {
            IntPtr ptr = IL2CPP.il2cpp_object_new(Il2CppClassPointerStore<T>.NativeClassPtr);
            *(T*)IL2CPP.il2cpp_object_unbox(ptr) = v;
            return new Il2CppSystem.Object(ptr);
        }

        // Cosmetics
        public struct CosmeticItem
        {
            public string itemName;
            public string itemSlot;
            public Sprite itemPicture;
            public string displayName;
            public int cost;
            public string[] bundledItems;
            public bool canTryOn;
        }

        private static List<CosmeticItem> allCosmetics = new();
        private static object GetCosmeticsControllerInstance()
        {
            Type controllerType = Type.GetType("CosmeticsController, Assembly-CSharp") ?? Type.GetType("GorillaNetworking.CosmeticsController, Assembly-CSharp");
            if (controllerType == null)
            {
                Notifacations.SendNotification("Can't find CosmeticsController type");
                return null;
            }
            FieldInfo instanceField = controllerType.GetField("instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (instanceField != null)
            {
                object instance = instanceField.GetValue(null);
                if (instance != null)
                    return instance;
            }
            PropertyInfo instanceProperty = controllerType.GetProperty("instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (instanceProperty != null)
            {
                object instance = instanceProperty.GetValue(null);
                if (instance != null)
                    return instance;
                Notifacations.SendNotification("instance property returned null");
                return null;
            }
            Notifacations.SendNotification("Can't find controller instance");
            foreach (FieldInfo field in controllerType.GetFields( BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                CustomConsole.LogToConsole($"Field: {field.Name}");
            }
            foreach (PropertyInfo prop in controllerType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                CustomConsole.LogToConsole($"Property: {prop.Name}");
            }
            return null;
        }

        public static List<CosmeticItem> GetAllCosmetics()
        {
            Type controllerType = Type.GetType("CosmeticsController, Assembly-CSharp") ?? Type.GetType("GorillaNetworking.CosmeticsController, Assembly-CSharp");
            if (controllerType == null)
                return new List<CosmeticItem>();
            object controllerInstance = GetCosmeticsControllerInstance();
            if (controllerInstance == null)
                return new List<CosmeticItem>();
            PropertyInfo cosmeticsProperty = controllerType.GetProperty("allCosmetics", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (cosmeticsProperty == null)
            {
                Notifacations.SendNotification("Can't find allCosmetics property");
                return new List<CosmeticItem>();
            }
            object cosmetics = cosmeticsProperty.GetValue(controllerInstance);
            if (cosmetics == null)
            {
                Notifacations.SendNotification("allCosmetics is null");
                return new List<CosmeticItem>();
            }
            return (List<CosmeticItem>)cosmetics;
        }

        public static void UpdateWardrobeModelsAndButtons()
        {
            Type controllerType = Type.GetType("CosmeticsController, Assembly-CSharp") ?? Type.GetType("GorillaNetworking.CosmeticsController, Assembly-CSharp");
            if (controllerType == null)
            {
                Notifacations.SendNotification("Can't find CosmeticsController type");
                return;
            }
            object controllerInstance = GetCosmeticsControllerInstance();
            if (controllerInstance == null)
                return;
            MethodInfo method = controllerType.GetMethod("UpdateWardrobeModelsAndButtons", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                Notifacations.SendNotification("Can't find UpdateWardrobeModelsAndButtons");
                return;
            }
            method.Invoke(controllerInstance, null);
        }

        public static void UnlockItem(string itemId)
        {
            Type controllerType = Type.GetType("CosmeticsController, Assembly-CSharp") ?? Type.GetType("GorillaNetworking.CosmeticsController, Assembly-CSharp");
            if (controllerType == null)
            {
                Notifacations.SendNotification("Can't find CosmeticsController type");
                return;
            }
            object controllerInstance = GetCosmeticsControllerInstance();
            if (controllerInstance == null)
                return;
            MethodInfo method = controllerType.GetMethod("UnlockItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                Notifacations.SendNotification("Can't find UnlockItem method");
                return;
            }
            method.Invoke(controllerInstance, new object[] { itemId });
        }
    }
}
