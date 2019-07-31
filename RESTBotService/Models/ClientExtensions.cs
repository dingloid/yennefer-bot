using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class ClientExtensions
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
