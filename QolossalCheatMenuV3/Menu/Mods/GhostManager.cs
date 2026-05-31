using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Menu
{
    public class GhostManager
    {
        private static List<GameObject> ghosts = new List<GameObject>();
        public static byte opacity;
        public static Color ghostColor;
        public static GameObject SpawnGhost()
        {
            GameObject ghost = GameObject.Instantiate(GorillaTagger.Instance.offlineVRRig.gameObject);
            var vrrig = ghost.GetComponent<VRRig>();

            switch (PluginConfig.GhostOpacity)
            {
                case 0:
                    opacity = 100;
                    break;
                case 1:
                    opacity = 80;
                    break;
                case 2:
                    opacity = 60;
                    break;
                case 3:
                    opacity = 30;
                    break;
                case 4:
                    opacity = 20;
                    break;
                case 5:
                    opacity = 0;
                    break;
            }
            switch (PluginConfig.GhostColour)
            {
                case 0:
                    ghostColor = new Color32(204, 51, 255, opacity);
                    break;
                case 1:
                    ghostColor = new Color32(255, 0, 0, opacity);
                    break;
                case 2:
                    ghostColor = new Color32(255, 255, 0, opacity);
                    break;
                case 3:
                    ghostColor = new Color32(0, 255, 0, opacity);
                    break;
                case 4:
                    ghostColor = new Color32(64, 255, 0, opacity);
                    break;
                case 5:
                    ghostColor = new Color32(0, 0, 255, opacity);
                    break;
                default:
                    ghostColor = new Color32(255, 255, 255, 255);
                    break;
            }
            GameObject.Destroy(vrrig.GetComponent<Rigidbody>());
            vrrig.mainSkin.material.color = ghostColor;
            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
            if (!vrrig.headMesh.active)
                vrrig.headMesh.active = true;
            if (!vrrig.mainSkin.enabled)
                vrrig.mainSkin.enabled = true;
            if (!vrrig.showName)
                vrrig.showName = true;
            ghosts.Add(ghost);
            return ghost;
        }

        public static void DestroyGhost(GameObject ghost)
        {
            if (ghosts.Contains(ghost))
            {
                ghosts.Remove(ghost);
                GameObject.Destroy(ghost);
            }
        }
    }
}