using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class TradeOpened
    {
        [JsonProperty("tradeID")]
        public string TradeId { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
