using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projecten2.Models.Domain
{
    public class Rapport
    {
        #region Fields

        #endregion

        #region Properties
        public int RapportNr { get; set; }
        public string RapportNaam { get; set; }
        public string Beschrijving { get; set; }

        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}