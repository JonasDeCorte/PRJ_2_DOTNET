﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class ContractType
    {
        #region Fields

        #endregion

        #region Properties
        public int ContractTypeId { get; set; }
        public string Naam { get; set; }
        public string Status { get; set; }
        public DateTime TijdstippenTicketAanmaken { get; set; }
        public DateTime MaximaleAfhaaltijd { get; set; }
        public DateTime MinimaleAfhaaltijd { get; set; }
        public double Prijs { get; set; }

        public ManierTicketAanmaken ManierTicketAanmaken { get; set; }
        public ICollection<Contract> Contracten { get; set; }
        #endregion

        #region Constructors
        public ContractType()
        {

        }
        public ContractType(string naam, string status, DateTime tijdstipaanmaken, DateTime maxAfhaaltijd, DateTime minAfhaaltijd, double prijs)
        {
            this.Naam = naam;
            this.Status = status;
            this.TijdstippenTicketAanmaken = tijdstipaanmaken;
            this.MaximaleAfhaaltijd = maxAfhaaltijd;
            this.MinimaleAfhaaltijd = minAfhaaltijd;
            this.Prijs = prijs;
        }
        #endregion

        #region Methods

        #endregion

    }
}