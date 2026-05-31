using System.Reflection;

namespace Qolossal
{
    public class HarmonyLoader
    {
        public static bool IsPatched { get; private set; }
        private static HarmonyLib.Harmony instance;
        public const string InstanceId = "org.Qolossal";

        internal static void ApplyHarmonyPatches()
        {
            if (!HarmonyLoader.IsPatched)
            {
                if (HarmonyLoader.instance == null)
                {
                    HarmonyLoader.instance = new HarmonyLib.Harmony("org.Qolossal");
                }
                HarmonyLoader.instance.PatchAll(Assembly.GetExecutingAssembly());
                HarmonyLoader.IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (HarmonyLoader.instance != null && HarmonyLoader.IsPatched)
            {
                HarmonyLoader.instance.UnpatchSelf();
                HarmonyLoader.IsPatched = false;
            }
        }
    }
}