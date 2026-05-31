using Photon.Voice.Unity;
using Qolossal.Menu;
using Qolossal.Notifacation;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Qolossal
{
    internal class Music
    {
        public static int BindMode = 0;
        public static string Subdirectory = "";
        public static string fileExtension = ".wav";
        public static string Song = "";
        public static bool AudioIsPlaying = false;
        public static float RecoverTime = -1f;
        private static bool SoundLoaded = false;
        private static AudioClip downloadedSound = null;
        public static AudioSource activeAudioSource;
        private static string HasNoMusic = "NoMusic";

        public static float volume;

        public static string[] GetSongFileNames()
        {
            try
            {
                string soundDirectory = Path.Combine(Configs.musicPath, Subdirectory.TrimStart('/', '\\'));
                Directory.CreateDirectory(soundDirectory);
                string searchPattern = "*" + (fileExtension.StartsWith(".") ? fileExtension : "." + fileExtension);
                string[] files = Directory.GetFiles(soundDirectory, searchPattern);

                if (files.Length == 0)
                {
                    return new string[] { HasNoMusic };
                }

                return Array.ConvertAll(files, Path.GetFileNameWithoutExtension);
            }
            catch (Exception ex)
            {
                return new string[] { "Error : " + ex.Message };
            }
        }


        public static void LoadAndPlaySound(string soundpath)
        {
            if (!File.Exists(soundpath))
            {
                Notifacations.SendNotification($"<color=red>[SOUNDBOARD]</color> File not found: {soundpath}");
                return;
            }

            string extension = Path.GetExtension(soundpath).ToLowerInvariant();
            if (extension != ".wav")
            {
                Notifacations.SendNotification($"<color=red>[SOUNDBOARD]</color> Unsupported file format: {extension}");
                return;
            }

            byte[] soundData = File.ReadAllBytes(soundpath);
            AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundpath));
            if (clip != null)
                PlayAudio(clip);
            else
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> AudioClip is null after WAV conversion.");
        }

        private static AudioClip CreateAudioClipFromWav(byte[] wavData, string clipName)
        {
            try
            {
                if (wavData.Length < 44) return null;

                int channels = BitConverter.ToInt16(wavData, 22);
                int sampleRate = BitConverter.ToInt32(wavData, 24);
                int bitsPerSample = BitConverter.ToInt16(wavData, 34);
                int dataSize = wavData.Length - 44;
                int sampleCount = dataSize / (channels * (bitsPerSample / 8));

                AudioClip audioClip = AudioClip.Create(clipName, sampleCount, channels, sampleRate, false);
                float[] samples = new float[sampleCount * channels];

                if (bitsPerSample == 16)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        short sample = BitConverter.ToInt16(wavData, 44 + i * 2);
                        samples[i] = sample / 32768f;
                    }
                }
                else if (bitsPerSample == 8)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        byte sample = wavData[44 + i];
                        samples[i] = (sample - 128) / 128f;
                    }
                }

                audioClip.SetData(samples, 0);
                return audioClip;
            }
            catch (Exception ex)
            {
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> WAV Error " + ex.Message);
                return null;
            }
        }

        private static void PlayAudio(AudioClip clip)
        {
            if (PluginConfig.soundboard)
                PlayAudioThroughMicrophone(clip);
            else
                PlayLocalAudio(clip);
        }

        private static void PlayAudioThroughMicrophone(AudioClip clip)
        {
            Recorder recorder =
                GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ??
                GameObject.Find("Photon Manager")?.GetComponent<Recorder>();

            if (recorder == null)
            {
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> Recorder not found.");
                return;
            }

            bool needsRestart = recorder.SourceType != Recorder.InputSourceType.AudioClip;

            recorder.SourceType = Recorder.InputSourceType.AudioClip;
            recorder.AudioClip = clip;
            recorder.LoopAudioClip = PluginConfig.loopmusic;

            // 🚨 Restart ONLY if we changed source type
            if (needsRestart)
                recorder.RestartRecording();

            AudioIsPlaying = true;
            RecoverTime = Time.time + clip.length + (PluginConfig.loopmusic ? 9999f : 0.4f);
            PlayLocalAudio(clip);
            //Notifacations.SendNotification($"<color=lime>[SUCCESS]</color> Playing (Mic): {clip.name}");
        }


        private static void PlayLocalAudio(AudioClip clip)
        {
            if (activeAudioSource == null)
            {
                GameObject obj = new GameObject("LocalAudioPlayer");
                UnityEngine.Object.DontDestroyOnLoad(obj);
                activeAudioSource = obj.AddComponent<AudioSource>();
            }

            activeAudioSource.Stop();
            activeAudioSource.clip = clip;
            activeAudioSource.volume = volume;
            activeAudioSource.loop = PluginConfig.loopmusic;
            activeAudioSource.Play();

            AudioIsPlaying = true;
            RecoverTime = Time.time + clip.length + (PluginConfig.loopmusic ? 9999f : 0.4f);

            //Notifacations.SendNotification($"<color=lime>[SUCCESS]</color> Playing (Local): {clip.name} ({clip.length:F2}s)");
        }

        public static void RestoreMicrophone()
        {
            Recorder recorder =
                GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ??
                GameObject.Find("Photon Manager")?.GetComponent<Recorder>();

            if (recorder != null)
            {
                if (recorder.SourceType != Recorder.InputSourceType.Microphone)
                {
                    recorder.SourceType = Recorder.InputSourceType.Microphone;
                    recorder.AudioClip = null;
                    recorder.RestartRecording(); // only once, when switching back
                }
            }

            if (activeAudioSource != null)
                activeAudioSource.Stop();

            AudioIsPlaying = false;
            RecoverTime = -1f;
        }


        public static void StopAllSounds()
        {
            RestoreMicrophone(); // restores your mic to normal
            if (activeAudioSource != null)
            {
                activeAudioSource.Stop();
                activeAudioSource.clip = null; // optional: free memory
            }
            AudioIsPlaying = false;
            RecoverTime = -1f;

            Notifacations.SendNotification("<color=yellow>[SOUNDBOARD]</color> Audio stopped.");
        }


        public static void PlayMusic()
        {
            string[] songFileNames = GetSongFileNames();
            int songIndex = (int)Menu.Menu.MusicPlayer[0].stringsliderind;
            if (songIndex < 0 || songIndex >= songFileNames.Length)
            {
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> Invalid song index.");
                return;
            }

            string songFileName = songFileNames[songIndex] + fileExtension;
            string soundDirectory = Path.Combine(Configs.musicPath, Subdirectory.TrimStart('/', '\\'));
            string fullPath = Path.Combine(soundDirectory, songFileName);

            LoadAndPlaySound(fullPath);
        }

        public static void PlaySoundFile(string soundFileName)
        {
            string soundDirectory = Path.Combine(Configs.musicPath, Subdirectory.TrimStart('/', '\\'));
            string fullPath = Path.Combine(soundDirectory, soundFileName);
            LoadAndPlaySound(fullPath);
        }

        public static void Update()
        {
            if (AudioIsPlaying && RecoverTime > 0 && Time.time >= RecoverTime && !PluginConfig.loopmusic)
                RestoreMicrophone();
        }

        public static byte[] LoadSoundFromResource(string soundFileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourcePath = $"QolossalCheatMenuV3.Sounds.{soundFileName}";
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                    else
                    {
                        Notifacations.SendNotification($"<color=red>[SOUNDBOARD]</color> Resource not found: {resourcePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Notifacations.SendNotification($"<color=red>[SOUNDBOARD]</color> Resource load error: {ex.Message}");
            }
            return null;
        }

        public static void PlayResourceSound(string soundFileName)
        {
            try
            {
                byte[] soundData = LoadSoundFromResource(soundFileName);
                if (soundData != null && soundData.Length > 0)
                {
                    AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundFileName));
                    if (clip != null)
                        PlayAudio(clip);
                }
            }
            catch (Exception ex)
            {
                Notifacations.SendNotification($"<color=red>[SOUNDBOARD]</color> Error playing sound: {ex.Message}");
            }
        }

        public static void PlayLoadedSound()
        {
            if (downloadedSound != null && SoundLoaded)
                PlayAudio(downloadedSound);
        }

        public static void ResetLoadedSound()
        {
            SoundLoaded = false;
            downloadedSound = null;
        }

        public static void Thing()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string[] allResources = assembly.GetManifestResourceNames();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ALL EMBEDDED RESOURCES ===");
            foreach (string resource in allResources)
                sb.AppendLine(resource);
            Notifacations.SendNotification("Resource Debug" + sb.ToString());
        }
    }
}