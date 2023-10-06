using Battleship2000.Logic;
using neXn.Lib.Wpf.ViewLogic;

namespace Battleship2000.ViewModels
{
    public class SettingsPlayerViewModel : ViewModelBase
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
