using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public abstract class Gebruiker:IdentityUser
    {

        #region Fields

        #endregion

        #region Properties
        //public string GebruikersNaam { get; set; }
        //public string Wachtwoord { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        //public string Email { get; set; }
        public bool Status { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        #endregion

        #region Constructors
        public Gebruiker()
        {

        }
       /* public Gebruiker ( string gebruikersNaam,string wachtwoord,string voornaam,string naam,string email,bool status)
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
        public void TicketAanmaken()
        {

        }

        public void TicketStopzetten()
        {

        } 

        public void TicketWijzigen()
        {

        }

        public void ContractAanmaken()
        {

        }

        public void ContractStopzetten()
        {

        }

        public void ContractRaadplegen()
        {

        }

        public void RapportRaadplegen()
        {

        }
        #endregion

    }
}
