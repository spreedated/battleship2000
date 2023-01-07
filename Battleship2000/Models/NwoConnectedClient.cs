using System;

namespace Battleship2000.Models
{
    public class NwoConnectedClient
    {
        public string IpPort { get; set; }
        public Version Version { get; set; }
        public DateTime ConnectedSince { get; } = DateTime.Now;
        public string Playername { get; set; }
    }
}
