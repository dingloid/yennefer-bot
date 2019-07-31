using System.Collections.Generic;
using System.Linq;
using RESTBotService.Models;

namespace YenneferBotCore.Algo.AlgoModels
{
    public class HeikinAshi
    {
        public double HA_Open { get; set; }
        public double HA_Close { get; set; }
        public double HA_High { get; set; }
        public double HA_Low { get; set; }
        public IList<double> Elements { get; set; }

        /// <summary>
        /// Creates a Heikin Ashi Candle
        /// </summary>
        /// <param name="current">The latest Candlestick in the series</param>
        /// <param name="previous">The previous Candlestick in the series</param>
        /// <returns>Returns a Heikin Ashi Candles</returns>
        public HeikinAshi(CandleBody current, CandleBody previous)
        {
            var currentCandle = current;
            var previousCandle = previous;

            HA_Close = (currentCandle.OpenPrice + currentCandle.ClosePrice + currentCandle.HighPrice + currentCandle.LowPrice) / 4;
            HA_Open = (previousCandle.OpenPrice + previousCandle.ClosePrice) / 2;
            Elements = new List<double> {currentCandle.HighPrice, currentCandle.LowPrice, HA_Open, HA_Close};
            HA_High = Elements.Max();
            HA_Low = Elements.Min();
        }
    }
}
