using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class FakeQuestMenu : MonoBehaviour
    {
        public FakeQuestMenu(IntPtr e) : base(e) { }
        public static bool fakeQuestMenuFinger = false;
        public virtual void Update()
        {
            if (PluginConfig.fakequestmenu)
            {
                if (!GorillaLocomotion.Player.Instance.inOverlay)
                    GorillaLocomotion.Player.Instance.inOverlay = true;
                if (!fakeQuestMenuFinger)
                    fakeQuestMenuFinger = true;
                if (GorillaTagger.Instance.rightHandTransform.transform.rotation != new Quaternion(0, 0, 0, 0))
                    GorillaTagger.Instance.rightHandTransform.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (GorillaTagger.Instance.leftHandTransform.transform.rotation != new Quaternion(0, 0, 0, 0))
                    GorillaTagger.Instance.leftHandTransform.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<FakeQuestMenu>());
                if (GorillaLocomotion.Player.Instance.inOverlay)
                    GorillaLocomotion.Player.Instance.inOverlay = false;
                if (fakeQuestMenuFinger)
                    fakeQuestMenuFinger = false;
            }
        }
    }
}