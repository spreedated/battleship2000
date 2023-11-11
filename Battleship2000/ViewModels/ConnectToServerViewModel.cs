﻿using Battleship2000.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetworkLayer.Logic;
using Serilog;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class ConnectToServerViewModel : ObservableObject
    {
        public ICommand BackCommand { get; } = new RelayCommand(() => { MainWindowViewModel.Navigate("mainmenu"); });
        public ICommand ConnectCommand { get; } = new RelayCommand(async () =>
        {
            ConnectToServer.Vm.ButtonEnabled = false;
            ConnectToServer.Vm.StatusText = "Connecting ...";
            ConnectToServer.Vm.StatusTextVisibility = Visibility.Visible;

            Log.Information($"Trying to connect to \"{ConnectToServer.Vm.ConnectText}\"");

            await Task.Factory.StartNew(async () =>
            {
                ConnectToServer.Vm.NetworkClient?.Dispose();
                ConnectToServer.Vm.NetworkClient = new();

                try
                {
                    ConnectToServer.Vm.NetworkClient.ConnectTo(ConnectToServer.Vm.ConnectText);
                    Log.Information($"Connected successfully to \"{ConnectToServer.Vm.ConnectText}\"");
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, $"Connection failed to \"{ConnectToServer.Vm.ConnectText}\" - ");

                    ConnectToServer.Instance.Dispatcher.Invoke(() =>
                    {
                        ConnectToServer.Vm.StatusText = "Connection error";
                    });
                    await Task.Delay(1500);
                    ConnectToServer.Instance.Dispatcher.Invoke(() =>
                    {
                        ConnectToServer.Vm.StatusTextVisibility = Visibility.Hidden;
                    });
                }
            });

            if (!ConnectToServer.Vm.NetworkClient.IsConnected)
            {
                ConnectToServer.Vm.ButtonEnabled = true;
                return;
            }

            ConnectToServer.Vm.StatusText = "Connection established!";
            await Task.Delay(1500);

            ConnectToServer.Vm.StatusTextVisibility = Visibility.Hidden;

            MainWindowViewModel.Navigate("shipplacement");
        });

        public NetworkClient NetworkClient { get; private set; }

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
