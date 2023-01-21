namespace Battleship2000.Models.Ships
{
    internal class Battleship : Ship
    {
        public Battleship()
        {
            base.ShipType = ShipTypes.Battleship;
            base.Width = 4;
        }
    }
}
