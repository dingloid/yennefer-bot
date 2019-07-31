using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTBotService.Models;
using RESTBotService.Responses;

namespace RESTBotService.Services
{
    public class OandaApiService
    {
        private readonly string _baseUrl;
        private readonly string _apiVersion;
        private readonly IDictionary<string, string> _defaultHeaders;

        public OandaApiService(string baseUrl, string apiToken, string apiVersion = "v3")
        {
            _baseUrl = baseUrl;
            _apiVersion = apiVersion;
            _defaultHeaders = new Dictionary<string, string> {{"Authorization", "Bearer " + apiToken } };
        }

        /// <summary>
        /// Gets the specific account details for the given account.
        /// </summary>
        /// <param name="accountId">The id of the account details we are retrieving.</param>
        /// <returns>The account details for the specified account. Will return null if an error occured</returns>
        public async Task<AccountDetails> GetAccountDetails(string accountId)
        {
            var apiUrl = $"{_baseUrl}{_apiVersion}/accounts/{accountId}";
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<AccountDetailsResponse>(uri);

            return response?.Account;
        }

        /// <summary>
        /// Gets the price of an instrument.
        /// </summary>
        /// <param name="curPair">Currency Pair you want to choose. i.e JPY_USD format</param>
        /// <returns>The candle stick prices of the past minute.</returns>
        public async Task<IList<Candle>> GetCandleStickData(string curPair)
        {
            //H12 = 12 Hour Candlestick
            //H8 = 8 Hour Candlestick
            //M5 = 5 Minute
            //M10 = 10 Minute Candlestick
            //M30 = 30 Minute Candlstick
            var apiUrl = $"{_baseUrl}{_apiVersion}/instruments/{curPair}/candles?count=4&price=M&granularity=M5";
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<InstrumentResponse>(uri);

            return response.Candles;
        }

        /// <summary>
        /// Creates a trade order
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <param name="instrument">The currency pair that is requested to be traded</param>
        /// <param name="amount">The amount of currency to trade</param>
        /// <returns></returns>
        public async Task<TradeResponse> CreateOrder(string accountId, string instrument, int amount)
        {
            var apiUrl = $"{_baseUrl}{_apiVersion}/accounts/{accountId}/orders";
            var orderRequest = new OrderRequest(amount, instrument, "FOK", "MARKET", "DEFAULT" );
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<TradeResponse>(uri, HttpMethod.Post, null, new Order{OrderRequest = orderRequest});

            return response;
        }

        /// <summary>
        /// Updates Account Details
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <param name="transactionId">The id of the last transaction</param>
        /// <returns>Updates the account details based off last Transaction Id</returns>
        public async Task<AccountChangesResponse> AccountUpdate(string accountId, int transactionId)
        {
            var apiUrl = $"{_baseUrl}{_apiVersion}/accounts/{accountId}/changes?sinceTransactionID={transactionId}";
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<AccountChangesResponse>(uri);

            return response;
        }

        /// <summary>
        /// Checks for open trade orders
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <returns>Nothing</returns>
        public async Task<OpenTradeResponse> CheckForOpenTrade(string accountId)
        {
            var apiUrl = $"{_baseUrl}{_apiVersion}/accounts/{accountId}/openTrades";
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<OpenTradeResponse>(uri);

            return response;
        }

        /// <summary>
        /// Closes a trade order
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <param name="instrument">The currency pair that is requested to be traded</param>
        /// <returns>Nothing</returns>
        public async Task<TradeCloseResponse> CloseOrder(string accountId, string instrument)
        {
            var apiUrl = $"{_baseUrl}{_apiVersion}/accounts/{accountId}/positions/{instrument}/close";
            var closeRequest = new CloseRequest("ALL");
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<TradeCloseResponse>(uri, HttpMethod.Put, null, closeRequest);

            return response;
        }


        private async Task<TData> SendRequestAsync<TData>(Uri uri, HttpMethod httpMethod = null, IDictionary<string, string> headers = null, object requestData = null)
        {
            TData result;

            // default to GET if the HttpMethod is null
            var method = httpMethod ?? HttpMethod.Get;
            // seriialize are requestData to a json string if we need to send something to the server
            var data = requestData == null ? null : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, uri))
            {
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }
                // add the headers if we passed in anymore headers
                // always default to sending the api token
                foreach (var header in _defaultHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                // create the request client
                using (var client = new HttpClient())
                {
                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                    {
                        result = JsonConvert.DeserializeObject<TData>(await response.Content.ReadAsStringAsync());
                    }
                }
            }

            return result;
        }
    }
}
