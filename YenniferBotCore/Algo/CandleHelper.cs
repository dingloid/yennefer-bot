using System;
using System.Collections.Generic;
using System.Text;
using RESTBotService.Models;

namespace YenneferBotCore.Algo
{
    public static class CandleHelper
    {
        /// <summary>
        /// Checks for Hanging Man Candle Stick
        /// </summary>
        /// <param name="candles">A list of candle sticks. It will check for the past three past candle sticks and compare it to the current one</param>
        /// <returns></returns>
        public static bool CheckHangingMan(IList<Candle> candles)
        {
            var currentCandle = candles[4].CandleBody;
            var bodyColor = Helpers.CheckBodyColor(currentCandle);

            if (Helpers.CheckPastThreePrices(candles) == Trend.Up)
            {
                double currentBody;
                if (bodyColor == BodyColor.Black)
                {
                    currentBody = currentCandle.ClosePrice - currentCandle.OpenPrice;
                    if ((2 * currentBody) > (currentCandle.ClosePrice - currentCandle.LowPrice))
                    {
                        return true;
                    }
                }
                else
                {
                    currentBody = currentCandle.OpenPrice = currentCandle.ClosePrice;
                    if ((2 * currentBody) > (currentCandle.OpenPrice - currentCandle.LowPrice))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}
