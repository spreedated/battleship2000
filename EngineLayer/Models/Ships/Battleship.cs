namespace EngineLayer.Models.Ships
{
    public sealed class Battleship : Ship
    {
        public Battleship()
        {
            base.ShipType = nameof(Battleship);
            base.Width = 4;
        }
    }
}
