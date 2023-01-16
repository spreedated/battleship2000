using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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

        public enum MenuCategories
        {
            None = 0,
            Player,
            Network,
            Visual,
            Audio,
            Credits
        }

        public static void SetArrowVisibility(Settings settingsInstance, MenuCategories menuItem)
        {
            if (settingsInstance == null)
            {
                Log.Error($"[SettingsViewModel][SetArrowVisibility] Parameter {nameof(settingsInstance)} is null");
                return;
            }
            ((SettingsViewModel)(settingsInstance).DataContext).PlayerArrowVisibility = Visibility.Hidden;
            ((SettingsViewModel)(settingsInstance).DataContext).NetworkArrowVisibility = Visibility.Hidden;
            ((SettingsViewModel)(settingsInstance).DataContext).VisualArrowVisibility = Visibility.Hidden;
            ((SettingsViewModel)(settingsInstance).DataContext).AudioArrowVisibility = Visibility.Hidden;
            ((SettingsViewModel)(settingsInstance).DataContext).CreditsArrowVisibility = Visibility.Hidden;

            switch (menuItem)
            {
                case MenuCategories.Player:
                    ((SettingsViewModel)(settingsInstance).DataContext).PlayerArrowVisibility = Visibility.Visible;
                    break;
                case MenuCategories.Network:
                    ((SettingsViewModel)(settingsInstance).DataContext).NetworkArrowVisibility = Visibility.Visible;
                    break;
                case MenuCategories.Visual:
                    ((SettingsViewModel)(settingsInstance).DataContext).VisualArrowVisibility = Visibility.Visible;
                    break;
                case MenuCategories.Audio:
                    ((SettingsViewModel)(settingsInstance).DataContext).AudioArrowVisibility = Visibility.Visible;
                    break;
                case MenuCategories.Credits:
                    ((SettingsViewModel)(settingsInstance).DataContext).CreditsArrowVisibility = Visibility.Visible;
                    break;
            }
        }

        public static void Navigate(Settings settingsInstance, string pagename)
        {
            Page p = ObjectStorage.pages.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(pagename.ToLower()));

            if (p == null)
            {
                Log.Warning($"[SettingsViewModel][NavigateSettingsframeTo] Cannot find page \"{pagename}\"");
                return;
            }

            ((SettingsViewModel)(settingsInstance).DataContext).CurrentFramePage = p;

            Log.Information($"[SettingsViewModel][NavigateSettingsframeTo] Navigated to \"{pagename}\" page");
        }

        public ICommand PlayerCommand { get; } = new RelayCommand((c) =>
        {
            Navigate((Settings)c, "settings_player");
            SetArrowVisibility((Settings)c, MenuCategories.Player);
        });
        public ICommand NetworkCommand { get; } = new RelayCommand((c) =>
        {
            Navigate((Settings)c, "settings_network");
            SetArrowVisibility((Settings)c, MenuCategories.Network);
        });
        public ICommand VisualCommand { get; } = new RelayCommand((c) =>
        {
            Navigate((Settings)c, "settings_visual");
            SetArrowVisibility((Settings)c, MenuCategories.Visual);
        });
        public ICommand AudioCommand { get; } = new RelayCommand((c) =>
        {
            Navigate((Settings)c, "settings_audio");
            SetArrowVisibility((Settings)c, MenuCategories.Audio);
        });
        public ICommand CreditsCommand { get; } = new RelayCommand((c) =>
        {
            Navigate((Settings)c, "settings_credits");
            SetArrowVisibility((Settings)c, MenuCategories.Credits);
        });
        public ICommand BackCommand { get; } = new RelayCommand((c) =>
        {
            MainWindowViewModel.Navigate("mainmenu");
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

                ObjectStorage.ConfigurationHandler.Save();
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

        private Visibility _VisualArrowVisibility = Visibility.Hidden;
        public Visibility VisualArrowVisibility
        {
            get
            {
                return _VisualArrowVisibility;
            }
            set
            {
                _VisualArrowVisibility = value;
                base.OnPropertyChanged(nameof(VisualArrowVisibility));
            }
        }

        private Visibility _AudioArrowVisibility = Visibility.Hidden;
        public Visibility AudioArrowVisibility
        {
            get
            {
                return _AudioArrowVisibility;
            }
            set
            {
                _AudioArrowVisibility = value;
                base.OnPropertyChanged(nameof(AudioArrowVisibility));
            }
        }

        private Visibility _CreditsArrowVisibility = Visibility.Hidden;
        public Visibility CreditsArrowVisibility
        {
            get
            {
                return _CreditsArrowVisibility;
            }
            set
            {
                _CreditsArrowVisibility = value;
                base.OnPropertyChanged(nameof(CreditsArrowVisibility));
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

        private Settings _Instance;
        public Settings Instance
        {
            get
            {
                return this._Instance;
            }
            set
            {
                this._Instance = value;
                base.OnPropertyChanged(nameof(this.Instance));
            }
        }

        private Page _CurrentFramePage;
        public Page CurrentFramePage
        {
            get
            {
                return this._CurrentFramePage;
            }
            set
            {
                this._CurrentFramePage = value;
                base.OnPropertyChanged(nameof(this.CurrentFramePage));
            }
        }
    }
}
