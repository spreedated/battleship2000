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
            InitializeComponent();
            Instance = this;
            Vm = (SettingsAudioViewModel)this.DataContext;

            Vm.MusicVolume = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Audio.MusicVolume;
            Vm.EffectVolume = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.Audio.EffectVolume;
        }
    }
}
