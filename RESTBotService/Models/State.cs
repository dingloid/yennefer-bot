using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class State
    {

        [JsonProperty("NAV")]
        public string Nav { get; set; }

        [JsonProperty("marginAvailable")]
        public string MarginAvailable { get; set; }

        [JsonProperty("marginCloseoutMarginUsed")]
        public string MarginCloseoutMarginUsed { get; set; }

        [JsonProperty("marginCloseoutNAV")]
        public string MarginCloseoutNav { get; set; }

        [JsonProperty("marginCloseoutPercent")]
        public string MarginCloseoutPercent { get; set; }

        [JsonProperty("marginCloseoutUnrealizedPL")]
        public string MarginCloseoutUnrealizedPl { get; set; }

        [JsonProperty("marginUsed")]
        public string MarginUsed { get; set; }

        [JsonProperty("orders")]
        public IList<object> Orders { get; set; }

        [JsonProperty("positionValue")]
        public string PositionValue { get; set; }

        [JsonProperty("positions")]
        public IList<Position> Positions { get; set; }

        [JsonProperty("trades")]
        public IList<Trade> Trades { get; set; }

        [JsonProperty("unrealizedPL")]
        public string UnrealizedPl { get; set; }

        [JsonProperty("withdrawalLimit")]
        public string WithdrawalLimit { get; set; }
    }
}
