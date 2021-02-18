using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class Klant : Gebruiker
    {
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
        #region Fields
        
        #endregion

        #region Properties
        public int KlantNummer { get; set; }
        public string GegevensContactPersonen { get; set; }
        public DateTime DatumRegistratie { get; set; }

<<<<<<< Updated upstream
        public Bedrijf Bedrijf { get; set; }
        public Contract Contracten { get; set; }
=======
        public ICollection<Bedrijf> Bedrijf { get; set; }
        public ICollection<Contract> Contracten { get; set; }
>>>>>>> Stashed changes
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion


<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }
}
