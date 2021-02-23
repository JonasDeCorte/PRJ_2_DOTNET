using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projecten2.Models.Domain
{
    public class Bedrijf
    {
        #region Fields

        #endregion

        #region Properties
        public int BedrijfsID { get; set; }
        public string Bedrijfsnaam { get; set; }
        public int[] Telefoonnummers { get; set; }
        public string LandHoofdzetel { get; set; }
        public string Straat { get; set; }
        #endregion

        #region Constructors
        public Bedrijf()
        {

        }
        public Bedrijf(string Bnaam, int[] nummers, string hoofdzetel, string straat)
        {
          this.Bedrijfsnaam = Bnaam;
            this.Telefoonnummers = nummers;
            this.LandHoofdzetel = hoofdzetel;
            this.Straat = straat;
        }
        #endregion

        #region Methods

        #endregion
    }
}