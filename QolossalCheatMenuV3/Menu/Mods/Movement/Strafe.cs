using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Strafe : MonoBehaviour
    {
        public Strafe(IntPtr e) : base(e) { }
        static readonly float[] speeds = { 6f, 8f, 10f, 12f, 14f, 16f, 18f, 20f };
        static float moveSpeed = 10f;

        static readonly float[] jumps = { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f };
        static float jumpForce = 3f;

        static float circleRadius = 0.5f;
        static Rigidbody rb;
        static bool isGrounded;
        static VRRig lockedTarget = null;
        static VRRig lockedTeamTarget = null;
        static float fovAngle = 60f;
        static GameObject targetIndicator;
        static float initialDistanceToTarget = -1f;

        public virtual void Start()
        {
            if (GorillaTagger.Instance == null || GorillaTagger.Instance.bodyCollider == null)
            {
                return;
            }
            rb = GorillaTagger.Instance.bodyCollider.attachedRigidbody;
            if (rb == null)
            {
                return;
            }
            GorillaTagger.Instance.bodyCollider.material.bounciness = 0.3f;
            GorillaTagger.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Average;
            GorillaTagger.Instance.bodyCollider.material.dynamicFriction = 0.2f;
        }

        public virtual void Update()
        {
            if (GorillaTagger.Instance == null)
                return;
            if (PluginConfig.strafe == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Strafe>());
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.bodyCollider != null)
                {
                    GorillaTagger.Instance.bodyCollider.material.bounciness = 0f;
                    GorillaTagger.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Average;
                    GorillaTagger.Instance.bodyCollider.material.dynamicFriction = 0.6f;
                }
                return;
            }

            if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
            {
                return;
            }

            if (Physics.Raycast(GorillaTagger.Instance.bodyCollider.transform.position - new Vector3(0f, 0.2f, 0f), Vector3.down, out RaycastHit hit, 0.5f, GorillaLocomotion.Player.Instance.locomotionEnabledLayers))
            {
                isGrounded = hit.distance < 0.25f;
            }
            else
            {
                isGrounded = false;
            }

            moveSpeed = speeds[Mathf.Min(PluginConfig.strafespeed, speeds.Length - 1)];
            jumpForce = jumps[Mathf.Min(PluginConfig.strafejumpamount, jumps.Length - 1)];

            string bind = CustomBinding.GetBinds("strafe");
            bool isBindHeld = !string.IsNullOrEmpty(bind) && bind != "UNBOUND" && ControlsV2.GetControl(bind);

            Vector3 moveDirection = Vector3.zero;
            bool shouldStrafe = true;

            switch (PluginConfig.strafe)
            {
                case 1: // Look
                    if (GorillaTagger.Instance.headCollider != null && isBindHeld)
                    {
                        if (isGrounded)
                        {
                            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                        }
                        moveDirection = GorillaTagger.Instance.headCollider.transform.forward.normalized;
                        moveDirection.y = 0f;
                        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
                    }
                    break;
                case 2: // L Joystick (Camera-Relative)
                    if (GorillaTagger.Instance.headCollider != null)
                    {
                        if (isGrounded)
                        {
                            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                        }

                        Vector2 joystickInput = ControlsV2.LeftJoystickAxis();
                        if (joystickInput.magnitude >= 0.1f)
                        {
                            Vector3 forward = GorillaTagger.Instance.headCollider.transform.forward;
                            forward.y = 0f;
                            forward = forward.normalized;
                            Vector3 right = GorillaTagger.Instance.headCollider.transform.right;
                            right.y = 0f;
                            right = right.normalized;

                            moveDirection = (forward * joystickInput.y + right * joystickInput.x).normalized;
                            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
                        }
                        else
                            shouldStrafe = false;
                    }
                    break;
            }
        }

        static VRRig GetTargetInFOV(bool oppositeTeam)
        {
            if (GorillaParent.instance == null || GorillaParent.instance.vrrigs == null)
            {
                return null;
            }
            VRRig closestRig = null;
            float smallestAngle = float.MaxValue;
            float maxRange = 10f;
            Vector3 cameraForward = GorillaTagger.Instance.headCollider.transform.forward;
            bool localPlayerInfected = WhatAmI.IsInfected(PhotonNetwork.LocalPlayer);
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null || vrrig.photonView.Owner == null) continue;
                if (vrrig.isOfflineVRRig) continue;
                bool rigInfected = WhatAmI.IsInfected(vrrig.photonView.Owner);
                if (oppositeTeam)
                {
                    if (GorillaGameManager.instance != null && GorillaGameManager.instance is GorillaTagManager)
                    {
                        if (rigInfected == localPlayerInfected)
                            continue;
                    }
                }
                Vector3 directionToRig = (vrrig.transform.position - GorillaTagger.Instance.headCollider.transform.position).normalized;
                float angle = Vector3.Angle(cameraForward, directionToRig);
                float distance = Vector3.Distance(GorillaTagger.Instance.myVRRig.transform.position, vrrig.transform.position);
                if (angle <= fovAngle / 2f && distance <= maxRange && angle < smallestAngle)
                {
                    smallestAngle = angle;
                    closestRig = vrrig;
                }
            }
            return closestRig;
        }

        static Vector3 GetDirectionToLockedTarget(VRRig target, bool circle)
        {
            if (target == null)
                return Vector3.zero;
            Vector3 selfPos = GorillaTagger.Instance.myVRRig.transform.position;
            Vector3 targetPos = target.transform.position;
            Vector3 offset = selfPos - targetPos;
            offset.y = 0;
            float currentDistance = offset.magnitude;
            float desiredDistance = initialDistanceToTarget > 0 ? initialDistanceToTarget : circleRadius;
            float distanceError = currentDistance - desiredDistance;
            if (circle)
            {
                Vector3 radialDir = offset.normalized;
                Vector3 tangentDir = Vector3.Cross(radialDir, Vector3.up).normalized;
                Vector3 correction = -radialDir * distanceError;
                return (tangentDir + correction).normalized;
            }
            return (targetPos - selfPos).normalized;
        }
    }
}