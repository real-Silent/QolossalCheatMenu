using Qolossal.Notifacation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Qolossal.Menu
{
    public static class PluginConfig
    {
        // Movement
        public static int excelfly = 0; public static string excelfly_bind = "";
        public static bool tfly = false; public static string tfly_bind = "";
        public static int wallwalk = 0; public static string wallwalk_bind = "";
        public static int speed = 0;
        public static int speedtoggle = 0; public static string speedtoggle_bind = "";
        public static int nearspeed = 0;
        public static int nearspeeddistance = 0;
        public static bool platforms = false; public static string platforms_bind = "";
        public static bool upsidedownmonkey = false;
        public static bool wateryair = false; public static string wateryair_bind = "";
        public static bool longarms = false;
        public static bool SpinBot = false;
        public static bool JoystickFly = false;
        public static int FloatyMonkey = 0; public static string floatymonkey_bind = "";
        public static int Timer = 0;
        public static bool ClimbableGorillas = false;
        public static int NearPulse = 0;
        public static int NearPulseDistance = 0;
        public static bool PlayerScale = false;
        public static bool NoClip = false; public static string noclip_bind = "";
        public static bool forcetagfreeze = false;
        public static bool Throw = false; public static string throw_bind = "";
        public static int hzhands = 0;
        public static int strafe = 0; public static string strafe_bind = "";
        public static int strafespeed = 0;
        public static int strafejumpamount = 0;
        public static int pullmod = 0; public static string pullmod_bind = "";

        // Visual
        public static bool chams = false;
        public static bool boxesp = false;
        public static bool hollowboxesp = false;
        public static bool whyiseveryonelookingatme = false;
        public static int tracers = 0;
        public static int tracersize = 0;
        public static bool boneesp = false;
        public static bool fullbright = false;
        public static bool ProximityAlert = false;
        public static bool showboards = true;
        public static bool NoLeaves = true;


        public static bool NameTags = false;
        public static bool ShowCreationDate = true;
        public static bool ShowColourCode = true;
        public static bool ShowDistance = true;

        // Player
        public static bool nofinger = false;
        public static bool taggun = false; public static string taggun_bind = "";
        public static bool legmod = false;
        public static bool creepermonkey = false;
        public static bool ghostmonkey = false; public static string ghostmonkey_bind = "";
        public static bool invismonkey = false; public static string invismonkey_bind = "";
        public static int tagaura = 0;
        public static bool tagall = false;
        public static bool freezemonkey = false;
        public static bool desync = false;
        public static int hitboxes = 0;
        public static bool fakelag = false;
        public static bool rainbowmonkey = false;
        public static bool namechanger = false;
        public static bool decapitation = false;
        public static bool antitag = false;
        public static bool Bees = false;

        // Exploits
        public static bool audiocrash = false;
        //public static bool becomenetworkplayer = false;
        public static bool becomeplayergun = false; public static string becomeplayergun_bind = "";
        public static bool spazallcosmeticstryon = false;
        public static bool spazallcosmetics = false;
        public static bool breaknametags = false;
        
        public static bool anticrash = false;
        public static int anticrashtype = 0;
        public static bool MaterialSpamAll = false; public static string materialspamall_bind = "";
        public static bool MaterialSpamGun = false; public static string materialspamgun_bind = "";
        public static bool CrashAll = false;
        public static bool CrashGun = false; public static string crashgun_bind = "";
        public static bool ChangeNameAll = false; 
        public static bool ChangeNameGun = false; public static string changenamegun_bind = "";
        public static bool lagall = false;
        public static bool laggun = false; public static string laggun_bind = "";
        public static bool rigspam = false; public static string rigspam_bind = "";
        //public static bool cubespam = false; public static string cubespam_bind = "";
        public static bool snowballgun = false; public static string snowballgun_bind = "";
        public static int projectiletype = 0;
        //public static bool cubegun = false; public static string cubegun_bind = "";
        public static bool bangun = false; public static string bangun_bind = "";
        public static bool kickgun = false; public static string kickgun_bind = "";
        //public static bool givecustompropertiesgun = false; public static string givecustompropertiesgun_bind = "";
        //public static bool stickabletargetgun = false; public static string stickabletargetgun_bind = "";
        //public static bool crashallcustomproperties = false;
        public static bool SpazInfection = false;
        //public static int ModSpoofer = 0;
        //public static int emojiname = 0;

        // Menu
        public static bool Notifications = true;
        public static bool overlay = true;
        public static bool tooltips = true;
        public static bool PlayerLogging = false;
        public static bool invertedControls = false;
        public static bool legacyui = true;

        public static bool loopmusic = false;
        public static bool soundboard = false;

        // Safety
        public static bool Turning = false;
        public static bool moddedgamemode = false;
        public static bool competitivegamemode = false;
        public static bool Panic = false;
        public static int antireport = 0;
        public static bool pccheckbypass = false;
        public static bool fakequestmenu = false;

        //Settings
        public static int MenuPosition = 0;
        public static int menufont = 0;
        public static int MenuColour = 0;
        public static int GhostColour = 0;
        public static int BeamColour = 0;
        public static int ESPColour = 0;
        public static int GhostOpacity = 2;
        public static int HitBoxesOpacity = 0;
        public static int HitBoxesColour = 0;

        public static int volume = 0; public static string playmusic_bind = "";

        //idfk why this has to go here specifically ---
        public static int nametagheight = 0;
        public static int nametagsize = 0;
        public static int nametagcolour = 0;
        // ---

        public static int skycolour = 0;

        // Macro
        public static bool recordmacro = false; public static string recordmacro_bind = "";
        public static bool autoplayproximity = false;
        public static int autoplaydistance = 0;
        public static int macrolerpspeed = 0;

        // Dev
        public static bool consoleusersnametags = false;
        public static bool consolequitgun = false;
        public static bool consolebringgun = false;
        public static bool consolekickgun = false;
        public static bool consolechangenamegun = false;
        public static bool consolerestartmicgun = false;
        public static bool consoleghostgun = false;
        public static bool consoleunghostgun = false;
        public static bool consolemutegun = false;
        public static bool consoleunmutegun = false;
        public static bool consoledisablemovementgun = false;
        public static bool consoleenablemovementgun = false;
        public static bool consoletargetplayergun = false;
        public static bool consoleflinggun = false;
    }

    [MelonLoader.RegisterTypeInIl2Cpp]
    internal class Configs : MonoBehaviour
    {
        public Configs(IntPtr e) : base(e) { }

        public static string folderPath = "Qolossal";

        public static string logPath = "Qolossal/Logs";
        public static string musicPath = "Qolossal/Music";
        public static string macroPath = "Qolossal/Macro";

        public static string configPath = "Qolossal/Configs";
        public static string fileExtension = ".json";
        public static string fileName = "NewConfig";

        public virtual void Start()
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!Directory.Exists(configPath))
                Directory.CreateDirectory(configPath);
            else if (Directory.GetFiles(configPath).Length == 0)
                SaveConfig();

            if (!Directory.Exists(musicPath))
                Directory.CreateDirectory(musicPath);
            if (!Directory.Exists(macroPath))
                Directory.CreateDirectory(macroPath);
        }

        public static string[] GetConfigFileNames()
        {
            string[] result;
            try
            {
                string[] files = Directory.GetFiles(Configs.configPath, "*" + Configs.fileExtension);
                string[] array = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    array[i] = Path.GetFileNameWithoutExtension(files[i]);
                }
                result = array;
            }
            catch { result = new string[] { "Error" }; }
            return result;
        }
        public static void SaveConfig()
        {
            try
            {
                //CustomConsole.LogToConsole("[QOLOSSAL] Saving Config");
                string[] existingFiles = Directory.GetFiles(configPath, "*" + fileExtension);
                int nextFileNumber = 1;
                while (existingFiles.Any(file => Path.GetFileNameWithoutExtension(file).EndsWith(nextFileNumber.ToString())))
                {
                    nextFileNumber++;
                }
                string newFileName = $"{fileName}_{nextFileNumber}{fileExtension}";
                string filePath = Path.Combine(configPath, newFileName);
                var values = new Dictionary<string, object>();
                foreach (var prop in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    values[prop.Name] = prop.GetValue(null);
                }
                string jsonContent = JsonConvert.SerializeObject(values, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
                Notifacations.SendNotification($"<color=blue>[CONFIG]</color> SAVED : {filePath}");
            }
            catch { }
        }
        public static void LoadConfig(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    foreach (var prop in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                    {
                        if (values.ContainsKey(prop.Name))
                        {
                            object parsedValue = values[prop.Name];
                            if (parsedValue is long longValue)
                                parsedValue = (int)longValue;
                            prop.SetValue(null, parsedValue);
                        }
                    }
                    Notifacations.SendNotification($"<color=blue>[CONFIG]</color> LOADED : {filePath}");
                }
                else
                    Notifacations.SendNotification($"[QOLOSSAL] Config file not found: {filePath}");
            }
            catch (Exception ex)
            {
                Notifacations.SendNotification($"[QOLOSSAL] Error loading config: {ex.Message}");
            }
        }
    }
}