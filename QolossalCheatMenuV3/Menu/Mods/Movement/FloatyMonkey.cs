using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class FloatyMonkey : MonoBehaviour
    {
        public FloatyMonkey(IntPtr e) : base(e) { }
        static float[] floatyLevels = new float[]
        {
            0f, 1.1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f, 2.2f, 2.4f, 2.6f, 2.8f, 3f, 3.2f, 3.4f, 3.6f, 3.8f, 4f, -Physics.gravity.y
        };

        static float ammount;
        public virtual void Update()
        {
            int floatyIndex = PluginConfig.FloatyMonkey;
            if (floatyIndex == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<FloatyMonkey>());
                return;
            }
            if (floatyIndex >= 1 && floatyIndex <= 17)
            {
                ammount = floatyLevels[floatyIndex];
            }
            if (GorillaTagger.Instance == null)
                return;
            string bind = CustomBinding.GetBinds("floatymonkey");
            if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
            {
                return;
            }
            if (ControlsV2.GetControl(bind))
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * ammount, ForceMode.Acceleration);
            }
        }
    }
}