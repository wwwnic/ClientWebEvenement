using ApplicationWebEvenements.Hubs;
using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    [Route("")]
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly ApiClient _client;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
            _client = new ApiClient();
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Recherche")]
        public IActionResult Recherche()
        {
            return View();
        }

        [Route("MesEvenements")]
        public IActionResult MesEvenements()
        {
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
