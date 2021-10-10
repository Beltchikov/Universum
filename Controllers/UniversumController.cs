using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universum.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversumController : Controller
    {
        // GET: UniversumController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
