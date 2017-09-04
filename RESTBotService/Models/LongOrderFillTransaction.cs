using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class LongOrderFillTransaction
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

        [JsonProperty("orderID")]
        public string OrderId { get; set; }

        [JsonProperty("pl")]
        public double Pl { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("tradesClosed")]
        public IList<TradesClosed> TradesClosed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("userID")]
        public int UserId { get; set; }
    }
}
