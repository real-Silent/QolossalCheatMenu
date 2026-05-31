using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace Colossal.Mods
{
    public class FreezeMonkey : MonoBehaviour
    {
        /*public void Update()
        {
            if (Plugin.freezemonkey)
            {
                bool freeze;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out freeze);
                if (freeze)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            else
            {
                Destroy(GorillaTagger.Instance.GetComponent<FreezeMonkey>());
            }
        }*/
    }
}
