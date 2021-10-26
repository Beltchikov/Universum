using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using SimpleBrowser;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Universum.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UniversumController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> CurrentPrice(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}";
            var xPath = "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div[1]/span[1]";

            var result = await HtmlWebResultAsync(url, xPath);
            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> Roe(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/key-statistics";
            var xPath = "/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/div[2]/div[3]/div/div[3]/div/div/table/tbody/tr[2]/td[2]";
            
            var result = await HtmlWebResultAsync(url, xPath);
            return Json(result);
        }

        private async Task<string[]> HtmlWebResultAsync(string url, string xPath)
        {
            var htmlWeb = new HtmlWeb();
            var htmlDocument = await Task.Factory.StartNew(() => htmlWeb.Load(url));
            var nodes = htmlDocument.DocumentNode.SelectNodes(xPath);
            var result = nodes.Select(n => n.InnerText);
            return result.ToArray();
        }

        [HttpGet]
        public ActionResult TargetPrice(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/analysis?p={symbol}";
            var pattern1 = @"targetMeanPrice(\""|:|{|\w|\.)*";
            var pattern2 = @"\d+\.+\d+";

            var result = BrowserResult(url, pattern1, pattern2);
            return Json(new [] { result });
        }

        [HttpGet]
        public ActionResult EarningsDate(string symbol)
        {

            // TODO
            //tsPrevious Close308.13Open311.00Bid310.71 x 1200Ask310.63 x 800Day &#x27;s Range308.60 - 312.3952 Week Range199.62 - 312.39Volume11,332,174Avg. Volume22,281,673Market Cap2.331TBeta (5Y Monthly)0.80PE Ratio (TTM)38.57EPS (TTM)8.05Earnings DateOct 26, 2021Forward Dividend &amp; Yield2.48 (0.80%)Ex-Dividend DateNov 17, 20211y Target Est339.26if (window.performance) {window.performance.mark && window.performance.mark('Col1-0-QuoteSummary');window.performance.measure && window.performance.measure('Col1-0-QuoteSummaryDone','PageStart','Col1-0-QuoteSummary');}if (window.performance) {window.performance.mark && window.performance.mark('Col1-1-Null');
            //Earnings Date.+(\d\w){ 1}

            var url = @$"https://finance.yahoo.com/quote/{symbol}?p={symbol}";
            var pattern1 = @"targetMeanPrice(\""|:|{|\w|\.)*";
            var pattern2 = @"\d+\.+\d+";

            var result = BrowserResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        private string BrowserResult(string url, string regExPattern1, string regExPattern2)
        {
            // https://github.com/SimpleBrowserDotNet/SimpleBrowser
            var browser = new Browser();
            browser.Navigate(url);
            var responseText = browser.Text;

            var rx = new Regex(regExPattern1, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var responseExtract = rx.Matches(responseText).First().Value;

            rx = new Regex(regExPattern2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = rx.Matches(responseExtract).First().Value;
            return result;
        }
    }
}
