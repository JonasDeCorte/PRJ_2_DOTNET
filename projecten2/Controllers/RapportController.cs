using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten2.filter;
using projecten2.Models.Domain;

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
                RapportNaam = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Beschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ut mi in nisi posuere varius. In pharetra, risus a sagittis pharetra, lacus massa viverra turpis,  ",
                Ticket = ticket,
                Oplossing = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ut mi in nisi posuere varius. In pharetra, risus a sagittis pharetra, lacus massa viverra turpis, sit amet rutrum magna augue at neque. Mauris commodo, dui eu varius tempus, elit elit lobortis tellus, ac aliquam mi diam vel ante. Etiam ac tortor vel orci laoreet viverra. Nulla lobortis commodo massa, et porta augue sodales nec. Cras sollicitudin ipsum in ante mattis, id lacinia magna dictum. Donec ut ligula et risus tempus interdum fringilla non nisl. Vestibulum non pellentesque tellus, id sagittis ex. Sed commodo semper turpis et luctus. Donec in ipsum vel nulla finibus venenatis quis ac quam. Integer sit amet dui massa. In ac ipsum consequat neque vehicula malesuada. Vestibulum in nunc risus. Morbi sodales iaculis augue aliquam volutpat. In non egestas tortor. Integer interdum congue lorem quis dictum."
            };
            _notyf.Information($"Raadpleeg hier het rapport van {ticket.Titel}");
            ViewBag.ticketnaam = ticket.Titel;
            return View(rapport);
        }
    }
}
