using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    public class AuthentificationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Succes()
        {
            return View();

        }

        /// <summary>
        /// Permet à un utilisateur de se connecter
        /// </summary>
        /// <param name="conom">le nom de l'utilisateur</param>
        /// <param name="comdp">mot de passe</param>
        /// <returns>la vue home</returns>
        public IActionResult Connexion(String conom, String comdp)
        {
            ViewBag.estMaintenantEnregistre = false;
            if (true)
            {
                ViewBag.messageSucces = "Vous êtes maintenant connecté";
                return View("Succes");
            }
            else
            {
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
        public IActionResult Enregistrement(String ennom, String enmdp1, String enmdp2)
        {
            ViewBag.estMaintenantEnregistre = false;
            if (true)
            {
                ViewBag.messageSucces = "Vous êtes maintenant enregistré";
                ViewBag.estMaintenantEnregistre = true;
                return View("Succes");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
