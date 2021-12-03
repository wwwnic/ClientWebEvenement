using ApplicationWebEvenements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var utilisateur = await _client.LoginUtilisateur(model);
            if (utilisateur != null)
            {
                HttpContext.Session.SetInt32("login", utilisateur.IdUtilisateur);
                HttpContext.Session.SetString("nomLogin", utilisateur.NomUtilisateur);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View();
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
            return View();
        }
    }
}
