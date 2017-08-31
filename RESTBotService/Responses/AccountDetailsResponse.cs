using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RESTBotService.Models;

namespace RESTBotService.Responses
{
    public class AccountDetailsResponse
    {

        [JsonProperty("account")]
        public AccountDetails Account { get; set; }

        [JsonProperty("lastTransactionID")]
        public string LastTransactionId { get; set; }
    }

}
