using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class RainbowMonkey : MonoBehaviour
    {
        public RainbowMonkey(IntPtr e) : base(e) { }

        float speed = 1.0f;
        float time;
        public virtual void Update()
        {
            if (PluginConfig.rainbowmonkey)
            {
                time += Time.deltaTime * speed;
                float r = Mathf.Sin(time) * 0.5f + 0.5f;
                float g = Mathf.Sin(time + 2f) * 0.5f + 0.5f;
                float b = Mathf.Sin(time + 4f) * 0.5f + 0.5f;
                Plugin.RigRPC("InitializeNoobMaterial", RpcTarget.All, new object[] { r, g, b });
                Plugin.RigRPC("InitializeNoobMaterial", RpcTarget.All, new object[] { r, g, b, true });
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<RainbowMonkey>());
            }
        }
    }
}