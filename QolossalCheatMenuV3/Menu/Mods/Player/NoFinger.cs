using Qolossal.Menu;
using HarmonyLib;

namespace Qolossal.Mods
{
    [HarmonyPatch(typeof(VRMapMiddle), "MapMyFinger", MethodType.Normal)]
    internal static class MiddleIndex
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (PluginConfig.nofinger || FakeQuestMenu.fakeQuestMenuFinger)
            {
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(VRMapIndex), "MapMyFinger", MethodType.Normal)]
    internal class FingerIndex
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (PluginConfig.nofinger || FakeQuestMenu.fakeQuestMenuFinger)
            {
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(VRMapThumb), "MapMyFinger", MethodType.Normal)]
    internal class ThumbIndex
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (PluginConfig.nofinger || FakeQuestMenu.fakeQuestMenuFinger)
            {
                return false;
            }
            return true;
        }
    }
}