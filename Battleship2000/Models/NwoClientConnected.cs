using System;
using Newtonsoft.Json;

namespace Battleship2000.Models
{
    public class NwoClientConnected
    {
        [JsonProperty("appname")]
        public string AppName
        {
            get
            {
                return typeof(NwoClientConnected).Assembly.GetName().Name;
            }
            set
            {
                _ = value;
            }
        }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("playername")]
        public string Playername { get; set; }

        [JsonIgnore()]
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(this, Formatting.None);
            }
        }
    }
}
