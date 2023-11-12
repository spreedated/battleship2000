using AudioLayer.Models;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace Battleship2000.Models
{
    internal class Configuration
    {
        [JsonProperty("network")]
        public Network Network { get; set; } = new();

        [JsonProperty("player")]
        public Player Player { get; set; } = new();

        [JsonProperty("visual")]
        public Visual Visual { get; set; } = new();

        [JsonProperty("audio")]
        public AudioVolumes Audio { get; set; } = new();

        [JsonProperty("windowsSize")]
        public Size WindowsSize { get; set; }
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

    internal class Visual
    {
        [JsonProperty("background")]
        public string Background { get; set; } = "Blue";
    }
}
