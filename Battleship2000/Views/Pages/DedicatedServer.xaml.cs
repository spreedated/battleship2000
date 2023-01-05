using Battleship2000.ViewModels;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for DedicatedServer.xaml
    /// </summary>
    public partial class DedicatedServer : Page
    {
        public static DedicatedServer Instance { get; private set; } = null;
        public static DedicatedServerViewModel Vm { get; private set; } = null;
        public DedicatedServer()
        {
            InitializeComponent();
            Instance = this;
            Vm = (DedicatedServerViewModel)this.DataContext;
        }
    }
}
