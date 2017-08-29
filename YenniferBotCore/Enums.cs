using System;
using System.Collections.Generic;
using System.Text;

namespace YenniferBotCore
{
    public enum BodyType
    {
        WhiteBody,
        BlackBody,
        Doji
    }

    public enum Shadow
    {
        Long,
        Short
    }

    public enum Trend
    {
        Up,
        Down,
        None
    }

    public enum OrderType
    {
        Buy,
        Sell,
        NoAction
    }
}
