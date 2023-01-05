using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly Timer saveIconAnimationTimer = null;
        private bool saveIconAnimationRunning = false;

        public SettingsViewModel()
        {
            this.saveIconAnimationTimer = new()
            {
                Interval = 202
            };
            this.saveIconAnimationTimer.Elapsed += this.AnimationTimer_Elapsed;
        }

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Settings.Instance.Dispatcher.Invoke(() =>
            {
                switch (Settings.Vm.SaveIconVisibility)
                {
                    case Visibility.Visible:
                        Settings.Vm.SaveIconVisibility = Visibility.Hidden;
                        break;
                    case Visibility.Hidden:
                        Settings.Vm.SaveIconVisibility = Visibility.Visible;
                        break;
                }
            });
        }

        public void SaveIconAnimationStart()
        {
            if (!this.saveIconAnimationRunning)
            {
                this.saveIconAnimationTimer.Start();
                this.saveIconAnimationRunning = true;
            }
        }

        public void SaveIconAnimationStop()
        {
            if (this.saveIconAnimationRunning)
            {
                this.saveIconAnimationTimer.Stop();
                this.saveIconAnimationRunning = false;
            }
        }

        public enum MenuItems
        {
            None = 0,
            Player,
            Network
        }

        public void SetArrow(MenuItems menuItem)
        {
            this.PlayerArrowVisibility = Visibility.Hidden;
            this.NetworkArrowVisibility = Visibility.Hidden;

            switch (menuItem)
            {
                case MenuItems.Player:
                    this.PlayerArrowVisibility = Visibility.Visible;
                    break;
                case MenuItems.Network:
                    this.NetworkArrowVisibility = Visibility.Visible;
                    break;
            }
        }

        public ICommand PlayerCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateSettingsframeTo("settings_player");
            Views.Pages.Settings.Vm.SetArrow(MenuItems.Player);
        });
        public ICommand NetworkCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateSettingsframeTo("settings_network");
            Views.Pages.Settings.Vm.SetArrow(MenuItems.Network);
        });
        public ICommand BackCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("mainmenu");
        });
        public ICommand SaveToDiskCommand { get; } = new RelayCommand((c) =>
        {
            Task.Factory.StartNew(async () =>
            {
                Settings.Instance.Dispatcher.Invoke(() =>
                {
                    Settings.Vm.ButtonSaveToDiskEnabled = false;
                    Settings.Vm.SaveIconAnimationStart();
                });

                Logic.Configuration.Save();
                await Task.Delay(2500);

                Settings.Instance.Dispatcher.Invoke(() =>
                {
                    Settings.Vm.SaveIconAnimationStop();
                    Settings.Vm.SaveIconVisibility = Visibility.Hidden;
                    Settings.Vm.ButtonSaveToDiskEnabled = true;
                });
            });
        });

        private Visibility _PlayerArrowVisibility = Visibility.Visible;
        public Visibility PlayerArrowVisibility
        {
            get
            {
                return _PlayerArrowVisibility;
            }
            set
            {
                _PlayerArrowVisibility = value;
                base.OnPropertyChanged(nameof(PlayerArrowVisibility));
            }
        }

        private Visibility _NetworkArrowVisibility = Visibility.Hidden;
        public Visibility NetworkArrowVisibility
        {
            get
            {
                return _NetworkArrowVisibility;
            }
            set
            {
                _NetworkArrowVisibility = value;
                base.OnPropertyChanged(nameof(NetworkArrowVisibility));
            }
        }

        private Visibility _SaveIconVisibility = Visibility.Hidden;
        public Visibility SaveIconVisibility
        {
            get
            {
                return _SaveIconVisibility;
            }
            set
            {
                _SaveIconVisibility = value;
                base.OnPropertyChanged(nameof(SaveIconVisibility));
            }
        }

        private bool _ButtonSaveToDiskEnabled = true;
        public bool ButtonSaveToDiskEnabled
        {
            get
            {
                return _ButtonSaveToDiskEnabled;
            }
            set
            {
                _ButtonSaveToDiskEnabled = value;
                base.OnPropertyChanged(nameof(ButtonSaveToDiskEnabled));
            }
        }
    }
}
