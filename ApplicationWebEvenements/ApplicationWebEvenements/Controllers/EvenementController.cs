using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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



        [Route("Creer")]
        public IActionResult Creer(Evenement model)
        {
            var idUtilisateurSession = HttpContext.Session.GetInt32("login");
            if (idUtilisateurSession == null) return RedirectToAction("Login", "Authentification");

            bool estUnModeleInstancié = model.NomEvenement?.Length > 1;
            if (estUnModeleInstancié)
            {
                return SoumettreEvenement(model, idUtilisateurSession);
            }
            else if (idUtilisateurSession != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Evenement");
        }

        private IActionResult SoumettreEvenement(Evenement model, int? idUtilisateurSession)
        {
            model.IdOrganisateur = (int)idUtilisateurSession;

            Evenement evenementCrée = null;
            try
            {
                evenementCrée = _client.CreerEvenement(model).Result;
            }
            catch
            {
                ViewBag.messageErreur = "Une erreur de connexion est survenue";
                return View(model);

            }

            bool estUnEvenementInstancié = evenementCrée != null && evenementCrée.IdEvenement != 0;
            if (estUnEvenementInstancié)
            {
                return RedirectToAction($"{evenementCrée.IdEvenement}", "Evenement");
            }
            else
            {
                ViewBag.messageErreur = "Une erreur est survenue";
                return View(model);
            }
        }
    }
}
