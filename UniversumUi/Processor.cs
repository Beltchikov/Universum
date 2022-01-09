using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UniversumUi
{
    public class Processor : IProcessor
    {
        private HttpClient _httpClient;

        public Processor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event MessageEventHandler? MessageEvent;

        public async Task ProcessAsync(string apiUrl, string symbolsAsText)
        {
            var symbolList = symbolsAsText.Split(Environment.NewLine);
            foreach (var symbol in symbolList)
            {
                try
                {
                    // TODO Roe
                    string roe = await GetValueFromApi(apiUrl, "Roe", symbol);
                    // LastEquity
                    string lastEquity = await GetValueFromApi(apiUrl, "LastEquity", symbol);

                    // TargetPrice
                    string targetPrice = await GetValueFromApi(apiUrl, "TargetPrice", symbol);

                    string currentPrice = await GetValueFromApi(apiUrl, "CurrentPrice", symbol);

                    // SharesOutstanding
                    string sharesOutstanding = await GetValueFromApi(apiUrl, "SharesOutstanding", symbol);

                    string csvLine = $"{roe};{lastEquity};{targetPrice};{currentPrice};{sharesOutstanding}";
                    MessageEvent?.Invoke(this, new MessageEventArgs(csvLine));
                }
                catch (Exception e)
                {
                    MessageEvent?.Invoke(this, new MessageEventArgs(e.ToString()));
                }
            }
        }

        private async Task<string> GetValueFromApi(string apiUrl, string actionMethod, string symbol)
        {
            var uriCurrentPrice = $"{apiUrl}/{actionMethod}?symbol={symbol}";
            string responseCurrentPrice = await _httpClient.GetStringAsync(uriCurrentPrice);
            responseCurrentPrice = responseCurrentPrice.Replace("[", "").Replace("]", "");
            return responseCurrentPrice;
        }
    }
}
