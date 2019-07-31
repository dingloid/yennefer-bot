using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class AccountDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdTime")]
        public string CreatedTime { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("createdByUserID")]
        public int CreatedByUserID { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("marginRate")]
        public string MarginRate { get; set; }

        [JsonProperty("hedgingEnabled")]
        public bool HedgingEnabled { get; set; }

        [JsonProperty("lastTransactionID")]
        public string LastTransactionId { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("openTradeCount")]
        public int OpenTradeCount { get; set; }

        [JsonProperty("openPositionCount")]
        public int OpenPositionCount { get; set; }

        [JsonProperty("pendingOrderCount")]
        public int PendingOrderCount { get; set; }

        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("resettablePL")]
        public string ResettablePL { get; set; }

        [JsonProperty("financing")]
        public string Financing { get; set; }

        [JsonProperty("commission")]
        public string Commission { get; set; }

        [JsonProperty("orders")]
        public IList<object> Orders { get; set; }

        [JsonProperty("positions")]
        public IList<object> Positions { get; set; }

        [JsonProperty("trades")]
        public IList<object> Trades { get; set; }

        [JsonProperty("unrealizedPL")]
        public string UnrealizedPL { get; set; }

        [JsonProperty("NAV")]
        public string NAV { get; set; }

        [JsonProperty("marginUsed")]
        public string MarginUsed { get; set; }

        [JsonProperty("marginAvailable")]
        public string MarginAvailable { get; set; }

        [JsonProperty("positionValue")]
        public string PositionValue { get; set; }

        [JsonProperty("marginCloseoutUnrealizedPL")]
        public string MarginCloseoutUnrealizedPL { get; set; }

        [JsonProperty("marginCloseoutNAV")]
        public string MarginCloseoutNAV { get; set; }

        [JsonProperty("marginCloseoutMarginUsed")]
        public string MarginCloseoutMarginUsed { get; set; }

        [JsonProperty("marginCloseoutPositionValue")]
        public string MarginCloseoutPositionValue { get; set; }

        [JsonProperty("marginCloseoutPercent")]
        public string MarginCloseoutPercent { get; set; }

        [JsonProperty("withdrawalLimit")]
        public string WithdrawalLimit { get; set; }

        [JsonProperty("marginCallMarginUsed")]
        public string MarginCallMarginUsed { get; set; }

        [JsonProperty("marginCallPercent")]
        public string MarginCallPercent { get; set; }
    }
}
