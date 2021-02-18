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
        public string Type { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }

        public Gebruiker Gebruiker { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public Rapport Rapport { get; set; }
        public Contract Contract { get; set; }

        public ICollection<Bijlage> Bijlages { get; set; }
        public Bijlage Oplossing { get; set; }
        #endregion

        #region Constructors
        public Ticket()
        {
            Bijlages = new List<Bijlage>();
        }
        #endregion

        #region Methods

        #endregion

    }
}