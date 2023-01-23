using Battleship2000.Models.Ships;
using Battleship2000.ViewElements;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Models
{
    internal class PlayfieldCellCommandArgs
    {
        public ButtonCell ButtonCell { get; set; }
        public Playfield PlayfieldInstance { get; set; }
        public Point Coords { get; set; }
    }
}
