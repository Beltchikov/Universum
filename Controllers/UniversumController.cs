using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

    }
}
