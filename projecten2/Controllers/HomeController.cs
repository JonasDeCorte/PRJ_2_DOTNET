using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projecten2.filter;
using projecten2.Models;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace projecten2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        public HomeController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;

        }
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant)
         {
            ViewBag.TotaalAantalContracten = klant.GetAantalActieveContracten();
            ViewBag.TotaalAantalTickets = klant.GetAantalActieveTickets();
            string datum = DateTime.Now.Hour > 12 ? DateTime.Now.Hour > 18 ? "Goedenavond ":"Goeiemiddag ": "Goeiemorgen ";
            ViewBag.Begroeting = datum + klant.Naam;
            _notyf.Success("Logged in succesful", 5);

            return View(klant.GetAllActiveTickets().OrderBy(x => x.LaatstGewijzigd).Take(5));
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
