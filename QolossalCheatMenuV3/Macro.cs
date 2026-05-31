using MelonLoader;
using Newtonsoft.Json;
using Qolossal.Menu;
using Qolossal.Notifacation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Qolossal
{
    [Serializable]
    public class TransformData
    {
        public float px;
        public float py;
        public float pz;

        public float rx;
        public float ry;
        public float rz;
        public float rw;

        public TransformData() { }

        public TransformData(Transform t)
        {
            px = t.position.x;
            py = t.position.y;
            pz = t.position.z;

            rx = t.rotation.x;
            ry = t.rotation.y;
            rz = t.rotation.z;
            rw = t.rotation.w;
        }

        [JsonIgnore]
        public Vector3 Position => new Vector3(px, py, pz);

        [JsonIgnore]
        public Quaternion Rotation => new Quaternion(rx, ry, rz, rw);
    }

    [Serializable]
    public class MacroFrame
    {
        public float time;

        public TransformData body;
        public TransformData leftHand;
        public TransformData rightHand;
        public TransformData head;

        public Vector3 velocity;
    }

    [Serializable]
    public class MacroRecording
    {
        public List<MacroFrame> frames = new List<MacroFrame>();
    }

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Macro : MonoBehaviour
    {
        public Macro(IntPtr ptr) : base(ptr) { }

        public static Macro Instance;

        public static List<string> CachedMacros = new List<string>();

        public MacroRecording currentRecording = new MacroRecording();

        private MacroRecording loadedPlayback;
        private string loadedPlaybackName;

        public bool isRecording;
        public bool isPlaying;

        public float recordInterval = 0.02f;

        private float nextRecordTime;
        private float recordingStartTime;

        private float playbackTime;
        private float playbackStartTime;

        private int currentFrame;

        private float lastBindPressTime = -1f;
        private const float bindCooldown = 0.5f;

        public string selectedMacro = "Macro";

        public GameObject proximityIndicator;

        private Rigidbody rb;

        private bool rbStateStored;
        private bool oldKinematic;
        private bool oldGravity;

        private bool proximityReady;
        private float lastProximityCheck;

        private Vector3 lastVelocity;

        private static string NormalizedExtension
        {
            get
            {
                string ext = Configs.fileExtension;
                return (string.IsNullOrEmpty(ext) || ext.StartsWith(".")) ? ext : "." + ext;
            }
        }

        public virtual void Awake()
        {
            Instance = this;

            if (GorillaLocomotion.Player.Instance != null)
                rb = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody;

            RefreshMacroCache();
        }

        public virtual void Update()
        {
            HandleRecordBind();

            if (isRecording)
                RecordTick();

            if (isPlaying)
                PlaybackTick();

            CheckProximity();
        }

        public virtual void OnDestroy()
        {
            DestroyIndicator();

            if (isPlaying)
                StopPlayback();
        }

        public void HandleRecordBind()
        {
            if (!PluginConfig.recordmacro)
                return;

            string bind = CustomBinding.GetBinds("recordmacro");

            if (string.IsNullOrEmpty(bind) || bind == "UNBOUND")
                return;

            if (ControlsV2.GetControl(bind))
            {
                if (Time.time - lastBindPressTime >= bindCooldown)
                {
                    lastBindPressTime = Time.time;

                    if (!isRecording)
                        StartRecording();
                    else
                        StopRecording();
                }
            }
        }

        public void StartRecording()
        {
            if (isRecording)
                return;

            StopPlayback();

            currentRecording = new MacroRecording();
            isRecording = true;
            recordingStartTime = Time.unscaledTime;
            nextRecordTime = Time.unscaledTime;

            CreateIndicator();

            Notifacations.SendNotification("<color=green>[MACRO]</color> Started Recording");
        }

        public void StopRecording()
        {
            if (!isRecording)
                return;

            isRecording = false;

            DestroyIndicator();

            if (currentRecording != null &&
                currentRecording.frames != null &&
                currentRecording.frames.Count > 1)
            {
                SaveMacro(selectedMacro);
            }

            Notifacations.SendNotification("<color=red>[MACRO]</color> Stopped Recording");
        }

        public void RecordTick()
        {
            if (Time.unscaledTime < nextRecordTime)
                return;

            nextRecordTime = Time.unscaledTime + Mathf.Max(0.01f, recordInterval);

            if (GorillaTagger.Instance == null)
                return;

            Transform body = GorillaTagger.Instance.bodyCollider.transform;
            Transform left = GorillaTagger.Instance.leftHandTransform;
            Transform right = GorillaTagger.Instance.rightHandTransform;
            Transform head = GorillaTagger.Instance.mainCamera.transform;

            if (body == null || left == null || right == null || head == null)
                return;

            MacroFrame frame = new MacroFrame
            {
                time = Time.unscaledTime - recordingStartTime,
                body = new TransformData(body),
                leftHand = new TransformData(left),
                rightHand = new TransformData(right),
                head = new TransformData(head),
                velocity = rb != null ? rb.velocity : Vector3.zero
            };

            currentRecording.frames.Add(frame);

            if (proximityIndicator != null)
                proximityIndicator.transform.position = body.position;
        }

        public void StartPlayback(string macroName)
        {
            if (isPlaying)
                return;

            if (string.IsNullOrWhiteSpace(macroName))
                return;

            if (loadedPlayback == null || loadedPlaybackName != macroName)
            {
                LoadMacro(macroName);
                loadedPlayback = currentRecording;
                loadedPlaybackName = macroName;
            }

            if (loadedPlayback == null ||
                loadedPlayback.frames == null ||
                loadedPlayback.frames.Count < 2)
            {
                return;
            }

            currentRecording = loadedPlayback;
            playbackStartTime = Time.unscaledTime;
            playbackTime = 0f;
            currentFrame = 0;

            if (rb != null)
            {
                rbStateStored = true;
                oldKinematic = rb.isKinematic;
                oldGravity = rb.useGravity;

                rb.isKinematic = true;
                rb.useGravity = false;
                rb.detectCollisions = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            isPlaying = true;

            DestroyIndicator();

            Notifacations.SendNotification($"<color=yellow>[MACRO]</color> Playing {macroName}");
        }

        public void StopPlayback()
        {
            if (!isPlaying)
                return;

            isPlaying = false;
            playbackTime = 0f;
            currentFrame = 0;

            if (rb != null && rbStateStored)
            {
                rb.isKinematic = oldKinematic;
                rb.useGravity = oldGravity;
                rb.detectCollisions = true;
                rb.velocity = lastVelocity * 0.5f;
                rb.angularVelocity = Vector3.zero;
            }

            Notifacations.SendNotification("<color=yellow>[MACRO]</color> Playback Stopped");
        }

        public void PlaybackTick()
        {
            if (!isPlaying)
                return;

            if (currentRecording == null ||
                currentRecording.frames == null ||
                currentRecording.frames.Count < 2)
            {
                StopPlayback();
                return;
            }

            playbackTime = Time.unscaledTime - playbackStartTime;

            List<MacroFrame> frames = currentRecording.frames;

            if (playbackTime >= frames[frames.Count - 1].time)
            {
                StopPlayback();
                return;
            }

            while (currentFrame < frames.Count - 2 &&
                   frames[currentFrame + 1].time <= playbackTime)
            {
                currentFrame++;
            }

            MacroFrame a = frames[currentFrame];
            MacroFrame b = frames[currentFrame + 1];

            float length = b.time - a.time;
            float t = length <= 0f ? 1f : (playbackTime - a.time) / length;
            t = Mathf.Clamp01(t);

            ApplyFrame(a, b, t);

            if (rb != null)
            {
                Vector3 currentPos = GorillaTagger.Instance.bodyCollider.transform.position;
                Vector3 targetPos = Vector3.Lerp(a.body.Position, b.body.Position, t);
                lastVelocity = (targetPos - currentPos) / Mathf.Max(Time.deltaTime, 0.001f);
            }
        }

        public void ApplyFrame(MacroFrame a, MacroFrame b, float t)
        {
            if (GorillaTagger.Instance == null)
                return;

            Transform body = GorillaTagger.Instance.bodyCollider.transform;
            Transform left = GorillaTagger.Instance.leftHandTransform;
            Transform right = GorillaTagger.Instance.rightHandTransform;
            Transform head = GorillaTagger.Instance.mainCamera.transform;

            if (body == null || left == null || right == null || head == null)
                return;

            Vector3 targetBodyPos = Vector3.Lerp(a.body.Position, b.body.Position, t);
            Quaternion targetBodyRot = Quaternion.Slerp(a.body.Rotation, b.body.Rotation, t);

            body.position = Vector3.Lerp(body.position, targetBodyPos, 0.9f);
            body.rotation = Quaternion.Slerp(body.rotation, targetBodyRot, 0.9f);

            left.SetPositionAndRotation(
                Vector3.Lerp(a.leftHand.Position, b.leftHand.Position, t),
                Quaternion.Slerp(a.leftHand.Rotation, b.leftHand.Rotation, t)
            );

            right.SetPositionAndRotation(
                Vector3.Lerp(a.rightHand.Position, b.rightHand.Position, t),
                Quaternion.Slerp(a.rightHand.Rotation, b.rightHand.Rotation, t)
            );

            head.SetPositionAndRotation(
                Vector3.Lerp(a.head.Position, b.head.Position, t),
                Quaternion.Slerp(a.head.Rotation, b.head.Rotation, t)
            );
        }

        public void CheckProximity()
        {
            if (!PluginConfig.autoplayproximity)
            {
                proximityReady = false;
                DestroyIndicator();
                return;
            }

            if (Time.time - lastProximityCheck < 0.05f)
                return;

            lastProximityCheck = Time.time;

            if (isPlaying)
            {
                DestroyIndicator();
                return;
            }

            if (CachedMacros == null || CachedMacros.Count <= 0)
            {
                DestroyIndicator();
                return;
            }

            int index = Menu.Menu.Macro[0].stringsliderind;

            if (index < 0 || index >= CachedMacros.Count)
            {
                DestroyIndicator();
                return;
            }

            string macroName = CachedMacros[index];

            if (Issentinel(macroName))
            {
                DestroyIndicator();
                return;
            }

            if (loadedPlayback == null || loadedPlaybackName != macroName)
            {
                LoadMacro(macroName);
                loadedPlayback = currentRecording;
                loadedPlaybackName = macroName;
            }

            if (loadedPlayback == null ||
                loadedPlayback.frames == null ||
                loadedPlayback.frames.Count <= 0)
            {
                DestroyIndicator();
                return;
            }

            MacroFrame first = loadedPlayback.frames[0];
            float distance = Mathf.Clamp(PluginConfig.autoplaydistance, 0.01f, 20f);

            CreateIndicator();

            if (proximityIndicator != null)
            {
                proximityIndicator.transform.position = first.body.Position;
                float visualScale = Mathf.Max(distance * 2f, 0.05f);
                proximityIndicator.transform.localScale = Vector3.one * visualScale;
            }

            Vector3 playerPos = GorillaTagger.Instance.bodyCollider.transform.position;
            float playerDistance = Vector3.Distance(playerPos, first.body.Position);

            if (playerDistance <= distance)
            {
                if (!proximityReady)
                {
                    proximityReady = true;
                    StartPlayback(macroName);
                }
            }
            else
            {
                proximityReady = false;
            }
        }

        public void SaveMacro(string name)
        {
            try
            {
                if (currentRecording == null ||
                    currentRecording.frames == null ||
                    currentRecording.frames.Count <= 0)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(Configs.macroPath))
                {
                    MelonLogger.Error("SaveMacro: macroPath is null or empty");
                    return;
                }

                Directory.CreateDirectory(Configs.macroPath);

                if (string.IsNullOrWhiteSpace(name))
                    name = "Macro_" + DateTime.Now.ToString("HHmmss");

                string safeName = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
                string path = Path.Combine(Configs.macroPath, safeName + NormalizedExtension);
                string json = JsonConvert.SerializeObject(currentRecording, Formatting.Indented);

                File.WriteAllText(path, json);

                RefreshMacroCache();

                Notifacations.SendNotification($"<color=green>[MACRO]</color> Saved {safeName}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Save Error: {ex}");
            }
        }

        public void LoadMacro(string name)
        {
            try
            {
                if (Issentinel(name))
                    return;

                if (string.IsNullOrWhiteSpace(Configs.macroPath))
                {
                    MelonLogger.Warning("LoadMacro: macroPath is null or empty");
                    return;
                }

                string path = Path.Combine(Configs.macroPath, name + NormalizedExtension);

                if (!File.Exists(path))
                {
                    MelonLogger.Warning($"LoadMacro: file not found at {path}");
                    return;
                }

                string json = File.ReadAllText(path);
                currentRecording = JsonConvert.DeserializeObject<MacroRecording>(json);

                if (currentRecording == null)
                    currentRecording = new MacroRecording();
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Load Error: {ex}");
            }
        }

        public void DeleteMacro(string name)
        {
            try
            {
                if (Issentinel(name))
                    return;

                if (string.IsNullOrWhiteSpace(Configs.macroPath))
                {
                    MelonLogger.Warning("DeleteMacro: macroPath is null or empty");
                    return;
                }

                string path = Path.Combine(Configs.macroPath, name + NormalizedExtension);

                if (!File.Exists(path))
                {
                    MelonLogger.Warning($"DeleteMacro: file not found at {path}");
                    return;
                }

                File.Delete(path);

                RefreshMacroCache();

                Notifacations.SendNotification($"<color=red>[MACRO]</color> Deleted {name}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Delete Error: {ex}");
            }
        }

        public static void RefreshMacroCache()
        {
            try
            {
                CachedMacros = GetMacros().ToList();

                if (Menu.Menu.Macro == null ||
                    Menu.Menu.Macro.Length <= 0 ||
                    Menu.Menu.Macro[0] == null)
                    return;

                Menu.Menu.Macro[0].StringArray = CachedMacros.ToArray();

                if (Menu.Menu.Macro[0].stringsliderind >= CachedMacros.Count)
                    Menu.Menu.Macro[0].stringsliderind = 0;
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Refresh Error: {ex}");
            }
        }

        public static string[] GetMacros()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Configs.macroPath))
                {
                    MelonLogger.Warning("GetMacros: macroPath is null or empty");
                    return new string[] { "No Macros" };
                }

                Directory.CreateDirectory(Configs.macroPath);

                string ext = NormalizedExtension;

                if (string.IsNullOrWhiteSpace(ext))
                {
                    MelonLogger.Warning("GetMacros: fileExtension is null or empty");
                    return new string[] { "No Macros" };
                }

                string[] files = Directory.GetFiles(Configs.macroPath, "*" + ext);

                if (files.Length == 0)
                    return new string[] { "No Macros" };

                List<string> macros = new List<string>();

                foreach (string file in files)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    if (!string.IsNullOrWhiteSpace(name))
                        macros.Add(name);
                }

                return macros.Count > 0 ? macros.ToArray() : new string[] { "No Macros" };
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"GetMacros Error: {ex}");
                return new string[] { "Error" };
            }
        }

        private static bool Issentinel(string name)
        {
            return string.IsNullOrWhiteSpace(name) ||
                   name == "No Macros" ||
                   name == "Error";
        }

        public void CreateIndicator()
        {
            if (!PluginConfig.autoplayproximity && !isRecording)
            {
                DestroyIndicator();
                return;
            }

            if (proximityIndicator != null)
            {
                Collider existing = proximityIndicator.GetComponent<Collider>();
                if (existing != null)
                    Destroy(existing);
                return;
            }

            proximityIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            proximityIndicator.name = "Macro Indicator";

            Collider col = proximityIndicator.GetComponent<Collider>();
            if (col != null)
                Destroy(col);

            Rigidbody sphereRb = proximityIndicator.GetComponent<Rigidbody>();
            if (sphereRb != null)
                Destroy(sphereRb);

            Renderer renderer = proximityIndicator.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = new Color(1f, 0f, 1f, 0.35f);

            renderer.sharedMaterial = mat;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;

            proximityIndicator.layer = 2;
            proximityIndicator.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
        
        public void DestroyIndicator()
        {
            if (proximityIndicator == null)
                return;

            Renderer renderer = proximityIndicator.GetComponent<Renderer>();
            if (renderer != null && renderer.sharedMaterial != null)
                Destroy(renderer.sharedMaterial);

            Destroy(proximityIndicator);
            proximityIndicator = null;
        }
    }
}