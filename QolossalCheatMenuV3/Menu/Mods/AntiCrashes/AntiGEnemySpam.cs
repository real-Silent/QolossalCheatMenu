using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class AntiGEnemySpam : MonoBehaviour
    {
        public AntiGEnemySpam(IntPtr e) : base(e) { }

        private float checkdelay;
        private readonly string[] types =
        {
            "gorillaenemy",
            "gorillafireball",
            "network player",
            "stickable target",
            "bulletprefab"
        };
        public virtual void Update()
        {
            if (!PhotonNetwork.InRoom || !PluginConfig.anticrash)
                return;
            if (Time.time < checkdelay)
                return;
            checkdelay = Time.time + 0.4f;
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objects)
            {
                if (obj == null)
                    continue;
                string objName = obj.name.ToLower();
                if (PluginConfig.anticrashtype == 5)
                {
                    foreach (string t in types)
                    {
                        if (objName.Contains(t))
                        {
                            Destroy(obj);
                            break;
                        }
                    }
                }
                else
                {
                    if (PluginConfig.anticrashtype >= 0 && PluginConfig.anticrashtype < types.Length)
                    {
                        if (objName.Contains(types[PluginConfig.anticrashtype]))
                        {
                            Destroy(obj);
                        }
                    }
                }
            }
        }
    }
}