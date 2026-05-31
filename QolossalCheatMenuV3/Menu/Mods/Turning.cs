using easyInputs;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Turning : MonoBehaviour
    {
        public Turning(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.Turning)
            {
                if (GorillaTagger.Instance == null)
                    return;

                if (EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand).x >= 0.6f)
                {
                    GorillaLocomotion.Player.Instance.Turn(6f);
                }
                if (EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand).x <= -0.6f)
                {
                    GorillaLocomotion.Player.Instance.Turn(-6f);
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Turning>());
            }
        }
    }
}