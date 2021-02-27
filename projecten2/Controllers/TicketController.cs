using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public TicketController(ITicketRepository ticketRepository, ITicketTypeRepository ticketTypeRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketTypeRepository = ticketTypeRepository;
        }
        // GET: TicketController
        public ActionResult Index()
        {
            IEnumerable<Ticket> tickets = _ticketRepository.GetAll();
            return View(tickets);
        }

        // GET: TicketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TicketController/Edit/5
        public ActionResult Edit(int id)
        {
            Ticket ticket = _ticketRepository.GetByTicketNr(id);
              
            SelectList selectLists = new SelectList(_ticketTypeRepository.GetAll(), nameof(TicketType.id),nameof(TicketType.Naam));
            ViewData["ticketTypes"] = selectLists;
            return View(new TicketEditViewModel(ticket));
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TicketEditViewModel tevm)
        {
            Ticket ticket = null;
            try
            {
                 ticket = _ticketRepository.GetByTicketNr(id);
                MapTicketEditViewModelToTicket(tevm, ticket);
                _ticketRepository.SaveChanges();
                TempData["message"] = $"You successfully updated Ticket {ticket.TicketNr}.";
               
            }
            catch
            {
                TempData["error"] = $"Sorry, something went wrong, ticket {ticket?.TicketNr} was not updated...";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TicketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        private void MapTicketEditViewModelToTicket(TicketEditViewModel TicketEditViewModel, Ticket ticket)
        {
            ticket.TicketNr = TicketEditViewModel.TicketNr;
            ticket.Titel = TicketEditViewModel.Titel;
            ticket.TicketType = TicketEditViewModel.TicketType;
          ticket.Omschrijving = TicketEditViewModel.Omschrijving;
           ticket.Opmerkingen = TicketEditViewModel.Opmerkingen;
        }
    }
}
