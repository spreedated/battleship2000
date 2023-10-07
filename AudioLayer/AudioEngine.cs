using AudioLayer.EventArgs;
using AudioLayer.Models;
using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLayer
{
    public class AudioEngine
    {
        private readonly AudioVolumes volumes;
        private bool stopping;
        private CancellationTokenSource ctMusic;
        private LinkedListNode<Music> CurrentTrack;

        public event EventHandler<SoundEventArgs> PlayingSound;
        public event EventHandler<SoundEventArgs> PlayingMusic;
        public event EventHandler<SoundEventArgs> UnknownFile;
        public event EventHandler StoppedMusic;

        public static bool AreBanksLoaded
        {
            get
            {
                return AudioBanks.AreBanksLoaded;
            }
        }

        public bool IsMusicPlaying { get; private set; }

        public float EffectVolume
        {
            get
            {
                return this.volumes.Effect;
            }
            set
            {
                this.volumes.Effect = value;
            }
        }

        public float MusicVolume
        {
            get
            {
                return this.volumes.Music;
            }
            set
            {
                this.volumes.Music = value;
            }
        }

        #region Constructor
        public AudioEngine()
        {
            this.volumes = new();
        }
        #endregion

        private bool IsFileInAudioBank(string name)
        {
#pragma warning disable S3011
            IEnumerable<PropertyInfo> availableAudiobanks = typeof(AudioBanks).GetProperties(BindingFlags.Static | BindingFlags.NonPublic).Where(x => typeof(IEnumerable).IsAssignableFrom(x.PropertyType));
#pragma warning restore S3011

            if (availableAudiobanks.Select(x => (IEnumerable)x.GetValue(null)).OfType<IEnumerable<PayloadBase>>().Any(x => x.Any(y => y.Name.ToLower() == name.ToLower() || y.Name.ToLower().StartsWith(name.ToLower()))))
            {
                return true;
            }

            this.UnknownFile?.Invoke(this, new(name));
            return false;
        }

        public void PlaySoundEffect(string soundname)
        {
            if (!this.IsFileInAudioBank(soundname) && this.volumes.Effect <= 0.0f)
            {
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                Effect ef = AudioBanks.Effects.First(x => x.Name.ToLower().Contains(soundname.ToLower()));

                using (MemoryStream ms = new(ef.Payload))
                {
                    using (Mp3FileReader r = new(ms))
                    {
                        using (WaveOut w = new())
                        {
                            w.Volume = this.volumes.Effect;

                            w.Init(r);
                            w.Play();

                            string[] sndsplit = ef.Name.Split('.').ToArray();
                            string sndname = $"{sndsplit[^2]}.{sndsplit[^1]}";

                            this.PlayingSound?.Invoke(this, new(sndname));

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

        public void PlayMusic()
        {
            if (this.IsMusicPlaying && !stopping)
            {
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
                    CurrentTrack = AudioBanks.Musics.First;
                }

                using (MemoryStream ms = new(CurrentTrack.Value.Payload))
                {
                    using (Mp3FileReader r = new(ms))
                    {
                        using (WaveOut w = new())
                        {
                            string[] sndsplit = CurrentTrack.Value.Name.Split('.').ToArray();
                            string soundname = $"{sndsplit[^2]}.{sndsplit[^1]}";

                            w.Volume = this.volumes.Music;

                            w.Init(r);
                            w.Play();

                            this.PlayingMusic?.Invoke(this, new(soundname));

                            this.IsMusicPlaying = true;

                            while (w.PlaybackState == PlaybackState.Playing)
                            {
                                w.Volume = this.volumes.Music;

                                if (w.Volume <= 0.0f || ctMusic.Token.IsCancellationRequested)
                                {
                                    break;
                                }

                                await Task.Delay(50);
                            }

                            w.Stop();

                            this.IsMusicPlaying = false;

                            this.StoppedMusic?.Invoke(this, System.EventArgs.Empty);
                        }
                    }
                }
                this.stopping = false;

                if (!ctMusic.IsCancellationRequested && this.volumes.Music > 0.0f)
                {
                    this.NextTrack();
                }
            });
        }

        public void StopMusic()
        {
            if (this.IsMusicPlaying)
            {
                this.stopping = true;
                this.ctMusic?.Cancel();
                this.StoppedMusic?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public void NextTrack()
        {
            if (this.CurrentTrack != null)
            {
                if (this.CurrentTrack.Next == null)
                {
                    this.CurrentTrack = AudioBanks.Musics.First;
                }
                else
                {
                    CurrentTrack = CurrentTrack.Next;
                }
            }
            this.StopMusic();
            while (this.IsMusicPlaying)
            {
                Thread.Sleep(50);
            }
            this.PlayMusic();
        }
    }
}
