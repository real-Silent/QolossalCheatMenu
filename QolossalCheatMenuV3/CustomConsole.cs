using UnityEngine;

namespace Qolossal
{
    public class CustomConsole : MonoBehaviour
    {
        private static bool beta = false;

        public static void LogToConsole(string message)
        {
            if (!beta) return;
            MelonLoader.MelonLogger.Msg(message);
        }

        public static void Debug(string message)
        {
            if (!beta) return;
            MelonLoader.MelonLogger.Msg(message);
        }
        public static void Error(string message)
        {
            if (!beta) return;
            MelonLoader.MelonLogger.Msg(message);
        }
    }
}