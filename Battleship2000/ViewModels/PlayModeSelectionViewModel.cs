using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class PlayModeSelectionViewModel : ObservableObject
    {
        public ICommand SingleplayerCommand { get; } = new RelayCommand(() => { });
        public ICommand MultiplayerCommand { get; } = new RelayCommand(() => { ViewLogic.HelperFunctions.Navigate("multiplayer"); });
        public ICommand BackCommand { get; } = new RelayCommand(() => { ViewLogic.HelperFunctions.Navigate("mainmenu"); });
    }
}
