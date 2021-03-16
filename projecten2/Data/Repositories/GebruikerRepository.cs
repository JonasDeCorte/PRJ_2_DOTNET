using Microsoft.EntityFrameworkCore;
using projecten2.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace projecten2.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly DbSet<Contract> _contracten;
        private readonly DbSet<Ticket> _tickets;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context;
            _gebruikers = _context.Gebruikers;
            _contracten = _context.Contracten;
            _tickets = _context.Tickets;
        }

        #region Gebruiker

      

        public IEnumerable<Gebruiker> GetAll()
        {
            return _gebruikers.OrderBy(x => x.Naam).ToList();
        }

        public Gebruiker GetByEmail(string email)
        {
            return _gebruikers.Include(x => x.Contracten).Include(x => x.Tickets).ThenInclude(x => x.TicketType).FirstOrDefault(x => x.Email == email);
        }
        #endregion

        #region Contract

        public IEnumerable<Contract> GetAllContracten()
        {
            return _contracten.Include(x => x.Tickets).ThenInclude(x => x.TicketType).ToList();
        }

        public Contract GetByContractNr(int contractNr)
        {
            return _contracten.Include(x => x.Tickets).ThenInclude(x => x.TicketType).FirstOrDefault(x => x.ContractNr == contractNr);
        }

        public void AddContract(Contract contract)
        {
            _contracten.Add(contract);

        }
        #endregion

        #region Tickets
        public Ticket GetByTicketNr(int ticketNr)
        {
            return _tickets.Include(x => x.Contract).Include(x => x.TicketType).FirstOrDefault(x => x.TicketNr == ticketNr);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _tickets.Include(x => x.Contract).Include(x => x.TicketType).OrderBy(x => x.TicketNr).ToList();
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
        #endregion


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
