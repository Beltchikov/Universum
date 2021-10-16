using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using SimpleBrowser;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Universum.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UniversumController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> CurrentPrice(string symbol)
        {
            string url = @$"https://finance.yahoo.com/quote/{symbol}";
            string xPath = "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div[1]/span[1]";

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = await Task.Factory.StartNew(() => htmlWeb.Load(url));
            var nodes = htmlDocument.DocumentNode.SelectNodes(xPath);
            var result = nodes.Select(n => n.InnerText);
            
            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> Roe(string symbol)
        {
            string url = @$"https://finance.yahoo.com/quote/{symbol}/key-statistics";
            string xPath = "/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/div[2]/div[3]/div/div[3]/div/div/table/tbody/tr[2]/td[2]";

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = await Task.Factory.StartNew(() => htmlWeb.Load(url));
            var nodes = htmlDocument.DocumentNode.SelectNodes(xPath);
            var result = nodes.Select(n => n.InnerText);

            return Json(result);
        }

        [HttpGet]
        public ActionResult TargetPrice(string symbol)
        {
            // https://github.com/SimpleBrowserDotNet/SimpleBrowser
            var browser = new Browser();

            string url = @$"https://finance.yahoo.com/quote/{symbol}/analysis?p={symbol}";
            browser.Navigate(url);
            var responseText = browser.Text;

            var pattern = @"targetMeanPrice(\""|:|{|\w|\.)*";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(responseText);
            var responseExtract = matches.First().Value;

            int lastIdxOfColon = responseExtract.LastIndexOf(":");
            string targetPrice = responseExtract[(lastIdxOfColon + 1) ..];
            return Json(new string[] { targetPrice });
        }
    }
}
