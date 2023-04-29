using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Battleship2000.Views.Pages;
using neXn.Lib.Wpf.ViewLogic;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewModels
{
    public class HostServerViewModel : ViewModelBase
    {
        private readonly Timer animationTimer = null;
        private bool animationRunning = false;
        public NetworkServer NetworkServer { get; private set; } = null;
        public HostServerViewModel()
        {
            this.animationTimer = new()
            {
                Interval = 623
            };
            this.animationTimer.Elapsed += this.AnimationTimer_Elapsed;
        }

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            HostServer.Instance.Dispatcher.Invoke(() =>
            {
                switch (HostServer.Vm.StatusTextVisibility)
                {
                    case Visibility.Visible:
                        HostServer.Vm.StatusTextVisibility = Visibility.Hidden;
                        break;
                    case Visibility.Hidden:
                        HostServer.Vm.StatusTextVisibility = Visibility.Visible;
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

        public async Task<bool> NetworkServerStart()
        {
            this.NetworkServer?.Dispose();
            this.NetworkServer = new(RuntimeStorage.Config.Network.Interface, RuntimeStorage.Config.Network.Port);
            this.NetworkServer.BsClientConnected += this.OnBsClientConnected;
            return await this.NetworkServer.StartServerAsync();
        }

        public async Task<bool> NetworkServerStop()
        {
            if (this.NetworkServer != null)
            {
                this.NetworkServer.BsClientConnected -= this.OnBsClientConnected;
                return await this.NetworkServer?.StopServerAsync();
            }
            return true;
        }

        public ICommand StartCommand { get; } = new RelayCommand(() =>
        {
            HostServer.Vm.StatusText = "Starting ...";
            HostServer.Vm.AnimationStart();

            Task.Factory.StartNew(async () =>
            {
                bool res = await HostServer.Vm.NetworkServerStart();

                if (res)
                {
                    HostServer.Instance.Dispatcher.Invoke(() =>
                    {
                        HostServer.Vm.StatusColor = Brushes.Green;
                        HostServer.Vm.StatusText = $"Waiting for client on port {RuntimeStorage.Config.Network.Port}";
                    });
                }
                else
                {
                    HostServer.Instance.Dispatcher.Invoke(() =>
                    {
                        HostServer.Vm.StatusColor = Brushes.Red;
                        HostServer.Vm.StatusText = "Error, try again";
                    });
                    await Task.Delay(2000);
                    HostServer.Instance.Dispatcher.Invoke(() =>
                    {
                        HostServer.Vm.StopCommand.Execute(null);
                    });
                }

                HostServer.Instance.Dispatcher.Invoke(() =>
                {
                    HostServer.Vm.StartButtonVisibility = Visibility.Collapsed;
                    HostServer.Vm.StopButtonVisibility = Visibility.Visible;
                    HostServer.Vm.BackButtonEnabled = false;
                    HostServer.Vm.StatusTextVisibility = Visibility.Visible;
                });
            });
        });

        private void OnBsClientConnected(object sender, EventArgs e)
        {
            this.AnimationStop();
            this.StatusTextVisibility = Visibility.Visible;
            this.StartButtonVisibility = Visibility.Hidden;
            this.StopButtonVisibility = Visibility.Hidden;
            this.StatusText = $"Player \"{this.NetworkServer.ConnectedClient.Playername}\" connected";

            System.Threading.Thread.Sleep(1750);

            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                MainWindowViewModel.Navigate("shipplacement");
            });
        }

        public void ResetButtonStates()
        {
            this.BackButtonEnabled = true;
            this.StatusTextVisibility = Visibility.Hidden;
            this.StartButtonVisibility = Visibility.Visible;
            this.StopButtonVisibility = Visibility.Collapsed;
        }

        public ICommand StopCommand { get; } = new RelayCommand((c) =>
        {
            HostServer.Vm.StatusText = "Closing ...";

            Task.Factory.StartNew(async () =>
            {
                bool res = await HostServer.Vm.NetworkServerStop();

                if (res)
                {
                    HostServer.Instance.Dispatcher.Invoke(() =>
                    {
                        HostServer.Vm.StatusColor = Brushes.Yellow;
                        HostServer.Vm.StatusText = "Stopped";
                    });
                }

                HostServer.Instance.Dispatcher.Invoke(() =>
                {
                    HostServer.Vm.AnimationStop();
                    HostServer.Vm.StartButtonVisibility = Visibility.Visible;
                    HostServer.Vm.StopButtonVisibility = Visibility.Collapsed;
                    HostServer.Vm.BackButtonEnabled = true;
                    HostServer.Vm.StatusTextVisibility = Visibility.Hidden;
                });
            });
        });
        public ICommand BackCommand { get; } = new RelayCommand(() => { MainWindowViewModel.Navigate("mainmenu"); });

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
        private Brush _StatusColor = Brushes.Green;
        public Brush StatusColor
        {
            get
            {
                return _StatusColor;
            }
            set
            {
                _StatusColor = value;
                this.OnPropertyChanged(nameof(StatusColor));
            }
        }
    }
}
