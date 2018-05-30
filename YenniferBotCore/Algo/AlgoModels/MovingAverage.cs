using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using RESTBotService.Models;

namespace YenneferBotCore.Algo.AlgoModels
{
    public class MovingAverage
    {
        /// <summary>
        /// Calculates the Simple Moving Average based off of custom amount
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="timeFrame"></param>
        /// <returns></returns>
        public int CalculateSimpleMovingAverage(IList<Candle> candles, int timeFrame)
        {
            var sma = (int)(candles.Sum(x => x.CandleBody.ClosePrice) / timeFrame);

            return sma;
        }

        /// <summary>
        /// Calculates the Expotential Moving Average
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="timeFrame"></param>
        /// <returns></returns>
        public int CalculateExponetialMovingAverage(IList<Candle> candles, int timeFrame)
        {
            var sma = CalculateSimpleMovingAverage(candles, timeFrame);

            var weight = 2/(timeFrame + 1);

            return 0;
        }
    }
}
