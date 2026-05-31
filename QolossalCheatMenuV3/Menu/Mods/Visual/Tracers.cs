using Photon.Pun;
using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Tracers : MonoBehaviour
    {
        public Tracers(IntPtr e) : base(e) { }
        static Color espcolor;
        static Vector3 pos;
        static float size;

        static readonly Material lineMaterial = new Material(Shader.Find("GUI/Text Shader"));

        public virtual void Update()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PluginConfig.tracers == 0)
                {
                    GameObject.Destroy(Plugin.holder.GetComponent<Tracers>());
                    return;
                }
                if (GorillaParent.instance == null)
                    return;
                espcolor = GetEspColor(PluginConfig.ESPColour);
                pos = GetTracerPosition(PluginConfig.tracers);
                size = GetTracerSize(PluginConfig.tracersize);
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != null && vrrig != GorillaTagger.Instance.myVRRig && vrrig.photonView.Owner != PhotonNetwork.LocalPlayer && vrrig.photonView.Owner.UserId != PhotonNetwork.LocalPlayer.UserId && !vrrig.photonView.IsMine)
                    {
                        CreateTracer(vrrig);
                    }
                }
            }
        }

        static Color GetEspColor(int colorIndex)
        {
            switch (colorIndex)
            {
                case 0: return new Color(0.6f, 0f, 0.8f, 0.4f);
                case 1: return new Color(1f, 0f, 0f, 0.4f);
                case 2: return new Color(1f, 1f, 0f, 0.4f);
                case 3: return new Color(0f, 1f, 0f, 0.4f);
                case 4: return new Color(0f, 0f, 1f, 0.4f);
                default: return new Color(0.6f, 0f, 0.8f, 0.4f);
            }
        }

        static Vector3 GetTracerPosition(int tracerIndex)
        {
            switch (tracerIndex)
            {
                case 1: return GorillaTagger.Instance.rightHandTransform.position;
                case 2: return GorillaTagger.Instance.leftHandTransform.position;
                case 3: return GorillaTagger.Instance.headCollider.transform.position + (Vector3.up * 0.2f);
                case 4: return GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward / 2;
                default: return Vector3.zero;
            }
        }

        static float GetTracerSize(int sizeIndex)
        {
            switch (sizeIndex)
            {
                case 0: return 0.002f;
                case 1: return 0.01f;
                case 2: return 0.025f;
                case 3: return 0.05f;
                case 4: return 0.065f;
                case 5: return 0.08f;
                case 6: return 0.1f;
                default: return 0.01f;
            }
        }

        static void CreateTracer(VRRig vrrig)
        {
            GameObject lineObject = new GameObject("Line");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.startColor = (vrrig.mainSkin.material.name.Contains("fected")) ? Color.red : espcolor;
            lineRenderer.endColor = lineRenderer.startColor;
            lineRenderer.startWidth = size;
            lineRenderer.endWidth = size;
            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;
            lineRenderer.SetPosition(0, pos);
            lineRenderer.SetPosition(1, vrrig.transform.position);
            lineRenderer.material = lineMaterial;
            Destroy(lineObject, Time.deltaTime);
        }
    }
}