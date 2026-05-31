using System;
using System.Linq;
using UnityEngine;
using Qolossal;

[MelonLoader.RegisterTypeInIl2Cpp]
public class Outline : MonoBehaviour
{
    public Outline(IntPtr e) : base(e) { }
    private static System.Collections.Generic.HashSet<Mesh> registeredMeshes = new System.Collections.Generic.HashSet<Mesh>();

    public enum Mode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    public Mode OutlineMode
    {
        get { return outlineMode; }
        set
        {
            outlineMode = value;
            needsUpdate = true;
        }
    }

    public Color OutlineColor
    {
        get { return outlineColor; }
        set
        {
            outlineColor = value;
            needsUpdate = true;
        }
    }

    public float OutlineWidth
    {
        get { return outlineWidth; }
        set
        {
            outlineWidth = value;
            needsUpdate = true;
        }
    }

    [Serializable]
    private class ListVector3
    {
        public System.Collections.Generic.List<Vector3> data;
    }

    private Mode outlineMode;

    private Color outlineColor = Color.white;

    private float outlineWidth = 2f;

    private bool precomputeOutline;

    private System.Collections.Generic.List<Mesh> bakeKeys = new System.Collections.Generic.List<Mesh>();

    private System.Collections.Generic.List<ListVector3> bakeValues = new System.Collections.Generic.List<ListVector3>();

    private Renderer[] renderers;
    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;

    private bool needsUpdate;

    public virtual void Awake()
    {

        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();

        // Instantiate outline materials
        outlineMaskMaterial = AssetBundleLoader.outlineMaskMaterial;
        outlineFillMaterial = AssetBundleLoader.outlineFillMaterial;

        outlineMaskMaterial.name = "OutlineMask (Instance)";
        outlineFillMaterial.name = "OutlineFill (Instance)";

        // Retrieve or generate smooth normals
        LoadSmoothNormals();

        // Apply material properties immediately
        needsUpdate = true;
    }

    public virtual void OnEnable()
    {
        foreach (var renderer in renderers)
        {

            // Append outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Add(outlineMaskMaterial);
            materials.Add(outlineFillMaterial);

            renderer.materials = materials.ToArray();
        }
    }

    void OnValidate()
    {

        // Update material properties
        needsUpdate = true;

        // Clear cache when baking is disabled or corrupted
        if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
        {
            bakeKeys.Clear();
            bakeValues.Clear();
        }

        // Generate smooth normals when baking is enabled
        if (precomputeOutline && bakeKeys.Count == 0)
        {
            Bake();
        }
    }

    public virtual void Update()
    {
        if (needsUpdate)
        {
            needsUpdate = false;

            UpdateMaterialProperties();
        }
    }

    public virtual void OnDisable()
    {
        foreach (var renderer in renderers)
        {

            // Remove outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Remove(outlineMaskMaterial);
            materials.Remove(outlineFillMaterial);

            renderer.materials = materials.ToArray();
        }
    }

    void OnDestroy()
    {

        // Destroy material instances
        Destroy(outlineMaskMaterial);
        Destroy(outlineFillMaterial);
    }

    void Bake()
    {

        // Generate smooth normals for each mesh
        var bakedMeshes = new System.Collections.Generic.HashSet<Mesh>();

        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {

            // Skip duplicates
            if (!bakedMeshes.Add(meshFilter.sharedMesh))
            {
                continue;
            }

            // Serialize smooth normals
            var smoothNormals = SmoothNormals(meshFilter.sharedMesh);

            bakeKeys.Add(meshFilter.sharedMesh);
            bakeValues.Add(new ListVector3() { data = smoothNormals });
        }
    }

    void LoadSmoothNormals()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            if (meshFilter == null || meshFilter.sharedMesh == null)
            {
                Debug.LogWarning($"MeshFilter or sharedMesh is null on {gameObject.name}");
                continue;
            }

            if (!registeredMeshes.Add(meshFilter.sharedMesh))
            {
                continue;
            }

            var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
            var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

            if (smoothNormals == null)
            {
                Debug.LogWarning($"Smooth normals are null for mesh {meshFilter.sharedMesh.name}");
                continue;
            }

            Il2CppSystem.Collections.Generic.List<Vector4> uv4 = new Il2CppSystem.Collections.Generic.List<Vector4>();

            foreach (var n in smoothNormals)
            {
                uv4.Add(new Vector4(n.x, n.y, n.z, 0f));
            }

            meshFilter.sharedMesh.SetUVs(3, uv4);

            var renderer = meshFilter.GetComponent<Renderer>();
            if (renderer != null)
            {
                CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
            }
            else
            {
                Debug.LogWarning($"No Renderer found on {meshFilter.gameObject.name}");
            }
        }

        foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (skinnedMeshRenderer == null || skinnedMeshRenderer.sharedMesh == null)
            {
                Debug.LogWarning($"SkinnedMeshRenderer or sharedMesh is null on {gameObject.name}");
                continue;
            }

            if (!registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
            {
                continue;
            }

            Mesh mesh = skinnedMeshRenderer.sharedMesh;
            if (mesh.isReadable)
            {
                mesh.uv4 = new Vector2[mesh.vertexCount];
            }
            else
            {
                Debug.LogWarning($"Mesh '{mesh.name}' is not readable; skipping UV4 assignment.");
            }

            CombineSubmeshes(mesh, skinnedMeshRenderer.sharedMaterials);
        }
    }

    System.Collections.Generic.List<Vector3> SmoothNormals(Mesh mesh)
    {

        // Group vertices by location
        var groups = mesh.vertices.Select((vertex, index) => new System.Collections.Generic.KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

        // Copy normals to a new list
        var smoothNormals = new System.Collections.Generic.List<Vector3>(mesh.normals);

        // Average normals for grouped vertices
        foreach (var group in groups)
        {

            // Skip single vertices
            if (group.Count() == 1)
            {
                continue;
            }

            // Calculate the average normal
            var smoothNormal = Vector3.zero;

            foreach (var pair in group)
            {
                smoothNormal += smoothNormals[pair.Value];
            }

            smoothNormal.Normalize();

            // Assign smooth normal to each vertex
            foreach (var pair in group)
            {
                smoothNormals[pair.Value] = smoothNormal;
            }
        }

        return smoothNormals;
    }

    void CombineSubmeshes(Mesh mesh, Material[] materials)
    {

        // Skip meshes with a single submesh
        if (mesh.subMeshCount == 1)
        {
            return;
        }

        // Skip if submesh count exceeds material count
        if (mesh.subMeshCount > materials.Length)
        {
            return;
        }

        // Append combined submesh
        mesh.subMeshCount++;
        mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
    }

    void UpdateMaterialProperties()
    {

        // Apply properties according to mode
        outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

        switch (outlineMode)
        {
            case Mode.OutlineAll:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineVisible:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineHidden:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.OutlineAndSilhouette:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                break;

            case Mode.SilhouetteOnly:
                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
                break;
        }
    }
}