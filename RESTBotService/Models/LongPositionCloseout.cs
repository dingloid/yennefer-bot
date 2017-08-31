using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class LongPositionCloseout
    {
        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
