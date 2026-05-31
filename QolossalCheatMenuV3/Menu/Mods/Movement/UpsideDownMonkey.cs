using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UpsideDownMonkey : MonoBehaviour
    {
        public UpsideDownMonkey(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (GorillaTagger.Instance == null)
                return;

            if (PluginConfig.upsidedownmonkey)
            {
                GorillaTagger.Instance.transform.rotation = (Quaternion.Euler(0f, 0f, 180f));
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * ((-Physics.gravity.y * 2) / Time.deltaTime)), ForceMode.Acceleration); // think this is more accurate
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<UpsideDownMonkey>());
                GorillaTagger.Instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}