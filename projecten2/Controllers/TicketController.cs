using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projecten2.filter;
using projecten2.Models.Domain;
using projecten2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projecten2.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public TicketController(ITicketTypeRepository ticketTypeRepository, IGebruikerRepository gebruikerRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: TicketController
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant, int? contractid)
        {

            List<Ticket> tickets = new List<Ticket>();

            ViewData["contractenKlant"] = GetContractenAsSelectList(klant);

            if (contractid.HasValue && contractid.Value != 0)
            {
                tickets = klant.GetAllTicketsByContractId(contractid.Value);                               
            }
            else
            {
                tickets = klant.GetAllTickets();            
            }
            ViewData["selectedcontract"] = contractid;
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets.OrderByDescending(x => x.LaatstGewijzigd));
        }

        // GET: TicketController/Details/5
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Details(int id, Klant klant)
        {
            Ticket ticket = _gebruikerRepository.GetByTicketNr(id);

            if (ticket == null) 
            { 
                return NotFound(); 
            }

            return View(ticket);
        }

        // GET: TicketController/Create
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Create(Klant klant)
        {
            ICollection<Contract> contracts = klant.Contracten;
            ViewData["IsEdit"] = false;
            ViewData["contractenKlant"] = GetContractenAsSelectList(klant);
            ViewData["ticketTypes"] = GetTicketTypesAsSelectList();
            return View(nameof(Edit), new TicketEditViewModel());
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Create(TicketEditViewModel tevm, Klant klant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new Ticket();
                    MapTicketEditViewModelToTicket(tevm, ticket);
                    TicketType ticketType = _ticketTypeRepository.GetBy(tevm.TicketTypeId);
                    ticket.TicketType = ticketType;
                    klant.AddTicketByContractId(tevm.ContractId, ticket);
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"Je hebt het ticket {ticket.Titel} aangemaakt.";
                }
                catch
                {
                    TempData["error"] = "Sorry, er is iets fout gelopen waardoor het ticket niet is aangemaakt.";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IsEdit"] = false;
            ViewData["ticketTypes"] = GetTicketTypesAsSelectList();
            return View(nameof(Edit), tevm);

        }

        // GET: TicketController/Edit/5
          public IActionResult Edit(int id)
        {
            Ticket ticket =  _gebruikerRepository.GetByTicketNr(id);
            if(ticket.TicketStatus.Equals(TicketStatus.AFGEHANDELD) || ticket.TicketStatus.Equals(TicketStatus.GEANNULEERD)){
                TempData["message"] = $"Het ticket {ticket.Titel} kan niet worden gewijzigd want de status is: {(ticket.TicketStatus == TicketStatus.AFGEHANDELD? "Afgehandeld" : "Geannuleerd")}.";
                return RedirectToAction(nameof(Index));
            }
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["ticketTypes"] = GetTicketTypesAsSelectList();
            return View(new TicketEditViewModel(ticket));
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Edit(int id, TicketEditViewModel tevm, Klant klant)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = null;
                try
                {
                    ticket = _gebruikerRepository.GetByTicketNr(id);                 
                    MapTicketEditViewModelToTicket(tevm, ticket);
                    TicketType ticketType = _ticketTypeRepository.GetBy(tevm.TicketTypeId);
                    ticket.TicketType = ticketType;
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"Het ticket {ticket.Titel} is succesvol gewijzigd.";
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    TempData["error"] = $"Sorry, er is iets fout gelopen waardoor ticket {ticket?.Titel} niet is gewijzigd.";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IsEdit"] = true;
            ViewData["ticketTypes"] = GetTicketTypesAsSelectList();
            return View(nameof(Edit), tevm);
        }

        // GET: TicketController/Delete/5
       public IActionResult Delete(int id)
        {
            ViewData[nameof(Ticket.Titel)] = _gebruikerRepository.GetByTicketNr(id).Titel;
            return View();
        }

        // POST: TicketController/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = null;
            try
            {
                ticket = _gebruikerRepository.GetByTicketNr(id);
                ticket.AnnulerenTicket(ticket);
                _gebruikerRepository.SaveChanges();
                TempData["message"] = $"U annuleerde succesvol ticket {ticket.Titel}.";
            }
            catch
            {
                TempData["error"] = $"Sorry, er is iets fout gelopen waardoor ticket {ticket?.Titel} niet geannuleerd werd.";
            }
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetTicketTypesAsSelectList()
        {
            return new SelectList(_ticketTypeRepository.GetAll(),
                nameof(TicketType.id),
                nameof(TicketType.Naam));
        }

        private SelectList GetContractenAsSelectList(Klant klant)
        {
            return new SelectList(klant.Contracten,
                nameof(Contract.ContractNr),
                nameof(Contract.ContractTitel));
        }

        private void MapTicketEditViewModelToTicket(TicketEditViewModel TicketEditViewModel, Ticket ticket)
        {
           
            ticket.Titel = TicketEditViewModel.Titel;
            ticket.Omschrijving = TicketEditViewModel.Omschrijving;
            ticket.Opmerkingen = TicketEditViewModel.Opmerkingen;
            ticket.LaatstGewijzigd = DateTime.Now;
        }
    }
}
