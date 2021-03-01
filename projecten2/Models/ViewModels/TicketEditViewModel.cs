using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.ViewModels
{
    public class TicketEditViewModel
    {
        [Required]
        [StringLength(50, MinimumLength =  3 , ErrorMessage ="{0} Must contain atleast 3 characters or may not contain more than 50 characters")]
        public string Titel { get; set; }
        [Required]
        [Display(Name = "Link a contract to your ticket!")]
        public int ContractId { get; set; }
        [Required]
        [Display(Name = "Give a priority to your ticket!")]
        public int TicketTypeId { get; set; }
        [StringLength(500, ErrorMessage = "{0} may not contain more than 500 characters")]
        public string Omschrijving { get; set; }
        [StringLength(500, ErrorMessage = "{0} may not contain more than 500 characters")]
        public string Opmerkingen { get; set; }

        public TicketEditViewModel()
        {

        }
        public TicketEditViewModel(Ticket ticket)
        {
            Titel = ticket.Titel;
            ContractId = ticket.ContractId;
            TicketTypeId = ticket.TicketTypeId;
            Omschrijving = ticket.Omschrijving;
            Opmerkingen = ticket.Opmerkingen;
        }
    }
}
 