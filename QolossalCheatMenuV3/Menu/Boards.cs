using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Qolossal.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    internal class Boards : MonoBehaviour
    {
        public Boards(IntPtr e) : base(e) { }

        private static List<GameObject> objtochange = new List<GameObject>();
        private static GameObject cocktext = GameObject.Find("COC Text");
        private static GameObject motdtext = GameObject.Find("motdtext");
        private static GameObject cock = GameObject.Find("CodeOfConduct");

        public static Material boardmat;
        public static Material defaultboardmat;

        public static bool tempbool = false;

        public static string coctext;
        public static string defaultcoctext;
        public static string defaultmotdtext;
        public static void ChangeMaterialsRecursively(GameObject parent, Color color)
        {
            if (parent == null)
                return;
            Text[] texts = parent.GetComponentsInChildren<Text>(true);
            foreach (Text txt in texts)
            {
                if (txt != null)
                    txt.color = color;
            }
        }
        public static void DoBoardThingy(Material mat, Color color)
        {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                foreach (GameObject children in obj.GetComponentsInChildren<GameObject>())
                {
                    if (children.name == "Currency Board")
                        objtochange.Add(obj);
                }
                if (obj.name.ToLower().Contains("monitor") || obj.name.ToLower() == "board" || obj.name.ToLower() == "stand" || obj.name.ToLower().Contains("game modes") || obj.name.ToLower().Contains("current mode") || obj.name.ToLower().Contains("wallmonitor"))
                    objtochange.Add(obj);
            }
            if (objtochange.Count > 0 && objtochange != null)
            {
                foreach (GameObject obj in objtochange)
                {
                    if (obj.GetComponent<Renderer>() != null && obj.GetComponent<Renderer>().material != mat)
                        obj.GetComponent<Renderer>().material = mat;
                }
            }
            else
                CustomConsole.LogToConsole("[QOLOSSAL] WallMonitors is less than 0 or null");
            GameObject parentObject = GameObject.Find("lower level/UI");
            GameObject[] textObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject meow in textObjects)
            {
                if (meow.name.Contains("Currency Board Text") || meow.name.Contains("Daily Rocks Text"))
                    ChangeMaterialsRecursively(meow, color);
            }
            if (parentObject != null)
                ChangeMaterialsRecursively(parentObject, color);
        }

        public virtual void Start()
        {
            // Security stuff dont touch it
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

            if (GameObject.Find("COC Text") != null && GameObject.Find("motdtext") != null && GameObject.Find("CodeOfConduct") != null)
            {
                cocktext = GameObject.Find("COC Text");
                motdtext = GameObject.Find("motdtext");
                cock = GameObject.Find("CodeOfConduct");
                Boards.defaultboardmat = new Material(Shader.Find("Standard"));
                Boards.defaultcoctext = GameObject.Find("COC Text").GetComponent<Text>().text;
                Boards.defaultmotdtext = GameObject.Find("motdtext").GetComponent<Text>().text;
                Plugin.QG(); // pushes the initing to work
            }
            Boards.boardmat = new Material(Shader.Find("Standard"));
            Boards.boardmat.color = new Color(0.6f, 0f, 0.8f);
            //DoBoardThingy(boardmat, Color.cyan);
        }
        public virtual void Update()
        {
            if (PluginConfig.showboards)
            {
                if (!tempbool)
                {
                    //DoBoardThingy(boardmat, Color.cyan);
                    if (cock != null)
                        cock.GetComponent<Text>().text = "QOLOSSAL CHEAT MENU V3";
                    if (motdtext != null)
                    {
                        if (motdtext.GetComponent<Text>().text != Plugin.motd)
                            motdtext.GetComponent<Text>().text = Plugin.motd;
                    }
                    tempbool = true;
                }
            }
            else
            {
                if (tempbool)
                {
                    //DoBoardThingy(defaultboardmat, Color.white);
                    if (cocktext != null)
                        cocktext.GetComponent<Text>().text = defaultcoctext;
                    if (motdtext != null)
                        motdtext.GetComponent<Text>().text = defaultmotdtext;
                    if (cocktext != null)
                        cock.GetComponent<Text>().text = "GORILLA CODE OF CONDUCT";
                    tempbool = false;
                }
            }
            if (Plugin.lastUserCount != Plugin.usercount)
            {
                Plugin.lastUserCount = Plugin.usercount;
                if (cocktext != null)
                    cocktext.GetComponent<Text>().text = $"Thank you for using QCMV3, the successor to the first cheat menu!\n\nContributors:\nNova: Menu Maker/Porter\nColossusYTTV: Menu Maker/Mod Creator\nLars/LHAX: Menu Base\nStarry: Dev/Tester\nMios: Tester/Manager/Pain In The Ass\nWM/Will: No Fingers/Full Bright\nAntic/ChatGPT: Tester\n\nMenu Version: {Plugin.version}, Server Version: {Plugin.serverversion}\nQCMV3 Users Online: {Plugin.usercount}".ToUpper();
            }
        }
    }
}
