using System;

namespace YenniferBotCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.StartBot().Wait();
        }
    }
}