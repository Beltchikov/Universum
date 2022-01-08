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
                    var uri = $"{apiUrl}/CurrentPrice?symbol={symbol}";
                    string responseBody = await _httpClient.GetStringAsync(uri);
                    MessageEvent?.Invoke(this, new MessageEventArgs(responseBody));
                }
                catch (Exception e)
                {
                    MessageEvent?.Invoke(this, new MessageEventArgs(e.ToString()));
                }
            }
        }

    }
}
