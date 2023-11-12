using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Visual.xaml
    /// </summary>
    public partial class Settings_Visual : Page
    {
        public static Settings_Visual Instance { get; private set; }
        public static SettingsVisualViewModel Vm { get; private set; }
        public Settings_Visual()
        {
            this.InitializeComponent();
            Instance = this;
            Vm = (SettingsVisualViewModel)this.DataContext;
        }
    }
}
