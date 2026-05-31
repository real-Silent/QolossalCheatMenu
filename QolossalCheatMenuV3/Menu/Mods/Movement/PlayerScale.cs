using UnityEngine;

namespace Qolossal.Mods
{
    public class PlayerScale : MonoBehaviour
    {
        public static float scale = 1f;
        public void Update()
        {
            /*if (PluginConfig.PlayerScale)
            {
                if (Controls.LeftTrigger() && Controls.RightJoystick())
                {
                    scale -= 0.01f;
                    GorillaTagger.Instance.offlineVRRig.NativeScale = scale;
                }
                if (Controls.RightTrigger() && Controls.RightJoystick())
                {
                    scale += 0.01f;
                    GorillaTagger.Instance.offlineVRRig.NativeScale = scale;
                }
                if (Controls.RightTrigger() && Controls.LeftTrigger() && Controls.RightJoystick())
                {
                    GorillaTagger.Instance.offlineVRRig.NativeScale = 1f;
                    return;
                }


                if (GorillaTagger.Instance.offlineVRRig.NativeScale != scale)
                    GorillaTagger.Instance.offlineVRRig.NativeScale = scale;

                // stole this from longarms!!!!!!!!
            }
            else
            {
                Destroy(holder.GetComponent<PlayerScale>());
                GorillaTagger.Instance.offlineVRRig.NativeScale = 1f;
            }*/
        }
    }
}