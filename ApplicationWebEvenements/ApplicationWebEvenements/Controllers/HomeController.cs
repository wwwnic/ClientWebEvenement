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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _client;
        private readonly IHubContext<EvenementsHub> _evenementsHubContext;

        public HomeController(ILogger<HomeController> logger,IHubContext<EvenementsHub> evenementsHubContext)
        {
            _logger = logger;
            _client = new ApiClient();
            _evenementsHubContext = evenementsHubContext;
        }

        public async Task<IActionResult> Index()
        {
            var message = await _client.GetAllEvenements();
            await _evenementsHubContext.Clients.All.SendAsync("refreshEvenements", message);
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
