﻿using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Serilog;
using Battleship2000.Models;
using Battleship2000.Logic;

namespace Battleship2000.ViewModels
{
    public class ConnectToServerViewModel : ViewModelBase
    {
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        public ICommand BackCommand { get; } = new RelayCommand((c) => { HelperFunctions.NavigateMainframeTo("mainmenu"); });
        public ICommand ConnectCommand { get; } = new RelayCommand(async (c) =>
        {
            ConnectToServer.Vm.ButtonEnabled = false;

            Log.Information($"[ConnectToServerViewModel] Trying to connect to \"{ConnectToServer.Vm.ConnectText}\"");

            await Task.Factory.StartNew(() =>
            {
                ConnectToServer.Vm.NetworkClient?.Dispose();
                ConnectToServer.Vm.NetworkClient = new();

                try
                {
                    ConnectToServer.Vm.NetworkClient.ConnectTo(ConnectToServer.Vm.ConnectText);
                    Log.Information($"[ConnectToServerViewModel] Connected successfully to \"{ConnectToServer.Vm.ConnectText}\"");
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, $"[ConnectToServerViewModel] Connection failed to \"{ConnectToServer.Vm.ConnectText}\" - ");
                }
            });

            if (!ConnectToServer.Vm.NetworkClient.IsConnected)
            {
                ConnectToServer.Vm.ButtonEnabled = true;
                return;
            }

            HelperFunctions.NavigateMainframeTo("playfield");
        });

        public NetworkClient NetworkClient { get; private set; }

        private bool _ButtonEnabled = true;
        public bool ButtonEnabled
        {
            get
            {
                return _ButtonEnabled;
            }
            set
            {
                _ButtonEnabled = value;
                base.OnPropertyChanged(nameof(ButtonEnabled));
            }
        }

        private string _ConnectText = "127.0.0.1";
        public string ConnectText
        {
            get
            {
                return _ConnectText;
            }
            set
            {
                _ConnectText = value;
                base.OnPropertyChanged(nameof(ConnectText));
            }
        }
    }
}
