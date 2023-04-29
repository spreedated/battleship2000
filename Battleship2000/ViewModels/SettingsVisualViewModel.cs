using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using neXn.Lib.Wpf.ViewLogic;
using System.Collections.ObjectModel;

namespace Battleship2000.ViewModels
{
    public class SettingsVisualViewModel : ViewModelBase
    {
        public ObservableCollection<Background> Backgrounds { get; set; } = new();

        private Background _SelectedBackground;
        public Background SelectedBackground
        {
            get
            {
                return this._SelectedBackground;
            }
            set
            {
                this._SelectedBackground = value;
                RuntimeStorage.Config.Visual.Background = this._SelectedBackground.Name;
                base.OnPropertyChanged(nameof(SelectedBackground));
                MainWindow.Instance.RefreshBackground();
            }
        }
    }
}
