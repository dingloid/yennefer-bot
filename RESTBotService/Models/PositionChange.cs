using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class PositionChange
    {

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("longUnrealizedPL")]
        public string LongUnrealizedPl { get; set; }

        [JsonProperty("netUnrealizedPL")]
        public string NetUnrealizedPl { get; set; }

        [JsonProperty("shortUnrealizedPL")]
        public string ShortUnrealizedPl { get; set; }
    }
}
