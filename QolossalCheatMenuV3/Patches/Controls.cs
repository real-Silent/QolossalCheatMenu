using easyInputs;
using UnityEngine;

namespace Qolossal
{
    public class Controls
    {
        public static bool RightTrigger() =>
            EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
        public static bool LeftTrigger() =>
            EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand);
        public static bool LeftJoystick() =>
            EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand);
        public static bool RightJoystick() =>
            EasyInputs.GetThumbStickButtonDown(EasyHand.RightHand);
        public static Vector2 RightJoystickAxis() =>
            EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand);
        public static Vector2 LeftJoystickAxis() =>
            EasyInputs.GetThumbStick2DAxis(EasyHand.LeftHand);
    }
}