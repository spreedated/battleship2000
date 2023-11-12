using Battleship2000.Logic;
using Battleship2000.ViewModels;
using System.Windows;
using static Battleship2000.ViewLogic.HelperFunctions;

namespace Battleship2000.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static MainWindowViewModel InstanceVM { get; private set; }
        public MainWindow()
        {
            this.InitializeComponent();
            Instance = this;
            InstanceVM = (MainWindowViewModel)this.DataContext;

            if (RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize != default)
            {
                this.Width = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize.Width;
                this.Height = RuntimeStorage.ConfigurationHandler.RuntimeConfiguration.WindowsSize.Height;
            }

            RefreshBackground();
        }
    }
}
