using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using NSubstitute;
using Universum.Models;
using Xunit;

namespace Universum.Tests
{
    public class SimpleBrowser
    {
        [Theory, AutoNSubstituteData]
        public void BrowserNavigateCalled(
            [Frozen] IBrowserWrapper browserWrapper,
            ISimpleBrowser sut)
        {

            sut.OneValueResult(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());

            browserWrapper.Received().Navigate(Arg.Any<string>());


        }
    }
}
