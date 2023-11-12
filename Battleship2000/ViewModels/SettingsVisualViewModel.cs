using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Battleship2000.ViewModels
{
    public class SettingsVisualViewModel : ObservableObject
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
                RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Visual.Background = this._SelectedBackground.Name;
                base.OnPropertyChanged(nameof(this.SelectedBackground));
                MainWindow.RefreshBackground();
            }
        }
    }
}
