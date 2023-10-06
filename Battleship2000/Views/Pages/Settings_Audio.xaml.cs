using Battleship2000.Logic;
using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Audio.xaml
    /// </summary>
    public partial class Settings_Audio : Page
    {
        public static Settings_Audio Instance { get; private set; }
        public static SettingsAudioViewModel Vm { get; private set; }
        public Settings_Audio()
        {
            this.InitializeComponent();
            Instance = this;
            Vm = (SettingsAudioViewModel)this.DataContext;

            Vm.MusicVolume = RuntimeStorage.AudioEngine.MusicVolume;
            Vm.EffectVolume = RuntimeStorage.AudioEngine.EffectVolume;
        }
    }
}
