using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NoClip : MonoBehaviour
    {
        public NoClip(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.NoClip)
            {
                string bind = CustomBinding.GetBinds("noclip");
                if (GorillaTagger.Instance == null)
                    return;
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }
                if (ControlsV2.GetControl(bind))
                {
                    foreach (MeshCollider c in GameObject.FindObjectsOfType<MeshCollider>())
                        c.enabled = false;
                }
                else
                {
                    foreach (MeshCollider c in GameObject.FindObjectsOfType<MeshCollider>())
                        c.enabled = true;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<NoClip>());
                foreach (MeshCollider c in GameObject.FindObjectsOfType<MeshCollider>())
                    c.enabled = true;
            }
        }
    }
}