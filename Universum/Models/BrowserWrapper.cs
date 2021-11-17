using SimpleBrowser;

namespace Universum.Models
{
    public class BrowserWrapper : IBrowserWrapper
    {
        private Browser _browser;

        public BrowserWrapper()
        {
            _browser = new Browser();
        }

        public bool Navigate(string url)
        {
            return _browser.Navigate(url);
        }

        public string Text => _browser.Text;
    }
}
