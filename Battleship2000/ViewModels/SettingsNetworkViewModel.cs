using Battleship2000.Logic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Battleship2000.ViewModels
{
    public class SettingsNetworkViewModel : ObservableObject
    {
        public uint Port
        {
            get
            {
                return RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Network.Port;
            }
            set
            {
                RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Network.Port = value;
                base.OnPropertyChanged(nameof(this.Port));
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
                RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Network.Interface = this._Interface.IPAddress.ToString();
                base.OnPropertyChanged(nameof(this.Interface));
            }
        }

        public ObservableCollection<Models.NetworkInterface> NetworkInterfaces { get; set; } = new();
    }
}
