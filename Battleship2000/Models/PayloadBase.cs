namespace Battleship2000.Models
{
    public abstract class PayloadBase
    {
        public string Name { get; set; }
        public byte[] Payload { get; set; }
    }
}
