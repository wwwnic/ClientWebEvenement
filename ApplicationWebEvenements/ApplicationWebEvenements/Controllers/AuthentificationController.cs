using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    public class AuthentificationController : Controller
    {
        private ApiClient _apiClient;

        public AuthentificationController()
        {
            _apiClient = new ApiClient();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Succes()
        {
            return View();

        }

        /// <summary>
        /// Permet à un utilisateur de se connecter via son nom et mdp
        /// </summary>
        /// <param name="conom">connexion nom utilisateur</param>
        /// <param name="comdp">connexion mot de passe</param>
        /// <returns>la vue home</returns>
        public IActionResult Connexion(String conom, String comdp)
        {
            ViewBag.estMaintenantEnregistre = false;
            var utilisateur = _apiClient.PostConnexion(new Utilisateur(conom, comdp));
            if (utilisateur.NomUtilisateur != null)
            {
                ViewBag.messageSucces = "Vous êtes maintenant connecté";
                return View("Succes");
            }
            else
            {
                ViewBag.messageErreur = "echec_connexion";
                return View("Index");
            }
        }


        /// <summary>
        /// Permet à un utilisateur de s'enregistrer
        /// </summary>
        /// <param name="ennom">le nom de l'utilisateur</param>
        /// <param name="enmdp1">mot de passe 1</param>
        /// <param name="enmdp2">mot de passe 2 (répetition)</param>
        /// <returns>la vue home</returns>
        public IActionResult Enregistrement(string ennom, string enmdp1, string enmdp2, string encourriel, string entelephone)
        {
            ViewBag.estMaintenantEnregistre = false;
            if (enmdp1 == enmdp2) {
                var estSucces =_apiClient.PostEnregistrement(new Utilisateur(ennom,enmdp1, encourriel, entelephone));
                if (estSucces)
                {
                    ViewBag.messageSucces = "Vous êtes maintenant enregistré";
                    ViewBag.estMaintenantEnregistre = true;
                    return View("Succes");
                }
            } else
            {
                // afficher message mdp1 != mdp2
            }
            return View("Index");
    }
    }
}
