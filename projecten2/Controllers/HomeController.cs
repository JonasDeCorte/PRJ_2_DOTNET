using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projecten2.filter;
using projecten2.Models;
using projecten2.Models.Domain;

namespace projecten2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly IContractRepository _contractRepository;

        public HomeController(ILogger<HomeController> logger, ITicketRepository ticketRepository, IContractRepository contractRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _contractRepository = contractRepository;
        }
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant)
        {
            int[] aantal = new int[2];
            
            aantal[0] = klant.Contracten.Count();
            aantal[1] = _ticketRepository.GetAll().Where(x => x.gebruikersId.Equals(klant.GebruikersId)).Count();
            return View(aantal);
        }

        public IActionResult Privacy()
        {
            return View();
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
