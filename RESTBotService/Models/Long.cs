using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Long
    {
        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("resettablePL")]
        public string ResettablePl { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty("tradeIDs")]
        public IList<int> TradeIds { get; set; }

    }
}
