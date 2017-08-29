using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class OrderFillTransaction
    {
        [JsonProperty("accountBalance")]
        public string AccountBalance { get; set; }

        [JsonProperty("accountID")]
        public string AccountId { get; set; }

        [JsonProperty("batchID")]
        public string BatchId { get; set; }

        [JsonProperty("financing")]
        public string Financing { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }
        public string OrderId { get; set; }
        public string Pl { get; set; }
        public string Price { get; set; }
        public string Reason { get; set; }
        public string Time { get; set; }
        public TradeOpened TradeOpened { get; set; }
        public string Type { get; set; }
        public string Units { get; set; }
        public int UserId { get; set; }
    }
}
