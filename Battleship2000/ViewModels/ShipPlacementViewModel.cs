using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class ShipPlacementViewModel : ViewModelBase
    {
        public ICommand DisconnectCommand { get; } = new RelayCommand(async (c) =>
        {
            HostServer.Vm.ResetButtonStates();
            await HostServer.Vm.NetworkServerStop();
            HelperFunctions.NavigateMainframeTo("mainmenu");
        });

        public string Playername
        {
            get
            {
                return HostServer.Vm.NetworkServer?.ConnectedClient?.Playername;
            }
        }

        private bool _ButtonDisconnectEnabled = true;
        public bool ButtonDisconnectEnabled
        {
            get
            {
                return this._ButtonDisconnectEnabled;
            }
            set
            {
                this._ButtonDisconnectEnabled = value;
                base.OnPropertyChanged(nameof(this.ButtonDisconnectEnabled));
            }
        }

        private bool _ButtonReadyEnabled;
        public bool ButtonReadyEnabled
        {
            get
            {
                return this._ButtonReadyEnabled;
            }
            set
            {
                this._ButtonReadyEnabled = value;
                base.OnPropertyChanged(nameof(this.ButtonReadyEnabled));
            }
        }
    }
}
