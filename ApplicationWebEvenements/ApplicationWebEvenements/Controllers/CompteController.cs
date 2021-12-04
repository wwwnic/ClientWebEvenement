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

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GestionCompte")]
        public ActionResult GestionCompte()
        {
            var utilisateurJson = HttpContext.Session.GetString("utilisateur");
            if (utilisateurJson != null)
            {
                var utilisateur = JsonConvert.DeserializeObject<Utilisateur>(utilisateurJson);
                return View("GestionCompte", utilisateur);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [Route("ModifierCompte")]
        public ActionResult ModifierCompte(Utilisateur model)
        {
            //requete api
            var utilisateurJson = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetString("nomLogin", model.NomUtilisateur);
            HttpContext.Session.SetString("utilisateur", utilisateurJson);
            return View("Index");
        }


    }
}
