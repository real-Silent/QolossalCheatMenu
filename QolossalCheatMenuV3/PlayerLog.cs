using Qolossal.Menu;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Qolossal
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    internal class PlayerLog : MonoBehaviour
    {
        public PlayerLog(IntPtr e) : base(e) { }
        private static List<string> cachedLogs = new List<string>();
        private static string logFilePath;
        public virtual void Awake()
        {
            logFilePath = Path.Combine(Configs.logPath, "PlayerLog.txt");

            if (!Directory.Exists(Configs.logPath))
                Directory.CreateDirectory(Configs.logPath);
        }
        public virtual void Update()
        {
            if (PluginConfig.PlayerLogging)
            {
                if (!PhotonNetwork.InRoom)
                    return;
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig == null || vrrig.photonView == null || vrrig.photonView.Owner == null)
                        continue;
                    var owner = vrrig.photonView.Owner;
                    string info = $"{DateTime.Now},{owner.NickName},{owner.UserId},{vrrig.concatStringOfCosmeticsAllowed}";
                    bool found = false;
                    for (int i = 0; i < cachedLogs.Count; i++)
                    {
                        if (cachedLogs[i].Contains(owner.UserId))
                        {
                            cachedLogs[i] = info;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        cachedLogs.Add(info);
                }
                if (cachedLogs.Count > 0)
                    UpdateLogFile();
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<PlayerLog>());
            }
        }
        static void UpdateLogFile()
        {
            string[] lines = File.Exists(logFilePath) ? File.ReadAllLines(logFilePath) : new string[0];
            foreach (string log in cachedLogs)
            {
                string userId = log.Split(',')[2];
                bool updated = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(userId))
                    {
                        lines[i] = log;
                        updated = true;
                        break;
                    }
                }
                if (!updated)
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[lines.Length - 1] = log;
                }
            }
            File.WriteAllLines(logFilePath, lines);
            cachedLogs.Clear();
        }
        public virtual void OnApplicationQuit()
        {
            if (cachedLogs.Count > 0)
                UpdateLogFile();
        }
    }
}