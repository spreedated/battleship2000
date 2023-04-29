using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using neXn.Lib.Wpf.ViewLogic;
using System.Collections.ObjectModel;

namespace Battleship2000.ViewModels
{
    public class SettingsNetworkViewModel : ViewModelBase
    {
        public uint Port
        {
            get
            {
                return RuntimeStorage.Config.Network.Port;
            }
            set
            {
                RuntimeStorage.Config.Network.Port = value;
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
                RuntimeStorage.Config.Network.Interface = this._Interface.IPAddress.ToString();
                base.OnPropertyChanged(nameof(Interface));
            }
        }

        public ObservableCollection<Models.NetworkInterface> NetworkInterfaces { get; set; } = new();
    }
}
