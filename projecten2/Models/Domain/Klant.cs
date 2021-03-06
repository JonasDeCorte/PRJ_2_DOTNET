﻿using System;
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


        public new ICollection<Contract> Contracten
        {
            get;
        }
        public int NrOfContracten => Contracten.Count;


        #endregion

        #region Constructors
        public Klant()
        {
            bedrijven = new List<Bedrijf>();
            Contracten = new List<Contract>();
            DatumRegistratie = DateTime.Now;
            
        }
        public int GetAantalActieveContracten()
        {
            return Contracten.Count(x => x.ContractStatus.Equals(ContractStatus.LOPEND));
        }
        #endregion
        #region Methods
        public void VoegContractToe(Contract contract)
        {
            Contracten.Add(contract);

        }

        public void VoegContractToe(string v)
        {
            throw new NotImplementedException();
        }
        /*
        public void VoegContractToe(String contractTitel, ContractStatus contractStatus, DateTime StartDatum, ContractType type)
        {
            Contract contract = new Contract(contractTitel, this ,contractStatus, StartDatum, type);
            Contracten.Add(contract); 
           
        }
        */
        #endregion

    }
}
