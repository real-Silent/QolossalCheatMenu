using Qolossal.Menu;
using System;
using System.Reflection;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Panic : MonoBehaviour
    {
        public Panic(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.Panic)
            {
                // All face buttons idk
                if (ControlsV2.LeftSecondaryButton() && ControlsV2.RightPrimaryButton() && ControlsV2.LeftPrimaryButton() && ControlsV2.RightPrimaryButton())
                {
                    foreach (var field in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                    {
                        if (field.FieldType == typeof(bool))
                        {
                            field.SetValue(null, false);
                        }
                        else if (field.FieldType == typeof(int))
                        {
                            field.SetValue(null, 0);
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Panic>());
            }
        }
    }
}