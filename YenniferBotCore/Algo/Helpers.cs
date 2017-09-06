using System;
using System.Collections.Generic;
using RESTBotService.Models;
using YenneferBotCore.Algo.AlgoModels;

namespace YenneferBotCore.Algo
{
    public static class Helpers
    {
        /// <summary>
        /// Gets the Body Type of a Candle. 
        /// </summary>
        /// <param name="candle">Candle that you want to check the body type of</param>
        /// <returns>Returns the Enum body type of the candle</returns>
        public static BodyColor CheckBodyColor(CandleBody candle)
        {
             if (candle.OpenPrice > candle.ClosePrice)
            {
                return BodyColor.Black;
            }
            else
            {
                return BodyColor.White;
            }
        }

        
        /// <summary>
        /// Checks the past three prices for a list of Candlesticks
        /// </summary>
        /// <param name="candles">List of candles you want to check the prices for.</param>
        /// <returns>Returns the trend of the past candle stick prices. Uptrend, Downtrend or no trend.</returns>
        public static Trend CheckPastThreePrices(IList<Candle> candles)
        {
            if (candles[0].CandleBody.ClosePrice < candles[1].CandleBody.ClosePrice &&
                candles[1].CandleBody.ClosePrice < candles[2].CandleBody.ClosePrice)
            {
                return Trend.Up;
            }

            if (candles[0].CandleBody.ClosePrice > candles[1].CandleBody.ClosePrice &&
                candles[1].CandleBody.ClosePrice > candles[2].CandleBody.ClosePrice)
            {
                return Trend.Down;
            }

            return Trend.None;
        }

        /// <summary>
        /// Checks the length of a Candle
        /// </summary>
        /// <param name="candle">The Candle you want to check the size for.</param>
        /// <returns>Returns the size of the Candle Stick</returns>
        public static BodyType CheckBodyType(CandleBody candle)
        {
            var averageBody = ((candle.ClosePrice + candle.OpenPrice + candle.HighPrice + candle.LowPrice) /4) * 1.3;
            var body = Math.Abs(candle.OpenPrice - candle.ClosePrice);

            if (body > averageBody)
            {
                return BodyType.Long;
            }
            else
            {
                return BodyType.Short;
            }
        }

        /// <summary>
        /// Gets the Top Shadow of a Candle Stick
        /// </summary>
        /// <param name="candle">The candlestick that you want to check shadow for.</param>
        /// <returns>Determines whether or not the candle stick has a long or short top shadow</returns>
        public static TopShadow CheckTopShadow(CandleBody candle)
        {
            double body;
            double topShadow;
            var candleColor = CheckBodyColor(candle);

            if (candleColor == BodyColor.Black)
            {
                body = Math.Abs(candle.OpenPrice - candle.ClosePrice);
                topShadow = candle.HighPrice - candle.OpenPrice;

                if (topShadow > body)
                {
                    return TopShadow.Long;
                }

                if (Math.Abs(candle.HighPrice - candle.OpenPrice) < 0.01)
                {
                    return TopShadow.NoShadow;
                }
            }

            if (candleColor == BodyColor.White)
            {
                body = candle.OpenPrice - candle.ClosePrice;
                topShadow = candle.HighPrice - candle.ClosePrice;

                if (topShadow > body)
                {
                    return TopShadow.Long;
                }

                if (Math.Abs(candle.HighPrice - candle.ClosePrice) < 0.01)
                {
                    return TopShadow.NoShadow;
                }
            }
            
            return TopShadow.Short;
        }

