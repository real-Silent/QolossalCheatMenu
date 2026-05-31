using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class WallWalk : MonoBehaviour
    {
        public WallWalk(IntPtr e) : base(e) { }
        static Vector3 normal2;
        static Vector3 vel1;
        static Vector3 vel2;
        static float dist2;
        static int layers;
        static bool LeftClose2;
        static bool DoOnce2;
        static float maxD2;

        private static readonly float[] wallWalkAmounts = { 0f, 6.8f, 7f, 7.5f, 7.8f, 8f, 8.5f, 8.8f, 9f, 9.5f, 9.8f };

        static float ammount;
        public virtual void Update()
        {
            int wallWalkSetting = PluginConfig.wallwalk;

            if (wallWalkSetting == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<WallWalk>());
                return;
            }
            ammount = wallWalkAmounts[Mathf.Min(wallWalkSetting, wallWalkAmounts.Length - 1)];
            if (GorillaTagger.Instance == null)
                return;
            string bind = CustomBinding.GetBinds("wallwalk");
            if (string.IsNullOrEmpty(bind) || bind == "UNBOUND" || !ControlsV2.GetControl(bind))
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
                return;
            }

            if (ControlsV2.GetControl(bind))
            {
                if (!DoOnce2)
                {
                    maxD2 = 1f;
                    layers = int.MaxValue;
                    DoOnce2 = true;
                }
                RaycastHit raycastHit;
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.right, out raycastHit, 1f, layers);
                RaycastHit raycastHit2;
                Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.right, out raycastHit2, 1f, layers);
                if (raycastHit2.distance > raycastHit.distance)
                {
                    normal2 = raycastHit.normal;
                    dist2 = raycastHit.distance;
                }
                else
                {
                    normal2 = raycastHit2.normal;
                    dist2 = raycastHit2.distance;
                    LeftClose2 = true;
                }
                if (dist2 < maxD2)
                {
                    vel2 = normal2 * (ammount * Time.deltaTime);
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity -= vel2;
                }
                else
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
                }
            }
            else
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
        }
    }
}