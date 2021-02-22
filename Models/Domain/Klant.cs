using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public List<Bedrijf> Bedrijf { get; set; }
        public ICollection<Contract> Contracten { get; set; }
        #endregion

        #region Constructors
        public Klant()
        {

        }
        public Klant(int klantNummer, string gegevensContactPersonen, DateTime registratie)
        {
            this.KlantNummer = klantNummer;
            this.GegevensContactPersonen = gegevensContactPersonen;
            this.DatumRegistratie = registratie;
       
        }
      
        #endregion

        #region Methods

        #endregion

    }
}
