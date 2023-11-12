using Battleship2000.Logic;
using Battleship2000.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using static Battleship2000.ViewLogic.HelperFunctions;

namespace Battleship2000.ViewModels
{
    public class SettingsVisualViewModel : ObservableObject
    {
        public ObservableCollection<Background> Backgrounds { get; set; } = new();

        private Background _SelectedBackground;
        public Background SelectedBackground
        {
            get => this._SelectedBackground;
            set
            {
                base.SetProperty<Background>(ref this._SelectedBackground, value);
                RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Visual.Background = this._SelectedBackground.Name;
                RefreshBackground();
            }
        }
    }
}
