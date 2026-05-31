using HarmonyLib;
using Qolossal.Menu;
using System.Collections.Generic;

namespace Qolossal.Notifacation
{
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    internal class ReportNotifacation
    {
        private static List<string> notifiedPlayers = new List<string>();

        [HarmonyPrefix]
        private static void Postfix(string susReason, string susId, string susNick)
        {
            if (!notifiedPlayers.Contains(susId) && PluginConfig.Notifications && !susReason.Contains("PlayHandTap"))
            {
                notifiedPlayers.Add(susId);
                Notifacations.SendNotification($"<color=yellow>[ANTICHEAT]</color> Name: {susNick}");
                notifiedPlayers.Remove(susId);
            }
        }
    }
}