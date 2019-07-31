using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class OrdersFilled
    {
        [JsonProperty("createTime")]
        public string CreateTime { get; set; }

        [JsonProperty("filledTime")]
        public DateTime FilledTime { get; set; }

        [JsonProperty("fillingTransactionID")]
        public string FillingTransactionId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("positionFill")]
        public string PositionFill { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("tradeOpenedID")]
        public string TradeOpenedId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("units")]
        public int Units { get; set; }
    }
}
