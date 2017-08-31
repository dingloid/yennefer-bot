using System;
using System.Collections.Generic;
using System.Text;

namespace RESTBotService.Models
{
    public class LongOrderCreateTransaction
    {
        public string accountID { get; set; }
        public string batchID { get; set; }
        public string id { get; set; }
        public string instrument { get; set; }
        public LongPositionCloseout longPositionCloseout { get; set; }
        public string positionFill { get; set; }
        public string reason { get; set; }
        public string time { get; set; }
        public string timeInForce { get; set; }
        public string type { get; set; }
        public string units { get; set; }
        public int userID { get; set; }
    }
}
