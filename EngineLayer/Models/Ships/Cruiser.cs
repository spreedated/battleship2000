namespace EngineLayer.Models.Ships
{
    public sealed class Cruiser : Ship
    {
        public Cruiser()
        {
            base.ShipType = nameof(Cruiser);
            base.Width = 3;
        }
    }
}
