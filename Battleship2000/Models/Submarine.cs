namespace Battleship2000.Models
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
