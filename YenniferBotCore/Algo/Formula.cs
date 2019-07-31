using System;
using System.Collections.Generic;
using RESTBotService.Models;
using YenneferBotCore.Algo.AlgoModels;

namespace YenneferBotCore.Algo
{
    public class Formula
    {

        /// <summary>
        /// Runs the primary calculation for trading
        /// </summary>
        /// <param name="candles">List of candles. Everything will be checking based off of the last candle.</param>
        /// <returns>Does Math Stuff.</returns>
        public static OrderType RunCalculation(IList<Candle> candles)
        {
            
            var latestHaCandle = new HeikinAshi(candles[2].CandleBody, candles[1].CandleBody);
            var previousHaCandle = new HeikinAshi(candles[1].CandleBody, candles[0].CandleBody);

            if (Helpers.CheckHaCandleAnimalType(latestHaCandle) == AnimalType.Bear && Helpers.CheckHaCandleAnimalType(previousHaCandle) == AnimalType.Bear)
            {
                if (Math.Abs(latestHaCandle.HA_Open - latestHaCandle.HA_Close) > Math.Abs(previousHaCandle.HA_Open - previousHaCandle.HA_Close))
                {
                    if (Math.Abs(latestHaCandle.HA_Open - latestHaCandle.HA_High) < 0.09)
                    {
                        return OrderType.Buy;
                    }
                }
            }

            if (Helpers.CheckHaCandleAnimalType(latestHaCandle) == AnimalType.Bull && Helpers.CheckHaCandleAnimalType(previousHaCandle) == AnimalType.Bull)
            {
                if (Math.Abs(latestHaCandle.HA_Open - latestHaCandle.HA_Close) > Math.Abs(previousHaCandle.HA_Open - previousHaCandle.HA_Close))
                {
                    if (Math.Abs(latestHaCandle.HA_Close - latestHaCandle.HA_Low) < 0.09)
                    {
                        return OrderType.Sell;
                    }
                }
            }

            return OrderType.NoAction;
        }
    }
}

