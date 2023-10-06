namespace EngineLayer.Models.Ships
{
    public sealed class Submarine : Ship
    {
        public Submarine()
        {
            base.ShipType = nameof(Submarine);
            base.Width = 3;
        }
    }
}
