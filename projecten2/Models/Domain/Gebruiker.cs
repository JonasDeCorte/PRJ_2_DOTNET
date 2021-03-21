using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public abstract class Gebruiker
    {

        #region Fields

        #endregion

        #region Properties
        public int GebruikersId { get; set; }
        public string GebruikersNaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        

        public List<Ticket> Tickets { get; set; }
        public List<Contract> Contracten { get; set; }
        

        #endregion

        #region Constructors
        public Gebruiker()
        {

        }
     
            #endregion

        #region Methods

        #endregion

    }
}
