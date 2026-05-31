using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class SpinBot : MonoBehaviour
    {
        public SpinBot(IntPtr e) : base(e) { }
        static GameObject ghost;
        public virtual void Update()
        {
            if (PluginConfig.SpinBot)
            {
                if (PhotonNetwork.InRoom)
                {
                    if (ghost == null)
                        ghost = GhostManager.SpawnGhost();
                    if (ghost != null)
                    {
                        var rig = GorillaTagger.Instance.myVRRig;
                        rig.enabled = false;
                        rig.transform.position = ghost.transform.position;
                        rig.transform.Rotate(Vector3.up * 720f * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (PhotonNetwork.InRoom)
                {
                    GhostManager.DestroyGhost(ghost);
                    GorillaTagger.Instance.myVRRig.enabled = true;
                }
                GameObject.Destroy(Plugin.holder.GetComponent<SpinBot>());
            }
        }
    }
}