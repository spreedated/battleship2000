using Battleship2000.ViewModels;
using Serilog;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for ConnectToServer.xaml
    /// </summary>
    public partial class ConnectToServer : Page
    {
        public static ConnectToServer Instance { get; private set; }
        public static ConnectToServerViewModel Vm { get; private set; }
        public ConnectToServer()
        {
            InitializeComponent();
            Instance = this;
            Vm = (ConnectToServerViewModel)this.DataContext;
            Log.Verbose("[ConnectToServer] Page loaded");
        }
    }
}
