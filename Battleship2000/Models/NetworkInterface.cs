using System.Net;

namespace Battleship2000.Models
{
    public class NetworkInterface
    {
        public string Name { get; set; }
        public IPAddress IPAddress { get; set; }
        public string Display
        {
            get
            {
                return $"({this.IPAddress}) {this.Name}";
            }
        }
    }
}
