using easyInputs;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class JoystickFly : MonoBehaviour
    {
        public JoystickFly(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.JoystickFly)
            {
                if (GorillaTagger.Instance == null)
                    return;
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
                Rigidbody attachedRigidbody = GorillaTagger.Instance.bodyCollider.attachedRigidbody;
                Vector3 vector = new Vector3(EasyInputs.GetThumbStick2DAxis(0).x, EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand).y, EasyInputs.GetThumbStick2DAxis(0).y);
                Vector3 forward = GorillaTagger.Instance.bodyCollider.transform.forward;
                forward.y = 0f;
                Vector3 right = GorillaTagger.Instance.bodyCollider.transform.right;
                right.y = 0f;
                Vector3 vector2 = vector.x * right + EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand).y * Vector3.up + vector.z * forward;
                vector2 *= GorillaTagger.Instance.gameObject.transform.localScale.x * 15f;
                attachedRigidbody.velocity = Vector3.Lerp(attachedRigidbody.velocity, vector2, 0.12875f);
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<JoystickFly>());
            }
        }
    }
}