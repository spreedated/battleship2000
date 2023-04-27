using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using neXn.Lib.Wpf.ViewLogic;
using Serilog;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewElements
{
    /// <summary>
    /// Interaction logic for Playfield.xaml
    /// </summary>
    public partial class Playfield : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static readonly DependencyProperty ParentPageProperty = DependencyProperty.Register("ParentPage", typeof(Page), typeof(Playfield), new PropertyMetadata(null));
        public Page ParentPage
        {
            get => (Page)GetValue(ParentPageProperty);
            set => SetValue(ParentPageProperty, value);
        }

        private ICommand _FieldLeftClickOverride;
        public ICommand FieldLeftClickOverride
        {
            get
            {
                return this._FieldLeftClickOverride;
            }
            set
            {
                this._FieldLeftClickOverride = value;
                this.OnPropertyChanged(nameof(this.FieldLeftClickOverride));
            }
        }

        public ICommand FieldClickCommand { get; } = new RelayCommand<PlayfieldCellCommandArgs>((args) =>
        {
            Log.Debug($"{args.ButtonCell.Name} left click");

            args.Coords = CellnameToCoordinates(args.ButtonCell.Name);

            if (args.PlayfieldInstance.FieldLeftClickOverride != null)
            {
                args.PlayfieldInstance.FieldLeftClickOverride.Execute(args);
            }

            args.PlayfieldInstance.RefreshPlayfieldGui();
        });

        public ICommand FieldRightClickCommand { get; } = new RelayCommand<PlayfieldCellCommandArgs>((args) =>
        {
            Log.Debug($"{args.ButtonCell.Name} right click");

            Point coords = CellnameToCoordinates(args.ButtonCell.Name);

            args.PlayfieldInstance.PlayfieldLogic.Cells[(int)coords.Y, (int)coords.X].CellState = Cell.CellStates.Empty;

            Log.Debug($"Ship part removed on [{coords.Y},{coords.X}]");

            args.PlayfieldInstance.RefreshPlayfieldGui();
        });

        public void RefreshPlayfieldGui()
        {
            for (int i = 0; i < this.PlayfieldLogic.Cells.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.PlayfieldLogic.Cells.GetLength(1); ii++)
                {
                    if (this.PlayfieldLogic.Cells[ii, i].CellState == Cell.CellStates.Ship)
                    {
                        ((ButtonCell)this.stcks[ii].Children[i]).BackgroundCell = Brushes.Red;
                        continue;
                    }
                    ((ButtonCell)this.stcks[ii].Children[i]).BackgroundCell = Brushes.Transparent;
                }
            }
        }

        private readonly StackPanel[] stcks;

        public PlayfieldClassic PlayfieldLogic { get; private set; }

        public Playfield()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PlayfieldLogic = new();

            string alpha = "ABCDEFGHIJ";

            this.stcks = new StackPanel[]
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
                        Name = $"Field_{alpha[ii]}_{i + 1}",
                        Margin = new Thickness((ii == 0 ? 16 : 0), 0, 4.8, 0),
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
