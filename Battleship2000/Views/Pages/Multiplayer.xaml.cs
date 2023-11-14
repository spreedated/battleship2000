using Battleship2000.ViewModels;
using Serilog;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    public partial class Multiplayer : Page
    {
        public static Multiplayer Instance { get; private set; }
        public static MultiplayerViewModel Vm { get; private set; }
        public Multiplayer()
        {
            this.InitializeComponent();
            Instance = this;
            Vm = (MultiplayerViewModel)this.DataContext;
            Log.Verbose("Page loaded");
        }
    }
}
