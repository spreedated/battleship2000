using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Serilog;
using Battleship2000.Models;
using Battleship2000.Logic;
using System.Windows;
using System.Timers;

namespace Battleship2000.ViewModels
{
    public class DedicatedServerViewModel : ViewModelBase
    {
        private readonly Timer animationTimer = null;
        private bool animationRunning = false;
        private NetworkServer networkserver = null;
        public DedicatedServerViewModel()
        {
            this.animationTimer = new()
            {
                Interval = 623
            };
            this.animationTimer.Elapsed += this.AnimationTimer_Elapsed;
        }

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DedicatedServer.Instance.Dispatcher.Invoke(() =>
            {
                switch (DedicatedServer.Vm.StatusTextVisibility)
                {
                    case Visibility.Visible:
                        DedicatedServer.Vm.StatusTextVisibility = Visibility.Hidden;
                        break;
                    case Visibility.Hidden:
                        DedicatedServer.Vm.StatusTextVisibility = Visibility.Visible;
                        break;
                }
            });
        }

        public void AnimationStart()
        {
            if (!this.animationRunning)
            {
                this.animationTimer.Start();
                this.animationRunning = true;
            }
        }

        public void AnimationStop()
        {
            if (this.animationRunning)
            {
                this.animationTimer.Stop();
                this.animationRunning = false;
            }
        }

        public void NetworkServerStart()
        {
            this.networkserver?.Dispose();
            this.networkserver = new();
            this.networkserver.StartServer();
        }

        public void NetworkServerStop()
        {
            this.networkserver?.Dispose();
        }

        public ICommand StartCommand { get; } = new RelayCommand((c) =>
        {
            DedicatedServer.Vm.StartButtonVisibility = Visibility.Collapsed;
            DedicatedServer.Vm.StopButtonVisibility = Visibility.Visible;
            DedicatedServer.Vm.BackButtonEnabled = false;
            DedicatedServer.Vm.StatusTextVisibility = Visibility.Visible;

            DedicatedServer.Vm.StatusText = "Starting ...";
            DedicatedServer.Vm.AnimationStart();

            DedicatedServer.Vm.NetworkServerStart();
            DedicatedServer.Vm.StatusText = "Ready for connections";
        });
        public ICommand StopCommand { get; } = new RelayCommand((c) =>
        {
            DedicatedServer.Vm.StatusText = "Closing ...";
            DedicatedServer.Vm.NetworkServerStop();

            DedicatedServer.Vm.AnimationStop();

            DedicatedServer.Vm.StartButtonVisibility = Visibility.Visible;
            DedicatedServer.Vm.StopButtonVisibility = Visibility.Collapsed;
            DedicatedServer.Vm.BackButtonEnabled = true;
            DedicatedServer.Vm.StatusTextVisibility = Visibility.Hidden;
        });
        public ICommand BackCommand { get; } = new RelayCommand((c) => { HelperFunctions.NavigateMainframeTo("mainmenu"); });

        private string _StatusText = "Starting ...";
        public string StatusText
        {
            get
            {
                return this._StatusText;
            }
            set
            {
                this._StatusText = value;
                this.OnPropertyChanged(nameof(this.StatusText));
            }
        }

        private bool _BackButtonEnabled = true;
        public bool BackButtonEnabled
        {
            get
            {
                return _BackButtonEnabled;
            }
            set
            {
                _BackButtonEnabled = value;
                this.OnPropertyChanged(nameof(BackButtonEnabled));
            }
        }
        private Visibility _StartButtonVisibility = Visibility.Visible;
        public Visibility StartButtonVisibility
        {
            get
            {
                return _StartButtonVisibility;
            }
            set
            {
                _StartButtonVisibility = value;
                this.OnPropertyChanged(nameof(StartButtonVisibility));
            }
        }
        private Visibility _StopButtonVisibility = Visibility.Collapsed;
        public Visibility StopButtonVisibility
        {
            get
            {
                return _StopButtonVisibility;
            }
            set
            {
                _StopButtonVisibility = value;
                this.OnPropertyChanged(nameof(StopButtonVisibility));
            }
        }
        private Visibility _StatusTextVisibility = Visibility.Hidden;
        public Visibility StatusTextVisibility
        {
            get
            {
                return _StatusTextVisibility;
            }
            set
            {
                _StatusTextVisibility = value;
                this.OnPropertyChanged(nameof(StatusTextVisibility));
            }
        }
    }
}
