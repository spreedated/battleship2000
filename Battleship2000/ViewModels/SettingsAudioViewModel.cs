using AudioLayer;
using Battleship2000.Logic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class SettingsAudioViewModel : ObservableObject
    {
        public ICommand TestSoundCommand { get; } = new RelayCommand(() =>
        {
            Globals.AudioEngine.PlaySoundEffect(AudioBanks.GetEffectNames().GetRandomElement());
        });

        public ICommand PlayNextCommand { get; } = new RelayCommand(() =>
        {
            Globals.AudioEngine.NextTrack();
        });

        private float musicVolumeOldValue;

        public float MusicVolume
        {
            get
            {
                return Globals.AudioEngine.MusicVolume;
            }
            set
            {
                if (!Globals.AudioEngine.IsMusicPlaying && this.MusicVolume <= 0.0d && value != this.musicVolumeOldValue)
                {
                    Globals.AudioEngine.NextTrack();
                }
                Globals.AudioEngine.MusicVolume = value;
                Globals.ConfigurationHandler.RuntimeConfiguration.Audio.Music = value;

                base.OnPropertyChanged(nameof(this.MusicVolume));
                if (Globals.AudioEngine.IsMusicPlaying && this.MusicVolume <= 0.0d && this.MusicVolume != this.musicVolumeOldValue)
                {
                    Globals.AudioEngine.StopMusic();
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
                return Globals.AudioEngine.EffectVolume;
            }
            set
            {
                Globals.AudioEngine.EffectVolume = value;
                Globals.ConfigurationHandler.RuntimeConfiguration.Audio.Effect = value;

                base.OnPropertyChanged(nameof(this.EffectVolume));
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
