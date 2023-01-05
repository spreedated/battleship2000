using Newtonsoft.Json;
using System;

namespace Battleship2000.Models
{
    internal class Configuration
    {
        [JsonProperty("network")]
        public Network Network { get; set; } = new();

        [JsonProperty("player")]
        public Player Player { get; set; } = new();
    }

    internal class Network
    {
        [JsonProperty("interface")]
        public string Interface { get; set; } = "0.0.0.0";
        [JsonProperty("port")]
        public uint Port { get; set; } = 32485;
    }

    internal class Player
    {
        [JsonProperty("playername")]
        public string Playername { get; set; } = "User" + new Random().Next(100, 64000).ToString();
    }
}
