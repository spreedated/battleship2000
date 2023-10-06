namespace EngineLayer.Models.Ships
{
    public sealed class Destroyer : Ship
    {
        public Destroyer()
        {
            base.ShipType = nameof(Destroyer);
            base.Width = 2;
        }
    }
}
