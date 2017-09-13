using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chris.Web.Controllers
{
    public class IntroductionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
