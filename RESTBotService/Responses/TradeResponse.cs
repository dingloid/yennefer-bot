using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class TradeResponse
    {
        [JsonProperty("lastTransactionID")]
        public string LastrTransactionId { get; set; }

        [JsonProperty("orderCreateTransaction")]
        public OrderCreateTransaction OrderCreateTransaction { get; set; }

        [JsonProperty("relatedTransactionIDs")]
        public IList<string> RelatedTransactionIds { get; set; }
    }
}
