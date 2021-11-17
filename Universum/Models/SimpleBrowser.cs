using SimpleBrowser;
using System.Linq;
using System.Text.RegularExpressions;

namespace Universum.Models
{
    public class SimpleBrowser : ISimpleBrowser
    {
        public string OneValueResult(string url, string regExPattern1, string regExPattern2)
        {
            // https://github.com/SimpleBrowserDotNet/SimpleBrowser
            var browser = new Browser();
            browser.Navigate(url);
            var responseText = browser.Text;

            var rx = new Regex(regExPattern1, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var regExResult1 = rx.Matches(responseText).First().Value;

            if (string.IsNullOrWhiteSpace(regExPattern2))
            {
                return regExResult1;
            }

            rx = new Regex(regExPattern2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var regExResult2 = rx.Matches(regExResult1).First().Value;

            return regExResult2;
        }
    }
}
