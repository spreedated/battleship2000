using AudioLayer;
using Battleship2000.Views.Pages;
using Serilog;
using System;
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
            Log.Information("started");

            Task.Factory.StartNew(() =>
            {
                LoadAudio();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                Views.MainWindow.Instance.Dispatcher.Invoke(() =>
                {
                    LoadPages();
                });
                PreloadStep?.Invoke(null, EventArgs.Empty);

                LoadNetworkInterfaces();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                LoadBackgrounds();
                PreloadStep?.Invoke(null, EventArgs.Empty);

                if (RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Audio.Music > 0.0f)
                {
                    RuntimeStorage.AudioEngine.PlayMusic();
                }

                PreloadComplete?.Invoke(null, EventArgs.Empty);
            });
        }

        private static void LoadPages()
        {
            Log.Information("Loading pages...");
            RuntimeStorage.Pages.Add(new ConnectToServer());
            RuntimeStorage.Pages.Add(new MainMenu());
            RuntimeStorage.Pages.Add(new HostServer());
            RuntimeStorage.Pages.Add(new Settings());
            RuntimeStorage.Pages.Add(new Settings_Player());
            RuntimeStorage.Pages.Add(new Settings_Network());
            RuntimeStorage.Pages.Add(new Settings_Visual());
            RuntimeStorage.Pages.Add(new Settings_Audio());
            RuntimeStorage.Pages.Add(new Settings_Credits());
            RuntimeStorage.Pages.Add(new ShipPlacement());
            Log.Information("Loading pages finished");
        }

        private static void LoadAudio()
        {
            AudioBanks.SoundLoaded += (s, e) => Log.Information($"Sound \"{e.Soundname}\" loaded");
            AudioBanks.AudioBanksLoadedFinished += (s, e) => Log.Information($"All audiobanks loaded");
            AudioBanks.LoadAudioBanks();
            RuntimeStorage.AudioEngine = new()
            {
                EffectVolume = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Audio.Effect,
                MusicVolume = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Audio.Music
            };

            Log.Information($"Loaded successfully {AudioBanks.GetEffectCount} soundfiles. {AudioBanks.GetMusicCount} Music files, {AudioBanks.GetEffectCount} Effect files.");
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
                if (Settings_Network.Vm.NetworkInterfaces.Any(x => x.IPAddress.ToString() == RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Network.Interface))
                {
                    Settings_Network.Instance.CMB_Interface.SelectedItem = Settings_Network.Vm.NetworkInterfaces.First(x => x.IPAddress.ToString() == RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Network.Interface);
                }
            });
        }

        private static void LoadBackgrounds()
        {
            Settings_Visual.Instance.Dispatcher.Invoke(() =>
            {
                Settings_Visual.Vm.Backgrounds.Add(new() { Name = "Oldschool", Filename = "battleship1-1280x736.png" });
                Settings_Visual.Vm.Backgrounds.Add(new() { Name = "Blue", Filename = "blue.png" });
                if (Settings_Visual.Vm.Backgrounds.Any(x => x.Name == RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Visual.Background))
                {
                    Settings_Visual.Instance.CMB_Background.SelectedItem = Settings_Visual.Vm.Backgrounds.First(x => x.Name == RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Visual.Background);
                }
            });
        }
    }
}
