using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class HitBoxes : MonoBehaviour
    {
        public HitBoxes(IntPtr e) : base(e) { }
        private static readonly float[] HitBoxAmmounts = { 0.05f, 0.07f, 0.09f, 0.11f, 0.13f, 0.2f, 0.3f };
        private static readonly Color32[] HitBoxColors = {
            new Color32(204, 51, 255, 100),
            new Color32(255, 0, 0, 100),
            new Color32(255, 255, 0, 100),
            new Color32(0, 255, 0, 100),
            new Color32(0, 0, 255, 100),
            new Color32(255, 255, 255, 255)
        };

        static GameObject visualizerL;
        static GameObject visualizerR;

        public static float ammount;
        private static Color color;

        public virtual void Update()
        {
            if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                return;
            if (PluginConfig.hitboxes == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<HitBoxes>());
                CleanupVisualizers();
                return;
            }
            ammount = HitBoxAmmounts[Mathf.Min(PluginConfig.hitboxes - 1, HitBoxAmmounts.Length - 1)];
            int opacity = GetHitBoxOpacity(PluginConfig.HitBoxesOpacity);
            color = GetHitBoxColor(PluginConfig.HitBoxesColour, opacity);
            CreateAndConfigureVisualizer(ref visualizerL, GorillaTagger.Instance.leftHandTransform);
            CreateAndConfigureVisualizer(ref visualizerR, GorillaTagger.Instance.rightHandTransform);
            if (visualizerL != null)
            {
                UpdateVisualizer(visualizerL);
            }
            if (visualizerR != null)
            {
                UpdateVisualizer(visualizerR);
            }
        }

        static void CleanupVisualizers()
        {
            if (visualizerL != null) Destroy(visualizerL);
            if (visualizerR != null) Destroy(visualizerR);
        }
        static void CreateAndConfigureVisualizer(ref GameObject visualizer, Transform parent)
        {
            if (visualizer == null)
            {
                visualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(visualizer.GetComponent<Collider>());
                visualizer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                visualizer.transform.SetParent(parent);
            }
            visualizer.transform.position = parent.position;
        }

        static void UpdateVisualizer(GameObject visualizer)
        {
            visualizer.GetComponent<Renderer>().material.color = color;
            visualizer.transform.localScale = new Vector3(HitBoxes.ammount * 1.5f, HitBoxes.ammount * 1.5f, HitBoxes.ammount * 1.5f);
        }
        private static int GetHitBoxOpacity(int setting)
        {
            switch (setting)
            {
                case 1: return 80;
                case 2: return 60;
                case 3: return 30;
                case 4: return 20;
                case 5: return 0;
                default: return 100;
            }
        }

        private static Color GetHitBoxColor(int setting, int opacity)
        {
            var baseColor = HitBoxColors[Mathf.Min(setting, HitBoxColors.Length - 1)];
            return new Color(baseColor.r / 255f, baseColor.g / 255f, baseColor.b / 255f, opacity / 255f);
        }
    }
}