using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.ViewModels
{
    public class ContractEditViewModel
    {
        [Required(ErrorMessage = "De titel van het contract is verplicht in te vullen.")]
        [Display(Name = "Contract titel")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De contract titel moet minstens 3 karakters bevatten, maximaal 50.")]
        public string ContractTitel { get; set; }
        [Required(ErrorMessage = "De startdatum van het contract is verplicht in te vullen.")]
        [Display(Name = "Startdatum van het contract")]
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        [Required(ErrorMessage = "De doorloop tijd van het contract is verplicht in te vullen. (1, 2 of 3 jaar)")]
        [Display( Name = "Doorloop tijd")]
        [Range(1,3)]
        public int DoorloopTijd { get; set; }
        [Required(ErrorMessage = "Het contract type is verplicht in te vullen.")]
        [Display(Name = "Contract type")]
        public int ContractTypeId { get; set; }

        public ContractEditViewModel()
        {
            StartDatum = DateTime.Today;
        }

        public ContractEditViewModel(Contract contract)
        {
            ContractTitel = contract.ContractTitel;
            StartDatum = contract.StartDatum;
            DoorloopTijd = contract.Doorlooptijd;
          
        }
    }
}


