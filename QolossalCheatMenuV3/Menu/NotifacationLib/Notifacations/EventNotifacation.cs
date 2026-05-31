using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Qolossal.Menu;

namespace Qolossal.Notifacation
{
    [HarmonyPatch(typeof(PhotonNetwork), "RaiseEvent")]
    internal class EventNotifacation
    {
        [HarmonyPrefix]
        private static void Postfix(byte eventCode, object eventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
        {
            if (PluginConfig.Notifications)
            {
                Notifacations.SendNotification($"<color=yellow>[EVENT]</color> Code: {eventCode}");
            }
        }
    }
}