using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTBotService.Services;
using YenniferBotCore.Models;

namespace YenniferBotCore
{
    public class Bot
    {
        private readonly ApiSettings _apiSettings;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly OandaApiService _botService;
        private CancellationToken _cancellationToken;
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
            // initailize and start all the tasks
            var strategy = ExecuteStrategy();

            // await all tasks
            await strategy;
            
        }

        private async Task ExecuteStrategy()
        {
            Console.WriteLine("#####################\n Yennefer Bot Online \n#####################");

            var accountDetails = await _botService.GetAccountDetails(_apiSettings.AccountId);
            var getCandles = await _botService.GetCandleStickData("USD_JPY");

            Console.WriteLine();
            Console.WriteLine($"Monopoly Money: {accountDetails.Balance}\n");
            

            foreach (var body in getCandles.Select(x => x.CandleBody))
            {

                var open = body.OpenPrice;
                var close = body.ClosePrice;
                var low = body.LowPrice;
                var high = body.HighPrice;

                Console.WriteLine("\n");
                Console.WriteLine($"Open: {open}");
                Console.WriteLine($"Close: {close}");
                Console.WriteLine($"Low: {low}");
                Console.WriteLine($"High: {high}");
            }
            

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
            }
        }
    }
}
