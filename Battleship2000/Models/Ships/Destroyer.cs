namespace Battleship2000.Models.Ships
{
    internal class Destroyer : Ship
    {
        public Destroyer()
        {
            base.ShipType = ShipTypes.Destroyer;
            base.Width = 2;
        }
    }
}
