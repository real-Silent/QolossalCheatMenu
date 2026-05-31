using easyInputs;
using UnityEngine;

namespace Qolossal
{
    public class ControlsV2
    {
        public static bool GetControl(string controlName)
        {
            switch (controlName)
            {
                case "LJoystick":
                    return ControlsV2.LeftJoystick();
                case "RJoystick":
                    return ControlsV2.RightJoystick();
                case "RTrigger":
                    return ControlsV2.RightTrigger();
                case "LTrigger":
                    return ControlsV2.LeftTrigger();
                case "RGrip":
                    return ControlsV2.RightGrip();
                case "LGrip":
                    return ControlsV2.LeftGrip();
                case "LPrimary":
                    return ControlsV2.LeftPrimaryButton();
                case "RPrimary":
                    return ControlsV2.RightPrimaryButton();
                case "LSecondary":
                    return ControlsV2.LeftSecondaryButton();
                case "RSecondary":
                    return ControlsV2.RightSecondaryButton();
                default:
                    return false;
            }
        }
        public static bool LeftJoystick() =>
            EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand);
        public static Vector2 LeftJoystickAxis() =>
            EasyInputs.GetThumbStick2DAxis(EasyHand.LeftHand);
        public static bool RightJoystick() =>
            EasyInputs.GetThumbStickButtonDown(EasyHand.RightHand);
        public static Vector2 RightJoystickAxis() =>
            EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand);
        public static bool RightTrigger() =>
            EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
        public static bool LeftTrigger() =>
            EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand);
        public static bool RightGrip() =>
            EasyInputs.GetGripButtonDown(EasyHand.RightHand);
        public static bool LeftGrip() =>
            EasyInputs.GetGripButtonDown(EasyHand.LeftHand);
        public static bool LeftPrimaryButton() =>
            EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand);
        public static bool RightPrimaryButton() =>
            EasyInputs.GetPrimaryButtonDown(EasyHand.RightHand);
        public static bool LeftSecondaryButton() =>
            EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand);
        public static bool RightSecondaryButton() =>
            EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand);
    }
}