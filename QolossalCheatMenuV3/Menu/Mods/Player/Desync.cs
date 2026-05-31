using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Desync : MonoBehaviour
    {
        public Desync(IntPtr e) : base(e) { }
        public static GameObject ghost;

        static float prevtime;
        static Vector3 prevpos;
        static Quaternion prevrot;

        static GameObject lefthand;
        static GameObject righthand;
        static Vector3 prevrpos;
        static Vector3 prevlpos;
        static Quaternion prevrrot;
        static Quaternion prevlrot;
        public virtual void Update()
        {
            if (PluginConfig.desync)
            {
                if (GorillaTagger.Instance == null || GorillaTagger.Instance.myVRRig == null)
                    return;
                if (Time.time - prevtime >= (1 / 28))
                {
                    prevtime = Time.time;
                    if (Time.time - prevtime >= (PhotonNetwork.GetPing() / 500))
                    {
                        if (ghost == null)
                            ghost = GhostManager.SpawnGhost();
                        var vrrig = ghost.GetComponent<VRRig>();
                        ghost.transform.position = prevpos;
                        ghost.transform.rotation = prevrot;
                        if (lefthand == null || righthand == null)
                        {
                            lefthand = vrrig.leftHandPlayer.gameObject;
                            righthand = vrrig.rightHandPlayer.gameObject;
                        }
                        lefthand.transform.position = prevlpos;
                        lefthand.transform.rotation = prevlrot;
                        righthand.transform.position = prevrpos;
                        righthand.transform.rotation = prevrrot;
                        vrrig.leftHandPlayer.Pause();
                        vrrig.rightHandPlayer.Pause();
                        vrrig.mainSkin.material.color = GhostManager.ghostColor;
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        vrrig.enabled = false;
                        prevpos = GorillaTagger.Instance.myVRRig.transform.position;
                        prevrot = GorillaTagger.Instance.myVRRig.transform.rotation;
                        prevlpos = GorillaTagger.Instance.myVRRig.leftHandTransform.position;
                        prevlrot = GorillaTagger.Instance.myVRRig.leftHandTransform.rotation;
                        prevrpos = GorillaTagger.Instance.myVRRig.rightHandTransform.position;
                        prevrrot = GorillaTagger.Instance.myVRRig.rightHandTransform.rotation;
                        prevtime = Time.time;
                    }
                }
            }
            else
            {
                GameObject.Destroy(Plugin.holder.GetComponent<Desync>());
                if (Desync.ghost != null)
                    GhostManager.DestroyGhost(Desync.ghost);
            }
        }
    }
}