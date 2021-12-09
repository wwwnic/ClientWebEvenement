using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        [HttpGet]
        public IActionResult Recherche()
        {
            List<Evenement> listeVide = new List<Evenement>();
            return View(listeVide);
        }

        [Route("Recherche")]
        [HttpPost]
        public async Task<IActionResult> Recherche(Recherche recherche)
        {
            var evenements = await _client.GetEvenementsParRecherche(recherche);
            return View(evenements);
        }

        [Route("MesEvenements")]
        public async Task<IActionResult> MesEvenements()
        {
            var presences = await _client.GetEvenementsParParticipant((int)HttpContext.Session.GetInt32("login"));
            var crees = await _client.GetEvenementsParOrganisateur((int)HttpContext.Session.GetInt32("login"));
            var modelVue = new MesEvenements
            {
                listePresences = presences,
                listeCrees = crees
            };
            return View(modelVue);
        }


        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
