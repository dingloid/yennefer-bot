using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class CandleBody
    {
        [JsonProperty("o")]
        public double OpenPrice { get; set; }

        [JsonProperty("h")]
        public double HighPrice { get; set; }

        [JsonProperty("l")]
        public double LowPrice { get; set; }

        [JsonProperty("c")]
        public double ClosePrice { get; set; }
    }
}
