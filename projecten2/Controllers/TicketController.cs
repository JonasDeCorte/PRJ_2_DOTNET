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
                tickets = _gebruikerRepository.GetAllTickets().Where(x => x.ContractId == contractid.Value).OrderBy(x => x.AanmaakDatum).ToList();
            }
            else
            {
                tickets = _gebruikerRepository.GetAllTickets().Where(x => x.gebruikersId.Equals(klant.GebruikersId)).OrderBy(x => x.AanmaakDatum).ToList();
            }

            ViewData["selectedcontract"] = contractid;
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets.OrderByDescending(x => x.LaatstGewijzigd));
        }

        // GET: TicketController/Details/5
        public IActionResult Details(int id)
        {
            return View();
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
                    MapTicketEditViewModelToTicket(tevm, ticket, klant);
                    ticket.ContractId = tevm.ContractId;
                    _gebruikerRepository.AddTicket(ticket);
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"Je hebt het ticket ${ticket.Titel} aangemaakt.";
                }
                catch
                {
                    TempData["error"] = "Sorry, er is iets fout gegaan. Het ticket is niet aangemaakt...";
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
            Ticket ticket = _gebruikerRepository.GetByTicketNr(id);
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
                    MapTicketEditViewModelToTicket(tevm, ticket, klant);
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated Ticket {ticket.TicketNr}.";

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    // TempData["error"] = $"Sorry, something went wrong, ticket {ticket?.TicketNr} was not updated...";
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
                TempData["error"] = $"Sorry, iets ging mis, ticket {ticket?.Titel} werd niet geannuleerd..";
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

        private void MapTicketEditViewModelToTicket(TicketEditViewModel TicketEditViewModel, Ticket ticket, Klant klant)
        {
            ticket.gebruikersId = klant.GebruikersId;
            ticket.Titel = TicketEditViewModel.Titel;
            ticket.TicketTypeId = TicketEditViewModel.TicketTypeId;
            ticket.Omschrijving = TicketEditViewModel.Omschrijving;
            ticket.Opmerkingen = TicketEditViewModel.Opmerkingen;
            ticket.LaatstGewijzigd = DateTime.Now;
        }
    }
}
