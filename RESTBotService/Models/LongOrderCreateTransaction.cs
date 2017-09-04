using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class LongOrderCreateTransaction
    {
        [JsonProperty("acconutID")]
        public string AccountId { get; set; }

        [JsonProperty("batchID")]
        public string BatchId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("longPositionCloseout")]
        public LongPositionCloseout LongPositionCloseout { get; set; }

        [JsonProperty("positionFill")]
        public string PositionFill { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("userID")]
        public int UserId { get; set; }
    }
}
