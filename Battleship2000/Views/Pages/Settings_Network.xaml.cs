using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Network.xaml
    /// </summary>
    public partial class Settings_Network : Page
    {
        public static Settings_Network Instance { get; private set; }
        public static SettingsNetworkViewModel Vm { get; private set; }
        public Settings_Network()
        {
            InitializeComponent();
            Instance = this;
            Vm = (SettingsNetworkViewModel)this.DataContext;
        }
    }
}
