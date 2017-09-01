using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RESTBotService.Models
{
    public class OrderRequest
    {
        [JsonProperty("units")]
        public int Units { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("timeInForce")]
        public string Fok { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("positionFill")]
        public string PositionFill { get; set; }

        public OrderRequest(int units, string instrument, string fok, string type, string positionFill)
        {
            Units = units;
            Instrument = instrument;
            Fok = fok;
            Type = type;
            PositionFill = positionFill;
        }
    }
}
