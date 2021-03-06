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
        [Required]
        [Display(Name = "Contract titel")]
        public string ContractTitel { get; set; }
        [Required]
        [Display(Name = "Startdatum van het contract")]
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        [Required]
        [Display( Name = "Doorloop tijd")]
        [Range(1,3)]
        public int DoorloopTijd { get; set; }
        [Required]
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
            ContractTypeId = contract.ContractTypeId;
        }
    }
}
