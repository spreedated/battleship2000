using AudioLayer;
using Battleship2000.Views.Pages;
using Serilog;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Battleship2000.Logic.Globals;

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

                if (Globals.ConfigurationHandler.RuntimeConfiguration.Audio.Music > 0.0f)
                {
                    Globals.AudioEngine.PlayMusic();
                }

                PreloadComplete?.Invoke(null, EventArgs.Empty);
            });
        }

        private static void LoadPages()
        {
            Log.Information("Loading pages...");

            foreach (Type typePage in MyAssembly.GetTypes().Where(x => x.Namespace.Contains("Pages") && x.IsSubclassOf(typeof(Page))).Where(x => x.Name != "Preload"))
            {
                Globals.Pages.Add((Page)Activator.CreateInstance(typePage));
                Log.Information($"Loaded page \"{Globals.Pages[Globals.Pages.Count - 1].Name}\"");
            }

            Log.Information("Loading pages finished");
        }

        private static void LoadAudio()
        {
            AudioBanks.SoundLoaded += (s, e) => Log.Information($"Sound \"{e.Soundname}\" loaded");
            AudioBanks.AudioBanksLoadedFinished += (s, e) => Log.Information($"All audiobanks loaded");
            AudioBanks.LoadAudioBanks();
            Globals.AudioEngine = new()
            {
                EffectVolume = Globals.ConfigurationHandler.RuntimeConfiguration.Audio.Effect,
                MusicVolume = Globals.ConfigurationHandler.RuntimeConfiguration.Audio.Music
            };
            Globals.AudioEngine.PlayingSound += (s, e) => Log.Information($"Playing \"{e.Soundname}\" sound");
            Globals.AudioEngine.PlayingMusic += (s, e) => Log.Information($"Playing \"{e.Soundname}\" music");
            Globals.AudioEngine.StoppedMusic += (s, e) => Log.Information("Music stopped");

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
                if (Settings_Network.Vm.NetworkInterfaces.Any(x => x.IPAddress.ToString() == Globals.ConfigurationHandler.RuntimeConfiguration.Network.Interface))
                {
                    Settings_Network.Instance.CMB_Interface.SelectedItem = Settings_Network.Vm.NetworkInterfaces.First(x => x.IPAddress.ToString() == Globals.ConfigurationHandler.RuntimeConfiguration.Network.Interface);
                }
            });
        }

        private static void LoadBackgrounds()
        {
            Settings_Visual.Instance.Dispatcher.Invoke(() =>
            {
                Settings_Visual.Vm.Backgrounds.Add(new() { Name = "Oldschool", Filename = "battleship1-1280x736.png" });
                Settings_Visual.Vm.Backgrounds.Add(new() { Name = "Blue", Filename = "blue.png" });
                if (Settings_Visual.Vm.Backgrounds.Any(x => x.Name == Globals.ConfigurationHandler.RuntimeConfiguration.Visual.Background))
                {
                    Settings_Visual.Instance.CMB_Background.SelectedItem = Settings_Visual.Vm.Backgrounds.First(x => x.Name == Globals.ConfigurationHandler.RuntimeConfiguration.Visual.Background);
                }
            });
        }
    }
}
