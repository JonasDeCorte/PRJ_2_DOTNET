using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class SupportManager : Gebruiker
    {
        #region Fields

        #endregion

        #region Properties
        public int PersoneelsNummer { get; set; }
        public string Adres { get; set; }
        public DateTime StartDatumTeWerkStelling { get; set; }
       
        #endregion

        #region Constructors
        public SupportManager()
        {

        }
        
    #endregion

    #region Methods
    public void KlantAanmaken()
        {

        }
        #endregion
    }
}
