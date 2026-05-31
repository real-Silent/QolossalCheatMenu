using UnityEngine;

namespace Qolossal.Mods
{
    public class FreezeMonkey : MonoBehaviour
    {
        public static void Update()
        {
            /*if (PluginConfig.freezemonkey)
            {
                if (Controls.LeftGrip())
                {
                    if(GorillaTagger.Instance.myVRRig.enabled)
                        GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.transform.rotation;
                }
                else
                {
                    if (!GorillaTagger.Instance.myVRRig.enabled)
                        GorillaTagger.Instance.myVRRig.enabled = true;
                }
            }
            else
            {
                Destroy(GorillaTagger.Instance.GetComponent<FreezeMonkey>());
            }*/
        }
    }
}