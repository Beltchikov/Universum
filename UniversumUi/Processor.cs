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

                    // LastEquity

                    // TargetPrice

                    string responseCurrentPrice = await GetValueFromApi(apiUrl, "CurrentPrice", symbol);

                    // SharesOutstanding

                    MessageEvent?.Invoke(this, new MessageEventArgs(responseCurrentPrice));
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
