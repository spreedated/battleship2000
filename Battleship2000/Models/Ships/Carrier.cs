namespace Battleship2000.Models.Ships
{
    internal class Carrier : Ship
    {
        public Carrier()
        {
            base.ShipType = nameof(Carrier);
            base.Width = 5;
        }
    }
}
