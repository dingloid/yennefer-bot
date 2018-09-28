using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTBotService.Models;
using RESTBotService.Services;
using Serilog;
using YenneferBotCore.Algo;
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
        private IList<Candle> _candles;
     
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
            Log.Information($"Bot Started using Instrument Type: {_instrumentType}");
            
            // initailize and start all the tasks
            //var candles = RetrieveCandles(TimeSpan.FromSeconds(4));
            var strategy = ExecuteStrategy();

            // await all tasks
            // treat this as a "fire and forget" to start the task of retrieving candles every 4 seconds.
            RetrieveCandles(TimeSpan.FromMinutes(5));//Don't await this
            await strategy;

        }

        private async Task ExecuteStrategy()
        {
            Console.WriteLine("---------------------\n Yennefer Bot Online \n---------------------");

            var accountDetails = await _botService.GetAccountDetails(_apiSettings.AccountId);
//            var updateAccount = await _botService.AccountUpdate(_apiSettings.AccountId, 7);

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
                if(_candles == null)
                    continue;

                var currentCandle = Formula.RunCalculation(_candles);

                if (Math.Abs(_pl - (-30.00)) > 0.1)
                {
                    switch (currentCandle)
                    {
                        case OrderType.Buy:
                            var buyOrder = await _botService.CreateOrder(_apiSettings.AccountId, _instrumentType, 1500);

                            //Get Time Stamp of Buy Request
                            _buyTimeStamp = buyOrder.OrderCreateTransaction.Time;
                            Console.WriteLine($"Buy Order Created at: " + _buyTimeStamp + "\n");
                            Log.Information($"Buy Order Created at {_buyTimeStamp}");
                            _candles = null;
                            break;

                        case OrderType.Sell:
                            var getOpenOrders = await _botService.CheckForOpenTrade(_apiSettings.AccountId);
                            if (getOpenOrders.Trades.Any(x => x.CurrentUnits > 0) && getOpenOrders.Trades.Any(x => x.Instrument == _instrumentType))
                            {
                                var closeOrder = await _botService.CloseOrder(_apiSettings.AccountId, _instrumentType);
                                if (closeOrder == null)
                                {
                                    Log.Information($"{nameof(closeOrder)} was null after selling order. About to break..." );
                                }
                                if (closeOrder.LongOrderFillTransaction == null)
                                {
                                    Log.Information($"{nameof(closeOrder.LongOrderFillTransaction)} was null after selling order. About to break...");
                                }
                                //Grabs the current Profit/Loss
                                _pl = closeOrder.LongOrderFillTransaction.Pl;

                                //Get Time Stamp of Sell Request
                                _sellTimeStamp = closeOrder.LongOrderCreateTransaction.Time;
                                Console.WriteLine($"Sell Order Created at: " + _sellTimeStamp + "\n");
                                Log.Information($"Sell Order Created at {_sellTimeStamp}");
                            }
                            break;

                        case OrderType.NoAction:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Something has went wrong...check logs...");

                }
            }
        }

        private async Task RetrieveCandles(TimeSpan interval)
        {
            // run this action until we need to quit/cancel
            // run it for the interval passed in
            while (true)
            {
                _candles = await _botService.GetCandleStickData(_instrumentType);
                //Log.Information("Candle Information: {@Candles}", _candles);
                var delayTask = Task.Delay(interval, _cancellationToken);
                try
                {
                    await delayTask;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
                catch (AggregateException ae)
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
}