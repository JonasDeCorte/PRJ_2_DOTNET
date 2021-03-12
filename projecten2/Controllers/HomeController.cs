using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projecten2.filter;
using projecten2.Models;
using projecten2.Models.Domain;
using System.Diagnostics;
using System.Linq;

namespace projecten2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGebruikerRepository _gebruikerRepository;

        public HomeController(ILogger<HomeController> logger, IGebruikerRepository gebruikerRepository)
        {
            _logger = logger;
            _gebruikerRepository = gebruikerRepository;
        }
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant)
        {
            int[] aantal = new int[2];

            aantal[0] = klant.Contracten.Count();
            aantal[1] = _gebruikerRepository.GetAllTickets().Where(x => x.gebruikersId.Equals(klant.GebruikersId)).Count();
            return View(aantal);
        }

        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Chart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
