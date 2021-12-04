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
        public void RemoveCommaAndDivideBy1000000(string recieved, double expected)
        {
            var sut = new YahooConverter();
            double result = sut.ConvertLastEquity(recieved);
            Assert.Equal(expected, result);
        }
    }
}
