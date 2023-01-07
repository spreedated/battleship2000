using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System.Windows.Input;

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
        public ICommand ExitCommand { get; } = new RelayCommand((c) => { MainWindow.Instance.Close(); });
        public ICommand SettingsCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("settings");
        });
    }
}
