using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Server.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("home")]
        public IActionResult Index()
        {
            return View("Home");
        }
    }
}
