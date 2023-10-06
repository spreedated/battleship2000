using AudioLayer.Models;
using NAudio.Wave;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLayer
{
    public class AudioEngine
    {
        private readonly AudioVolumes volumes;
        private bool stopping;
        private CancellationTokenSource ctMusic;
        private LinkedListNode<Models.Music> CurrentTrack;

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

        public AudioEngine()
        {
            this.volumes = new();
        }

        public void PlaySoundEffect(string soundname)
        {
            if (this.volumes.Effect <= 0.0f || !AudioBanks.Effects.Exists(x => x.Name.ToLower().Contains(soundname.ToLower())))
            {
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                EffectSound ef = AudioBanks.Effects.First(x => x.Name.ToLower().Contains(soundname.ToLower()));

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
                            string sndname = $"{sndsplit[sndsplit.Count() - 2]}.{sndsplit[sndsplit.Count() - 1]}";

                            //Log.Information($"Playing sound \"{sndname}\"");

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

        //public static void ButtonEnterSoundPlay(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    PlaySoundEffect("button-");
        //}

        public void PlayMusic()
        {
            if (this.IsMusicPlaying && !stopping)
            {
                //Log.Warning($"Music already playing");
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
                            string soundname = $"{sndsplit[sndsplit.Count() - 2]}.{sndsplit[sndsplit.Count() - 1]}";

                            w.Volume = this.volumes.Music;

                            w.Init(r);
                            w.Play();

                            //Log.Information($"Playing music \"{soundname}\"");

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

                            IsMusicPlaying = false;
                            //Log.Information($"Music stopped");
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
                //Log.Information($"Music stopped");
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
