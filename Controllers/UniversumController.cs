using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
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

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = await Task.Factory.StartNew(() => htmlWeb.Load(url));
            //var nodes = htmlDocument.DocumentNode.SelectNodes(xPath);
            //return nodes.Select(n => n.InnerText);

            return View(htmlDocument.DocumentNode.InnerText);
        }

    }
}
