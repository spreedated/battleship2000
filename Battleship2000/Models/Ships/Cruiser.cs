namespace Battleship2000.Models.Ships
{
    internal class Cruiser : Ship
    {
        public Cruiser()
        {
            base.ShipType = nameof(Cruiser);
            base.Width = 3;
        }
    }
}
