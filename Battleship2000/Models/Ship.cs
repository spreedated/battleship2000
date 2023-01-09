using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship2000.Models
{
    internal abstract class Ship
    {
        public enum ShipTypes
        {
            Carrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }
        public ShipTypes ShipType { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Width;
            }
        }
    }
}
