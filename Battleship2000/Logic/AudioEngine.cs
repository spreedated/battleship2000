﻿using Battleship2000.Models;
using NAudio.Wave;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship2000.Logic
{
    internal static class AudioEngine
    {
        public static bool IsMusicPlaying { get; private set; }

        private static CancellationTokenSource ctMusic;

        private static LinkedListNode<Models.Music> CurrentTrack;

        private static bool stopping;

        public static void PlaySoundEffect(string soundname)
        {
            if (ObjectStorage.Config.Audio.EffectVolume <= 0.0f)
            {
                Log.Warning($"[AudioEngine] Sound muted");
                return;
            }

            if (!ObjectStorage.sounds.Any(x => x.Name.ToLower().Contains(soundname.ToLower())))
            {
                Log.Warning($"[AudioEngine] Sound not found \"{soundname}\"");
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                EffectSound ef = ObjectStorage.sounds.First(x => x.Name.ToLower().Contains(soundname.ToLower()));

                using (MemoryStream ms = new(ef.Payload))
                {
                    using (Mp3FileReader r = new(ms))
                    {
                        using (WaveOut w = new())
                        {
                            w.Volume = ObjectStorage.Config.Audio.EffectVolume;

                            w.Init(r);
                            w.Play();

                            string[] sndsplit = ef.Name.Split('.').ToArray();
                            string sndname = $"{sndsplit[sndsplit.Count() - 2]}.{sndsplit[sndsplit.Count() - 1]}";

                            Log.Information($"[AudioEngine] Playing sound \"{sndname}\"");

                            while (w.PlaybackState == PlaybackState.Playing)
                            {
                                await Task.Delay(50);
                            }

                            w.Stop();
                        }
                    }
                }
            });
        }

        public static void ButtonEnterSoundPlay(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PlaySoundEffect("button-");
        }

        public static void PlayMusic()
        {
            if (IsMusicPlaying && !stopping)
            {
                Log.Warning($"[AudioEngine] Music already playing");
                return;
            }

            ctMusic = new();

            Task.Factory.StartNew(async () =>
            {
                while (stopping)
                {
                    await Task.Delay(50);
                }

                if (CurrentTrack == null)
                {
                    CurrentTrack = ObjectStorage.musics.First;
                }

                using (MemoryStream ms = new(CurrentTrack.Value.Payload))
                {
                    using (Mp3FileReader r = new(ms))
                    {
                        using (WaveOut w = new())
                        {
                            string[] sndsplit = CurrentTrack.Value.Name.Split('.').ToArray();
                            string soundname = $"{sndsplit[sndsplit.Count() - 2]}.{sndsplit[sndsplit.Count() - 1]}";

                            w.Volume = ObjectStorage.Config.Audio.MusicVolume;

                            w.Init(r);
                            w.Play();

                            Log.Information($"[AudioEngine] Playing music \"{soundname}\"");

                            IsMusicPlaying = true;

                            while (w.PlaybackState == PlaybackState.Playing)
                            {
                                w.Volume = ObjectStorage.Config.Audio.MusicVolume;

                                if (w.Volume <= 0.0f || ctMusic.Token.IsCancellationRequested)
                                {
                                    break;
                                }

                                await Task.Delay(50);
                            }

                            w.Stop();

                            IsMusicPlaying = false;
                            Log.Information($"[AudioEngine] Music stopped");
                        }
                    }
                }
                stopping = false;

                if (!ctMusic.IsCancellationRequested && ObjectStorage.Config.Audio.MusicVolume > 0.0f)
                {
                    NextTrack();
                }
            });
        }

        public static void StopMusic()
        {
            if (IsMusicPlaying)
            {
                stopping = true;
                ctMusic.Cancel();
                Log.Information($"[AudioEngine] Music stopped");
            }
        }

        public static void NextTrack()
        {
            if (CurrentTrack != null)
            {
                if (CurrentTrack.Next == null)
                {
                    CurrentTrack = ObjectStorage.musics.First;
                }
                else
                {
                    CurrentTrack = CurrentTrack.Next;
                }
            }
            StopMusic();
            while (IsMusicPlaying)
            {
                Thread.Sleep(50);
            }
            PlayMusic();
        }
    }
}