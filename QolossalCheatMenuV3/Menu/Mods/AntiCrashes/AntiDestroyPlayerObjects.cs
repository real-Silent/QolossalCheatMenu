using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class AntiDestroyPlayerObjects : MonoBehaviour
    {
        public AntiDestroyPlayerObjects(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PluginConfig.anticrash)
                {
                    if (PhotonNetwork.LocalPlayer == null || GorillaTagger.Instance.myVRRig.photonView == null || GorillaTagger.Instance.myVRRig == null)
                    {
                        PhotonNetwork.RegisterPhotonView(GorillaTagger.Instance.myVRRig.photonView);
                    }
                }
            }
        }
    }
}