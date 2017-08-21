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

        public static bool CheckPastThreePrices(IList<Candle> candles)
        {

           
            var lastCandle = candles[5];

           return false;
        }

        public static Shadow CalculateShadow(CandleBody candle)
        {
            return Shadow.Long;
        }


      }
}
