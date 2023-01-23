using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.ViewModels;
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

            this.PLF_Playfield.FieldLeftClickOverride = new RelayCommand((c) =>
            {
                PlayfieldCellCommandArgs args = (PlayfieldCellCommandArgs)c;
                ShipPlacementViewModel sp = (ShipPlacementViewModel)((ShipPlacement)args.PlayfieldInstance.ParentPage).DataContext;

                if (sp.SelectedShip != null)
                {
                    bool isValid = true;

                    for (int i = 0; i < sp.SelectedShip.Width + 1; i++)
                    {
                        if (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Right && (int)args.Coords.X + i > args.PlayfieldInstance.PlayfieldLogic.Cells.GetLength(0))
                        {
                            isValid = false;
                        }
                        if (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down && (int)args.Coords.Y + i > args.PlayfieldInstance.PlayfieldLogic.Cells.GetLength(1))
                        {
                            isValid = false;
                        }
                    }

                    if (isValid)
                    {
                        for (int i = 0; i < sp.SelectedShip.Width; i++)
                        {
                            if (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Right && args.PlayfieldInstance.PlayfieldLogic.Cells[(int)args.Coords.X + i, (int)args.Coords.Y].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                            }
                            if (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down && args.PlayfieldInstance.PlayfieldLogic.Cells[(int)args.Coords.X, (int)args.Coords.Y + i].CellState != Cell.CellStates.Empty)
                            {
                                isValid = false;
                            }
                        }
                    }

                    //TODO: Validate no ships around placed ship

                    if (isValid)
                    {
                        for (int i = 0; i < sp.SelectedShip.Width; i++)
                        {
                            args.PlayfieldInstance.PlayfieldLogic.Cells[(int)args.Coords.X + (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down ? 0 : i), (int)args.Coords.Y + (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down ? i : 0)].CellState = Cell.CellStates.Ship;
                            Log.Debug($"Ship part placed on [{args.Coords.X + (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down ? 0 : i)},{args.Coords.Y + (sp.RotationArrow == ViewElements.SelectionArrow.Rotations.Down ? i : 0)}]");
                        }
                    }
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.TXT_Playername.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }
    }
}
