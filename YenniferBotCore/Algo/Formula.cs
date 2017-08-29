using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RESTBotService.Services;
using RESTBotService.Models;

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
            var candleBodyColor = Helpers.CheckBodyType(candles[5].CandleBody);
            var candleBodyType = Helpers.CheckBodyType(candles[5].CandleBody);

            if (candleBodyType == BodyType.Doji)
            {
                var trend = Helpers.CheckPastThreePrices(candles);
                if (trend == Trend.Up)
                {
                    return OrderType.Sell;
                }
                else if (trend == Trend.Down)
                {
                    return OrderType.Buy;
                }

            }
            else
            {
                return OrderType.NoAction;
            }
            return OrderType.NoAction;
        }
    }
}

