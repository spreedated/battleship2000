namespace Battleship2000.Models.Ships
{
    internal class Destroyer : Ship
    {
        public Destroyer()
        {
            base.ShipType = nameof(Destroyer);
            base.Width = 2;
        }
    }
}
