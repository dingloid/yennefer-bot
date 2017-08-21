using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class InstrumentResponse
    {
        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("granularity")]
        public string Granularity { get; set; }

        [JsonProperty("candles")]
        public IList<Candle> Candles { get; set; }
    }
}
