using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class CustomBinding : MonoBehaviour
    {
        public CustomBinding(IntPtr e) : base(e) { }

        static bool isListeningForBind = false;
        static string bindingTargetKey = null;
        static bool waitingForRelease = false;

        public virtual void Update()
        {
            if (isListeningForBind)
            {
                if (waitingForRelease)
                {
                    if (AnyInputPressed()) return;
                    waitingForRelease = false;
                }
                else
                {
                    CheckBindings();
                }
            }
        }

        public static void CheckBindings()
        {
            Dictionary<string, bool> inputChecks = new Dictionary<string, bool>
            {
                { "LJoystick", ControlsV2.LeftJoystick() },
                { "RJoystick", ControlsV2.RightJoystick() },
                { "RTrigger", ControlsV2.RightTrigger() },
                { "LTrigger", ControlsV2.LeftTrigger() },
                { "RGrip", ControlsV2.RightGrip() },
                { "LGrip", ControlsV2.LeftGrip() },
                { "LPrimary", ControlsV2.LeftPrimaryButton() },
                { "RPrimary", ControlsV2.RightPrimaryButton() },
                { "LSecondary", ControlsV2.LeftSecondaryButton() },
                { "RSecondary", ControlsV2.RightSecondaryButton() }
            };
            foreach (var input in inputChecks)
            {
                if (input.Value)
                {
                    AddBindKey(bindingTargetKey, input.Key);
                    isListeningForBind = false;
                    return;
                }
            }
        }

        public static void StartListeningForBind(string featureKey)
        {
            if (isListeningForBind) return;
            isListeningForBind = true;
            bindingTargetKey = featureKey;
            waitingForRelease = true;
        }

        public static bool AnyInputPressed()
        {
            return ControlsV2.LeftJoystick() || ControlsV2.RightJoystick() ||
                   ControlsV2.RightTrigger() || ControlsV2.LeftTrigger() ||
                   ControlsV2.RightGrip() || ControlsV2.LeftGrip() ||
                   ControlsV2.LeftPrimaryButton() || ControlsV2.RightPrimaryButton() ||
                   ControlsV2.LeftSecondaryButton() || ControlsV2.RightSecondaryButton();
        }

        public static void AddBindKey(string featureKey, string key)
        {
            var field = typeof(PluginConfig).GetField(featureKey.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower() + "_bind");
            if (field != null)
                field.SetValue(null, key);
        }

        public static string GetBinds(string featureKey)
        {
            var field = typeof(PluginConfig).GetField(featureKey.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower() + "_bind");
            if (field != null)
            {
                string bind = (string)field.GetValue(null);

                if (string.IsNullOrWhiteSpace(bind))
                    return "UNBOUND";
                return bind;
            }
            return "";
        }

        public static string MirrorBind(string bind, bool isLeftHand)
        {
            switch (bind)
            {
                case "LTrigger": return isLeftHand ? "LTrigger" : "RTrigger";
                case "RTrigger": return isLeftHand ? "LTrigger" : "RTrigger";
                case "LGrip": return isLeftHand ? "LGrip" : "RGrip";
                case "RGrip": return isLeftHand ? "LGrip" : "RGrip";
                case "LPrimary": return isLeftHand ? "LPrimary" : "RPrimary";
                case "RPrimary": return isLeftHand ? "LPrimary" : "RPrimary";
                case "LSecondary": return isLeftHand ? "LSecondary" : "RSecondary";
                case "RSecondary": return isLeftHand ? "LSecondary" : "RSecondary";
                case "LeftJoystick": return isLeftHand ? "LeftJoystick" : "RightJoystick";
                case "RightJoystick": return isLeftHand ? "LeftJoystick" : "RightJoystick";
                default: return bind;
            }
        }

        public static void ClearBinds(string featureKey)
        {
            var field = typeof(PluginConfig).GetField(featureKey.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower() + "_bind");
            if (field != null)
                field.SetValue(null, "");
        }
    }
}