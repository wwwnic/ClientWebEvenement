using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Controllers
{
    [Route("Login")]
    public class AuthentificationController : Controller
    {
        private readonly ILogger<AuthentificationController> _logger;
        private readonly ApiClient _client;
        public AuthentificationController(ILogger<AuthentificationController> logger)
        {
            _logger = logger;
            _client = new ApiClient();
        }

        [Route("")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Login(Utilisateur model)
        {
            Utilisateur? utilisateur;
            try
            {
                utilisateur = await _client.Login(model);
            }
            catch
            {
                ViewBag.messageErreur = "Erreur de connexion avec le serveur";
                utilisateur = null;
                return View(model);
            }
            if (utilisateur?.NomUtilisateur != null)
            {
                HttpContext.Session.SetInt32("login", utilisateur.IdUtilisateur);
                HttpContext.Session.SetString("nomLogin", utilisateur.NomUtilisateur);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ViewBag.messageErreur = "Une erreur est survenue durant votre connexion";
                return View(model);
            }
        }

        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Remove("login");
            HttpContext.Session.Remove("nomLogin");
            return RedirectToAction("Login");
        }

        [Route("Signup")]
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("Signup")]
        [HttpPost]
        public IActionResult Signup(Utilisateur model)
        {
            bool? isRegistered;
            try
            {
                isRegistered = _client.SignUp(model)?.Result;
            } catch
            {
                ViewBag.messageErreur = "Erreur de connexion avec le serveur";
                isRegistered = null;
                return View(model);
            }
            if (isRegistered.HasValue && isRegistered.Value)
            {
                return View("Login");

            } else
            {
                ViewBag.messageErreur = "Une erreur est survenue durant votre inscription";
                return View(model);
            }
        }
    }
}
