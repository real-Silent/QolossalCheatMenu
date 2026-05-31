using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Decapitation : MonoBehaviour
    {
        public Decapitation(IntPtr e) : base(e) { }
        public static float yRotation;
        public virtual void Update()
        {
            if (PluginConfig.decapitation)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;

                if (AreHandsDown())
                {
                    float targetYRotation = CalculateTorsoYRotation();
                    yRotation = Mathf.LerpAngle(yRotation, targetYRotation, .8f);
                }
                else
                {
                    yRotation = GorillaTagger.Instance.mainCamera.transform.eulerAngles.y;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Decapitation>());
            }
        }
        private static bool AreHandsDown()
        {
            return GorillaTagger.Instance.leftHandTransform.position.y < GorillaTagger.Instance.mainCamera.transform.position.y && GorillaTagger.Instance.rightHandTransform.position.y < GorillaTagger.Instance.mainCamera.transform.position.y;
        }
        private static float CalculateTorsoYRotation()
        {
            Vector3 headForward = GorillaTagger.Instance.mainCamera.transform.forward;
            headForward.y = 0;
            headForward.Normalize();
            Vector3 handCenter = (GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.rightHandTransform.position) / 2f;
            Vector3 handDirection = handCenter - GorillaTagger.Instance.mainCamera.transform.position;
            handDirection.y = 0;
            handDirection.Normalize();
            Vector3 torsoDirection = Vector3.Lerp(headForward, handDirection, 0.45f);
            torsoDirection.Normalize();
            if (Vector3.Dot(torsoDirection, headForward) < 0)
                torsoDirection = headForward;
            return Quaternion.LookRotation(torsoDirection, Vector3.up).eulerAngles.y;
        }
    }
}