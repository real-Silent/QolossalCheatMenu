using Qolossal.Menu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NoLeaves : MonoBehaviour
    {
        public NoLeaves(IntPtr e) : base(e) { }
        public List<GameObject> CachedLeaves = new List<GameObject>();
        public virtual void Start()
        {
            if (PluginConfig.NoLeaves)
            {
                CachedLeaves.Clear();
                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj.name.ToLower().Contains("smallleaves"))
                    {
                        CachedLeaves.Add(obj);
                        obj.SetActive(false);
                    }
                }
            }
        }
        public virtual void Update()
        {
            if (!PluginConfig.NoLeaves)
            {
                foreach (GameObject obj in CachedLeaves)
                {
                    obj.SetActive(true);
                }
                GameObject.Destroy(Plugin.holder.GetComponent<NoLeaves>());
                return;
            }
        }
    }
}