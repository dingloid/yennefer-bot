using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using RESTBotService.Models;

namespace YenniferBotCore.Algo.AlgoModels
{
    public class HeikinAshi
    {
        public double HA_Open { get; set; }
        public double HA_Close { get; set; }
        public double HA_High { get; set; }
        public double HA_Low { get; set; }
        public IList<double> Elements { get; set; }

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
