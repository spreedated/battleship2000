using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand ExitCommand { get; } = new RelayCommand((c) => { MainWindow.Instance.Close(); });
    }
}
