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
        private string _bedrijfsnaam;
        public string Bedrijfsnaam
        {
            get { return _bedrijfsnaam; }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException(nameof(Bedrijfsnaam), "Bedrijfsnaam moet een waarde hebben");
                }
                if (value == null)
                    throw new ArgumentNullException(nameof(Bedrijfsnaam));
                _bedrijfsnaam = value;
            }
        }
        public String[] Telefoonnummers { get; set; }
        
        private string _landHoofdZetel;
        public string LandHoofdzetel
        {
            get { return _landHoofdZetel; }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException(nameof(LandHoofdzetel), "LandHoofdzetel moet een waarde hebben");
                }
                if (value == null)
                    throw new ArgumentNullException(nameof(LandHoofdzetel));
                _landHoofdZetel = value;
            }
        }
        private string _straat;
        public string Straat
        {
            get { return _straat; }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException(nameof(Straat), "Straat moet een waarde hebben");
                }
                if (value == null)
                    throw new ArgumentNullException(nameof(Straat));
                _straat = value;
            }
        }
        #endregion

        #region Constructors
        public Bedrijf()
        {

        }
        public Bedrijf(string Bnaam, string[] nummers, string hoofdzetel, string straat)
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