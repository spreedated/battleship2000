﻿using Battleship2000.Logic;
using Battleship2000.ViewLogic;

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
                base.OnPropertyChanged(nameof(Playername));
            }
        }
    }
}
