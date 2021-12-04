using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    [Route("Compte")]
    public class CompteController : Controller
    {
        ApiClient _client;
        public CompteController()
        {
            _client = new ApiClient();

        }

        [Route("")]
        public ActionResult Index()
        {
            var utilisateurJson = HttpContext.Session.GetString("utilisateur");
            if (utilisateurJson != null)
            {
                var utilisateur = JsonConvert.DeserializeObject<Utilisateur>(utilisateurJson);
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /*
         * Permet de voir les informations du compte
         */
        [Route("GestionCompte")]
        public ActionResult GestionCompte()
        {
            var utilisateurJson = HttpContext.Session.GetString("utilisateur");
            if (utilisateurJson != null)
            {
                var utilisateur = JsonConvert.DeserializeObject<Utilisateur>(utilisateurJson);
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /*
        * Permet de modifier les informations du compte
        */
        [Route("ModifierCompte")]
        public ActionResult ModifierCompte(Utilisateur model)
        {

            var utilisateurSessionJson = HttpContext.Session.GetString("utilisateur");
            if (utilisateurSessionJson != null)
            {
                var utilisateurSession = JsonConvert.DeserializeObject<Utilisateur>(utilisateurSessionJson);
                utilisateurSession.NomUtilisateur = model.NomUtilisateur;
                utilisateurSession.MotDePasse = model.MotDePasse;
                utilisateurSession.Courriel = model.Courriel;
                utilisateurSession.Telephone = model.Telephone;


                var utilisateuSessionModifiéJson = JsonConvert.SerializeObject(utilisateurSession);
                HttpContext.Session.SetString("nomLogin", utilisateurSession.NomUtilisateur);
                HttpContext.Session.SetString("utilisateur", utilisateuSessionModifiéJson);

                try
                {
                    var estRéussi = _client.EditAccount(utilisateurSession).Result;
                    if (estRéussi)
                    {
                        return RedirectToAction("Index", "Compte");
                    }
                    else
                    {
                        ViewBag.messageErreur = "Une erreur est servenue durant l'opération";
                    }
                }
                catch
                {
                    ViewBag.messageErreur = "Erreur de connexion avec le serveur";
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View("GestionCompte");
        }
    }
}
