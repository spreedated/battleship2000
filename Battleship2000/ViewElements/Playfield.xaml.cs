using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using Serilog;
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

namespace Battleship2000.ViewElements
{
    /// <summary>
    /// Interaction logic for Playfield.xaml
    /// </summary>
    public partial class Playfield : UserControl
    {
        public ICommand FieldClickCommand { get; } = new RelayCommand((c) =>
        {
            PlayfieldCellCommandArgs args = (PlayfieldCellCommandArgs)c;

            args.ButtonCell.BackgroundCell = Brushes.Red;

            Log.Debug($"[Playfield] {args.ButtonCell.Name} left click");

            Point coords = CellnameToCoordinates(args.ButtonCell.Name);


            args.PlayfieldInstance.PlayfieldLogic.Cells[(int)coords.X, (int)coords.Y].CellState = Cell.CellStates.Ship;

            Log.Debug($"[Playfield] Ship part placed on [{coords.X},{coords.Y}]");
        });

        public ICommand FieldRightClickCommand { get; } = new RelayCommand((c) =>
        {
            PlayfieldCellCommandArgs args = (PlayfieldCellCommandArgs)c;

            args.ButtonCell.BackgroundCell = Brushes.Transparent;

            Log.Debug($"[Playfield] {args.ButtonCell.Name} right click");

            Point coords = CellnameToCoordinates(args.ButtonCell.Name);

            args.PlayfieldInstance.PlayfieldLogic.Cells[(int)coords.X, (int)coords.Y].CellState = Cell.CellStates.Empty;

            Log.Debug($"[Playfield] Ship part removed on [{coords.X},{coords.Y}]");
        });

        public PlayfieldClassic PlayfieldLogic { get; private set; }

        public Playfield()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PlayfieldLogic = new();

            string alpha = "ABCDEFGHIJ";

            StackPanel[] stcks = new StackPanel[]
            {
                this.StckPnl_L1,
                this.StckPnl_L2,
                this.StckPnl_L3,
                this.StckPnl_L4,
                this.StckPnl_L5,
                this.StckPnl_L6,
                this.StckPnl_L7,
                this.StckPnl_L8,
                this.StckPnl_L9,
                this.StckPnl_L10
            };

            for (int i = 0; i < 10; i++)
            {
                for (int ii = 0; ii < 10; ii++)
                {
                    stcks[i].Children.Add(new ButtonCell()
                    {
                        Name = $"Field_{alpha[ii]}_{i+1}",
                        Margin = new Thickness((ii == 0 ? 16 : 0), 0 ,4.8, 0),
                        Style = (Style)Application.Current.Resources["ButtonField"],
                        Command = this.FieldClickCommand
                    });
                    stcks[i].Children.OfType<ButtonCell>().Last().CommandParameter = new PlayfieldCellCommandArgs() { ButtonCell = stcks[i].Children.OfType<ButtonCell>().Last(), PlayfieldInstance = this };
                    stcks[i].Children.OfType<ButtonCell>().Last().InputBindings.Add(new MouseBinding(this.FieldRightClickCommand, new MouseGesture(MouseAction.RightClick)) { CommandParameter = new PlayfieldCellCommandArgs() { ButtonCell = stcks[i].Children.OfType<ButtonCell>().Last(), PlayfieldInstance = this } });
                }
            }
            
        }

        private static Point CellnameToCoordinates(string cellname)
        {
            string alpha = "ABCDEFGHIJ";
            int xCoord = Array.IndexOf<char>(alpha.ToCharArray(), cellname.Substring(cellname.IndexOf('_') + 1, 1)[0]);
            int yCoord = Convert.ToInt32(cellname.Substring(cellname.LastIndexOf('_') + 1)) - 1;

            return new Point(xCoord, yCoord);
        }
    }
}
