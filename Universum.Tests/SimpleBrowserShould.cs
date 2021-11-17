using NSubstitute;
using Universum.Models;
using Xunit;

namespace Universum.Tests
{
    public class SimpleBrowserShould
    {
        [Theory, AutoNSubstituteData]
        public void CallBrowserNavigate(IBrowserWrapper browserWrapper)
        {
            string pattern1 = "";
            string pattern2 = "";
            string url = "testUrl";

            ISimpleBrowser sut = new Models.SimpleBrowser(browserWrapper);
            sut.OneValueResult(url, pattern1, pattern2);

            browserWrapper.Received().Navigate(url);
        }

        [Theory, AutoNSubstituteData]
        public void ReturnEmptyIfNoMatchesRegex1(IBrowserWrapper browserWrapper)
        {
            string pattern1 = "dog";
            string pattern2 = "";
            string url = "testUrl";

            browserWrapper.Text.Returns("cat, cat and once cat more");

            ISimpleBrowser sut = new Models.SimpleBrowser(browserWrapper);
            var result = sut.OneValueResult(url, pattern1, pattern2);

            Assert.Empty(result);
        }

        [Theory, AutoNSubstituteData]
        public void ReturnEmptyIfNoMatchesRegex2(IBrowserWrapper browserWrapper)
        {
            string pattern1 = @"\d+";
            string pattern2 = "(1|2)";
            string url = "testUrl";

            browserWrapper.Text.Returns("sasf34567890svsdf");

            ISimpleBrowser sut = new Models.SimpleBrowser(browserWrapper);
            var result = sut.OneValueResult(url, pattern1, pattern2);

            Assert.Empty(result);
        }

        [Theory, AutoNSubstituteData]
        public void ReturnNotEmptyRegex2(IBrowserWrapper browserWrapper)
        {
            string pattern1 = @"\d+";
            string pattern2 = "(3|4)";
            string url = "testUrl";

            browserWrapper.Text.Returns("sasf34567890svsdf");

            ISimpleBrowser sut = new Models.SimpleBrowser(browserWrapper);
            var result = sut.OneValueResult(url, pattern1, pattern2);

            Assert.NotEmpty(result);
        }
    }
}
