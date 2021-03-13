using System.Collections.Generic;

namespace projecten2.Models.Domain
{
    public interface IGebruikerRepository
    {
        #region Gebruiker 
        Gebruiker GetByEmail(string email);
        IEnumerable<Gebruiker> GetAll();
        #endregion

        #region Contract
        Contract GetByContractNr(int contractNr);
        IEnumerable<Contract> GetAllContracten();
        void AddContract(Contract contract);

        void DeleteContract(Contract contract);

        #endregion

        #region Ticket
       
        Ticket GetByTicketNr(int ticketNr);
        IEnumerable<Ticket> GetAllTickets();
        void AddTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);
        #endregion

        void SaveChanges();
    }
}
