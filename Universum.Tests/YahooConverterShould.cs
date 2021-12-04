using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universum.Models;
using Xunit;

namespace Universum.Tests
{
    public class YahooConverterShould
    {
        [Theory]
        [InlineData("133360000000", 133360)]
        [InlineData("133,360,000,000", 133360)]
        [InlineData("123456789123", 123456.79)]
        public void RemoveCommaivideBy1000000Round2(string recieved, double expected)
        {
            var sut = new YahooConverter();
            double result = sut.RemoveCommaivideBy1000000Round2(recieved);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 1000, 10)]
        public void CalculateRoe(double income, double equity, double roe)
        {
            var sut = new YahooConverter();
            double result = sut.Roe(income, equity);
            Assert.Equal(roe, result);
        }

        [Theory]
        [InlineData(100, 0, 100000000000000)]
        public void CalculateRoeIfEquityIsZero(double income, double equity, double roe)
        {
            var sut = new YahooConverter();
            double result = sut.Roe(income, equity);
            Assert.Equal(roe, result);
        }
    }
}
