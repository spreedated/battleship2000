using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Serilog;
using Battleship2000.Models;
using Battleship2000.Logic;
using System.Windows;

namespace Battleship2000.ViewModels
{
    public class DedicatedServerViewModel : ViewModelBase
    {
        public ICommand StartCommand { get; } = new RelayCommand((c) =>
        {
            DedicatedServer.Vm.StartButtonVisibility = Visibility.Collapsed;
            DedicatedServer.Vm.StopButtonVisibility = Visibility.Visible;
            DedicatedServer.Vm.BackButtonEnabled = false;
            DedicatedServer.Vm.StatusTextVisibility = Visibility.Visible;


        });
        public ICommand StopCommand { get; } = new RelayCommand((c) =>
        {
            DedicatedServer.Vm.StartButtonVisibility = Visibility.Visible;
            DedicatedServer.Vm.StopButtonVisibility = Visibility.Collapsed;
            DedicatedServer.Vm.BackButtonEnabled = true;
            DedicatedServer.Vm.StatusTextVisibility = Visibility.Hidden;
        });
        public ICommand BackCommand { get; } = new RelayCommand((c) => { HelperFunctions.NavigateMainframeTo("mainmenu"); });
        public string ConnectionText { get; set; } = "Starting ...";

        private bool _BackButtonEnabled = true;
        public bool BackButtonEnabled
        {
            get
            {
                return _BackButtonEnabled;
            }
            set
            {
                _BackButtonEnabled = value;
                this.OnPropertyChanged(nameof(BackButtonEnabled));
            }
        }
        private Visibility _StartButtonVisibility = Visibility.Visible;
        public Visibility StartButtonVisibility
        {
            get
            {
                return _StartButtonVisibility;
            }
            set
            {
                _StartButtonVisibility = value;
                this.OnPropertyChanged(nameof(StartButtonVisibility));
            }
        }
        private Visibility _StopButtonVisibility = Visibility.Collapsed;
        public Visibility StopButtonVisibility
        {
            get
            {
                return _StopButtonVisibility;
            }
            set
            {
                _StopButtonVisibility = value;
                this.OnPropertyChanged(nameof(StopButtonVisibility));
            }
        }
        private Visibility _StatusTextVisibility = Visibility.Hidden;
        public Visibility StatusTextVisibility
        {
            get
            {
                return _StatusTextVisibility;
            }
            set
            {
                _StatusTextVisibility = value;
                this.OnPropertyChanged(nameof(StatusTextVisibility));
            }
        }
    }
}
