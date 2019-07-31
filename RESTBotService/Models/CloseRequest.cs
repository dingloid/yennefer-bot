using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class CloseRequest
    {
        [JsonProperty("longUnits")]
        public string LongUnits { get; set; }

        public CloseRequest(string longUnits)
        {
            LongUnits = longUnits;
        }
    }
}
