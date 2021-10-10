using Microsoft.AspNetCore.Mvc;

namespace Universum.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UniversumController : Controller
    {
        [HttpGet]
        public ActionResult CurrentPrice(string symbol)
        {
            return View();
        }

    }
}
