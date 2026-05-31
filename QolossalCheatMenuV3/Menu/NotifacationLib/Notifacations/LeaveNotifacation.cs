using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Qolossal.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Notifacation
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    internal class LeaveNotifacation
    {
        private static Dictionary<string, float> recentlyLeft = new Dictionary<string, float>();
        private const float cooldownTime = 5f; // seconds

        [HarmonyPostfix]
        private static void Postfix(Player otherPlayer)
        {
            if (otherPlayer == null || string.IsNullOrEmpty(otherPlayer.UserId)) return;

            float currentTime = Time.realtimeSinceStartup;
            if (recentlyLeft.TryGetValue(otherPlayer.UserId, out float lastTime))
            {
                if (currentTime - lastTime < cooldownTime)
                    return;
            }
            recentlyLeft[otherPlayer.UserId] = currentTime;
            JoinNotifacation.ClearNotifiedUser(otherPlayer.UserId);
            if (PluginConfig.Notifications)
            {
                Notifacations.SendNotification($"<color=cyan>[LEAVE]</color> Name: {otherPlayer.NickName}");
            }
        }
    }
}