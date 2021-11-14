using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using SimpleBrowser;
using System;
using System.Globalization;
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
            var pattern1 = @"(?<=""currentPrice"":{""raw"":)(\d|\.)+";
            var pattern2 = @"";

            var result = BrowserResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        [HttpGet]
        public async Task<ActionResult> Roe(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}";
            var pattern1 = @"(?<=""returnOnEquity"":{""raw"":)(\d|\.)+";
            var pattern2 = @"";

            var result = BrowserResult(url, pattern1, pattern2);
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) * 100;
            doubleResult = Math.Round(doubleResult, 0);

            return Json(new[] { doubleResult });
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
            return Json(new[] { result });
        }

        [HttpGet]
        public ActionResult EarningsDate(string symbol)
        {

            var url = @$"https://finance.yahoo.com/quote/{symbol}?p={symbol}";
            var pattern1 = @"(?<=Earnings Date).+?(?=Forward)";
            var pattern2 = @"";

            var result = BrowserResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        [HttpGet]
        public ActionResult LastEquity(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/balance-sheet?p={symbol}";
            var pattern1 = @"(?<=""totalStockholderEquity"":{""raw"":)\d+";
            var pattern2 = @"";

            var result = BrowserResult(url, pattern1, pattern2);
            
            result = result.Replace(",", "");
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) / 1000000;
            doubleResult = Math.Round(doubleResult, 2);

            return Json(new[] { doubleResult });
        }

        [HttpGet]
        public ActionResult SharesOutstanding(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/key-statistics?p={symbol}";
            var pattern1 = @"(?<=Shares Outstanding\s*\d)\d+\.?\d+.";
            var pattern2 = @"";

            var result = BrowserResult(url, pattern1, pattern2);
            var unit = result[^1..];
            var value = result[..^1];
            double valueDouble = Convert.ToDouble(value, new CultureInfo("EN-us"));

            valueDouble = unit switch
            {
                "B" => valueDouble * 1000,
                "M" => valueDouble ,
                _ => throw new ApplicationException($"Unknown unit {unit}")
            };

            return Json(new[] { valueDouble });
        }

        private string BrowserResult(string url, string regExPattern1, string regExPattern2)
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
