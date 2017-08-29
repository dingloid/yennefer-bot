using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RESTBotService.Models;

namespace YenniferBotCore.Algo
{
    public static class Helpers
    {
        /// <summary>
        /// Gets the Body Type of a Candle. 
        /// i.e. Black, White or Doji
        /// </summary>
        /// <param name="candle">Candle that you want to check the body type of</param>
        /// <returns>Returns the Enum body type of the candle</returns>
        public static BodyType CheckBodyType(CandleBody candle)
        {
            if (candle.ClosePrice == candle.OpenPrice)
            {
                return BodyType.Doji;
            }
            else if (candle.OpenPrice > candle.ClosePrice)
            {
                return BodyType.BlackBody;
            }
            else
            {
                return BodyType.WhiteBody;
            }
        }


        /// <summary>
        /// Checks the past three prices for a list of Candlesticks
        /// </summary>
        /// <param name="candles">List of candles you want to check the prices for.</param>
        /// <returns>Returns the trend of the past candle stick prices. Uptrend, Downtrend or no trend.</returns>
        public static Trend CheckPastThreePrices(IList<Candle> candles)
        {

            if (candles[2].CandleBody.ClosePrice < candles[3].CandleBody.ClosePrice &&
                candles[3].CandleBody.ClosePrice < candles[4].CandleBody.ClosePrice &&
                candles[4].CandleBody.ClosePrice < candles[5].CandleBody.ClosePrice)
            {
                return Trend.Up;
            }
            else if (candles[2].CandleBody.ClosePrice > candles[3].CandleBody.ClosePrice &&
                     candles[3].CandleBody.ClosePrice > candles[4].CandleBody.ClosePrice &&
                     candles[4].CandleBody.ClosePrice > candles[5].CandleBody.ClosePrice)
            {
                return Trend.Down;
            }
            else
            {
                return Trend.None;
            }
        }

        /// <summary>
        /// Gets the shadow of the Candlestick
        /// </summary>
        /// <param name="candle">The candlestick that you want to check shadow type for.</param>
        /// <returns>Determines whether or not the candle stick has a long or short shadow</returns>
        public static Shadow CalculateShadow(CandleBody candle)
        {
            return Shadow.Long;
        }
    }
}