        /// <summary>
        /// Gets the Bottom Shadow of a Candle Stick
        /// </summary>
        /// <param name="candle">The candlestick that you want to check shadow for.</param>
        /// <returns>Determines whether or not the candle stick has a long or short top shadow</returns>
        public static BottomShadow CheckBottomShadow(CandleBody candle)
        {
            double body;
            double bottomShadow;
            var candleColor = CheckBodyColor(candle);

            if (candleColor == BodyColor.Black)
            {
                body = Math.Abs(candle.OpenPrice - candle.ClosePrice);
                bottomShadow = candle.LowPrice - candle.ClosePrice;

                if (bottomShadow > body)
                {
                    return BottomShadow.Long;
                }

                if (Math.Abs(candle.ClosePrice - candle.LowPrice) < 0.01)
                {
                    return BottomShadow.NoShadow;
                }
            }

            if (candleColor == BodyColor.White)
            {
                body = candle.OpenPrice - candle.ClosePrice;
                bottomShadow = candle.LowPrice - candle.OpenPrice;

                if (bottomShadow > body)
                {
                    return BottomShadow.Long;
                }

                if (Math.Abs(candle.OpenPrice - candle.LowPrice) < 0.01)
                {
                    return BottomShadow.NoShadow;
                }
            }

            return BottomShadow.Short;
        }

        /// <summary>
        /// Checks to see if Candle Stick is Bearish or Bullish
        /// </summary>
        /// <param name="candle">The candlestick that you want to check.</param>
        /// <returns>Returns the animal type of the candle</returns>
        public static AnimalType CheckAnimalType(CandleBody candle)
        {
            var bodyType = CheckBodyType(candle);
            var bodyColor = CheckBodyColor(candle);
            var topShadow = CheckTopShadow(candle);
            var bottomShadow = CheckBottomShadow(candle);
            
            if (bodyType == BodyType.Long && bodyColor == BodyColor.White)
            {
                return AnimalType.Bull;
            }

            if (bodyType == BodyType.Long && bodyColor == BodyColor.Black)
            {
                return AnimalType.Bear;
            }

            if (bottomShadow == BottomShadow.Long && topShadow == TopShadow.Short)
            {
                return  AnimalType.Bull;
            }

            if (bottomShadow == BottomShadow.Short && topShadow == TopShadow.Long)
            {
                return  AnimalType.Bear;
            }
            
            return AnimalType.NoAnimal;
        }


        /// <summary>
        /// Checks for Stop Loss
        /// </summary>
        /// <param name="buyPrice">The price a candle stick order was created at.</param>
        /// <param name="currentPrice">Current Price of the candle stick</param>
        /// <returns>Returns true if stop loss needs to be used otherwise false.</returns>
        public static bool CheckStopLoss(double buyPrice, double currentPrice)
        {
            if ((buyPrice - (buyPrice * 0.10)) <= currentPrice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to see if Candle Stick is Bearish or Bullish
        /// </summary>
        /// <param name="haCandle">The candlestick that you want to check.</param>
        /// <returns>Returns the animal type of the Heikin Ashi candle</returns>
        public static AnimalType CheckHaCandleAnimalType(HeikinAshi haCandle)
        {
            if (haCandle.HA_Close > haCandle.HA_Open) //White Candle
            {
                if (haCandle.HA_Close > (haCandle.HA_High + haCandle.HA_Low)/2) 
                {
                    return AnimalType.Bull;
                }
            }

            if (haCandle.HA_Open > haCandle.HA_Close) //Black Candle
            {
                if (haCandle.HA_Close < (haCandle.HA_High + haCandle.HA_Low) / 2)
                {
                    return AnimalType.Bear;
                }
            }

            return AnimalType.NoAnimal;
        }

        /// <summary>
        /// Converts Heikin Ashi into CandleBody.
        /// </summary>
        /// <param name="haCandle">The Heikin Ashi candlestick that you want to convert into a CandleBody object</param>
        /// <returns>Returns CandleBody object</returns>
        public static CandleBody ConvertHaCandle(HeikinAshi haCandle)
        {
            var candle = new CandleBody
            {
                OpenPrice = haCandle.HA_Open,
                HighPrice = haCandle.HA_High,
                LowPrice = haCandle.HA_Low,
                ClosePrice = haCandle.HA_Close
            };

            return candle;
        }
    }
}
