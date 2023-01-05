using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Net;

namespace Battleship2000.ViewModels
{
    public class SettingsNetworkViewModel : ViewModelBase
    {
        public SettingsNetworkViewModel()
        {
            Task.Factory.StartNew(async () =>
            {
                while (Settings_Network.Vm == null || Settings_Network.Vm.NetworkInterfaces == null)
                {
                    await Task.Delay(50);
                }

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
            });
        }

        public uint Port
        {
            get
            {
                return ObjectStorage.Config.Network.Port;
            }
            set
            {
                ObjectStorage.Config.Network.Port = value;
                base.OnPropertyChanged(nameof(Port));
            }
        }

        private Models.NetworkInterface _Interface;
        public Models.NetworkInterface Interface
        {
            get
            {
                return this._Interface;
            }
            set
            {
                this._Interface = value;
                ObjectStorage.Config.Network.Interface = this._Interface.IPAddress.ToString();
                base.OnPropertyChanged(nameof(Interface));
            }
        }

        public ObservableCollection<Models.NetworkInterface> NetworkInterfaces { get; set; } = new();
    }
}
