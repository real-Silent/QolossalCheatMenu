using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class CreeperMonkey : MonoBehaviour
    {
        public CreeperMonkey(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.creepermonkey)
            {
                if (Controls.LeftTrigger())
                {
                    if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                        return;

                    float num = float.PositiveInfinity;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            float sqrMagnitude = (vrrig.transform.position - GorillaTagger.Instance.transform.position).sqrMagnitude;
                            if (sqrMagnitude < num)
                            {
                                num = sqrMagnitude;
                                GorillaTagger.Instance.myVRRig.headConstraint.LookAt(vrrig.headMesh.transform);
                                GorillaTagger.Instance.rightHandTransform.position = vrrig.headMesh.transform.position;
                            }
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<CreeperMonkey>());
            }
        }
    }
}