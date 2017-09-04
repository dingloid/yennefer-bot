using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RESTBotService.Services;
using RESTBotService.Models;
using YenniferBotCore.Algo.AlgoModels;

namespace YenniferBotCore.Algo
{
    public class Formula
    {
        /// <summary>
        /// Runs the primary calculation for trading
        /// </summary>
        /// <param name="candles">List of candles. Everything will be checking based off of the last candle.</param>
        /// <returns>The candle stick prices of the past minute.</returns>
        public static OrderType RunCalculation(IList<Candle> candles)
        {
           var latestCandle = new HeikinAshi(candles[2].CandleBody, candles[1].CandleBody);
           var previousCandle = new HeikinAshi(candles[1].CandleBody, candles[0].CandleBody);

           return OrderType.NoAction;
            
        }
    }
}

