using HarmonyLib;
using Qolossal.Mods;
using Qolossal.Menu;
using UnityEngine;

namespace Qolossal.Patches
{
    [HarmonyPatch(typeof(VRRig), "LateUpdate")]
    class VRRigTorsoPatch
    {
        public static void Postfix(VRRig __instance)
        {
            if (!PluginConfig.decapitation)
                return;
            if (__instance == GorillaTagger.Instance.myVRRig)
            {
                __instance.transform.rotation = Quaternion.Euler(0f, Decapitation.yRotation, 0f);
                float scaleFactor = __instance.transform.localScale.x;
                __instance.head.MapMine(scaleFactor, __instance.playerOffsetTransform);
                __instance.rightHand.MapMine(scaleFactor, __instance.playerOffsetTransform);
                __instance.leftHand.MapMine(scaleFactor, __instance.playerOffsetTransform);
            }
        }
    }
}