namespace Battleship2000.Models
{
    internal class Battleship : Ship
    {
        public Battleship()
        {
            base.ShipType = ShipTypes.Battleship;
            base.Width = 5;
        }
    }
}
