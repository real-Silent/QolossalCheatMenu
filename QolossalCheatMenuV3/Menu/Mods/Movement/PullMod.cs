using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class PullMod : MonoBehaviour
    {
        public PullMod(IntPtr e) : base(e) { }
        private static readonly float[] pullspeed = { 10f, 20f, 30f, 40f, 50f, 60f };
        static float PullModAmount;
        public virtual void Update()
        {
            string bind = CustomBinding.GetBinds("pullmod");
            if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
            {
                return;
            }
            int pullSetting = PluginConfig.pullmod;
            if (pullSetting == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<PullMod>());
                return;
            }
            if (GorillaTagger.Instance == null)
                return;
            PullModAmount = pullspeed[Math.Min(pullSetting, pullspeed.Length - 1)];
            if (pullSetting != 0)
            {
                if (ControlsV2.GetControl(bind))
                {
                    if (GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching)
                    {
                        Vector3 vector = GorillaTagger.Instance.GetComponent<Rigidbody>().velocity;
                        GorillaTagger.Instance.transform.position += new Vector3(vector.x / pullSetting, vector.y / pullSetting, vector.z / pullSetting);
                    }
                }
            }
        }
    }
}