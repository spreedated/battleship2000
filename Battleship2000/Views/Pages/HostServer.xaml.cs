using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for HostServer.xaml
    /// </summary>
    public partial class HostServer : Page
    {
        public static HostServer Instance { get; private set; } = null;
        public static HostServerViewModel Vm { get; private set; } = null;
        public HostServer()
        {
            InitializeComponent();
            Instance = this;
            Vm = (HostServerViewModel)this.DataContext;
        }
    }
}
