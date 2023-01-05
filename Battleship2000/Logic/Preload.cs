using Battleship2000.Views.Pages;
using Serilog;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
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

                PreloadComplete?.Invoke(null, EventArgs.Empty);
            });
        }

        private static void LoadPages()
        {
            Log.Information("[Preload] Loading pages...");
            ObjectStorage.pages.Add(new ConnectToServer());
            ObjectStorage.pages.Add(new MainMenu());
            ObjectStorage.pages.Add(new Playfield());
            ObjectStorage.pages.Add(new DedicatedServer());
            ObjectStorage.pages.Add(new Settings());
            ObjectStorage.pages.Add(new Settings_Player());
            ObjectStorage.pages.Add(new Settings_Network());
            ObjectStorage.pages.Add(new Settings_Appearance());
            Log.Information("[Preload] Loading pages finished");
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
