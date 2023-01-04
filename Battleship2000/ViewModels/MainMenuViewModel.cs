using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";
        public ICommand PlayCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("connecttoserver");
        });
        public ICommand DedicatedServerCommand { get; } = new RelayCommand((c) =>
        {
            HelperFunctions.NavigateMainframeTo("dedicatedserver");
        });
        public ICommand ExitCommand { get; } = new RelayCommand((c) => { MainWindow.Instance.Close(); });
    }
}
