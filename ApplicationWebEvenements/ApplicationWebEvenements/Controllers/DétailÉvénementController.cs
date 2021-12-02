using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    [Route("détail")]
    public class DétailÉvénementController : Controller
    {
        [Route("index/{idEvenement}")]
        public IActionResult Index(int idEvenement)
        {
            ViewBag.id = idEvenement;
            return View();
        }
    }
}
