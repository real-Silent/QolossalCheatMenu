using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Throw : MonoBehaviour
    {
        public Throw(IntPtr e) : base(e) { }
        LocalGorillaVelocityTracker right;
        LocalGorillaVelocityTracker left;
        public virtual void Awake()
        {
            right = GorillaTagger.Instance.leftHandTransform.gameObject.AddComponent<LocalGorillaVelocityTracker>();
            left = GorillaTagger.Instance.rightHandTransform.gameObject.AddComponent<LocalGorillaVelocityTracker>();
        }
        public virtual void Update()
        {
            if (PluginConfig.Throw)
            {
                string bind = CustomBinding.GetBinds("throw");
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }
                string leftBind = CustomBinding.MirrorBind(bind, true);
                string rightBind = CustomBinding.MirrorBind(bind, false);
                if (ControlsV2.GetControl(rightBind))
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity -= right.GetVelocity() / 8;
                }
                if (ControlsV2.GetControl(leftBind))
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity -= left.GetVelocity() / 8;
                }
            }
            else
            {
                Destroy(this.GetComponent<Throw>());
            }
        }
    }
    public class LocalGorillaVelocityTracker : MonoBehaviour
    {
        public LocalGorillaVelocityTracker(IntPtr e) : base(e) { }
        private Vector3 previousLocalPosition;
        private Vector3 velocity;
        public virtual void Start()
        {
            previousLocalPosition = transform.localPosition;
        }
        public virtual void Update()
        {
            if (PluginConfig.Throw)
            {
                Vector3 localDisplacement = transform.localPosition - previousLocalPosition;
                Vector3 localVelocity = localDisplacement / Time.deltaTime;

                velocity = transform.parent.TransformDirection(localVelocity);

                previousLocalPosition = transform.localPosition;
            }
        }
        public Vector3 GetVelocity()
        {
            return velocity;
        }
    }
}