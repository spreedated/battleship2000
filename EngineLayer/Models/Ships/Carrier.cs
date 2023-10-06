namespace EngineLayer.Models.Ships
{
    public sealed class Carrier : Ship
    {
        public Carrier()
        {
            base.ShipType = nameof(Carrier);
            base.Width = 5;
        }
    }
}
