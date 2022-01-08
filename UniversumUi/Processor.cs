using System;
using System.Net.Http;
using System.Threading;
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

        public event MessageEventHandler MessageEvent;

        public async Task ProcessAsync(string apiUrl, string symbolsAsText)
        {
            var symbolList = symbolsAsText.Split(Environment.NewLine);
            foreach (var symbol in symbolList)
            {
                try
                {
                    //HttpResponseMessage response = await _httpClient.GetAsync("http://www.contoso.com/");
                    //response.EnsureSuccessStatusCode();
                    //string responseBody = await response.Content.ReadAsStringAsync();

                    //var uri = "http://localhost:1967/Yahoo/CurrentPrice?symbol=goog";
                    var uri = $"{apiUrl}/CurrentPrice?symbol={symbol}";
                    string responseBody = await _httpClient.GetStringAsync(uri);
                    MessageEvent?.Invoke(this, new MessageEventArgs(responseBody));
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }
                catch (Exception e)
                {
                    MessageEvent?.Invoke(this, new MessageEventArgs(e.ToString()));
                }
            }
            
            //MessageEvent?.Invoke(this, new MessageEventArgs("Processing started"));

            //Thread.Sleep(10000);

            //MessageEvent?.Invoke(this, new MessageEventArgs("Message 1"));

            //Thread.Sleep(10000);

            //MessageEvent?.Invoke(this, new MessageEventArgs("Message 2"));
        }

    }
}
