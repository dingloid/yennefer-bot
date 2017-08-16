using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Candle
    {
        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("mid")]
        public CandleBody CandleBody { get; set; }
    }
}
