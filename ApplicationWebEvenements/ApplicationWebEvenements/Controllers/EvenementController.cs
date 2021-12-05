using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationWebEvenements.Hubs;

namespace ApplicationWebEvenements.Controllers
{
    [Route("Evenement")]
    public class EvenementController : Controller
    {
        private readonly ILogger<EvenementController> _logger;
        private readonly ApiClient _client;

        public EvenementController(ILogger<EvenementController> logger)
        {
            _logger = logger;
            _client = new ApiClient();
        }

        [Route("{idEvenement}")]
        public async Task<IActionResult> Details(int idEvenement)
        {
            var evenement = await _client.GetEvenementParId(idEvenement);
            if (evenement != null)
            {
                var listeUtilisateur = await _client.GetUtilisateurParEvenement(idEvenement);
                EvenementsHub.idEvenementDetails = idEvenement;
                ViewBag.estParticipant = 0;
                ViewBag.client = _client;
                // Participant = 1, non-participant = 0,organisateur = 2
                foreach (Utilisateur u in listeUtilisateur)
                {
                    if (u.IdUtilisateur == HttpContext.Session.GetInt32("login"))
                    {
                        ViewBag.estParticipant = 1;
                    }
                }
                if (evenement.IdOrganisateur == HttpContext.Session.GetInt32("login"))
                {
                    ViewBag.estParticipant = 2;
                }
                return View(evenement);
            }
            else
            {
                return RedirectToAction("Aucun", "Evenement");
            }

        }

        [Route("")]
        public IActionResult Aucun()
        {
            return View();
        }
    }
}
