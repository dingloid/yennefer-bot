using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class InstrumentResponse
    {
        [JsonProperty("candles")]
        public CandleRequest CandleRequest { get; set; }
    }
}
