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
            var apiUrl = $"{_baseUrl}{_apiVersion}/instruments/{curPair}/candles?count=6&price=M&granularity=M1";
            var uri = new Uri(apiUrl);

            var response = await SendRequestAsync<InstrumentResponse>(uri);

            return response.Candles;
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
