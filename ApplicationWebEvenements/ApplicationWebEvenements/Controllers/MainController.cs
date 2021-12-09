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


        /// <summary>
        /// Retourne la page des événements récents
        /// </summary>
        /// <returns>La vue événements récents</returns>
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

        /// <summary>
        /// Affichage de la page Recherche.
        /// </summary>
        /// <returns>Vue avec liste vide</returns>
        [Route("Recherche")]
        [HttpGet]
        public IActionResult Recherche()
        {
            List<Evenement> listeVide = new List<Evenement>();
            return View(listeVide);
        }

        /// <summary>
        /// Affichage des résultats de la recherche
        /// </summary>
        /// <param name="recherche">Mot cles de la recherche</param>
        /// <returns>La liste d'événements recherchés</returns>
        [Route("Recherche")]
        [HttpPost]
        public async Task<IActionResult> Recherche(Recherche recherche)
        {
            var evenements = await _client.GetEvenementsParRecherche(recherche);
            return View(evenements);
        }

        /// <summary>
        /// Retourne la vue Mes Événements
        /// </summary>
        /// <returns>La vue avec les liste de présences et d'événements crées</returns>
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
