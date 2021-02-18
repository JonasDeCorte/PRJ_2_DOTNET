using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public class Gebruiker
    {
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
        #region Fields

        #endregion

        #region Properties
        public string GebruikersNaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

<<<<<<< Updated upstream
        public Ticket Tickets { get; set; }
=======
        public ICollection<Ticket> Tickets { get; set; }
>>>>>>> Stashed changes
        #endregion

        #region Constructors

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
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }
}
