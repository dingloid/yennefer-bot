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
            bot.StartBot().Wait();
        }
    }
}