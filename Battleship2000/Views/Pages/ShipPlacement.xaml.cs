using Battleship2000.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
