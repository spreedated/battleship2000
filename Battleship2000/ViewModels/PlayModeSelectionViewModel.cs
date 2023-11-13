using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class PlayModeSelectionViewModel : ObservableObject
    {
        public ICommand SingleplayerCommand { get; } = new RelayCommand(() => {  });
        public ICommand MultiplayerCommand { get; } = new RelayCommand(() => { MainWindowViewModel.Navigate("connecttoserver"); });
        public ICommand BackCommand { get; } = new RelayCommand(() => { MainWindowViewModel.Navigate("mainmenu"); });
    }
}
