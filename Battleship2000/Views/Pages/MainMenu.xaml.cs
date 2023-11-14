using Battleship2000.ViewModels;
using Serilog;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
            Log.Verbose("Page loaded");

            ((MainMenuViewModel)this.DataContext).Instance = this;
            ((MainMenuViewModel)this.DataContext).ParentWindow = (MainWindow)Application.Current.MainWindow;
        }
    }
}
