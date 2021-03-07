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
        public void VoegContractToe(Contract contract)
        {
            Contracten.Add(contract);

        }

      
        #endregion

    }
}
