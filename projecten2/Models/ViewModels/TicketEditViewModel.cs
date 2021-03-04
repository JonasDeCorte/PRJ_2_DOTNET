using projecten2.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace projecten2.Models.ViewModels
{
    public class TicketEditViewModel
    {
        [Required(ErrorMessage = "De titel van het ticket is verplicht in te vullen.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De titel van het ticket moet minstens 3 karakters bevatten, maximaal 50.")]
        public string Titel { get; set; }
        [Required(ErrorMessage = "U moet een contract kiezen waaronder dit ticket valt.")]
        [Display(Name = "Bijhorend contract")]
        public int ContractId { get; set; }
        [Required(ErrorMessage = "U moet een prioriteit toekkennen aan het ticket.")]
        [Display(Name = "Ticket prioriteit")]
        public int TicketTypeId { get; set; }
        [Required(ErrorMessage = "U moet het probleem omschrijven waarvoor dit ticket is aangemaakt.")]
        [Display(Name = "Omschrijving van het probleem")]
        public string Omschrijving { get; set; }
        [Display(Name = "Extra opmerkingen")]
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
