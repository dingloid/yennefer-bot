using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class AccountChangesResponse
    {
        [JsonProperty("changes")]
        public Changes Changes { get; set; }

        [JsonProperty("lastTransactionID")]
        public int LastTransactionId { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }
    }
}
