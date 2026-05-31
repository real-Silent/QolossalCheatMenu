using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class HzHands : MonoBehaviour
    {
        public HzHands(IntPtr e) : base(e) { }
        private static readonly int[] vols = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        private int vol;

        public virtual void Update()
        {
            int Setting = PluginConfig.hzhands;
            Setting = Mathf.Clamp(Setting - 1, 0, vols.Length - 1);

            if (Setting == 0)
            {
				GorillaLocomotion.Player.Instance.velocityHistorySize = 6;
				GorillaLocomotion.Player.Instance.InitializeValues();
                Destroy(this.GetComponent<HzHands>());
            }
            else
            {
                vol = vols[Setting];
                if (GorillaLocomotion.Player.Instance.velocityHistorySize != vol)
                {
                    GorillaLocomotion.Player.Instance.velocityHistorySize = vol;
                    GorillaLocomotion.Player.Instance.InitializeValues();
                }
            }
        }
    }
}