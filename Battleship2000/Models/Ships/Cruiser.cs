namespace Battleship2000.Models.Ships
{
    internal class Cruiser : Ship
    {
        public Cruiser()
        {
            base.ShipType = ShipTypes.Cruiser;
            base.Width = 3;
        }
    }
}
