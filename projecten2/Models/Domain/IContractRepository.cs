using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Models.Domain
{
    public interface IContractRepository
    {
        Contract GetByContractNr(int contractNr);
        IEnumerable<Contract> GetAll();
      
        void Add(Contract contract);
        void SaveChanges();
    }
}
