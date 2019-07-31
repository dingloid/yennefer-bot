using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Order
    {
        [JsonProperty("order")]
        public OrderRequest OrderRequest { get; set; }
    }
}
