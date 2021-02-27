using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projecten2.Models.Domain
{
    public class Ticket
    {
        #region Fields

        #endregion

        #region Properties
        public int TicketNr { get; set; }
        public string Titel { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }

        public Gebruiker Gebruiker { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public Rapport Rapport { get; set; }
        public Contract Contract { get; set; }

        public List<Bijlage> Bijlages { get; set; }
        public Bijlage Oplossing { get; set; }
        #endregion

        #region Constructors
        public Ticket()
        {

        }
        public Ticket(string titel, TicketType ticketType, DateTime aanmaakDatum, string omschrijving, string opmerkingen, Gebruiker gebruiker, Contract contract)
        {
            this.Titel = titel;
            this.TicketType = ticketType;
            this.AanmaakDatum = aanmaakDatum;
            this.Omschrijving = omschrijving;
            this.Opmerkingen = opmerkingen;
            this.Gebruiker = gebruiker;
            this.Contract = contract;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();
        }
        #endregion

        #region Methods

        #endregion
      
    }
}