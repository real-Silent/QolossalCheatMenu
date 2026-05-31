using Photon.Pun;
using Qolossal.Menu;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NameChanger : MonoBehaviour
    {
        public NameChanger(IntPtr e) : base(e) { }
        private static string filepath = "Qolossal/NameChanger.txt";
        private static int currentLineIndex;
        private static string[] lines;
        private static bool coroutineRunning = false;
        private static Coroutine currentCoroutine;

        public virtual void Update()
        {
            if (PluginConfig.namechanger && !coroutineRunning)
            {
                if (!Directory.Exists("Qolossal"))
                    Directory.CreateDirectory("Qolossal");
                if (!File.Exists(filepath))
                    File.WriteAllText(filepath, "Qolossal\nOn\nTop");
                lines = File.ReadAllLines(filepath);
                if (lines.Length > 0)
                {
                    currentCoroutine = (Coroutine)MelonLoader.MelonCoroutines.Start(ProcessLinesWithDelay());
                    coroutineRunning = true;
                }
            }
            else
            {
                if (coroutineRunning)
                {
                    StopCoroutine();
                    return;
                }
            }
        }
        private static IEnumerator ProcessLinesWithDelay()
        {
            while (true)
            {
                if (lines.Length > 0)
                {
                    string text = lines[currentLineIndex];
                    PhotonNetwork.LocalPlayer.NickName = text;
                    currentLineIndex = (currentLineIndex + 1) % lines.Length;
                }
                yield return new WaitForSeconds(0.3f);
                if (!PluginConfig.namechanger)
                {
                    StopCoroutine();
                    yield break;
                }
            }
        }

        private static void StopCoroutine()
        {
            if (currentCoroutine != null)
            {
                MelonLoader.MelonCoroutines.Stop(currentCoroutine);
                currentCoroutine = null;
            }
            coroutineRunning = false;
        }
    }
}