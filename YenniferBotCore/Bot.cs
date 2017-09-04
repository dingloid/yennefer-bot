using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using RESTBotService.Models;
using RESTBotService.Services;
using YenniferBotCore.Algo;
using YenniferBotCore.Models;

namespace YenniferBotCore
{
    public class Bot
    {
        private readonly ApiSettings _apiSettings;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly OandaApiService _botService;
        private CancellationToken _cancellationToken;
        private string _instrumentType;
        private double _pl;
        
        public Bot()
        {
            var settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "bot-settings/bot-settings.json");
            var apiSettingsText = File.ReadAllText(settingsFilePath);
            _apiSettings = JsonConvert.DeserializeObject<ApiSettings>(apiSettingsText);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _botService = new OandaApiService(_apiSettings.BaseUrl, _apiSettings.ApiKey);
        }

        public Bot(string filePath)
        {
            var apiSettingsText = File.ReadAllText(filePath);
            _apiSettings = JsonConvert.DeserializeObject<ApiSettings>(apiSettingsText);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _botService = new OandaApiService(_apiSettings.BaseUrl, _apiSettings.ApiKey);
        }

        /// <summary>
        /// Starts the bot up to start running tasks.
        /// </summary>
        /// <returns>Task that can be awaited or acted upon.</returns>
        public async Task StartBot()
        {
            //Choose the Currency Pair you want to trade
            Console.WriteLine("Choose a Currency Pair: ");
            Console.WriteLine("1. USD/JPY ");
            Console.WriteLine("2. EUR/USD ");

            var key = Console.ReadKey();
            var validInput = false;
            if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
            {
                _instrumentType = "USD_JPY";
                validInput = true;
            }
            else if (key.Key == ConsoleKey.NumPad2 || key.Key == ConsoleKey.D2)
            {
                _instrumentType = "EUR_USD";
                validInput = true;
            }

            while (!validInput)
            {
                Console.WriteLine("Please select 1 or 2.");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
                {
                    _instrumentType = "USD_JPY";
                    validInput = true;
                }
                else if (key.Key == ConsoleKey.NumPad2 || key.Key == ConsoleKey.D2)
                {
                    _instrumentType = "EUR_USD";
                    validInput = true;
                }
            }

            

            // initailize and start all the tasks
            var strategy = ExecuteStrategy();

            // await all tasks
            await strategy;

        }

        private async Task ExecuteStrategy()
        {
            Console.WriteLine("#####################\n Yennefer Bot Online \n#####################");

            var accountDetails = await _botService.GetAccountDetails(_apiSettings.AccountId);
            var getCandles = await _botService.GetCandleStickData(_instrumentType);

            Console.WriteLine();
            Console.WriteLine($"Monopoly Money: {accountDetails.Balance}\n");
  
//            foreach (var body in getCandles.Select(x => x.CandleBody))
//            {
//                var open = body.OpenPrice;
//                var close = body.ClosePrice;
//                var low = body.LowPrice;
//                var high = body.HighPrice;
//
//                Console.WriteLine("\n");
//                Console.WriteLine($"Open: {open}");
//                Console.WriteLine($"Close: {close}");
//                Console.WriteLine($"Low: {low}");
//                Console.WriteLine($"High: {high}");
//            }
            
            while (!_cancellationToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.X)
                    {
                        _cancellationTokenSource.Cancel();
                    }
                }

                var currentCandle = Formula.RunCalculation(getCandles);

                if (_pl != -30.00)
                {
                    switch (currentCandle)
                    {
                        case OrderType.Buy:
                            await _botService.CreateOrder(_apiSettings.AccountId, _instrumentType, 200);
                            break;
                        case OrderType.Sell:
                            var closeOrder = await _botService.CloseOrder(_apiSettings.AccountId, _instrumentType);
                            _pl = closeOrder.LongOrderFillTransaction.Pl;
                            break;
                        case OrderType.NoAction:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Your shit sucks and doesn't work. Try again");
                    _cancellationTokenSource.Cancel();
                }
          
            }
        }
    }
}