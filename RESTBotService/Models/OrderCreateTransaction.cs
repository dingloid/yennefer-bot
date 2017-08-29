using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class OrderCreateTransaction
    {
        [JsonProperty("accountID")]
        public string AccountId { get; set; }

        [JsonProperty("batchID")]
        public string BatchId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
        public string Instrument { get; set; }
        public string PositionFill { get; set; }
        public string Reason { get; set; }
        public DateTime Time { get; set; }
        public string TimeInForce { get; set; }
        public string Type { get; set; }
        public string Units { get; set; }
        public int UserId { get; set; }
    }
}
