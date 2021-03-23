using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten2.filter;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Controllers
{
    public class ChartController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        

        public ChartController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
            
        }

        // GET: ContractController
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(Klant klant)
        {
            ViewBag.GemiddeldAantalUrenAlleTickets = klant.GetGemiddeldAantalUurPerTicket();
            return View();
        }
    }
}
