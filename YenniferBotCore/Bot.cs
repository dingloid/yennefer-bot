using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTBotService.Services;
using YenneferBotCore.Algo;
using YenneferBotCore.Algo.AlgoModels;
using YenneferBotCore.Models;

namespace YenneferBotCore
{
    public class Bot
    {
        private readonly ApiSettings _apiSettings;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly OandaApiService _botService;
        private CancellationToken _cancellationToken;
        private string _instrumentType;
        private double _pl;
        private DateTime _buyTimeStamp;
        private DateTime _sellTimeStamp;
     
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
            Console.WriteLine("---------------------\n Yennefer Bot Online \n---------------------");

            var accountDetails = await _botService.GetAccountDetails(_apiSettings.AccountId);
            var getCandles = await _botService.GetCandleStickData(_instrumentType);
            var getOpenOrders = await _botService.CheckForOpenTrade(_apiSettings.AccountId);
            
            Console.WriteLine();
            Console.WriteLine($"Monopoly Money: {accountDetails.Balance}\n");
  
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

                if (Math.Abs(_pl - (-30.00)) > 0.1)
                {
                    switch (currentCandle)
                    {
                        case OrderType.Buy:
                            var buyOrder = await _botService.CreateOrder(_apiSettings.AccountId, _instrumentType, 200);

                            //Get Time Stamp of Buy Request
                            _buyTimeStamp = buyOrder.OrderCreateTransaction.Time;
                            Console.WriteLine($"Buy Order Created at: " + _buyTimeStamp + "\n");
                            break;

                        case OrderType.Sell:
                            if (getOpenOrders.Trades.Any(x => x.CurrentUnits > 0))
                            {
                                var closeOrder = await _botService.CloseOrder(_apiSettings.AccountId, _instrumentType);

                                //Grabs the current Profit/Loss
                                _pl = closeOrder.LongOrderFillTransaction.Pl;

                                //Get Time Stamp of Sell Request
                                _sellTimeStamp = closeOrder.LongOrderCreateTransaction.Time;
                                Console.WriteLine($"Sell Order Created at: " + _sellTimeStamp + "\n");
                            }
                            break;

                        case OrderType.NoAction:
                            break;
                    }
                }
                else
                {
                    //This is where you know you fucked up.
                    for (int i = 0; i < 15; i++)
                    {
                        Console.WriteLine("You Died");
                    }
                }
            }
        }
    }
}