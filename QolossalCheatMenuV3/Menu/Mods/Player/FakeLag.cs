using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class FakeLag : MonoBehaviour
    {
        public FakeLag(IntPtr e) : base(e) { }
        static float lagTimer = 0f;
        static float lagInterval = 0f;
        static bool isLagging = false;

        public virtual void Update()
        {
            if (PluginConfig.fakelag)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;
                if (!PhotonNetwork.InRoom)
                    return;
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                {
                    return;
                }
                lagTimer += Time.deltaTime;
                if (lagTimer >= lagInterval)
                {
                    isLagging = !isLagging;
                    lagTimer = 0f;
                    lagInterval = UnityEngine.Random.Range(0.2f, 1f);

                    if (isLagging)
                    {
                        if (GorillaTagger.Instance.myVRRig.enabled)
                            GorillaTagger.Instance.myVRRig.enabled = false;
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.myVRRig.enabled)
                            GorillaTagger.Instance.myVRRig.enabled = true;
                    }
                }
            }
            else 
            {
                GameObject.Destroy(Plugin.holder.GetComponent<FakeLag>());
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.myVRRig != null)
                {
                    if (!GorillaTagger.Instance.myVRRig.enabled && !PluginConfig.SpinBot && !PluginConfig.desync && !PluginConfig.tagall && !PluginConfig.taggun && !PluginConfig.ghostmonkey && !PluginConfig.invismonkey)
                        GorillaTagger.Instance.myVRRig.enabled = true;
                }
                return;
            }
        }
    }
}