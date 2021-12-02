using Microsoft.AspNetCore.Mvc;
using ApplicationWebEvenements.Models;

namespace ApplicationWebEvenements.Controllers
{
    [Route("détail")]
    public class DétailÉvénementController : Controller
    {
        private ApiClient _apiClient;

        public DétailÉvénementController()
        {
            _apiClient = new ApiClient();
        }

        [Route("index/{idEvenement}")]
        public IActionResult Index(int idEvenement)
        {
            if (true)
            {
                Evenement evenement = _apiClient.GetEvenementParId(idEvenement);
                ViewBag.nomEvenement = evenement.NomEvenement;
                return View("Index");
            } 
            else
            {

            }
        }
    }
}
