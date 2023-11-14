using Battleship2000.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetworkLayer.Logic;
using Serilog;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class MultiplayerViewModel : ObservableObject
    {
        public ICommand HostServerCommand { get; } = new RelayCommand(() => ViewLogic.HelperFunctions.Navigate("hostserver"));
        public ICommand BackCommand { get; } = new RelayCommand(() => { ViewLogic.HelperFunctions.Navigate("playmodeselection"); });
        public ICommand ConnectCommand { get; } = new RelayCommand(async () =>
        {
            Multiplayer.Vm.ButtonHostEnabled = false;
            Multiplayer.Vm.ButtonEnabled = false;
            Multiplayer.Vm.StatusText = "Connecting ...";
            Multiplayer.Vm.StatusTextVisibility = Visibility.Visible;

            Log.Information($"Trying to connect to \"{Multiplayer.Vm.ConnectText}\"");

            await Task.Factory.StartNew(async () =>
            {
                Multiplayer.Vm.NetworkClient?.Dispose();
                Multiplayer.Vm.NetworkClient = new();

                try
                {
                    Multiplayer.Vm.NetworkClient.ConnectTo(Multiplayer.Vm.ConnectText);
                    Log.Information($"Connected successfully to \"{Multiplayer.Vm.ConnectText}\"");
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, $"Connection failed to \"{Multiplayer.Vm.ConnectText}\" - ");

                    Multiplayer.Instance.Dispatcher.Invoke(() =>
                    {
                        Multiplayer.Vm.StatusText = "Connection error";
                    });
                    await Task.Delay(1500);
                    Multiplayer.Instance.Dispatcher.Invoke(() =>
                    {
                        Multiplayer.Vm.StatusTextVisibility = Visibility.Hidden;
                    });
                }
            });

            if (!Multiplayer.Vm.NetworkClient.IsConnected)
            {
                Multiplayer.Vm.ButtonHostEnabled = true;
                Multiplayer.Vm.ButtonEnabled = true;
                return;
            }

            Multiplayer.Vm.StatusText = "Connection established!";
            await Task.Delay(1500);

            Multiplayer.Vm.StatusTextVisibility = Visibility.Hidden;

            ViewLogic.HelperFunctions.Navigate("shipplacement");
        });

        public NetworkClient NetworkClient { get; private set; }

        private bool _ButtonHostEnabled = true;
        public bool ButtonHostEnabled
        {
            get => _ButtonHostEnabled;
            set => base.SetProperty(ref this._ButtonHostEnabled, value);
        }

        private bool _ButtonEnabled = true;
        public bool ButtonEnabled
        {
            get => _ButtonEnabled;
            set => base.SetProperty(ref this._ButtonEnabled, value);
        }

        private string _ConnectText = "127.0.0.1";
        public string ConnectText
        {
            get => _ConnectText;
            set => base.SetProperty(ref this._ConnectText, value);
        }

        private string _StatusText = "Connecting ...";
        public string StatusText
        {
            get => _StatusText;
            set => base.SetProperty(ref this._StatusText, value);
        }

        private Visibility _StatusTextVisibility = Visibility.Hidden;
        public Visibility StatusTextVisibility
        {
            get => _StatusTextVisibility;
            set => base.SetProperty(ref this._StatusTextVisibility, value);
        }
    }
}
