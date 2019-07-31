using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class TradeChange
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("unrealizedPL")]
        public string UnrealizedPl { get; set; }
    }
}
