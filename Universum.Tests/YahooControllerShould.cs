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

        [Fact]
        public void ReturnRoe()
        {
            double roeExpected = 32;
            var roeReceived= "0.324";

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(roeReceived);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.Roe("MSFT");
            double[] roeArray = (double[])jsonResult.Value;
            var roe = roeArray[0];

            Assert.Equal(roeExpected, roe);
        }

        [Fact]
        public void ReturnTargetPrice()
        {
            string targetPriceExpected = "45.78";

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(targetPriceExpected);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.CurrentPrice("MSFT");
            string[] targetPriceArray = (string[])jsonResult.Value;
            var targetPrice = targetPriceArray[0];

            Assert.Equal(targetPriceExpected, targetPrice);
        }
    }
}