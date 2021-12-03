using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
            if (HttpContext.Session.GetInt32("login") == null)
            {
                return RedirectToAction("Login", "Authentification");
            } else
            {
                return View();
            }
        }

        [Route("Recherche")]
        public IActionResult Recherche()
        {
            return View();
        }

        [Route("MesEvenements")]
        public async Task<IActionResult> MesEvenements()
        {
            var evenements = await _client.GetEvenementsParOrganisateur((int)HttpContext.Session.GetInt32("login"));
            return View(evenements);
        }

        [Route("GestionCompte")]
        public IActionResult GestionCompte()
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
