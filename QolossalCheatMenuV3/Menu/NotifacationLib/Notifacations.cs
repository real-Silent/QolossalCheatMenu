using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Qolossal.Menu;

namespace Qolossal.Notifacation
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Notifacations : MonoBehaviour
    {
        public Notifacations(IntPtr e) : base(e) { }

        static int NotificationDecayTime = 150;
        static int NotificationDecayTimeCounter = 0;
        public static int NoticationThreshold = 5;
        static string[] Notifilines;
        static string newtext;
        public static string PreviousNotifi;

        public static GameObject NotiHub;
        public static Text NotiHubText;

        public static void SpawnNoti()
        {
            // Security stuff dont touch it
            Plugin.CheckIntegrity2(Plugin.DecodeString(Plugin.anti2));

            (NotiHub, NotiHubText) = GUICreator.CreateTextGUI("", "NotiHub", TextAnchor.UpperRight, new Vector3(0, 0.4f, 3.6f));
        }

        public virtual void FixedUpdate()
        {
            if (PluginConfig.Notifications && Menu.Menu.agreement)
            {
                if (Plugin.update)
                {
                    NotiHubText.text = "<color=red>UPDATE NEEDED</color>";
                    return;
                }
                if (Plugin.locked || Plugin.serverLocked)
                {
                    NotiHubText.text = "<color=red>QOLOSSAL HAS BEEN LOCKED</color>";
                    return;
                }
                if (!Plugin.hasvalidkey)
                {
                    NotiHubText.text = "<color=yellow>DONT TRY TO CRACK QOLOSSAL\nYOUR IP AND HWID\nHAS BEEN COLLECTED\nDUE TO SAFETY CONCERNS</color>";
                    return;
                }

                if (NotiHubText.text != null)
                {
                    NotificationDecayTimeCounter++;
                    if (NotificationDecayTimeCounter > NotificationDecayTime)
                    {
                        Notifilines = null;
                        newtext = "";
                        NotificationDecayTimeCounter = 0;
                        Notifilines = NotiHubText.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
                        foreach (string Line in Notifilines)
                        {
                            if (Line != "")
                                newtext = newtext + Line + "\n";
                        }
                        NotiHubText.text = newtext;
                    }
                }
                else
                {
                    if (NotificationDecayTimeCounter != null)
                        NotificationDecayTimeCounter = 0;
                }
            }
            else if (NotiHubText != null)
                NotiHubText.text = "";
        }

        public static void SendNotification(string NotificationText)
        {
            if (PluginConfig.Notifications)
            {
                if (!NotificationText.Contains(Environment.NewLine)) { NotificationText = NotificationText + Environment.NewLine; }
                NotiHubText.text = NotiHubText.text + NotificationText;
                PreviousNotifi = NotificationText;
            }
        }
        public static void ClearPastNotifications(int amount)
        {
            string[] Notifilines = null;
            string newtext = "";
            Notifilines = NotiHubText.text.Split(Environment.NewLine.ToCharArray()).Skip(amount).ToArray();
            foreach (string Line in Notifilines)
            {
                if (Line != "")
                    newtext = newtext + Line + "\n";
            }
            NotiHubText.text = newtext;
        }
    }
}