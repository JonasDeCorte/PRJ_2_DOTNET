using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projecten2.Models.Domain
{
    public class Ticket
    {
        #region Fields
        private string _titel;
        #endregion

        #region Properties
        public int TicketNr { get; set; }
        public string Titel
        {
            get
            {
                return _titel;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een ticket moet een naam hebben");
                _titel = value;
            }
        }


        public DateTime AanmaakDatum { get; set; }
        public DateTime LaatstGewijzigd { get; set; }
        public DateTime DatumAfgewerkt { get; set; }
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }


        public Gebruiker Gebruiker { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public Rapport Rapport { get; set; }
        public Contract Contract { get; set; }
        public TicketType TicketType { get; set; }
        public List<AppFile> bijlages { get; set; }

        public int NrOfBijlages => bijlages.Count;
        #endregion

        #region Constructors
        public Ticket()
        {
            this.AanmaakDatum = DateTime.Now;
            this.LaatstGewijzigd = DateTime.Today;
            this.DatumAfgewerkt = DateTime.Today;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.bijlages = new List<AppFile>();

        }
        public Ticket(string titel)
        {
            this.Titel = titel;
            this.AanmaakDatum = DateTime.Now;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.bijlages = new List<AppFile>();
        }
        public Ticket(Gebruiker klant, string titel, string omschrijving, string opmerkingen, TicketType tickettype, DateTime aanmaakDatum, DateTime DatumAfgewerkt)
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
            this.bijlages = new List<AppFile>();


        }

        public double berekenAantaluren()
        {
            TimeSpan tijdTussenDatums = DatumAfgewerkt.Subtract(AanmaakDatum);
            double aantalUren = tijdTussenDatums.TotalHours;
            return aantalUren;
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
            this.bijlages = new List<AppFile>();


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
