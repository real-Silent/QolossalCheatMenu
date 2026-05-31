using Qolossal.Menu;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NameTags : MonoBehaviour
    {
        public NameTags(IntPtr e) : base(e) { }
        static HashSet<string> requestedIds = new HashSet<string>();
        static Dictionary<string, int> tagIdentifiers = new Dictionary<string, int>();
        static Dictionary<string, string> creationDates = new Dictionary<string, string>();
        static int nextIdentifier = 1;

        static Vector3 height;
        static Vector3 size;
        static Color colour;

        static float distance;
        static string colourcode;

        static bool identifiersInitialized;

        public virtual void Start()
        {
            InitializeTagIdentifiers();
        }

        private static void InitializeTagIdentifiers()
        {
            if (identifiersInitialized)
                return;
            tagIdentifiers["CreationDate"] = nextIdentifier++;
            tagIdentifiers["ColorCode"] = nextIdentifier++;
            tagIdentifiers["Distance"] = nextIdentifier++;
            identifiersInitialized = true;
        }

        public virtual void Update()
        {
            if (!PhotonNetwork.InRoom ||
                GorillaParent.instance == null ||
                GorillaParent.instance.vrrigs == null)
                return;
            InitializeTagIdentifiers();
            if (!PluginConfig.NameTags)
                return;
            if (GorillaParent.instance == null)
                return;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null ||
                    vrrig.photonView == null ||
                    vrrig.photonView.Owner == null ||
                    vrrig.playerText == null ||
                    Camera.main == null ||
                    vrrig.photonView.Owner.IsLocal)
                    continue;

                switch (PluginConfig.nametagheight) { case 0: height = new Vector3(25.30f, 25.00f, 0f); break; case 1: height = new Vector3(25.30f, 220.00f, 0f); break; } // ---------- SIZE ----------
                switch (PluginConfig.nametagsize) { case 0: size = new Vector3(1, 1, 1); break; case 1: size = new Vector3(3f, 3f, 3f); break; case 2: size = new Vector3(4f, 4f, 4f); break; case 3: size = new Vector3(5f, 5f, 5f); break; } 
                switch (PluginConfig.nametagcolour) { case 0: colour = Color.white; break; case 1: colour = Color.yellow; break; case 2: colour = Color.green; break; case 3: colour = Color.blue; break; case 4: colour = Color.red; break; case 5: colour = Color.cyan; break; case 6: colour = Color.black; break; }

                vrrig.playerText.transform.localPosition = height;
                vrrig.playerText.transform.localScale = size;
                vrrig.playerText.rectTransform.sizeDelta = new Vector2(500, 500);
                vrrig.playerText.color = colour;

                vrrig.playerText.transform.rotation = PluginConfig.nametagheight == 1 ? Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up) : vrrig.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                string userId = vrrig.photonView.Owner.UserId;

                if (PluginConfig.ShowCreationDate)
                {
                    if (!requestedIds.Contains(userId))
                    {
                        requestedIds.Add(userId);
                        CreationDate.GetCreationDate(vrrig, resultDate =>
                        {
                            if (!string.IsNullOrEmpty(resultDate))
                                creationDates[userId] = resultDate;
                        });
                    }

                    if (creationDates.TryGetValue(userId, out string cachedDate))
                        addline(vrrig, "CreationDate", $"Created: {cachedDate}");
                }
                else
                {
                    requestedIds.Remove(userId);
                    removeline(vrrig, tagIdentifiers["CreationDate"]);
                }

                if (PluginConfig.ShowColourCode && vrrig.mainSkin?.material != null)
                {
                    Color c = vrrig.mainSkin.material.color;
                    colourcode = $"{(int)(c.r * 9)}, {(int)(c.g * 9)}, {(int)(c.b * 9)}";
                    addline(vrrig, "ColorCode", colourcode);
                }
                else
                {
                    removeline(vrrig, tagIdentifiers["ColorCode"]);
                }

                if (PluginConfig.ShowDistance)
                {
                    distance = Vector3.Distance(vrrig.transform.position, Camera.main.transform.position);
                    addline(vrrig, "Distance", $"[{(int)distance}M]");
                }
                else
                {
                    removeline(vrrig, tagIdentifiers["Distance"]);
                }
            }
        }
        static void addline(VRRig vrrig, string tag, string value)
        {
            int identifier = AssignIdentifier(tag);
            string line = $"{value} ({identifier})";
            line.Replace($"({identifier})", "");
            string[] lines = vrrig.playerText.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].EndsWith($"({identifier})"))
                {
                    lines[i] = line;
                    vrrig.playerText.text = string.Join("\n", lines);
                    return;
                }
            }
            vrrig.playerText.text += "\n" + line;
        }

        static void removeline(VRRig vrrig, int identifier)
        {
            string id = $"({identifier})";
            var lines = new List<string>(vrrig.playerText.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            lines.RemoveAll(l => l.EndsWith(id));
            vrrig.playerText.text = string.Join("\n", lines);
        }

        static int AssignIdentifier(string tag)
        {
            if (!tagIdentifiers.ContainsKey(tag))
                tagIdentifiers[tag] = nextIdentifier++;

            return tagIdentifiers[tag];
        }
    }
}