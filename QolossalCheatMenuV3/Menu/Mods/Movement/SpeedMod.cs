using Photon.Pun;
using Qolossal;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class SpeedMod : MonoBehaviour
    {
        public SpeedMod(IntPtr e) : base(e) { }
        float[] speeds = { 7f, 7.2f, 7.4f, 7.6f, 7.8f, 8f, 8.2f, 8.4f, 8.6f };

        public virtual void Update()
        {
            bool speedApplied = false;
            if (PluginConfig.speed > 0)
            {
                SetJumpSpeed(speeds[PluginConfig.speed]);
                speedApplied = true;
            }

            string speedBind = CustomBinding.GetBinds("speedbind");
            if (string.IsNullOrEmpty(speedBind) || speedBind == "UNBOUND") return;
            if (ControlsV2.GetControl(speedBind))
            {
                SetJumpSpeed(speeds[PluginConfig.speed]);
                speedApplied = true;
            }
            if (PluginConfig.nearspeed > 0)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isOfflineVRRig && !WhatAmI.IsInfected(PhotonNetwork.LocalPlayer))
                    {
                        if (WhatAmI.IsInfected(vrrig.photonView.Owner))
                        {
                            if (PluginConfig.nearspeeddistance <= Vector3.Distance(GorillaTagger.Instance.transform.position, vrrig.transform.position))
                            {
                                SetJumpSpeed(speeds[PluginConfig.nearspeed]);
                                speedApplied = true;
                            }
                        }
                    }
                }
            }
            if (!speedApplied)
            {
                ResetJumpSpeed();
            }
        }

        void SetJumpSpeed(float speed)
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = speed;
        }

        void ResetJumpSpeed()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = WhatAmI.GetDefaultSpeeds();
        }
    }
}