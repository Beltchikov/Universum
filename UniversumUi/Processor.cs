using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UniversumUi
{
    public class Processor : IProcessor
    {
        private HttpClient _httpClient;
        private const string LOG_FILE = "UniversumUi.log";

        public Processor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event MessageEventHandler? MessageEvent;

        public async Task ProcessAsync(string apiUrl, string symbolsAsString, string separator, string decimalSeparator)
        {
            var symbolList = symbolsAsString.Split(Environment.NewLine);
            foreach (var symbol in symbolList)
            {
                string csvLine;
                try
                {
                    string roe = await GetValueFromApi(apiUrl, "Roe", symbol, decimalSeparator);
                    roe = (Convert.ToDouble(roe) / 100).ToString();
                    string lastEquity = await GetValueFromApi(apiUrl, "LastEquity", symbol, decimalSeparator);
                    string lastIncome = await GetValueFromApi(apiUrl, "LastIncome", symbol, decimalSeparator);
                    string capEx = await GetValueFromApi(apiUrl, "CapEx", symbol, decimalSeparator);
                    string targetPrice = await GetValueFromApi(apiUrl, "TargetPrice", symbol, decimalSeparator);
                    string currentPrice = await GetValueFromApi(apiUrl, "CurrentPrice", symbol, decimalSeparator);
                    string sharesOutstanding = await GetValueFromApi(apiUrl, "SharesOutstanding", symbol, decimalSeparator);

                    csvLine = $"{symbol}{separator}{roe}{separator}{lastEquity}{separator}" +
                        $"{lastIncome}{separator}{capEx}{separator}" +
                        $"{targetPrice}{separator}{currentPrice}{separator}{sharesOutstanding}";

                }
                catch (Exception e)
                {
                    File.AppendAllText(LOG_FILE, e.ToString());
                    csvLine = $"{symbol}{separator}{separator}{separator}" +
                        $"{separator}{separator}" +
                        $"{separator}{separator}";
                }
                MessageEvent?.Invoke(this, new MessageEventArgs(csvLine));
            }
        }

        private async Task<string> GetValueFromApi(string apiUrl, string actionMethod, string symbol, string decimalSeparator)
        {
            var uri = $"{apiUrl}/{actionMethod}?symbol={symbol}";
            string responseCurrentPrice = await _httpClient.GetStringAsync(uri);
            responseCurrentPrice = responseCurrentPrice.Replace("[", "").Replace("]", "");
            responseCurrentPrice = responseCurrentPrice.Replace(".", decimalSeparator);
            return responseCurrentPrice;
        }
    }
}
