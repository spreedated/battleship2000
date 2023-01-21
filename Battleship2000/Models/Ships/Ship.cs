using System.Windows;

namespace Battleship2000.Models.Ships
{
    public abstract class Ship
    {
        public enum ShipTypes
        {
            Carrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }
        public enum Orientations
        {
            Vertical,
            Horizontal
        }
        public Orientations Orientation { get; set; }
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
        public Point Coordinate { get; set; }
    }
}
