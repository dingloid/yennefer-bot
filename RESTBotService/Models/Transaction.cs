using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Transaction
    {

        [JsonProperty("accountID")]
        public string AccountID { get; set; }

        [JsonProperty("batchID")]
        public string BatchID { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("positionFill")]
        public string PositionFill { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("userID")]
        public string UserID { get; set; }

        [JsonProperty("accountBalance")]
        public string AccountBalance { get; set; }

        [JsonProperty("financing")]
        public string Financing { get; set; }

        [JsonProperty("orderID")]
        public string OrderId { get; set; }

        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("tradeOpened")]
        public TradeOpened TradeOpened { get; set; }
    }

}
