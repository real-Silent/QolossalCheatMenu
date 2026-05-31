using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Platforms : MonoBehaviour
    {
        public Platforms(IntPtr e) : base(e) { }

        public static GameObject PlatL;
        static bool PlatLonce = false;

        public static GameObject PlatR;
        static bool PlatRonce = false;
        public virtual void Update()
        {
            if (PluginConfig.platforms)
            {
                if (GorillaTagger.Instance == null)
                    return;

                string bind = CustomBinding.GetBinds("platforms");
                if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                {
                    return;
                }
                string leftBind = CustomBinding.MirrorBind(bind, true);
                string rightBind = CustomBinding.MirrorBind(bind, false);
                if (ControlsV2.GetControl(leftBind))
                {
                    if (!PlatLonce)
                    {
                        Platforms.PlatL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Platforms.PlatL.GetComponent<Renderer>().material.color = Color.magenta;
                        Platforms.PlatL.transform.localScale = new Vector3(0.025f, 0.15f, 0.2f);
                        Platforms.PlatL.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                        Platforms.PlatL.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                        PlatLonce = true;
                    }
                }
                else if (PlatLonce)
                {
                    UnityEngine.Object.Destroy(Platforms.PlatL);
                    PlatLonce = false;
                }
                if (ControlsV2.GetControl(rightBind))
                {
                    if (!PlatRonce)
                    {
                        Platforms.PlatR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Platforms.PlatR.GetComponent<Renderer>().material.color = Color.magenta;
                        Platforms.PlatR.transform.localScale = new Vector3(0.025f, 0.15f, 0.2f);
                        Platforms.PlatR.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                        Platforms.PlatR.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                        PlatRonce = true;
                        return;
                    }
                }
                else if (PlatRonce)
                {
                    UnityEngine.Object.Destroy(Platforms.PlatR);
                    PlatRonce = false;
                    return;
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Platforms>());
            }
        }
    }
}