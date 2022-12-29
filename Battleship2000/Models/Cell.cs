using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship2000.Models
{
    public class Cell
    {
        public enum CellStates
        {
            Unknown = 0,
            Hit,
            Empty,
            Miss
        }

        public CellStates CellState { get; set; }
    }
}
