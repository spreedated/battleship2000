#pragma warning disable CA1822

using neXn.Lib.Wpf.ViewLogic;

namespace Battleship2000.ViewModels
{
    public class SettingsCreditsViewModel : ViewModelBase
    {
        public string SoundCredits
        {
            get
            {
                return "button-pressed by StavSounds\nbutton by Universfield\nExplosion drop by Dasgoat\nBig impact by AudioPapkin\nSci-Fi Explosion 1 by Anomaex\nMedium Explosion by JuveriSetila";
            }
        }

        public string MusicCredits
        {
            get
            {
                return "Password Infinity by EvgenyBardyuzha\nInspirational Background by AudioCofee\nHappy Day by Sockaudios\nPrice of Freedom by Daddy s Music";
            }
        }

        public string BattleshipCredits
        {
            get
            {
                return "2022-2023 (c) entirely written by Markus Wackermann";
            }
        }
    }
}
