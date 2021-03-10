﻿using System;
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
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }

        public int gebruikersId { get; set; }
          public int TicketTypeId { get; set; }
        public int ContractId { get; set; }

      //  public Gebruiker Gebruiker { get; set; }
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
            this.LaatstGewijzigd = DateTime.Now;
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
        public Ticket(Gebruiker klant, string titel, string omschrijving, string opmerkingen, int tickettypeId)
        {
            
            this.gebruikersId = klant.GebruikersId;
            this.Opmerkingen = opmerkingen;
            this.Titel = titel;
            this.Omschrijving = omschrijving;
            this.TicketTypeId = tickettypeId;
            this.AanmaakDatum = DateTime.Now;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();

        }
        public Ticket(Gebruiker klant, string titel, string omschrijving, string opmerkingen, int tickettypeId,DateTime aanmaakDatum)
        {

            this.gebruikersId = klant.GebruikersId;
            this.Opmerkingen = opmerkingen;
            this.Titel = titel;
            this.Omschrijving = omschrijving;
            this.TicketTypeId = tickettypeId;
            this.AanmaakDatum = aanmaakDatum;
            this.LaatstGewijzigd = DateTime.Now;
            this.TicketStatus = TicketStatus.AANGEMAAKT;
            this.Bijlages = new List<Bijlage>();

        }
        /*
                public Ticket(string titel, int ticketTypeId, int contractId, DateTime aanmaakDatum, string omschrijving, string opmerkingen, Gebruiker gebruiker)
                {
                    this.Titel = titel;
                    this.TicketTypeId = ticketTypeId;
                    this.ContractId = contractId;
                    this.AanmaakDatum = aanmaakDatum;
                    this.Omschrijving = omschrijving;
                    this.Opmerkingen = opmerkingen;
                    this.Gebruiker = gebruiker;
                    this.TicketStatus = TicketStatus.AANGEMAAKT;
                    this.Bijlages = new List<Bijlage>();
                } */

        #endregion

        #region Methods

        #endregion

    }
}