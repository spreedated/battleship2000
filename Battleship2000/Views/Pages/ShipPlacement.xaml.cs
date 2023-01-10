using Battleship2000.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for ShipPlacement.xaml
    /// </summary>
    public partial class ShipPlacement : Page
    {
        public static ShipPlacement Instance { get; set; }
        public static ShipPlacementViewModel Vm { get; set; }
        public ShipPlacement()
        {
            InitializeComponent();
            Instance = this;
            Vm = (ShipPlacementViewModel)this.DataContext;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.TXT_Playername.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }
    }
}
