using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class TFly : MonoBehaviour
    {
        public TFly(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.tfly)
            {
                string bind = CustomBinding.GetBinds("tfly");
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }

                if (GorillaTagger.Instance == null)
                    return;
                if (ControlsV2.LeftSecondaryButton())
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = new Vector3(0f, 0.01f, 0f);
                }
                if (ControlsV2.GetControl(bind))
                {
                    GorillaTagger.Instance.transform.position += GorillaTagger.Instance.rightHandTransform.forward * 0.45f;
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                    return;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<TFly>());
            }
        }
    }
}