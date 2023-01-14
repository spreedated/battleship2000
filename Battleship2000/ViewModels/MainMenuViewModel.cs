using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Battleship2000.Views.Pages;
using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand PlayCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("connecttoserver");
        });

        public ICommand HostServerCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("hostserver");
        });

        public ICommand ExitCommand { get; } = new RelayCommand((c) =>
        {
            DialogWindow d = new($"Quit?", "Are you sure you want to exit?", DialogWindow.DialogButtons.Yes | DialogWindow.DialogButtons.No)
            {
                Owner = Window.GetWindow(MainWindow.Instance)
            };
            d.ShowDialog();

            if (d.DialogWindowResult == DialogWindow.DialogResults.Yes)
            {
                MainWindow.Instance.Close();
            }
        });

        public ICommand SettingsCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("settings");
        });

#if DEBUG
        public ICommand DebugCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("shipplacement");
        });
#endif

#if DEBUG
        private Visibility _DebugButtonVisibility = Visibility.Visible;
#else
        private Visibility _DebugButtonVisibility = Visibility.Collapsed;
#endif
        public Visibility DebugButtonVisibility
        {
            get
            {
                return this._DebugButtonVisibility;
            }
            set
            {
                this._DebugButtonVisibility = value;
                base.OnPropertyChanged(nameof(this.DebugButtonVisibility));
            }
        }
    }
}
