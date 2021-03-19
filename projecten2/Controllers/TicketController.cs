﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
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
        private readonly INotyfService _notyf;

        public TicketController(ITicketTypeRepository ticketTypeRepository, IGebruikerRepository gebruikerRepository, INotyfService notyf)
        {
            _ticketTypeRepository = ticketTypeRepository;
            _gebruikerRepository = gebruikerRepository;
            _notyf = notyf;
        }

        // GET: TicketController
        [ServiceFilter(typeof(KlantFilter))]
        [Authorize]
        public IActionResult Index(Klant klant, bool ticketstatus, int contractid = 0)
        {

            List<Ticket> tickets = new List<Ticket>();

            ViewData["contractenKlant"] = GetContractenAsSelectList(klant, contractid);

            if  (contractid != 0)
            {
                tickets = klant.GetAllActiveTicketsByContractId(contractid, ticketstatus);
                ViewBag.ContractNaam = "van " + klant.GetContractById(contractid).ContractTitel;
            }         
            else
                tickets = klant.GetAllActiveTickets(ticketstatus);

            if (tickets == null)
            {
                return NotFound();
            }

            

            return View(tickets.OrderByDescending(x => x.LaatstGewijzigd));
        }

        // GET: TicketController/Details/5
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Details(int id)
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
                    _notyf.Success("Ticket succesvol aangemaakt", 5);
                    TempData["message"] = $"Je hebt het ticket {ticket.Titel} aangemaakt.";
                }
                catch
                {
                    _notyf.Error("Er is iets misgelopen. Probeer opnieuw.", 5);
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
            Ticket ticket = _gebruikerRepository.GetByTicketNr(id);
            if (ticket.IsTicketStatus(TicketStatus.AFGEHANDELD) || ticket.IsTicketStatus(TicketStatus.GEANNULEERD))
            {
                TempData["message"] = $"Het ticket {ticket.Titel} kan niet worden gewijzigd want de status is: {(ticket.IsTicketStatus(TicketStatus.AFGEHANDELD) ? "Afgehandeld" : "Geannuleerd")}.";
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
                    _notyf.Success("Ticket is succesvol bewerkt");
                    TempData["message"] = $"Het ticket {ticket.Titel} is succesvol gewijzigd.";
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    _notyf.Error("Ticket is niet bewerkt. Probeer opnieuw");
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
                _notyf.Success("Uw ticket annuleren is gelukt.");
                TempData["message"] = $"U annuleerde succesvol ticket {ticket.Titel}.";
            }
            catch
            {
                _notyf.Error("Oops... er is iets misgegaan. Probeer opnieuw");
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

        private SelectList GetContractenAsSelectList(Klant klant, int selected = 0)
        {
            return new SelectList(klant.GetContracten(),
                nameof(Contract.ContractNr),
                nameof(Contract.ContractTitel), selected);
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
