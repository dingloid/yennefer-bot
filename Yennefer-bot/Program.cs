using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestApi;
using RestApi.DataTypes;

namespace Yennefer_bot
{
    internal static class Program
    {
        private static CancellationTokenSource TokenSource = new CancellationTokenSource();
        private static CancellationToken Token = TokenSource.Token;
        private static APIConfig Config;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var readConfig = File.ReadAllText("Config.json");
            Config = JsonConvert.DeserializeObject<APIConfig>(readConfig);
            Rest.ApiKey = Config.API_KEY;

            MultiTask().Wait();
        }

        //Method to Run Forever Until Cancelled
        private static void ExecuteStrategy()
        {
            Console.WriteLine("#####################\n Yennefer Bot Online \n#####################");

            var account = Rest.GetAccountDetails(Config.ACCOUNT_ID);
            var candles = Rest.GetCandles("EUR_USD", Config.ACCOUNT_ID);
                      
            Console.WriteLine("\n");
            Console.WriteLine(@"Monopoly Money: " + account.balance);
            Console.WriteLine(@"Candles: " + candles );
            
            while (!Token.IsCancellationRequested)
            {
                //Kill Key Value
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.X)
                    {
                        TokenSource.Cancel();
                    }
                }     
            }
        }
        
        //Create New Task to run. 
        static async Task MultiTask()
        {
            var testThread = new Task(ExecuteStrategy);
            testThread.Start();
            await testThread;    
        }
    }
}
