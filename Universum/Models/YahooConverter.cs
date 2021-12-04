using System;
using System.Globalization;

namespace Universum.Models
{
    public class YahooConverter : IYahooConverter
    {
        public double RemoveCommaivideBy1000000Round2(string lastEquityString)
        {
            var result = lastEquityString.Replace(",", "");
            double doubleResult = Convert.ToDouble(result, new CultureInfo("EN-us")) / 1000000;
            doubleResult = Math.Round(doubleResult, 2);
            return doubleResult;
        }

        public double Roe(double income, double equity)
        {
            var equityZeroSafe = equity == 0 
                ? 0.0000000001
                : equity;

            var result = Math.Round((income / equityZeroSafe) * 100,0);
            return result;
        }
    }
}
