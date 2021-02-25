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
        public DateTime ActiefSinds { get; set; }
        #endregion

        #region Constructors
        public SupportManager()
        {

        }
        /*public SupportManager( string gebruikersNaam,string wachtwoord,
            string voornaam,string naam,string email,bool status)
        {
            
            GebruikersNaam = gebruikersNaam;
            Wachtwoord = wachtwoord;
            Voornaam = voornaam;
            Naam = naam;
            Email = email;
            Status = status;
        }*/
    #endregion

    #region Methods
    public void KlantAanmaken()
        {

        }
        #endregion
    }
}
