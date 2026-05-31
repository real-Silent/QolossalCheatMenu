using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Bees : MonoBehaviour
    {
        public Bees(IntPtr e) : base(e) { }

        public virtual void Update()
        {
            if (!PluginConfig.Bees)
            {
                Destroy(Plugin.holder.GetComponent<Bees>());
                if (PhotonNetwork.InRoom && GorillaTagger.Instance?.myVRRig != null)
                    GorillaTagger.Instance.myVRRig.enabled = true;
                return;
            }
            if (GorillaTagger.Instance?.myVRRig == null) return;
            if (!PhotonNetwork.InRoom) return;
            GorillaTagger.Instance.myVRRig.enabled = false;
            VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();
            if (rigs.Length == 0) return;
            VRRig target = null;
            foreach (var rig in rigs)
            {
                if (!rig.isMyPlayer && rig != GorillaTagger.Instance.myVRRig && rig.photonView.Owner != PhotonNetwork.LocalPlayer)
                {
                    target = rig;
                    break;
                }
            }
            if (target?.headMesh == null) return;
            Vector3 dir = (target.transform.position - transform.position).normalized;
            GorillaTagger.Instance.myVRRig.rightHandTransform.position += dir * 0.1f;
            GorillaTagger.Instance.myVRRig.leftHandTransform.position += dir * 0.1f;
            GorillaTagger.Instance.myVRRig.transform.position = Vector3.Lerp(GorillaTagger.Instance.myVRRig.transform.position, target.headMesh.transform.position, Time.deltaTime * 8f);
        }
    }
}