using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApplicationWebEvenements.Hubs;
using System;

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


        /// <summary>
        /// Ajoute un evenement via l'api
        /// </summary>
        /// <param name="model">le modele Evenement</param>
        /// <returns>La prochaine vue</returns>
        [Route("SoumettreCommentaire")]
        public IActionResult SoumettreCommentaireAsync(Evenement model)
        {
            var commentaire = new Commentaire();
            commentaire.IdEvenement = model.IdEvenement;
            commentaire.IdUtilisateur = (int)HttpContext.Session.GetInt32("login");
            commentaire.Date = model.Date;//
            commentaire.Texte = model.Commentaire;
            _client.AddCommentaire(commentaire);
            return RedirectToAction($"{model.IdEvenement}", "Evenement");

        }


        /// <summary>
        /// Ajoute un evenement via l'api
        /// </summary>
        /// <param name="model">le modele Evenement</param>
        /// <returns>La prochaine vue</returns>
        [Route("Creer")]
        public IActionResult Creer(Evenement model)
        {
            var idUtilisateurSession = HttpContext.Session.GetInt32("login");
            if (idUtilisateurSession == null) return RedirectToAction("Login", "Authentification");

            bool estUnModeleInstancié = model.NomEvenement?.Length > 1;
            if (estUnModeleInstancié)
            {
                model.IdOrganisateur = (int)idUtilisateurSession;
                return SoumettreEvenement(model, false);
            }
            else if (idUtilisateurSession != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Evenement");
        }



        /// <summary>
        /// Permet d'afficher la vue modifier evenement
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Le modele evenement</returns>
        [Route("Modification")]
        public IActionResult Modification(Evenement model)
        {
            return View("Modifier",model);
        }



        /// <summary>
        /// Modifie un evenement via l'api
        /// </summary>
        /// <param name="model">le modele Evenemenet</param>
        /// <returns>La prochaine vue</returns>
        /// 
        [Route("Modifier")]
        public IActionResult Modifier(Evenement model)
        {
            var idUtilisateurSession = HttpContext.Session.GetInt32("login");
            if (idUtilisateurSession == null) return RedirectToAction("Login", "Authentification");

            bool estUnModeleInstancié = model.NomEvenement?.Length > 1;
            if (estUnModeleInstancié)
            {
                model.IdOrganisateur = (int)idUtilisateurSession;
                return SoumettreEvenement(model, true);
            }
            else if (idUtilisateurSession != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Evenement");
        }


        /// <summary>
        /// Permet de soumettre un evenement
        /// </summary>
        /// <param name="model">le modele evenement</param>
        /// <param name="editerEvenement">Indique si l'evenement doit etre crée ou edité</param>
        /// <returns>La prochaine vue</returns>
        private IActionResult SoumettreEvenement(Evenement model, bool editerEvenement)
        {
            Evenement evenementCréé = null;
            try
            {
                if (editerEvenement)
                {
                    bool estReussi = _client.EditEvenement(model).Result;
                    if (estReussi)
                        return RedirectToAction($"{model.IdEvenement}", "Evenement");
                }
                else
                {
                    evenementCréé = _client.CreerEvenement(model).Result;
                }
            }
            catch
            {
                ViewBag.messageErreur = "Une erreur de connexion est survenue";
                return View(model);

            }
            return VerifierInstanciationEvenement(model, evenementCréé);
        }


        /// <summary>
        /// Retourne la vue details evenement si l'evenement a des données 
        /// ou reviens à la vue creation evenement avec un message d'erreur 
        /// </summary>
        /// <param name="model">le model evenement</param>
        /// <param name="evenementCréé">l'evenement retourné par l'api</param>
        /// <returns></returns>
        private IActionResult VerifierInstanciationEvenement(Evenement model, Evenement evenementCréé)
        {
            bool estUnEvenementInstancié = evenementCréé != null && evenementCréé.IdEvenement != 0;
            if (estUnEvenementInstancié)
            {
                return RedirectToAction($"{evenementCréé.IdEvenement}", "Evenement");
            }
            else
            {
                ViewBag.messageErreur = "Une erreur est survenue";
                return View(model);
            }
        }
    }
}
