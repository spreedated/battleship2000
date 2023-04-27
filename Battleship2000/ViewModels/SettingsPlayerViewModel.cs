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
                return ObjectStorage.Config.Player.Playername;
            }
            set
            {
                ObjectStorage.Config.Player.Playername = value;
                base.OnPropertyChanged(nameof(this.Playername));
            }
        }
    }
}
