using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.ViewModels
{
    public class ContractEditViewModel
    {
        public string ContractTitel { get; set; }
        public DateTime StartDatum { get; set; }
        public int DoorloopTijd { get; set; }
        public int ContractTypeId { get; set; }

        public ContractEditViewModel()
        {

        }

        public ContractEditViewModel(Contract contract)
        {
            ContractTitel = contract.ContractTitel;
            StartDatum = contract.StartDatum;
            DoorloopTijd = contract.Doorlooptijd;
            ContractTypeId = contract.ContractType.ContractTypeId;
        }
    }
}
