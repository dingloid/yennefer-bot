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
            
            var latestHaCandle = Helpers.ConvertHACandle(new HeikinAshi(candles[2].CandleBody, candles[1].CandleBody));
            var previousHaCandle = Helpers.ConvertHACandle(new HeikinAshi(candles[1].CandleBody, candles[0].CandleBody));

            if (Helpers.CheckAnimalType(latestHaCandle) == AnimalType.Bear && Helpers.CheckAnimalType(previousHaCandle) == AnimalType.Bear)
            {
                if (Math.Abs(latestHaCandle.OpenPrice - latestHaCandle.ClosePrice) > Math.Abs(previousHaCandle.OpenPrice - previousHaCandle.ClosePrice))
                {
                    if (Helpers.CheckTopShadow(latestHaCandle) == TopShadow.NoShadow)
                    {
                        return OrderType.Buy;
                    }
                }
            }

            if (Helpers.CheckAnimalType(latestHaCandle) == AnimalType.Bull && Helpers.CheckAnimalType(previousHaCandle) == AnimalType.Bull)
            {
                if (Math.Abs(latestHaCandle.OpenPrice - latestHaCandle.ClosePrice) > Math.Abs(previousHaCandle.OpenPrice - previousHaCandle.ClosePrice))
                {
                    if (Helpers.CheckBottomShadow(latestHaCandle) == BottomShadow.NoShadow)
                    {
                        return OrderType.Sell;
                    }
                }
            }
            return OrderType.NoAction;
        }
    }
}

