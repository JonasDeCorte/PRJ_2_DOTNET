using System;
using System.Collections.Generic;
using System.Linq;

namespace projecten2.Models.Domain
{
    public class Klant : Gebruiker
    {
        #region Fields

        #endregion

        #region Properties
        public int KlantNummer { get; set; }
        public string GegevensContactPersonen { get; set; }
        public DateTime DatumRegistratie { get; set; }

     
        public List<Bedrijf> bedrijven { get; set; }
       
        #endregion

        #region Constructors
        public Klant()
        {
            bedrijven = new List<Bedrijf>();
            Contracten = new List<Contract>();
            DatumRegistratie = DateTime.Now;
        }

        #endregion
        #region Methods
        public int GetAantalActieveContracten()
        {
            return Contracten.Count(x => x.ContractStatus.Equals(ContractStatus.LOPEND));
        }
        public List<Ticket> AllTickets()
        {
            return Contracten.SelectMany(x => x.Tickets).ToList();
        }
        public List<Ticket> AllTicketsByContractId(int contractId)
        {
            List<Ticket> tickets = new List<Ticket>();       
            Contract contract = Contracten.FirstOrDefault(x => x.ContractNr.Equals(contractId));
            return contract.Tickets;     
        }

        public Contract GetContractById(int id)
        {
            return Contracten.FirstOrDefault(x => x.ContractNr.Equals(id));
        }

        public Ticket AddTicketByContractId(int contractId, Ticket ticket)
        {
            Contract contract = Contracten.FirstOrDefault(x => x.ContractNr.Equals(contractId));
            if(ticket != null)
            {
                contract.VoegTicketToe(ticket);
                VoegTicketToe(ticket);
            }
            return ticket;

        }
         
        public void VoegTicketToe(Ticket ticket)
        {
            if(ticket != null)
            {
                 Tickets.Add(ticket);
            }
           
        }
        public void VoegContractToe(Contract contract)
        {
            if(contract != null)
            {
                 
                Contracten.Add(contract);
            }
          
        }

      
        #endregion

    }
}
