using AudioLayer.EventArgs;
using AudioLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AudioLayer
{
    public static class AudioBanks
    {
        public static bool AreBanksLoaded { get; private set; }
        internal static LinkedList<Music> Musics { get; } = new();
        internal static List<Effect> Effects { get; } = new();

        public static event EventHandler<SoundEventArgs> SoundLoaded;
        public static event EventHandler AudioBanksLoadedFinished;
        private static bool banksLoading;

        public static int GetMusicCount()
        {
            return Musics.Count;
        }

        public static int GetEffectCount()
        {
            return Effects.Count;
        }

        public static IEnumerable<string> GetMusicNames()
        {
            return Musics.Select(x => x.Name);
        }

        public static IEnumerable<string> GetEffectNames()
        {
            return Effects.Select(x => x.Name);
        }

        public static void LoadAudioBanks()
        {
            if (banksLoading)
            {
                return;
            }

            banksLoading = true;

            IEnumerable<string> soundlist = typeof(AudioBanks).Assembly.GetManifestResourceNames().Where(x => x.ToLower().EndsWith("mp3"));

            foreach (string snd in soundlist)
            {
                string[] sndplit = snd.Split('.');
                string soundname = $"{sndplit[^2]}.{sndplit[^1]}";

                using (Stream s = typeof(AudioBanks).Assembly.GetManifestResourceStream(snd))
                {
                    if (snd.Contains("snd_effects"))
                    {
                        Effects.Add(new()
                        {
                            Name = soundname,
                            Payload = new byte[s.Length]
                        });
                        s.Read(Effects[^1].Payload, 0, Effects[^1].Payload.Length);
                    }

                    if (snd.Contains("snd_music"))
                    {
                        Musics.AddLast(new Music()
                        {
                            Name = soundname,
                            Payload = new byte[s.Length]
                        });
                        s.Read(Musics.Last.Value.Payload, 0, Musics.Last.Value.Payload.Length);
                    }
                }

                SoundLoaded?.Invoke(null, new(soundname));
            }

            banksLoading = false;
            AudioBanksLoadedFinished?.Invoke(null, System.EventArgs.Empty);
            AreBanksLoaded = true;
        }
    }
}
