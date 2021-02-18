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


        public ICollection<Bedrijf> Bedrijf { get; set; }
        public ICollection<Contract> Contracten { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}
