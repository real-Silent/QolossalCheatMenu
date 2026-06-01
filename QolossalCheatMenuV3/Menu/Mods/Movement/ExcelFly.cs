using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    // [MelonLoader.RegisterTypeInIl2Cpp]
    public class ExcelFly : MonoBehaviour
    {
        //public ExcelFly(IntPtr e) : base(e) { }
        private static readonly float[] speeds = { 0f, 8f, 6f, 4f, 2f, 1f };
        static float speed;
        public virtual void Update()
        {
            int flySetting = PluginConfig.excelfly;
            if (flySetting == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<ExcelFly>());
                return;
            }
            else
            {
                speed = speeds[Math.Min(flySetting, speeds.Length - 1)];
            }
            if (GorillaTagger.Instance == null)
                return;
            string bind = CustomBinding.GetBinds("excelfly");
            if (!string.IsNullOrEmpty(bind) && bind != "UNBOUND")
            {
                string leftBind = CustomBinding.MirrorBind(bind, true);
                string rightBind = CustomBinding.MirrorBind(bind, false);
                if (ControlsV2.GetControl(leftBind))
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += -GorillaTagger.Instance.leftHandTransform.right / speed;
                if (ControlsV2.GetControl(rightBind))
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.rightHandTransform.right / speed;
            }
        }
    }
}
