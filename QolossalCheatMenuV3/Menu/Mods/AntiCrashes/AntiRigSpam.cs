using HarmonyLib;
using Photon.Pun;
using Qolossal.Menu;

namespace Qolossal.Mods
{
    [HarmonyPatch(typeof(PhotonNetwork), "OnEvent")]
    internal class Instantate
    {
        private static bool Prefix(ExitGames.Client.Photon.EventData photonEvent)
        {
            if (PhotonNetwork.InRoom && photonEvent.Code == 202)
            {
                if (PluginConfig.anticrash)
                {
                    Plugin.called += 1;

                    if (Plugin.called >= 15)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}