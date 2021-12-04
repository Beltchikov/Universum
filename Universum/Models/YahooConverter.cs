using System;
using System.Globalization;

namespace Universum.Models
{
    public class YahooConverter : IYahooConverter
    {
        public double ConvertLastEquity(string lastEquityString)
        {
            var result = lastEquityString.Replace(",", "");
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) / 1000000;
            doubleResult = Math.Round(doubleResult, 2);
            return doubleResult;
        }
    }
}
