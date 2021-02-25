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
        /*public Klant ( string gebruikersNaam, string wachtwoord, 
            string voornaam, string naam, string email, bool status, int klantNummer, 
            string gegevensContactPersonen, DateTime registratie)
        {
            
            GebruikersNaam = gebruikersNaam;
            Wachtwoord = wachtwoord;
            Voornaam = voornaam;
            Naam = naam;
            Email = email;
            Status = status;
            KlantNummer = klantNummer;
            GegevensContactPersonen = gegevensContactPersonen;
            DatumRegistratie = registratie;
        }*/
        public Klant(int klantNummer, string gegevensContactPersonen, DateTime registratie)
        {
            KlantNummer = klantNummer;
            GegevensContactPersonen = gegevensContactPersonen;
            DatumRegistratie = registratie;
       
        }
      
        #endregion

        #region Methods

        #endregion

    }
}
