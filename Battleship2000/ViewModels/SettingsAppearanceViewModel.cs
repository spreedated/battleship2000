using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System.Collections.ObjectModel;

namespace Battleship2000.ViewModels
{
    public class SettingsAppearanceViewModel : ViewModelBase
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
                ObjectStorage.Config.Appearance.Background = this._SelectedBackground.Name;
                base.OnPropertyChanged(nameof(SelectedBackground));
                MainWindow.Instance.RefreshBackground();
            }
        }
    }
}
