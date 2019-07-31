using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Position
    {
        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("long")]
        public Long LongPrice { get; set; }

        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("resettablePL")]
        public string ResettablePl { get; set; }

        [JsonProperty("short")]
        public Short ShortPrice { get; set; }
    }
}
