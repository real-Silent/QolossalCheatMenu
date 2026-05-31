using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Qolossal.Menu;

namespace Qolossal.Notifacation
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnMasterClientSwitched")]
    internal class MasterChangeNotifacation
    {
        private static string lastMasterUserId = string.Empty;

        [HarmonyPostfix]
        private static void Postfix(Player newMasterClient)
        {
            if (newMasterClient == null || string.IsNullOrEmpty(newMasterClient.UserId)) return;

            if (newMasterClient.UserId != lastMasterUserId && PluginConfig.Notifications)
            {
                lastMasterUserId = newMasterClient.UserId;
                Notifacations.SendNotification($"<color=green>[MASTER]</color> Changed, Name: {newMasterClient.NickName}");
            }
        }
    }
}