namespace Battleship2000.Models.Ships
{
    internal class Submarine : Ship
    {
        public Submarine()
        {
            base.ShipType = ShipTypes.Submarine;
            base.Width = 3;
        }
    }
}
