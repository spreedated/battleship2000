using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.ViewModels;
using neXn.Lib.Wpf.ViewLogic;
using Serilog;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for ShipPlacement.xaml
    /// </summary>
    public partial class ShipPlacement : Page
    {
        public ShipPlacement()
        {
            InitializeComponent();
            ((ShipPlacementViewModel)this.DataContext).Instance = this;

            this.PLF_Playfield.ParentPage = this;

            this.PLF_Playfield.FieldLeftClickOverride = new RelayCommand<PlayfieldCellCommandArgs>((args) =>
            {
                ShipPlacementViewModel sp = (ShipPlacementViewModel)((ShipPlacement)args.PlayfieldInstance.ParentPage).DataContext;

                if (sp.SelectedShip != null)
                {
                    sp.SelectedShip.Orientation = sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down ? Models.Ships.Ship.Orientations.Vertical : Models.Ships.Ship.Orientations.Horizontal;
                    sp.SelectedShip.Coordinate = args.Coords;
                    this.PLF_Playfield.PlayfieldLogic.PlaceShip(sp.SelectedShip);
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.TXT_Playername.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }
    }
}
