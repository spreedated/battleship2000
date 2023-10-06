using Newtonsoft.Json;
using System;

namespace NetworkLayer.Models
{
    public class NwoClientConnected
    {
        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("playername")]
        public string Playername { get; set; }

        [JsonIgnore()]
        public string Json
        {
            get
            {
                return JsonConvert.SerializeObject(this, Formatting.None);
            }
        }
    }
}
