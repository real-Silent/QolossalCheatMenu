using Newtonsoft.Json;
using Photon.Pun;
using PlayFab;
using Qolossal;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Console.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ConsoleGuns : MonoBehaviour
    {
        public ConsoleGuns(IntPtr e) : base(e) { }

        private GameObject pointer;
        private LineRenderer radiusLine;
        private Material lineMaterial = new Material(Shader.Find("GUI/Text Shader"));
        private Vector3 originalPosition;

        private Color beamColour;

        public virtual void Update()
        {
            SetBeamColor();

            if (PluginConfig.consolequitgun)
            {
                Gun("\n\nquitgun");
            }
            if (PluginConfig.consolebringgun)
            {
                Gun("\n\ngotouser");
            }
            if (PluginConfig.consolekickgun)
            {
                Gun("\n\nkickgun");
            }
            if (PluginConfig.consolechangenamegun)
            {
                Gun("\n\nchangenamegun");
            }
            if (PluginConfig.consolerestartmicgun)
            {
                Gun("\n\nrestartmicgun");
            }
            if (PluginConfig.consoleghostgun)
            {
                Gun("\n\nghostgun");
            }
            if (PluginConfig.consoleunghostgun)
            {
                Gun("\n\nunghostgun");
            }
            if (PluginConfig.consolemutegun)
            {
                Gun("\n\nmutegun");
            }
            if (PluginConfig.consoleunmutegun)
            {
                Gun("\n\nunmutegun");
            }
            if (PluginConfig.consoledisablemovementgun)
            {
                Gun("\n\ndisablemovementgun");
            }
            if (PluginConfig.consoleenablemovementgun)
            {
                Gun("\n\nenablemovementgun");
            }
            if (PluginConfig.consoletargetplayergun)
            {
                Gun("\n\ntargetspawngun");
            }
            if (PluginConfig.consoleflinggun)
            {
                Gun("\n\nadminflinggun");
            }

            bool anyGunEnabled =
    PluginConfig.consolequitgun ||
    PluginConfig.consolebringgun ||
    PluginConfig.consolekickgun ||
    PluginConfig.consolechangenamegun ||
    PluginConfig.consolerestartmicgun ||
    PluginConfig.consoleghostgun ||
    PluginConfig.consoleunghostgun ||
    PluginConfig.consolemutegun ||
    PluginConfig.consoleunmutegun ||
    PluginConfig.consoledisablemovementgun ||
    PluginConfig.consoleenablemovementgun ||
    PluginConfig.consoletargetplayergun ||
    PluginConfig.consoleflinggun;

            if (!anyGunEnabled)
            {
                if (pointer != null)
                {
                    Destroy(pointer);
                    pointer = null;
                }
            }
        }

        public static void ConsoleKickAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nkickall");
        public static void ConsoleQuitAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nquitall");
        public static void ConsoleDisableMovementAll() => Console.ConsoleQolossal.ExecuteCommand("\n\ndisablemovementall");
        public static void ConsoleEnableMovementAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nenablemovementall");
        public static void ConsoleGhostAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nghostall");
        public static void ConsoleUnGhostAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nunghostall");
        public static void ConsoleBringAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nbringall");
        public static void ConsoleFlingAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nflingall");
        public static void ConsoleMuteAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nmuteall");
        public static void ConsoleUnMuteAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nunmuteall");
        public static void ConsoleNetworkPlayerAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nnetworkplayerspawnall");
        public static void ConsoleTargetPlayerAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nstickabletargetspawnall");
        public static void ConsoleChangeNameAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nchangenameall");
        public static void ConsoleRestartMicAll() => Console.ConsoleQolossal.ExecuteCommand("\n\nrestartmicall");

        private void Gun(string command)
        {
            RaycastHit raycastHit;
            LayerMask combinedLayerMask = GorillaLocomotion.Player.Instance.locomotionEnabledLayers | 16384;

            if (!Physics.Raycast(
                GorillaTagger.Instance.rightHandTransform.position - GorillaTagger.Instance.rightHandTransform.up,
                -GorillaTagger.Instance.rightHandTransform.up,
                out raycastHit,
                float.PositiveInfinity,
                combinedLayerMask))
            {
                return;
            }

            CreatePointer();
            pointer.transform.position = raycastHit.point;

            originalPosition = GorillaTagger.Instance.myVRRig.transform.position;

            if (ControlsV2.RightJoystick())
            {
                if (radiusLine == null)
                {
                    lineMaterial.color = beamColour;

                    radiusLine = new GameObject("RadiusLine").AddComponent<LineRenderer>();
                    radiusLine.transform.parent = pointer.transform;

                    radiusLine.positionCount = 2;
                    radiusLine.startWidth = 0.05f;
                    radiusLine.endWidth = 0.05f;
                    radiusLine.material = lineMaterial;
                    radiusLine.startColor = beamColour;
                    radiusLine.endColor = beamColour;
                }

                radiusLine.SetPosition(0, raycastHit.point);
                radiusLine.SetPosition(1, GorillaTagger.Instance.rightHandTransform.position);

                VRRig rig = raycastHit.collider.GetComponentInParent<VRRig>();

                if (rig != null && rig.photonView != null && rig.photonView.Owner != null)
                {
                    Console.ConsoleQolossal.ExecuteCommand(rig.photonView.Owner.UserId + command);
                }

                return;
            }

            // cleanup
            if (radiusLine != null)
            {
                Destroy(radiusLine);
                radiusLine = null;
            }
        }

        private void SetBeamColor()
        {
            switch (PluginConfig.BeamColour)
            {
                case 0: beamColour = new Color(0.6f, 0f, 0.8f, 0.5f); break; // Purple
                case 1: beamColour = new Color(1f, 0f, 0f, 0.5f); break;    // Red
                case 2: beamColour = new Color(1f, 1f, 0f, 0.5f); break;    // Yellow
                case 3: beamColour = new Color(0f, 1f, 0f, 0.5f); break;    // Green
                case 4: beamColour = new Color(0f, 0f, 1f, 0.5f); break;    // Blue
            }
        }

        private void CreatePointer()
        {
            if (pointer == null)
            {
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                pointer.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));
                pointer.GetComponent<Renderer>().material.color = beamColour;
            }
        }
    }
}