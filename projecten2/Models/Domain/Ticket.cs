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
        public DateTime AanmaakDatum { get; set; }
        public DateTime LaatstGewijzigd { get; set; }
        public DateTime DatumAfgewerkt { get; set; }
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }

       // public int gebruikersId { get; set; }
         // public int TicketTypeId { get; set; }
       // public int ContractId { get; set; }

        public Gebruiker Gebruiker { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public Rapport Rapport { get; set; }
        public Contract Contract { get; set; }
        public TicketType TicketType { get; set; }
        public List<Bijlage> Bijlages { get; set; }
        public Bijlage Oplossing { get; set; }
        #endregion

        #region Constructors
        public Ticket()
        {
            this.AanmaakDatum = DateTime.Now;
            this.LaatstGewijzigd = DateTime.Today;
            this.DatumAfgewerkt = DateTime.Today;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();
            
        }
        public Ticket(string titel)
        {
            this.Titel = titel;
            this.AanmaakDatum = DateTime.Now;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
        }  
        public Ticket(Gebruiker klant, string titel, string omschrijving, string opmerkingen, TicketType tickettype,DateTime aanmaakDatum,DateTime DatumAfgewerkt)
        {
            this.Gebruiker = klant;
            this.Opmerkingen = opmerkingen;
            this.Titel = titel;
            this.Omschrijving = omschrijving;
            this.TicketType = tickettype;
            this.AanmaakDatum = aanmaakDatum;
            this.DatumAfgewerkt = DatumAfgewerkt;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();

        }
        public Ticket(Gebruiker klant, string titel, string omschrijving, string opmerkingen, TicketType tickettype, DateTime aanmaakDatum)
        {
            this.Gebruiker = klant;
            this.Opmerkingen = opmerkingen;
            this.Titel = titel;
            this.Omschrijving = omschrijving;
            this.TicketType = tickettype;
            this.AanmaakDatum = aanmaakDatum;
            this.DatumAfgewerkt = DateTime.Today;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();

        }
        #endregion

        #region Methods
        public void AnnulerenTicket(Ticket ticket)
        {
            ticket.TicketStatus = TicketStatus.GEANNULEERD;
            ticket.DatumAfgewerkt = DateTime.Today;
            this.LaatstGewijzigd = DateTime.Today;
        }
        public bool IsTicketStatus(TicketStatus ticketStatus)
        {
            return TicketStatus.Equals(ticketStatus) ? true : false;
        }
        #endregion

    }
}