using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class SettingsAudioViewModel : ViewModelBase
    {
        public ICommand TestSoundCommand { get; } = new RelayCommand((c) =>
        {
            AudioEngine.PlaySoundEffect(ObjectStorage.sounds.GetRandomElement().Name);
        });

        public ICommand PlayNextCommand { get; } = new RelayCommand((c) =>
        {
            AudioEngine.NextTrack();
        });

        public float MusicVolume
        {
            get
            {
                return ObjectStorage.Config.Audio.MusicVolume;
            }
            set
            {
                ObjectStorage.Config.Audio.MusicVolume = value;
                base.OnPropertyChanged(nameof(MusicVolume));
            }
        }

        public float EffectVolume
        {
            get
            {
                return ObjectStorage.Config.Audio.EffectVolume;
            }
            set
            {
                ObjectStorage.Config.Audio.EffectVolume = value;
                base.OnPropertyChanged(nameof(EffectVolume));
            }
        }
    }
}
