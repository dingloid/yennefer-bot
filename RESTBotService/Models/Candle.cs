using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public override string ToString()
        {
            return "\n High: " + CandleBody.HighPrice + "\n Low: " + CandleBody.LowPrice + "\n Open: " + CandleBody.OpenPrice + "\n Close: " + CandleBody.ClosePrice;
        }
    }
}
