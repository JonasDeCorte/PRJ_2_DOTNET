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
using System.Threading.Tasks;

namespace projecten2.Controllers
{
    public class TicketController : Controller
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IContractRepository _contractRepository;

        public TicketController(ITicketRepository ticketRepository, ITicketTypeRepository ticketTypeRepository, IContractRepository contractRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketTypeRepository = ticketTypeRepository;
            _contractRepository = contractRepository;
        }
        // GET: TicketController
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant)
        {
            // gewoon een voorbeeld klant moet eigenlijk zelf in de view een contract selecteren zodat wij alle tickets kunnen weergeven
            Contract contract = klant.Contracten.First();
            IEnumerable<Ticket> tickets = _ticketRepository.GetAll().Where(x => x.ContractId == contract.ContractNr);
            if (tickets == null) {
                return NotFound();
            }
            return View(tickets);
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
        public IActionResult Create(TicketEditViewModel tevm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new Ticket();
                    MapTicketEditViewModelToTicket(tevm, ticket);
                    ticket.ContractId = tevm.ContractId;
                    _ticketRepository.Add(ticket);
                    _ticketRepository.SaveChanges();
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
            Ticket ticket = _ticketRepository.GetByTicketNr(id);
            if(ticket == null)
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
        public IActionResult Edit(int id, TicketEditViewModel tevm)
        {
            if (ModelState.IsValid)
           {
                Ticket ticket = null;
                try
                {
                    ticket = _ticketRepository.GetByTicketNr(id);
                   MapTicketEditViewModelToTicket(tevm, ticket);
                    _ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated Ticket {ticket.TicketNr}.";

                }
                catch(Exception e)
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
            return View();
        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
            ticket.TicketTypeId = TicketEditViewModel.TicketTypeId;
            ticket.Omschrijving = TicketEditViewModel.Omschrijving;
            ticket.Opmerkingen = TicketEditViewModel.Opmerkingen;
        }   
    }
}
