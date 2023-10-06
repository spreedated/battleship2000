using Battleship2000.Logic;
using MahApps.Metro.IconPacks;
using neXn.Lib.Wpf.ViewLogic;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class SettingsAudioViewModel : ViewModelBase
    {
        public ICommand TestSoundCommand { get; } = new RelayCommand(() =>
        {
            AudioEngine.PlaySoundEffect(RuntimeStorage.Sounds.GetRandomElement().Name);
        });

        public ICommand PlayNextCommand { get; } = new RelayCommand(() =>
        {
            AudioEngine.NextTrack();
        });

        private float musicVolumeOldValue;

        public float MusicVolume
        {
            get
            {
                return RuntimeStorage.Config.Audio.MusicVolume;
            }
            set
            {
                if (!AudioEngine.IsMusicPlaying && this.MusicVolume <= 0.0d && value != this.musicVolumeOldValue)
                {
                    AudioEngine.NextTrack();
                }
                RuntimeStorage.Config.Audio.MusicVolume = value;
                base.OnPropertyChanged(nameof(MusicVolume));
                if (AudioEngine.IsMusicPlaying && this.MusicVolume <= 0.0d && this.MusicVolume != this.musicVolumeOldValue)
                {
                    AudioEngine.StopMusic();
                }
                if (this.MusicVolume <= 0.0d)
                {
                    this.MusicVolumeVisibility = Visibility.Visible;
                    this.MusicVolumeIconKind = PackIconMaterialKind.VolumeOff;
                }
                else
                {
                    this.MusicVolumeVisibility = Visibility.Collapsed;
                }

                if (this.MusicVolume >= 1.0d)
                {
                    this.MusicVolumeVisibility = Visibility.Visible;
                    this.MusicVolumeIconKind = PackIconMaterialKind.VolumeHigh;
                }
                this.musicVolumeOldValue = value;
            }
        }

        public float EffectVolume
        {
            get
            {
                return RuntimeStorage.Config.Audio.EffectVolume;
            }
            set
            {
                RuntimeStorage.Config.Audio.EffectVolume = value;
                base.OnPropertyChanged(nameof(EffectVolume));
                if (this.EffectVolume <= 0.0d)
                {
                    this.EffectVolumeVisibility = Visibility.Visible;
                    this.EffectVolumeIconKind = PackIconMaterialKind.VolumeOff;
                }
                else
                {
                    this.EffectVolumeVisibility = Visibility.Collapsed;
                }

                if (this.EffectVolume >= 1.0d)
                {
                    this.EffectVolumeVisibility = Visibility.Visible;
                    this.EffectVolumeIconKind = PackIconMaterialKind.VolumeHigh;
                }
            }
        }

        private Visibility _MusicVolumeVisibility = Visibility.Collapsed;
        public Visibility MusicVolumeVisibility
        {
            get
            {
                return this._MusicVolumeVisibility;
            }
            set
            {
                this._MusicVolumeVisibility = value;
                base.OnPropertyChanged(nameof(this.MusicVolumeVisibility));
            }
        }


        private PackIconMaterialKind _MusicVolumeIconKind = PackIconMaterialKind.None;
        public PackIconMaterialKind MusicVolumeIconKind
        {
            get
            {
                return this._MusicVolumeIconKind;
            }
            set
            {
                this._MusicVolumeIconKind = value;
                base.OnPropertyChanged(nameof(this.MusicVolumeIconKind));
            }
        }

        private Visibility _EffectVolumeVisibility = Visibility.Collapsed;
        public Visibility EffectVolumeVisibility
        {
            get
            {
                return this._EffectVolumeVisibility;
            }
            set
            {
                this._EffectVolumeVisibility = value;
                base.OnPropertyChanged(nameof(this.EffectVolumeVisibility));
            }
        }


        private PackIconMaterialKind _EffectVolumeIconKind = PackIconMaterialKind.None;
        public PackIconMaterialKind EffectVolumeIconKind
        {
            get
            {
                return this._EffectVolumeIconKind;
            }
            set
            {
                this._EffectVolumeIconKind = value;
                base.OnPropertyChanged(nameof(this.EffectVolumeIconKind));
            }
        }
    }
}
