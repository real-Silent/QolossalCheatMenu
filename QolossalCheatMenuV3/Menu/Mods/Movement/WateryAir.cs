using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class WateryAir : MonoBehaviour
    {
        public WateryAir(IntPtr e) : base(e) { }
        public static void Update()
        {
            if (PluginConfig.wateryair)
            {
                string bind = CustomBinding.GetBinds("wateryair");
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }
                if (GorillaTagger.Instance == null)
                    return;
                string leftBind = CustomBinding.MirrorBind(bind, true);
                string rightBind = CustomBinding.MirrorBind(bind, false);
                if (ControlsV2.GetControl(leftBind) && ControlsV2.GetControl(rightBind))
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = false;
                else
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<WateryAir>());
                if (GorillaTagger.Instance == null)
                    return;
                if (GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity)
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
        }
    }
}