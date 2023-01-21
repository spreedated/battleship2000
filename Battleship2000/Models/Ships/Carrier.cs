namespace Battleship2000.Models.Ships
{
    internal class Carrier : Ship
    {
        public Carrier()
        {
            base.ShipType = ShipTypes.Carrier;
            base.Width = 5;
        }
    }
}
