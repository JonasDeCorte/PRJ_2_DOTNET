using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class RapportController : Controller
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly INotyfService _notyf;

        public RapportController(ITicketTypeRepository ticketTypeRepository, IGebruikerRepository gebruikerRepository, INotyfService notyf)
        {
            _ticketTypeRepository = ticketTypeRepository;
            _gebruikerRepository = gebruikerRepository;
            _notyf = notyf;
        }
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(int id)
        {
            Ticket ticket = _gebruikerRepository.GetByTicketNr(id);
            Rapport rapport = new Rapport
            {
                RapportNaam = "test",
                Beschrijving = "test beschrijving",
                Ticket = ticket,
                Oplossing = "de oplossing van dit ticket was... lorem ipsum..."
            };
            _notyf.Information($"Raadpleeg hier het rapport van {ticket.Titel}");
            ViewBag.ticketnaam = ticket.Titel;
            return View(rapport);
        }
    }
}
