using static Qolossal.Menu.Menu;

namespace Qolossal.Menu
{
    public class MenuLoader
    {
        public static MenuOption[] LoadMenu()
        {
            MainMenu = new MenuOption[13];
            MainMenu[0] = new MenuOption { DisplayName = "Movement", _type = Plugin.submenuthingy, AssociatedString = "Movement" };
            MainMenu[1] = new MenuOption { DisplayName = "Visual", _type = Plugin.submenuthingy, AssociatedString = "Visual" };
            MainMenu[2] = new MenuOption { DisplayName = "Player", _type = Plugin.submenuthingy, AssociatedString = "Player" };
            MainMenu[3] = new MenuOption { DisplayName = "Computer", _type = Plugin.submenuthingy, AssociatedString = "Computer" };
            MainMenu[4] = new MenuOption { DisplayName = "Exploits", _type = Plugin.submenuthingy, AssociatedString = "Exploits" };
            MainMenu[5] = new MenuOption { DisplayName = "Safety", _type = Plugin.submenuthingy, AssociatedString = "Safety" };
            MainMenu[6] = new MenuOption { DisplayName = "MusicPlayer", _type = Plugin.submenuthingy, AssociatedString = "MusicPlayer" };
            MainMenu[7] = new MenuOption { DisplayName = "Settings", _type = Plugin.submenuthingy, AssociatedString = "Settings" };
            MainMenu[8] = new MenuOption { DisplayName = "Info", _type = Plugin.submenuthingy, AssociatedString = "Info" };
            MainMenu[9] = new MenuOption { DisplayName = "Macro", _type = Plugin.submenuthingy, AssociatedString = "Macro" };
            MainMenu[10] = new MenuOption { DisplayName = "Notifications", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.Notifications };
            MainMenu[11] = new MenuOption { DisplayName = "Overlay", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.overlay };
            MainMenu[12] = new MenuOption { DisplayName = "Tool Tips", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.tooltips };

            Movement = new MenuOption[12];
            Movement[0] = new MenuOption { DisplayName = "ExcelFly", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "Super Slow", "Slow", "Medium", "Fast", "Super Fast" } };
            Movement[1] = new MenuOption { DisplayName = "TFly", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.tfly };
            Movement[2] = new MenuOption { DisplayName = "WallWalk", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "6.8", "7", "7.5", "7.8", "8", "8.5", "8.8", "9", "9.5", "9.8" } };
            Movement[3] = new MenuOption { DisplayName = "Speed Options", _type = Plugin.submenuthingy, AssociatedString = "Speed Options" };
            Movement[4] = new MenuOption { DisplayName = "Platforms", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.platforms };
            Movement[5] = new MenuOption { DisplayName = "UpsideDown Monkey", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.upsidedownmonkey };
            Movement[6] = new MenuOption { DisplayName = "WateryAir", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.wateryair };
            Movement[7] = new MenuOption { DisplayName = "LongArms", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.longarms };
            Movement[8] = new MenuOption { DisplayName = "SpinBot", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.SpinBot };
            Movement[9] = new MenuOption { DisplayName = "JoystickFly", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.JoystickFly };
            Movement[10] = new MenuOption { DisplayName = "Next", _type = Plugin.submenuthingy, AssociatedString = "Movement2" };
            Movement[11] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Movement2 = new MenuOption[14];
            Movement2[0] = new MenuOption { DisplayName = "Timer", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "1.03x", "1.06x", "1.09x", "1.1x", "1.13x", "1.16x", "1.19x", "1.2x", "1.23x", "1.26", "1.29", "1.3x", "2x", "3x", "4x", "5x" } };
            Movement2[1] = new MenuOption { DisplayName = "FloatyMonkey", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "1.1", "1.2", "1.4", "1.6", "1.8", "2", "2.2", "2.4", "2.6", "2.8", "3", "3.2", "3.4", "3.6", "3.8", "4", "Anti Grav" } };
            Movement2[2] = new MenuOption { DisplayName = "Climbable Gorillas", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ClimbableGorillas };
            Movement2[3] = new MenuOption { DisplayName = "Near Pulse", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" } };
            Movement2[4] = new MenuOption { DisplayName = "Near Pulse Distance", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" } };
            Movement2[5] = new MenuOption { DisplayName = "Player Scale", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.PlayerScale };
            Movement2[6] = new MenuOption { DisplayName = "No Clip", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.NoClip };
            Movement2[7] = new MenuOption { DisplayName = "Force Tag Freeze", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.forcetagfreeze };
            Movement2[8] = new MenuOption { DisplayName = "Teleport To Random", _type = Plugin.buttonthingy, AssociatedString = "teleporttorandom" };
            Movement2[9] = new MenuOption { DisplayName = "HZ Hands", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" } };
            Movement2[10] = new MenuOption { DisplayName = "Throw", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.Throw };
            Movement2[11] = new MenuOption { DisplayName = "Strafe Options", _type = Plugin.submenuthingy, AssociatedString = "Strafe Options" };
            Movement2[12] = new MenuOption { DisplayName = "Pull Mod", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "10", "20", "30", "40", "50", "60" } };
            Movement2[13] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Speed = new MenuOption[5];
            Speed[0] = new MenuOption { DisplayName = "Speed", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6" } };
            Speed[1] = new MenuOption { DisplayName = "Speed Toggle", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6" } };
            Speed[2] = new MenuOption { DisplayName = "Near Speed", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6" } };
            Speed[3] = new MenuOption { DisplayName = "Near Speed Distance", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" } };
            Speed[4] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Menu.Strafe = new MenuOption[4];
            Menu.Strafe[0] = new MenuOption { DisplayName = "Strafe", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "Look", "L Joystick" } };
            Menu.Strafe[1] = new MenuOption { DisplayName = "Strafe Speed", _type = Plugin.sliderthingy, StringArray = new string[] { "6", "8", "10", "12", "14", "16", "18", "20" } };
            Menu.Strafe[2] = new MenuOption { DisplayName = "Strafe Jump Amount", _type = Plugin.sliderthingy, StringArray = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" } };
            Menu.Strafe[3] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Visual = new MenuOption[12];
            Visual[0] = new MenuOption { DisplayName = "Chams", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.chams };
            Visual[1] = new MenuOption { DisplayName = "BoxESP", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.boxesp };
            Visual[2] = new MenuOption { DisplayName = "HollowBoxESP", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.hollowboxesp };
            Visual[3] = new MenuOption { DisplayName = "BoneESP", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.boneesp };
            Visual[4] = new MenuOption { DisplayName = "Tracers", _type = Plugin.submenuthingy, AssociatedString = "Tracers" };
            Visual[5] = new MenuOption { DisplayName = "NameTags", _type = Plugin.submenuthingy, AssociatedString = "NameTags" };
            Visual[6] = new MenuOption { DisplayName = "Proximity Alert", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ProximityAlert };
            Visual[7] = new MenuOption { DisplayName = "Full Bright", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.fullbright };
            Visual[8] = new MenuOption { DisplayName = "Sky Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "Default", "Purple", "Red", "Cyan", "Green", "Black" } };
            Visual[9] = new MenuOption { DisplayName = "WhyIsEveryoneLookingAtMe", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.whyiseveryonelookingatme };
            Visual[10] = new MenuOption { DisplayName = "Next", _type = Plugin.submenuthingy, AssociatedString = "Visual2" };
            Visual[11] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };
            Visual2 = new MenuOption[3];
            Visual2[0] = new MenuOption { DisplayName = "NoLeaves", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.NoLeaves };
            Visual2[1] = new MenuOption { DisplayName = "Show Boards", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.showboards };
            Visual2[2] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Menu.Tracers = new MenuOption[3];
            Menu.Tracers[0] = new MenuOption { DisplayName = "Tracers", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "RHand", "LHand", "Head", "Screen" } };
            Menu.Tracers[1] = new MenuOption { DisplayName = "Tracer Size", _type = Plugin.sliderthingy, StringArray = new string[] { "Extremely Small", "Super Small", "Small", "Medium", "Large", "Giant", "Huge" } };
            Menu.Tracers[2] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };
            Menu.NameTags = new MenuOption[8];
            Menu.NameTags[0] = new MenuOption { DisplayName = "NameTags", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.NameTags };
            Menu.NameTags[1] = new MenuOption { DisplayName = "Show Creation Date", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ShowCreationDate };
            Menu.NameTags[2] = new MenuOption { DisplayName = "Show Colour Code", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ShowColourCode };
            Menu.NameTags[3] = new MenuOption { DisplayName = "Show Distance", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ShowDistance };
            Menu.NameTags[4] = new MenuOption { DisplayName = "NameTag Height", _type = Plugin.sliderthingy, StringArray = new string[] { "Chest", "Above Head" } };
            Menu.NameTags[5] = new MenuOption { DisplayName = "NameTag Size", _type = Plugin.sliderthingy, StringArray = new string[] { "Chest Size", "Small", "Medium", "Large" } };
            Menu.NameTags[6] = new MenuOption { DisplayName = "NameTag Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "White", "Yellow", "Green", "Blue", "Red", "Cyan", "Black" } };
            Menu.NameTags[7] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Player = new MenuOption[14];
            Player[0] = new MenuOption { DisplayName = "NoFinger", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.nofinger };
            Player[1] = new MenuOption { DisplayName = "TagGun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.taggun };
            Player[2] = new MenuOption { DisplayName = "CreeperMonkey", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.creepermonkey };
            Player[3] = new MenuOption { DisplayName = "GhostMonkey", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ghostmonkey };
            Player[4] = new MenuOption { DisplayName = "InvisMonkey", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.invismonkey };
            Player[5] = new MenuOption { DisplayName = "TagAura", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "Really Close", "Close", "Legit", "Semi Legit", "Semi Blatant", "Blatant", "Rage" } };
            Player[6] = new MenuOption { DisplayName = "TagAll", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.tagall };
            Player[7] = new MenuOption { DisplayName = "Desync", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.desync };
            Player[8] = new MenuOption { DisplayName = "HitBoxes", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "Really Close", "Close", "Legit", "Semi Legit", "Semi Blatant", "Blatant", "Rage" } };
            Player[9] = new MenuOption { DisplayName = "Fake Lag", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.fakelag };
            Player[10] = new MenuOption { DisplayName = "Rainbow Monkey", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.rainbowmonkey, extra = "[STUMP]" };
            Player[11] = new MenuOption { DisplayName = "Name Changer", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.namechanger, extra = "[STUMP]" };
            Player[12] = new MenuOption { DisplayName = "Next", _type = Plugin.submenuthingy, AssociatedString = "Player2" };
            Player[13] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Player2 = new MenuOption[4];
            Player2[0] = new MenuOption { DisplayName = "Decapitation", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.decapitation };
            Player2[1] = new MenuOption { DisplayName = "Anti Tag", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.antitag };
            Player2[2] = new MenuOption { DisplayName = "Bees", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.Bees };
            Player2[3] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Exploits = new MenuOption[10];
            Exploits[0] = new MenuOption { DisplayName = "Break NameTags", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.breaknametags };
            Exploits[1] = new MenuOption { DisplayName = "Change Name All", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ChangeNameAll, extra = "[M] [SS]" };
            Exploits[2] = new MenuOption { DisplayName = "Audio Crash", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.audiocrash };
            Exploits[3] = new MenuOption { DisplayName = "Cosmetics Spoofer", _type = Plugin.submenuthingy, AssociatedString = "Cosmetics Spoofer" };
            Exploits[4] = new MenuOption { DisplayName = "Lag All", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.lagall };
            Exploits[5] = new MenuOption { DisplayName = "Clear Prefabs", _type = Plugin.buttonthingy, AssociatedString = "Clear Prefabs" };
            Exploits[6] = new MenuOption { DisplayName = "Set Master", _type = Plugin.buttonthingy, AssociatedString = "Set Master" };
            Exploits[7] = new MenuOption { DisplayName = "Crash All", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.CrashAll };
            Exploits[8] = new MenuOption { DisplayName = "Next", _type = Plugin.submenuthingy, AssociatedString = "Exploits2" };
            Exploits[9] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Exploits2 = new MenuOption[11];
            Exploits2[0] = new MenuOption { DisplayName = "Anti Ban", _type = Plugin.buttonthingy, AssociatedString = "Anti Ban" };
            Exploits2[1] = new MenuOption { DisplayName = "Snowball Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.snowballgun };
            Exploits2[2] = new MenuOption { DisplayName = "Projectile Type", _type = Plugin.sliderthingy, StringArray = new string[] { "Snowball", "Slingshot", "Cloud", "Cupid", "Ice", "Deadshot", "Elf" } };
            Exploits2[3] = new MenuOption { DisplayName = "Ban All", _type = Plugin.buttonthingy, AssociatedString = "banall" };
            Exploits2[4] = new MenuOption { DisplayName = "Kick All", _type = Plugin.buttonthingy, AssociatedString = "Kick All" };
            Exploits2[5] = new MenuOption { DisplayName = "Clone Self", _type = Plugin.buttonthingy, AssociatedString = "Clone Self" };
            Exploits2[6] = new MenuOption { DisplayName = "Spaz Infection", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.SpazInfection };
            Exploits2[7] = new MenuOption { DisplayName = "Ban Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.bangun };
            Exploits2[8] = new MenuOption { DisplayName = "Crash Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.CrashGun };
            Exploits2[9] = new MenuOption { DisplayName = "Next", _type = Plugin.submenuthingy, AssociatedString = "Exploits3" };
            Exploits2[10] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Exploits3 = new MenuOption[8];
            Exploits3[0] = new MenuOption { DisplayName = "Kick Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.kickgun };
            Exploits3[1] = new MenuOption { DisplayName = "Lag Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.laggun };
            Exploits3[2] = new MenuOption { DisplayName = "Rig Spam", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.rigspam };
            Exploits3[3] = new MenuOption { DisplayName = "Change Name Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.ChangeNameGun };
            Exploits3[4] = new MenuOption { DisplayName = "Material Spam All", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.MaterialSpamAll };
            Exploits3[5] = new MenuOption { DisplayName = "Material Spam Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.MaterialSpamGun };
            Exploits3[6] = new MenuOption { DisplayName = "Become Player Gun", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.becomeplayergun };
            Exploits3[7] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            CosmeticsSpoofer = new MenuOption[4];
            CosmeticsSpoofer[0] = new MenuOption { DisplayName = "Spaz All Cosmetics (Try On)", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.spazallcosmeticstryon, extra = "[CITY]" };
            CosmeticsSpoofer[1] = new MenuOption { DisplayName = "Spaz All Cosmetics", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.spazallcosmetics };
            CosmeticsSpoofer[2] = new MenuOption { DisplayName = "Unlock All Cosmetics", _type = Plugin.buttonthingy, AssociatedString = "Unlock All Cosmetics" };
            CosmeticsSpoofer[3] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Computer = new MenuOption[10];
            Computer[0] = new MenuOption { DisplayName = "Disconnect", _type = Plugin.buttonthingy, AssociatedString = "disconnect" };
            Computer[1] = new MenuOption { DisplayName = "Join GTC", _type = Plugin.buttonthingy, AssociatedString = "join GTC" };
            Computer[2] = new MenuOption { DisplayName = "Join TTT", _type = Plugin.buttonthingy, AssociatedString = "join TTT" };
            Computer[3] = new MenuOption { DisplayName = "Join YTTV", _type = Plugin.buttonthingy, AssociatedString = "join YTTV" };
            Computer[4] = new MenuOption { DisplayName = "Join 1", _type = Plugin.buttonthingy, AssociatedString = "join 1" };
            Computer[5] = new MenuOption { DisplayName = "Join Public", _type = Plugin.buttonthingy, AssociatedString = "join PUBLIC" };
            Computer[6] = new MenuOption { DisplayName = "Join QCMV3 Only", _type = Plugin.buttonthingy, AssociatedString = "join QCMV3 Only" };
            Computer[7] = new MenuOption { DisplayName = "Gamemodes", _type = Plugin.submenuthingy, AssociatedString = "Gamemodes" };
            Computer[8] = new MenuOption { DisplayName = "Turning", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.Turning };
            Computer[9] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Gamemodes = new MenuOption[7];
            Gamemodes[0] = new MenuOption { DisplayName = "Modded Gamemode", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.moddedgamemode };
            Gamemodes[1] = new MenuOption { DisplayName = "Competitive Gamemode", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.competitivegamemode };
            Gamemodes[2] = new MenuOption { DisplayName = "Infection", _type = Plugin.buttonthingy, AssociatedString = "cgamemode INFECTION" };
            Gamemodes[3] = new MenuOption { DisplayName = "Casual", _type = Plugin.buttonthingy, AssociatedString = "cgamemode CASUAL" };
            Gamemodes[4] = new MenuOption { DisplayName = "Hunt", _type = Plugin.buttonthingy, AssociatedString = "cgamemode HUNT" };
            Gamemodes[5] = new MenuOption { DisplayName = "PaintBrawl", _type = Plugin.buttonthingy, AssociatedString = "cgamemode BATTLE" };
            Gamemodes[6] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Safety = new MenuOption[8];
            Safety[0] = new MenuOption { DisplayName = "Panic", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.Panic };
            Safety[1] = new MenuOption { DisplayName = "AntiReport", _type = Plugin.sliderthingy, StringArray = new string[] { "[OFF]", "Disconnect", "Reconnect", "Join Random" } };
            Safety[2] = new MenuOption { DisplayName = "RandomIdentity", _type = Plugin.buttonthingy, AssociatedString = "randomidentity" };
            Safety[3] = new MenuOption { DisplayName = "Pc Check Bypass", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.pccheckbypass };
            Safety[4] = new MenuOption { DisplayName = "Fake Quest Menu", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.fakequestmenu };
            Safety[5] = new MenuOption { DisplayName = "Anti Crash", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.anticrash };
            Safety[6] = new MenuOption { DisplayName = "Anti Crash Type", _type = Plugin.sliderthingy, StringArray = new string[] { "Gorilla Enemy", "Gorilla Fireball", "Network Player", "Stickable Target", "bulletPrefab", "All" } };
            Safety[7] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Settings = new MenuOption[9];
            Settings[0] = new MenuOption { DisplayName = "Colour Settings", _type = Plugin.submenuthingy, AssociatedString = "ColourSettings" };
            Settings[1] = new MenuOption { DisplayName = "MenuPosition", _type = Plugin.sliderthingy, StringArray = new string[] { "Top Left", "Middle", "Top Right" } };
            Settings[2] = new MenuOption { DisplayName = "Config", _type = Plugin.sliderthingy, StringArray = new string[0] };
            Settings[3] = new MenuOption { DisplayName = "Load Config", _type = Plugin.buttonthingy, AssociatedString = "loadconfig" };
            Settings[4] = new MenuOption { DisplayName = "Save Config", _type = Plugin.buttonthingy, AssociatedString = "saveconfig" };
            Settings[5] = new MenuOption { DisplayName = "Player Logging", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.PlayerLogging };
            Settings[6] = new MenuOption { DisplayName = "Inverted Controls", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.invertedControls };
            //Settings[7] = new MenuOption { DisplayName = "Legacy UI", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.legacyui };
            Settings[7] = new MenuOption { DisplayName = "Menu Font", _type = Plugin.sliderthingy, StringArray = new string[] { "Gtag Font", "Arial" } };
            Settings[8] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            MusicPlayer = new MenuOption[7];
            MusicPlayer[0] = new MenuOption { DisplayName = "Music", _type = Plugin.sliderthingy, StringArray = new string[0] };
            MusicPlayer[1] = new MenuOption { DisplayName = "Play Music", _type = Plugin.buttonthingy, AssociatedString = "playmusic" };
            MusicPlayer[2] = new MenuOption { DisplayName = "Stop Music", _type = Plugin.buttonthingy, AssociatedString = "stopmusic" };
            MusicPlayer[3] = new MenuOption { DisplayName = "Loop Music", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.loopmusic };
            MusicPlayer[4] = new MenuOption { DisplayName = "Sound Board", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.soundboard };
            MusicPlayer[5] = new MenuOption { DisplayName = "Volume", _type = Plugin.sliderthingy, StringArray = new string[] { "100%", "90%", "80%", "70%", "60%", "50%", "40%", "30%", "20%", "10%" } };
            MusicPlayer[6] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            ColourSettings = new MenuOption[8];
            ColourSettings[0] = new MenuOption { DisplayName = "MenuColour", _type = Plugin.sliderthingy, StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue" } };
            ColourSettings[1] = new MenuOption { DisplayName = "Ghost Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue" } };
            ColourSettings[2] = new MenuOption { DisplayName = "Beam Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue" } };
            ColourSettings[3] = new MenuOption { DisplayName = "ESP Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue" } };
            ColourSettings[4] = new MenuOption { DisplayName = "Ghost Opacity", _type = Plugin.sliderthingy, StringArray = new string[] { "100%", "80%", "60%", "30%", "20%", "0%" } };
            ColourSettings[5] = new MenuOption { DisplayName = "HitBoxes Opacity", _type = Plugin.sliderthingy, StringArray = new string[] { "100%", "80%", "60%", "30%", "20%", "0%" } };
            ColourSettings[6] = new MenuOption { DisplayName = "HitBoxes Colour", _type = Plugin.sliderthingy, StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue" } };
            ColourSettings[7] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Info = new MenuOption[4];
            Info[0] = new MenuOption { DisplayName = "PlayerList", _type = Plugin.buttonthingy };
            Info[1] = new MenuOption { DisplayName = "Battery", _type = Plugin.buttonthingy };
            Info[2] = new MenuOption { DisplayName = "QCMV3 Menu Users", _type = Plugin.buttonthingy };
            Info[3] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            Menu.Macro = new MenuOption[9];
            Menu.Macro[0] = new MenuOption { DisplayName = "Macro", _type = Plugin.sliderthingy, StringArray = new string[0] };
            Menu.Macro[1] = new MenuOption { DisplayName = "Load Macro", _type = Plugin.buttonthingy, AssociatedString = "loadmacro" };
            Menu.Macro[2] = new MenuOption { DisplayName = "Stop Macro", _type = Plugin.buttonthingy, AssociatedString = "stopmacro" };
            Menu.Macro[3] = new MenuOption { DisplayName = "Record Macro", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.recordmacro };
            Menu.Macro[4] = new MenuOption { DisplayName = "Delete Macro", _type = Plugin.buttonthingy, AssociatedString = "deletemacro" };
            Menu.Macro[5] = new MenuOption { DisplayName = "Auto Play Proximity", _type = Plugin.togglethingy, AssociatedBool = PluginConfig.autoplayproximity };
            Menu.Macro[6] = new MenuOption { DisplayName = "Auto Play Distance", _type = Plugin.sliderthingy, StringArray = new string[] { "Really Close", "Close", "Legit", "Semi Legit", "Semi Blatant", "Blatant", "Rage" } };
            Menu.Macro[7] = new MenuOption { DisplayName = "Macro Lerp Speed", _type = Plugin.sliderthingy, StringArray = new string[] { "0.1", "0.2", "0.3", "0.4", "0.5", "0.6" } };
            Menu.Macro[8] = new MenuOption { DisplayName = "Back", _type = Plugin.submenuthingy, AssociatedString = Plugin.backthingy };

            return MainMenu;
        }
    }
}