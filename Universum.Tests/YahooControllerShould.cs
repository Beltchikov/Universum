using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SimpleBrowser;
using Universum.Controllers;
using Universum.Models;
using Xunit;

namespace Universum.Tests
{
    public class YahooControllerShould
    {
        [Fact]
        public void ReturnCurrentPrice()
        {
            var currentPriceExpected = "132.45";

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(currentPriceExpected);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.CurrentPrice("MSFT");
            string[] currentPriceArray = (string[])jsonResult.Value;
            var currentPrice = currentPriceArray[0];

            Assert.Equal(currentPriceExpected, currentPrice);
        }
    }
}