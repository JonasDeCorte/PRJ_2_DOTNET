using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Ticket> _tickets;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
            _tickets = _context.Tickets;
        }
        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _tickets.OrderBy(x => x.TicketStatus).Include(x => x.Gebruiker).ToList();
        }

        public Ticket GetByTicketNr(int ticketNr)
        {
            return _tickets.OrderBy(x => x.TicketStatus).Include(x => x.Gebruiker).FirstOrDefault(x => x.TicketNr == ticketNr);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
