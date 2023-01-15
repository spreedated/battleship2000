using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public static Settings Instance { get; private set; }
        public static SettingsViewModel Vm { get; private set; }
        private bool isFirstLoad = true;
        public Settings()
        {
            InitializeComponent();
            ((SettingsViewModel)this.DataContext).Instance = this;
            Instance = this;
            Vm = (SettingsViewModel)this.DataContext;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.isFirstLoad)
            {
                Vm.PlayerCommand.Execute(this);
                this.isFirstLoad = false;
            }
        }
    }
}
