using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Short
    {
        [JsonProperty("averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("resettablePL")]
        public string ResettablePl { get; set; }

        [JsonProperty("tradeIDs")]
        public IList<string> TradeIds { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
