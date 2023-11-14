using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    public partial class Settings_Network : Page
    {
        public static Settings_Network Instance { get; private set; }
        public static SettingsNetworkViewModel Vm { get; private set; }
        public Settings_Network()
        {
            this.InitializeComponent();
            Instance = this;
            Vm = (SettingsNetworkViewModel)this.DataContext;
        }
    }
}
