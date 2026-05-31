using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Qolossal.Menu;
using System.Collections.Generic;

namespace Qolossal.Notifacation
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    internal class JoinNotifacation
    {
        private static HashSet<string> notifiedPlayerIds = new HashSet<string>();

        [HarmonyPrefix]
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer == null || string.IsNullOrEmpty(newPlayer.UserId)) return;

            if (!notifiedPlayerIds.Contains(newPlayer.UserId) && PluginConfig.Notifications)
            {
                notifiedPlayerIds.Add(newPlayer.UserId);
                Notifacations.SendNotification($"<color=cyan>[JOIN]</color> Name: {newPlayer.NickName}");
            }
        }
        public static void ClearNotifiedUser(string userId)
        {
            notifiedPlayerIds.Remove(userId);
        }
    }
}