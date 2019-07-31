using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class CandleRequest
    {
        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("granularity")]
        public string Granularity { get; set; }

        [JsonProperty("candles")]
        public IList<Candle> Candles { get; set; }
    }
}
