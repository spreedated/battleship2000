using Battleship2000.Views.Pages;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Battleship2000.Logic
{
    internal static class Preload
    {
        internal static event EventHandler<EventArgs> PreloadStep;
        internal static event EventHandler<EventArgs> PreloadComplete;

        internal static void Run()
        {
            Log.Information("[Preload] started");

            Task.Factory.StartNew(() =>
            {
                Views.MainWindow.Instance.Dispatcher.Invoke(() =>
                {
                    LoadPages();
                });
                PreloadStep?.Invoke(null, EventArgs.Empty);

                LoadNetworkInterfaces();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                LoadBackgrounds();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                LoadAudioFiles();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                if (ObjectStorage.Config.Audio.MusicVolume > 0.0f)
                {
                    AudioEngine.PlayMusic();
                }

                PreloadComplete?.Invoke(null, EventArgs.Empty);
            });
        }

        private static void LoadPages()
        {
            Log.Information("[Preload] Loading pages...");
            ObjectStorage.pages.Add(new ConnectToServer());
            ObjectStorage.pages.Add(new MainMenu());
            ObjectStorage.pages.Add(new HostServer());
            ObjectStorage.pages.Add(new Settings());
            ObjectStorage.pages.Add(new Settings_Player());
            ObjectStorage.pages.Add(new Settings_Network());
            ObjectStorage.pages.Add(new Settings_Appearance());
            ObjectStorage.pages.Add(new Settings_Audio());
            ObjectStorage.pages.Add(new Settings_Credits());
            ObjectStorage.pages.Add(new ShipPlacement());
            Log.Information("[Preload] Loading pages finished");
        }

        private static void LoadAudioFiles()
        {
            IEnumerable<string> soundlist = typeof(Preload).Assembly.GetManifestResourceNames().Where(x => x.ToLower().EndsWith("mp3"));

            Log.Information($"[Preload] Loading {soundlist.Count()} sounds");

            foreach (string snd in soundlist)
            {
                string[] sndplit = snd.Split('.');
                string soundname = $"{sndplit[sndplit.Length - 2]}.{sndplit[sndplit.Length - 1]}";

                using (Stream s = typeof(Preload).Assembly.GetManifestResourceStream(snd))
                {
                    if (snd.Contains("snd_effects"))
                    {
                        ObjectStorage.sounds.Add(new()
                        {
                            Name = soundname,
                            Payload = new byte[s.Length]
                        });
                        s.Read(ObjectStorage.sounds.Last().Payload, 0, ObjectStorage.sounds.Last().Payload.Length);
                    }
                    if (snd.Contains("snd_music"))
                    {
                        ObjectStorage.musics.AddLast(new Models.Music()
                        {
                            Name = soundname,
                            Payload = new byte[s.Length]
                        });
                        s.Read(ObjectStorage.musics.Last().Payload, 0, ObjectStorage.musics.Last().Payload.Length);
                    }
                }

                Log.Information($"[Preload] Sound \"{soundname}\" loaded");
            }

            Log.Information($"[Preload] Loaded successfully {soundlist.Count()} soundfiles. {ObjectStorage.musics.Count} Music files, {ObjectStorage.sounds.Count} Effect files.");
        }

        private static void LoadNetworkInterfaces()
        {
            Settings_Network.Instance.Dispatcher.Invoke(() =>
            {
                Settings_Network.Vm.NetworkInterfaces.Add(new() { IPAddress = IPAddress.Parse("0.0.0.0"), Name = "All - best option" });
            });

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Name: " + netInterface.Name);
                Console.WriteLine("Description: " + netInterface.Description);
                Console.WriteLine("Addresses: ");
                IPInterfaceProperties ipProps = netInterface.GetIPProperties();
                foreach (IPAddress addr in ipProps.UnicastAddresses.Select(x => x.Address).Where(x => x.ToString() != "127.0.0.1" && x.ToString().Count(y => y == '.') == 3))
                {
                    Settings_Network.Instance.Dispatcher.Invoke(() =>
                    {
                        Settings_Network.Vm.NetworkInterfaces.Add(new() { IPAddress = addr, Name = netInterface.Name });
                    });
                }
            }

            Settings_Network.Instance.Dispatcher.Invoke(() =>
            {
                if (Settings_Network.Vm.NetworkInterfaces.Any(x => x.IPAddress.ToString() == ObjectStorage.Config.Network.Interface))
                {
                    Settings_Network.Instance.CMB_Interface.SelectedItem = Settings_Network.Vm.NetworkInterfaces.First(x => x.IPAddress.ToString() == ObjectStorage.Config.Network.Interface);
                }
            });
        }

        private static void LoadBackgrounds()
        {
            Settings_Appearance.Instance.Dispatcher.Invoke(() =>
            {
                Settings_Appearance.Vm.Backgrounds.Add(new() { Name = "Oldschool", Filename = "battleship1-1280x736.png" });
                Settings_Appearance.Vm.Backgrounds.Add(new() { Name = "Blue", Filename = "blue.png" });
                if (Settings_Appearance.Vm.Backgrounds.Any(x => x.Name == ObjectStorage.Config.Appearance.Background))
                {
                    Settings_Appearance.Instance.CMB_Background.SelectedItem = Settings_Appearance.Vm.Backgrounds.First(x => x.Name == ObjectStorage.Config.Appearance.Background);
                }
            });
        }
    }
}
