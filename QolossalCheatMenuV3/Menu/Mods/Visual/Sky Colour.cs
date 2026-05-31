using Qolossal.Menu;
using System;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class SkyColour : MonoBehaviour
    {
        public SkyColour(IntPtr e) : base(e) { }
        static new GameObject gameObject;
        static Color originalColor;
        static Material original;

        public virtual void Start()
        {
            gameObject = GameObject.Find("Level/sky") ?? GameObject.Find("Level/newsky");
            original = GameObject.Find("Level/sky").GetComponent<Renderer>().material ?? GameObject.Find("Level/newsky").GetComponent<Renderer>().material;
            originalColor = original.color;
        }
        public virtual void Update()
        {
            switch (PluginConfig.skycolour)
            {
                case 0:
                    GameObject.Destroy(Plugin.holder.GetComponent<SkyColour>());
                    if (gameObject.GetComponent<MeshRenderer>().material != original)
                        gameObject.GetComponent<MeshRenderer>().material = original;
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != original.shader)
                        gameObject.GetComponent<MeshRenderer>().material.shader = original.shader;
                    if (gameObject.GetComponent<MeshRenderer>().material.color != originalColor)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
                    }
                    break;
                case 1:
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != Shader.Find("Standard"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    if (gameObject.GetComponent<MeshRenderer>().material.color != Color.magenta)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
                    }
                    break;
                case 2:
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != Shader.Find("Standard"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    if (gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                    break;
                case 3:
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != Shader.Find("Standard"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    if (gameObject.GetComponent<MeshRenderer>().material.color != Color.cyan)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                    }
                    break;
                case 4:
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != Shader.Find("Standard"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    if (gameObject.GetComponent<MeshRenderer>().material.color != Color.green)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                    break;
                case 5:
                    if (gameObject.GetComponent<MeshRenderer>().material.shader != Shader.Find("Standard"))
                    {
                        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    }
                    if (gameObject.GetComponent<MeshRenderer>().material.color != Color.black)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    break;
                default:
                    return;
            }
        }
    }
}