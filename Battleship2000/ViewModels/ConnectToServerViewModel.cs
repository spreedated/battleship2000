using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Battleship2000.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Battleship2000.ViewModels
{
    public class ConnectToServerViewModel : ViewModelBase
    {
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        public ICommand BackCommand { get; } = new RelayCommand((c) => { MainWindow.InstanceVM.FrameSource = new Uri(HelperFunctions.GetXamlPath("mainmenu")); });
        public ICommand ConnectCommand { get; } = new RelayCommand(async (c) =>
        {
            ConnectToServer.Vm.ButtonEnabled = false;
            Debug.Print(ConnectToServer.Vm.ConnectText);
            await Task.Delay(1500);
            ConnectToServer.Vm.ButtonEnabled = true;
        });

        private bool _ButtonEnabled = true;
        public bool ButtonEnabled
        {
            get
            {
                return _ButtonEnabled;
            }
            set
            {
                _ButtonEnabled = value;
                base.OnPropertyChanged(nameof(ButtonEnabled));
            }
        }

        private string _ConnectText = "127.0.0.1";
        public string ConnectText
        {
            get
            {
                return _ConnectText;
            }
            set
            {
                _ConnectText = value;
                base.OnPropertyChanged(nameof(ConnectText));
            }
        }
    }
}
