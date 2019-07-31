using System;
using System.IO;
using Serilog;

namespace YenneferBotCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "logs\\yennefer-bot.log";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Log.Logger = new LoggerConfiguration().WriteTo.File(filePath).CreateLogger();
            var bot = new Bot();
            try
            {
                bot.StartBot().Wait();
            }
            catch(AggregateException ae)
            {
                Log.Error($"ERROR: {ae.Message}");
                if (ae.InnerExceptions != null)
                {
                    Log.Error($"Number of Errors: {ae.InnerExceptions.Count}");
                    foreach (var exception in ae.InnerExceptions)
                    {
                        Log.Error(exception, "");
                    }
                }
                else
                {
                    Log.Error("Number of Errors: NULL");
                }
                throw ae.Flatten();
            }
        }
    }
}