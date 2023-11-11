using Battleship2000.Logic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Battleship2000.ViewModels
{
    public class SettingsPlayerViewModel : ObservableObject
    {
        public string Playername
        {
            get
            {
                return RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Player.Playername;
            }
            set
            {
                RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Player.Playername = value;
                base.OnPropertyChanged(nameof(this.Playername));
            }
        }
    }
}
