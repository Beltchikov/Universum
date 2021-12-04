using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using Universum.Models;

namespace Universum.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class YahooController : Controller
    {
        private readonly ISimpleBrowser _simpleBrowser;
        
        public YahooController(ISimpleBrowser simpleBrowser)
        {
            _simpleBrowser = simpleBrowser;
        }
        
        [HttpGet]
        public ActionResult CurrentPrice(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}";
            var pattern1 = @"(?<=""currentPrice"":{""raw"":)(\d|\.)+";
            var pattern2 = @"";

            var result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        [HttpGet]
        public ActionResult Roe(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}";
            var pattern1 = @"(?<=""returnOnEquity"":{""raw"":)(\d|\.)+";
            var pattern2 = @"";

            var result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) * 100;
            doubleResult = Math.Round(doubleResult, 0);

            return Json(new[] { doubleResult });
        }

        [HttpGet]
        public ActionResult TargetPrice(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/analysis?p={symbol}";
            var pattern1 = @"targetMeanPrice(\""|:|{|\w|\.)*";
            var pattern2 = @"\d+\.+\d+";

            var result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        [HttpGet]
        public ActionResult EarningsDate(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}?p={symbol}";
            var pattern1 = @"(?<=Earnings Date).+?(?=Forward)";
            var pattern2 = @"";

            var result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            return Json(new[] { result });
        }

        [HttpGet]
        public ActionResult LastEquity(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/balance-sheet?p={symbol}";
            var pattern1 = @"(?<=""totalStockholderEquity"":{""raw"":)\d+";
            var pattern2 = @"";

            string result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            
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

            var result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);
            var unit = result[^1..];
            string value = result[..^1];
            double valueDouble = Convert.ToDouble(value, new CultureInfo("EN-us"));

            valueDouble = unit switch
            {
                "B" => valueDouble * 1000,
                "M" => valueDouble ,
                _ => throw new ApplicationException($"Unknown unit {unit}")
            };

            return Json(new[] { valueDouble });
        }

        [HttpGet]
        public ActionResult LastIncome(string symbol)
        {
            var url = @$"https://finance.yahoo.com/quote/{symbol}/financials?p={symbol}";
            var pattern1 = @"(?<=\""minorityInterest\"":\{.+\},\""netIncome\"":{""raw"":)\d+";
            var pattern2 = @"";

            string result = _simpleBrowser.OneValueResult(url, pattern1, pattern2);

            result = result.Replace(",", "");
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) / 1000000;
            doubleResult = Math.Round(doubleResult, 2);

            return Json(new[] { doubleResult });
        }
    }
}
