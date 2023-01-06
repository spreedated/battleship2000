using Battleship2000.ViewModels;
using Serilog;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu Instance { get; private set; }
        public MainMenuViewModel Vm { get; private set; }
        public MainMenu()
        {
            InitializeComponent();
            Log.Verbose("[MainMenu] Page loaded");
            Instance = this;
            Vm = (MainMenuViewModel)this.DataContext;
        }
    }
}
