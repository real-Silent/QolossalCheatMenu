using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    public class LongArm : MonoBehaviour
    {
        public LongArm(IntPtr e) : base(e) { }

        static float armlenght = 1;
        public virtual void Update()
        {
            if (PluginConfig.longarms)
            {
                if (GorillaTagger.Instance == null)
                    return;

                if (Controls.LeftTrigger() && Controls.RightJoystick())
                {
                    armlenght -= 0.01f;
                    GorillaTagger.Instance.transform.localScale = new Vector3(armlenght, armlenght, armlenght);
                }
                if (Controls.RightTrigger() && Controls.RightJoystick())
                {
                    armlenght += 0.01f;
                    GorillaTagger.Instance.transform.localScale = new Vector3(armlenght, armlenght, armlenght);
                }
                if (Controls.RightTrigger() && Controls.LeftTrigger() && Controls.RightJoystick())
                {
                    GorillaTagger.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
                    return;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<LongArm>());
                GorillaTagger.Instance.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}