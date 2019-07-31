using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class TradesClosed
    {
        [JsonProperty("financing")]
        public string Financing { get; set; }

        [JsonProperty("realizedPL")]
        public string RealizedPl { get; set; }

        [JsonProperty("tradeID")]
        public string TradeId { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
