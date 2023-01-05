using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Appearance.xaml
    /// </summary>
    public partial class Settings_Appearance : Page
    {
        public static Settings_Appearance Instance { get; private set; }
        public static SettingsAppearanceViewModel Vm { get; private set; }
        public Settings_Appearance()
        {
            InitializeComponent();
            Instance = this;
            Vm = (SettingsAppearanceViewModel)this.DataContext;
        }
    }
}
