using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projecten2.filter;
using projecten2.Models;
using projecten2.Models.Domain;
using System;
using System.Diagnostics;
using System.Linq;

namespace projecten2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant)
        {
            ViewBag.TotaalAantalContracten = klant.GetAantalActieveContracten();
            ViewBag.TotaalAantalTickets = klant.GetAantalActieveTickets();

            string datum = DateTime.Now.Hour > 12 ? DateTime.Now.Hour > 18 ? "Goedenavond " : "Goedemiddag " : "Goedemorgen ";
            ViewBag.Begroeting = datum + klant.Voornaam;

            return View(klant.GetAllActiveTickets(true).OrderBy(x => x.LaatstGewijzigd).Take(5));
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
