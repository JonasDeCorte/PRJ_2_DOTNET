using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.ViewModels
{
    public class TicketEditViewModel
    {
        public int TicketNr { get; set; }
        public string Titel { get; set; }
        public TicketType TicketType { get; set; }
      
        public string Omschrijving { get; set; }
        public string Opmerkingen { get; set; }

        public TicketEditViewModel()
        {

        }
        public TicketEditViewModel(Ticket ticket)
        {
            TicketNr = ticket.TicketNr;
            Titel = ticket.Titel;
            TicketType = ticket.TicketType;
            Omschrijving = ticket.Omschrijving;
            Opmerkingen = ticket.Opmerkingen;
        }
    }
}
