using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class TradeCloseResponse
    {
        [JsonProperty("lastTransactionID")]
        public string LastTransactionId { get; set; }

        [JsonProperty("longOrderCreateTransaction")]
        public LongOrderCreateTransaction LongOrderCreateTransaction { get; set; }

        [JsonProperty("longOrderFillTransaction")]
        public LongOrderFillTransaction LongOrderFillTransaction { get; set; }

        [JsonProperty("relatedTransactionIDs")]
        public IList<string> RelatedTransactionIds { get; set; }
    }
}
