using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RESTBotService.Models
{
    public class Changes
    {

        [JsonProperty("ordersCancelled")]
        public IList<object> OrdersCancelled { get; set; }

        [JsonProperty("ordersCreated")]
        public IList<object> OrdersCreated { get; set; }

        [JsonProperty("ordersFilled")]
        public IList<OrdersFilled> OrdersFilled { get; set; }

        [JsonProperty("ordersTriggered")]
        public IList<object> OrdersTriggered { get; set; }

        [JsonProperty("positions")]
        public IList<Position> Positions { get; set; }

        [JsonProperty("tradesClosed")]
        public IList<TradesClosed> TradesClosed { get; set; }

        [JsonProperty("tradesOpened")]
        public IList<TradesOpened> TradesOpened { get; set; }

        [JsonProperty("tradesReduced")]
        public IList<object> TradesReduced { get; set; }

        [JsonProperty("transactions")]
        public IList<Transaction> Transactions { get; set; }
    }
}
