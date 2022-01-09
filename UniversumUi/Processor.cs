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

        public async Task ProcessAsync(string apiUrl, string symbolsAsText, string separator, string decimalSeparator)
        {
            var symbolList = symbolsAsText.Split(Environment.NewLine);
            foreach (var symbol in symbolList)
            {
                try
                {
                    string roe = await GetValueFromApi(apiUrl, "Roe", symbol, decimalSeparator);
                    string lastEquity = await GetValueFromApi(apiUrl, "LastEquity", symbol, decimalSeparator);
                    string targetPrice = await GetValueFromApi(apiUrl, "TargetPrice", symbol, decimalSeparator);
                    string currentPrice = await GetValueFromApi(apiUrl, "CurrentPrice", symbol, decimalSeparator);
                    string sharesOutstanding = await GetValueFromApi(apiUrl, "SharesOutstanding", symbol, decimalSeparator);
                    
                    string csvLine = $"{symbol}{separator}{roe}{separator}{lastEquity}{separator}{targetPrice}{separator}{currentPrice}{separator}{sharesOutstanding}";
                    MessageEvent?.Invoke(this, new MessageEventArgs(csvLine));
                }
                catch (Exception e)
                {
                    MessageEvent?.Invoke(this, new MessageEventArgs(e.ToString()));
                }
            }
        }

        private async Task<string> GetValueFromApi(string apiUrl, string actionMethod, string symbol, string decimalSeparator)
        {
            var uriCurrentPrice = $"{apiUrl}/{actionMethod}?symbol={symbol}";
            string responseCurrentPrice = await _httpClient.GetStringAsync(uriCurrentPrice);
            responseCurrentPrice = responseCurrentPrice.Replace("[", "").Replace("]", "");
            responseCurrentPrice = responseCurrentPrice.Replace(".", decimalSeparator);
            return responseCurrentPrice;
        }
    }
}
