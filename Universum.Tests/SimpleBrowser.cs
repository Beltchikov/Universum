using NSubstitute;
using Universum.Models;
using Xunit;

namespace Universum.Tests
{
    public class SimpleBrowser
    {
        [Theory, AutoNSubstituteData]
        public void BrowserNavigateCalledAf(IBrowserWrapper browserWrapper)
        {
            string pattern1 = "";
            string pattern2 = "";
            string url = "testUrl";

            ISimpleBrowser sut = new Models.SimpleBrowser(browserWrapper);
            sut.OneValueResult(url, pattern1, pattern2);

            browserWrapper.Received().Navigate(url);
        }
    }
}
