using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Trade
    {
        [JsonProperty("currentUnits")]
        public int  CurrentUnits { get; set; }

        [JsonProperty("financing")]
        public string Financing { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("initialUnits")]
        public string InitialUnits { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("realizedPL")]
        public string RealizedPl { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("unrealizedPL")]
        public string UnrealizedPl { get; set; }

        [JsonProperty("clientExtensions")]
        public ClientExtensions ClientExtensions { get; set; }
    }
}
