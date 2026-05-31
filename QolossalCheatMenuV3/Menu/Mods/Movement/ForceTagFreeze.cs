using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ForceTagFreeze : MonoBehaviour
    {
        public ForceTagFreeze(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (GorillaTagger.Instance == null)
                return;
            if (PluginConfig.forcetagfreeze)
            {
                GorillaLocomotion.Player.Instance.disableMovement = true;
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<ForceTagFreeze>());
                GorillaLocomotion.Player.Instance.disableMovement = false;
            }
        }
    }
}