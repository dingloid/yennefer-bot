using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class OpenTradeResponse
    {
        [JsonProperty("lastTransactionID")]
        public string LastTransactionId { get; set; }

        [JsonProperty("trades")]
        public IList<Trade> Trades { get; set; }
    }
}
