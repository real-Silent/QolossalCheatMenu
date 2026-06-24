using NLayer;
using Photon.Voice.Unity;
using Qolossal.Menu;
using Qolossal.Notifacation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Qolossal
{
    internal class Music
    {
        public static int BindMode = 0;
        public static string Subdirectory = "";
        public static readonly string[] SupportedExtensions =
        {
            ".wav",
            ".mp3"
        };
        public static string Song = "";
        public static bool AudioIsPlaying = false;
        public static float RecoverTime = -1f;
        public static AudioSource activeAudioSource;
        private static string HasNoMusic = "NoMusic";

        public static float volume;

        public static string[] GetSongFileNames()
        {
            try
            {
                string soundDirectory = Path.Combine(Configs.musicPath, Subdirectory.TrimStart('/', '\\'));
                Directory.CreateDirectory(soundDirectory);
                var files = Directory.GetFiles(soundDirectory).Where(f => SupportedExtensions.Contains(Path.GetExtension(f).ToLowerInvariant())).ToArray();

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
            switch (extension)
            {
                case ".wav":
                    byte[] soundData = File.ReadAllBytes(soundpath);
                    AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundpath));
                    if (clip != null)
                        PlayAudio(clip);
                    else
                        Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> AudioClip is null after WAV conversion.");
                    break;
                case ".mp3":
                    AudioClip clipmp3 = CreateAudioClipFromMp3(soundpath);
                    if (clipmp3 != null)
                        PlayAudio(clipmp3);
                    else
                        Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> AudioClip is null after MP3 conversion.");
                    break;
                default:
                    Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> Invalid file type.");
                    break;
            }
        }

        private static AudioClip CreateAudioClipFromMp3(string path)
        {
            try
            {
                MpegFile mpegFile = new MpegFile(path);
                int channels = mpegFile.Channels;
                int sampleRate = mpegFile.SampleRate;
                List<float> allSamples = new List<float>();
                float[] buffer = new float[16384];
                int read;
                while ((read = mpegFile.ReadSamples(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < read; i++)
                        allSamples.Add(buffer[i]);
                }
                mpegFile.Dispose();
                if (allSamples.Count == 0)
                    return null;
                float[] samples = allSamples.ToArray();
                int sampleCount = samples.Length / channels;
                AudioClip audioClip = AudioClip.Create(Path.GetFileNameWithoutExtension(path), sampleCount, channels, sampleRate, false);
                audioClip.SetData(samples, 0);
                return audioClip;
            }
            catch { return null; }
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
            Recorder recorder = GameObject.FindObjectsOfType<Recorder>().FirstOrDefault();

            if (recorder == null)
            {
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> Recorder not found.");
                return;
            }
            bool needsRestart = recorder.SourceType != Recorder.InputSourceType.AudioClip;
            recorder.SourceType = Recorder.InputSourceType.AudioClip;
            recorder.AudioClip = clip;
            recorder.LoopAudioClip = PluginConfig.loopmusic;
            if (needsRestart)
                recorder.RestartRecording();
            AudioIsPlaying = true;
            RecoverTime = Time.time + clip.length + (PluginConfig.loopmusic ? 9999f : 0.4f);
            PlayLocalAudio(clip);
        }


        private static void PlayLocalAudio(AudioClip clip)
        {
            if (activeAudioSource == null)
            {
                GameObject obj = new GameObject("Qolossal_Localizer");
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
        }

        public static void RestoreMicrophone()
        {
            Recorder recorder = GameObject.FindObjectsOfType<Recorder>().FirstOrDefault();

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

            string soundDirectory = Path.Combine(Configs.musicPath, Subdirectory.TrimStart('/', '\\'));
            string fullPath = Directory.GetFiles(soundDirectory).FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == songFileNames[songIndex]);
            if (string.IsNullOrEmpty(fullPath))
            {
                Notifacations.SendNotification("<color=red>[SOUNDBOARD]</color> Song file not found.");
                return;
            }
            LoadAndPlaySound(fullPath);
        }
    }
}