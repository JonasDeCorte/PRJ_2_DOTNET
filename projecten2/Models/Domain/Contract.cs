using System;
using System.Collections.Generic;

namespace projecten2.Models.Domain
{
    public class Contract
    {
        #region Fields

        #endregion

        #region Properties
        public int ContractNr { get; set; }
        public string ContractTitel { get; set; }
        public int Doorlooptijd { get; set; }
       
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

       // public int KlantNr { get; set; }
       // public Klant Klant { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ContractType ContractType { get; set; }
        #endregion

        #region Constructors
        public Contract()
        {
            EindDatum = StartDatum.AddYears(Doorlooptijd);
        }

        #endregion

        #region Methods
        public Ticket VoegTicketToe(String titel, int ticketTypeId, int contractId, DateTime aanmaakDatum, String omschrijving, String opmerkingen, Gebruiker gebruiker)
        {
            Ticket ticket = new Ticket(titel, ticketTypeId, contractId, aanmaakDatum, omschrijving, opmerkingen, gebruiker);
            Tickets.Add(ticket);
            return ticket;
        }
        #endregion

    }
}
