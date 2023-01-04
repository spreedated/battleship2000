using Newtonsoft.Json;

namespace Battleship2000.Models
{
    internal class Configuration
    {
        [JsonProperty("network")]
        public Network Network { get; set; } = new();
    }

    internal class Network
    {
        [JsonProperty("interface")]
        public string Interface { get; set; } = "0.0.0.0";
        [JsonProperty("port")]
        public uint Port { get; set; } = 32485;
    }
}
