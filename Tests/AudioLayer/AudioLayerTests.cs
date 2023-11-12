using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AudioLayer
{
    [TestFixture]
    public class AudioLayerTests
    {
        private AudioEngine audioengine;

        [SetUp]
        public void SetUp()
        {
            this.audioengine = new();
        }

        [Test]
        public void PlayUnknownFile()
        {
            bool fileUnknown = false;

            Assert.Multiple(() =>
            {
                Assert.That(AudioEngine.AreBanksLoaded, Is.False);
                Assert.That(this.audioengine.EffectVolume, Is.Not.EqualTo(default(float)));
            });

            this.audioengine.UnknownFile += (s, e) => fileUnknown = true;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    this.audioengine.PlaySoundEffect("foo");
                });
                Assert.That(fileUnknown, Is.True);
                Assert.That(AudioEngine.AreBanksLoaded, Is.False);
            });
        }

        [Test]
        public void PlayKnownFile()
        {
            bool fileUnknown = false;

            AudioBanks.Effects.Add(new() { Name = "foo.mp3", Payload = new byte[8] });

            this.audioengine.UnknownFile += (s, e) => fileUnknown = true;

            Assert.Multiple(() =>
            {
                Assert.That(fileUnknown, Is.False);
                Assert.That(AudioEngine.AreBanksLoaded, Is.False);
                Assert.DoesNotThrow(() =>
                {
                    this.audioengine.PlaySoundEffect("foo");
                });
                Assert.That(fileUnknown, Is.False);
            });
        }

        [Test]
        public void LoadAudioBanks()
        {
            bool loaded = false;
            List<string> soundlistloaded = new();
            AudioBanks.AudioBanksLoadedFinished += (s, e) => loaded = true;
            AudioBanks.SoundLoaded += (s, e) => soundlistloaded.Add(e.Soundname);

            Assert.That(AudioBanks.AreBanksLoaded, Is.False);

            Assert.DoesNotThrow(() =>
            {
                AudioBanks.LoadAudioBanks();
            });

            Assert.Multiple(() =>
            {
                Assert.That(AudioBanks.AreBanksLoaded, Is.True);
                Assert.That(loaded, Is.True);
                Assert.That(AudioBanks.Effects, Has.Count.GreaterThan(0));
                Assert.That(AudioBanks.Musics, Has.Count.GreaterThan(0));
                Assert.That(AudioBanks.GetEffectCount(), Is.GreaterThan(0));
                Assert.That(AudioBanks.GetMusicCount(), Is.GreaterThan(0));
                Assert.That(AudioBanks.GetEffectNames().Count(), Is.GreaterThan(0));
                Assert.That(AudioBanks.GetMusicNames().Count(), Is.GreaterThan(0));
                Assert.That(soundlistloaded, Has.Count.GreaterThan(0));
            });

#pragma warning disable S4158
            soundlistloaded.ForEach(x => Console.WriteLine($"Sound loaded: \"{x}\""));
#pragma warning restore S4158
        }

        [TearDown]
        public void TearDown()
        {
            AudioBanks.UnloadAudioBanks();
        }
    }
}
