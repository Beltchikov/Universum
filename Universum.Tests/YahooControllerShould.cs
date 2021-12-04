using System;
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
        public void ReturnsCurrentPrice()
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
        public void ReturnsRoe()
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
        public void ReturnsTargetPrice()
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

        [Fact]
        public void ReturnsEarningsDate()
        {
            string earningsDateExpected = "Jan 05, 2022 - Jan 10, 2022";

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(earningsDateExpected);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.EarningsDate("MSFT");
            string[] earningsDateArray = (string[])jsonResult.Value;
            var earningsDate = earningsDateArray[0];

            Assert.Equal(earningsDateExpected, earningsDate);
        }

        [Fact]
        public void ReturnsLastEquity()
        {
            string lastEquityReceived= "133360000000";
            double lastEquityExpected = 133360;

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(lastEquityReceived);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.LastEquity("FB");
            double[] lastEquityArray = (double[])jsonResult.Value;
            var lastEquity = lastEquityArray[0];

            Assert.Equal(lastEquityExpected, lastEquity);
        }

        [Fact]
        public void ReturnsSharesOutstandingInMillions()
        {
            string sharesOutstandingReceived = "7.24M";
            double sharesOutstandingExpected = 7.24;

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(sharesOutstandingReceived);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.SharesOutstanding("ESEA");
            double[] sharesOutstandingArray = (double[])jsonResult.Value;
            var sharesOutstanding = sharesOutstandingArray[0];

            Assert.Equal(sharesOutstandingExpected, sharesOutstanding);
        }

        [Fact]
        public void ReturnsSharesOutstandingInBillions()
        {
            string sharesOutstandingReceived = "2.79B";
            double sharesOutstandingExpected = 2790;

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(sharesOutstandingReceived);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.SharesOutstanding("ESEA");
            double[] sharesOutstandingArray = (double[])jsonResult.Value;
            var sharesOutstanding = sharesOutstandingArray[0];

            Assert.Equal(sharesOutstandingExpected, sharesOutstanding);
        }

        [Fact]
        public void ThrowExceptionInSharesOutstanding()
        {
            string sharesOutstandingReceived = "2.79";

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(sharesOutstandingReceived);

            var sut = new YahooController(simpleBrowser);

            Assert.Throws<ApplicationException>(() => sut.SharesOutstanding("SOME"));
        }

        [Fact]
        public void ReturnLastIncome()
        {
            string lastIncomeReceived = "133360000000";
            double lastIncomeExpected = 133360;

            var simpleBrowser = Substitute.For<ISimpleBrowser>();
            simpleBrowser.OneValueResult(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(lastIncomeReceived);

            var sut = new YahooController(simpleBrowser);
            JsonResult jsonResult = (JsonResult)sut.LastIncome("FB");
            double[] lastIncomeArray = (double[])jsonResult.Value;
            var lastIncome= lastIncomeArray[0];

            Assert.Equal(lastIncomeExpected, lastIncome);
        }
    }
}