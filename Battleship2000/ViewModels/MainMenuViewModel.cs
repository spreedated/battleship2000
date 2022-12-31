using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Battleship2000.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";
        public ICommand PlayCommand { get; } = new RelayCommand((c) =>
        {
            MainWindow.InstanceVM.BackgroundVis = System.Windows.Visibility.Visible;
            MainWindow.InstanceVM.FrameSource = new Uri("pack://application:,,,/" + "views/pages/Playfield.xaml");
        });
        public ICommand ExitCommand { get; } = new RelayCommand((c) => { MainWindow.Instance.Close(); });
    }
}
