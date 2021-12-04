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
    }
}
